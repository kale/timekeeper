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
			//if the user is not a Manager or Administrator we will not display the My Employees section
			if (Roles.IsUserInRole("Manager"))
			{
				DirectReportsDataSource.SelectParameters.Add((new Parameter("userID", TypeCode.String, Session["userID"].ToString())));
			}
			else
				MyEmployeeLabel.Visible = false;
			Output.Text = "Select the report you would like to view from the left navigation bar...";
		}
	}

	protected void DirectReportsBulletedList_Click(object sender, BulletedListEventArgs e)
	{
		int userID = Convert.ToInt32(DirectReportsBulletedList.Items[e.Index].Value);

		TasksBLL tasks = new TasksBLL();

		TimeKeeper.TasksDataTable task = tasks.GetTasksByUserID(userID);

		ByUserGridView.DataSource = task;
		ByUserGridView.DataBind();

		Output.Text = "All tasks for " + DirectReportsBulletedList.Items[e.Index].Text;
		MainMultiView.ActiveViewIndex = 0;
	}

	protected void ProjectsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ProjectsDropDownList.SelectedValue != "None")
		{
			int projectID = Convert.ToInt32(ProjectsDropDownList.SelectedValue);
			int userID = (int)Session["userID"];

			TasksBLL tasks = new TasksBLL();
			TimeKeeper.TasksDataTable task = tasks.GetTasksByProjectIDByUserID(projectID, userID);

			decimal ProjectTime = tasks.TotalTimeByUserIDByProjectID(userID, projectID);

			ByProjectGridView.DataSource = task;
			ByProjectGridView.DataBind();

			Output.Text = "You have worked " + ProjectTime.ToString() + " hrs on the " + ProjectsDropDownList.SelectedItem.Text + " project";
			MainMultiView.ActiveViewIndex = 1;
		}
		else
			Server.Transfer("Reports.aspx");
	}

	protected void ServicesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ServicesDropDownList.SelectedValue != "None")
		{
			int serviceID = Convert.ToInt32(ServicesDropDownList.SelectedValue);
			int userID = (int)Session["userID"];

			TasksBLL tasks = new TasksBLL();
			TimeKeeper.TasksDataTable task = tasks.GetTasksByServiceIDByUserID(serviceID, userID);

			ByServiceGridView.DataSource = task;
			ByServiceGridView.DataBind();

			Output.Text = "All your tasks for the " + ServicesDropDownList.SelectedItem.Text + " effort type.";
			MainMultiView.ActiveViewIndex = 2;
		}
		else
			Server.Transfer("Reports.aspx");
	}

	protected void DetailedReportsLinkButton_Click(object sender, EventArgs e)
	{
		Output.Text = "Detailed Reports";
		MainMultiView.ActiveViewIndex = 3;
	}

	protected void AllTasksLB_Click(object sender, EventArgs e)
	{
		int userID = (int)Session["userID"];

		TasksBLL tasks = new TasksBLL();
		TimeKeeper.TasksDataTable task = tasks.GetTasksByUserID(userID);

		decimal TotalTime = tasks.TotalTimeByUserIDByDateRange(userID, DateTime.Today.AddDays(-1000), DateTime.Today);

		ByUserGridView.DataSource = task;
		ByUserGridView.DataBind();

		Output.Text = "You have worked " + TotalTime + " hrs since using TimeKeeper.";

		MainMultiView.ActiveViewIndex = 0;
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

		Output.Text = "You have worked " + NonProjectTime + " hrs non-project time since using TimeKeeper.";

		MainMultiView.ActiveViewIndex = 0;
	}
	protected void DisplayTaskByDate(DateTime start, DateTime end)
	{
		TasksBLL tasks = new TasksBLL();
		TimeKeeper.TasksDataTable task = tasks.GetTasksByUserIDByDateRange((int)Session["userID"], start, end);

		decimal TotalTime = tasks.TotalTimeByUserIDByDateRange((int)Session["userID"], start, end);

		ByUserGridView.DataSource = task;
		ByUserGridView.DataBind();

		if (start == end)
			Output.Text = "You worked " + TotalTime + " hrs on " + start.ToShortDateString();
		else
			Output.Text = "You worked " + TotalTime + " hrs between " + start.ToShortDateString() + " and " + end.ToShortDateString();

		MainMultiView.ActiveViewIndex = 0;
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
		TimeKeeper.TasksDataTable task = tasks.GetTasksByUserIDByDateRange((int)Session["userID"], e.Day.Date, e.Day.Date);

		// If the month is CurrentMonth
		if (!e.Day.IsOtherMonth)
		{
			foreach (DataRow dr in task)
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

	protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
	{
		// Check to see the if the current sort (GridView1.SortExpression)
		// matches the requested sort (e.SortExpression).
		// This code tries to match the beginning of the GridView
		// sort expression. The final ASC or DESC part is ignored.
		if (ByUserGridView.SortExpression.StartsWith(e.SortExpression))
		{
			// This sort is being applied to the same field for the second time.
			// Reverse it.
			if (ByUserGridView.SortDirection == SortDirection.Ascending)
			{
				// This takes care of automatically adding the "DESC"
				// to the end of the sort expression.
				e.SortDirection = SortDirection.Descending;
			}
		}
	}
}
