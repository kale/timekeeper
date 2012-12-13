using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Reports : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			Output.Text = "You can either view your employees tasks all at once or individually.  Use the calendar to browse by day, week, or month...";
		}

		ByUserGridView.DataBind();
	}

	protected void AllTasksLB_Click(object sender, EventArgs e)
	{
		Decimal TotalTime = 0;
		int userID = (int)Session["userID"];

		if (DirectReportsDropDown.SelectedValue == "AllUsers")
		{
			UsersBLL users = new UsersBLL();
			TimeKeeper.UsersDataTable usersDT = users.GetUsersByDirectReportsOf(userID);

			TasksBLL tasks = new TasksBLL();
			TimeKeeper.TasksDataTable directReportTasks = tasks.GetTasksByUserID(0);

			foreach (DataRow user in usersDT.Rows)
			{
				int directReportUserID = Convert.ToInt32(user["userID"]);

				TimeKeeper.TasksDataTable task = tasks.GetTasksByUserID(directReportUserID);
				directReportTasks.Merge(task);

				TotalTime += tasks.TotalTimeByUserIDByDateRange(directReportUserID, DateTime.Today.AddDays(-1000), DateTime.Today);
			}

			ByUserGridView.DataSource = directReportTasks;
			ByUserGridView.DataBind();

			Output.Text = "Your employees have worked " + TotalTime + " hrs since using TimeKeeper.";
		}
		else
		{
			TasksBLL tasks = new TasksBLL();
			TimeKeeper.TasksDataTable directReportTasks = tasks.GetTasksByUserID(Convert.ToInt32(DirectReportsDropDown.SelectedValue));

			TotalTime = tasks.TotalTimeByUserIDByDateRange(Convert.ToInt32(DirectReportsDropDown.SelectedValue), DateTime.Today.AddDays(-1000), DateTime.Today);

			ByUserGridView.DataSource = directReportTasks;
			ByUserGridView.DataBind();

			Output.Text = "The employee has worked " + TotalTime + " hrs since using TimeKeeper.";
		}
	}

	protected void AllNonProjectsLB_Click(object sender, EventArgs e)
	{
		int projectID = -1;
		int userID = (int)Session["userID"];

		TasksBLL tasks = new TasksBLL();
		TimeKeeper.TasksDataTable task = tasks.GetTasksByProjectIDByUserID(-1, userID);

		decimal NonProjectTime = tasks.TotalTimeByUserIDByProjectID(userID, projectID);

		ByUserGridView.DataSource = task;
		ByUserGridView.DataBind();

		Output.Text = "They have worked " + NonProjectTime + " hrs non-project time since using TimeKeeper.";
	}
	protected void DisplayTaskByDate(DateTime start, DateTime end)
	{
		Decimal TotalTime = 0;

		if (DirectReportsDropDown.SelectedValue == "AllUsers")
		{
			int userID = (int)Session["userID"];
			UsersBLL users = new UsersBLL();
			TimeKeeper.UsersDataTable usersDT = users.GetUsersByDirectReportsOf(userID);

			TasksBLL tasks = new TasksBLL();
			TimeKeeper.TasksDataTable directReportTasks = tasks.GetTasksByUserIDByDateRange(0, start, end);

			foreach (DataRow user in usersDT.Rows)
			{
				int directReportUserID = Convert.ToInt32(user["userID"]);
				TimeKeeper.TasksDataTable task = tasks.GetTasksByUserIDByDateRange(directReportUserID, start, end);
				directReportTasks.Merge(task);

				TotalTime += tasks.TotalTimeByUserIDByDateRange(directReportUserID, start, end);
			}

			ByUserGridView.DataSource = directReportTasks;
			ByUserGridView.DataBind();

			Output.Text = "Your employees have worked ";
		}
		else
		{
			TasksBLL tasks = new TasksBLL();
			TimeKeeper.TasksDataTable directReportTasks = tasks.GetTasksByUserIDByDateRange(Convert.ToInt32(DirectReportsDropDown.SelectedValue), start, end);

			TotalTime = tasks.TotalTimeByUserIDByDateRange(Convert.ToInt32(DirectReportsDropDown.SelectedValue), start, end);

			ByUserGridView.DataSource = directReportTasks;
			ByUserGridView.DataBind();

			Output.Text = "The employee has worked ";
		}

		if (start == end)
			Output.Text += TotalTime + " hrs on " + start.ToShortDateString();
		else
			Output.Text += TotalTime + " hrs between " + start.ToShortDateString() + " and " + end.ToShortDateString();
	}

	protected void ReportCalendar_SelectionChanged(object sender, EventArgs e)
	{
		DateTime start = ReportCalendar.SelectedDates[0];
		DateTime end = ReportCalendar.SelectedDates[ReportCalendar.SelectedDates.Count - 1];

		DisplayTaskByDate(start, end);
	}

	protected void ReportCalendar_DayRender(object sender, DayRenderEventArgs e)
	{
		TasksBLL tasks = new TasksBLL();
		TimeKeeper.TasksDataTable directReportTasks = tasks.GetTasksByUserID(0);

		if (DirectReportsDropDown.SelectedValue == "AllUsers")
		{
			int userID = (int)Session["userID"];
			UsersBLL users = new UsersBLL();
			TimeKeeper.UsersDataTable usersDT = users.GetUsersByDirectReportsOf(userID);

			foreach (DataRow user in usersDT.Rows)
			{
				int directReportUserID = Convert.ToInt32(user["userID"]);
				TimeKeeper.TasksDataTable task = tasks.GetTasksByUserIDByDateRange(directReportUserID, e.Day.Date, e.Day.Date);
				directReportTasks.Merge(task);
			}
		}
		else
		{
			directReportTasks = tasks.GetTasksByUserIDByDateRange(Convert.ToInt32(DirectReportsDropDown.SelectedValue), e.Day.Date, e.Day.Date);
		}

		// If the month is CurrentMonth
		if (!e.Day.IsOtherMonth)
		{
			foreach (DataRow dr in directReportTasks)
			{

				if ((dr["Date"].ToString() != DBNull.Value.ToString()))
				{
					DateTime dtEvent = (DateTime)dr["Date"];

					if (dtEvent.Equals(e.Day.Date))
					{
						System.Web.UI.WebControls.Image image;
						image = new System.Web.UI.WebControls.Image();
						image.ImageUrl = "images/green_diamond.gif";
						e.Cell.Controls.Add(image);
					}
				}
				//just want the first row...THIS IS VERY UGLY!
				return;
			}
		}
		//If the month is not CurrentMonth then hide the Dates
		else
		{
			e.Cell.Text = "";
		}
	}
	protected void DirectReportsDropDown_SelectedIndexChanged(object sender, EventArgs e)
	{
		Output.Text = "Select a view...";

		if (ReportCalendar.SelectedDates.Count > 0)
		{
			DateTime start = ReportCalendar.SelectedDates[0];
			DateTime end = ReportCalendar.SelectedDates[ReportCalendar.SelectedDates.Count - 1];

			DisplayTaskByDate(start, end);
		}
		ByUserGridView.DataBind();
	}

	protected void ByUserGridView_Sorting(object sender, GridViewSortEventArgs e)
	{
		GridViewSortExpression = e.SortExpression;
		int pageIndex = ByUserGridView.PageIndex;

		ByUserGridView.DataSource = SortDataTable(ByUserGridView.DataSource as DataTable, false);
		ByUserGridView.DataBind();

		ByUserGridView.PageIndex = pageIndex;
	}

	protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
	{
		if (dataTable != null)
		{
			Output.Text = "SortDataTable";
			DataView dataView = new DataView(dataTable);
			
			if (GridViewSortExpression != string.Empty)
			{
				if (isPageIndexChanging)
				{
					dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
				}
				else
				{
					dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
				}
			}
			return dataView;
		}
		else
		{
			return new DataView();
		}
	}

	private string GridViewSortDirection
	{
		get { return ViewState["SortDirection"] as string ?? "ASC"; }
		set { ViewState["SortDirection"] = value; }
	}

	private string GridViewSortExpression
	{
		get { return ViewState["SortExpression"] as string ?? string.Empty; }
		set { ViewState["SortExpression"] = value; }
	}

	private string GetSortDirection()
	{
		switch (GridViewSortDirection)
		{
			case "ASC":
				GridViewSortDirection = "DESC";
				break;
			case "DESC":
				GridViewSortDirection = "ASC";
				break;
		}
		return GridViewSortDirection;
	}
}
