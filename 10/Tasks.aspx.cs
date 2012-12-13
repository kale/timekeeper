using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Tasks : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			ResetControls();

			TodayLB_Click(TodayLB, new System.EventArgs());
			SetInfoBar();

			//bind the Tasks gridview
			TasksGridView.DataBind();
		}
        
		//set focus on form to time field and submit button
		Page.Form.DefaultFocus = TimeTextBox.ClientID;
		Page.Form.DefaultButton = SubmitButton.UniqueID;
	}

	/// <summary>
	/// Sets the contents of the information bar which is above the task gridview.
	/// </summary>
	protected void SetInfoBar()
	{
		TasksBLL task = new TasksBLL();
		decimal time = task.TotalTimeByUserIDByDate((int)Session["userID"], DateTime.Today);

		//The timebar is a graphical view of the total time worked today.  It grows as you add tasks is based on a
		//percentage of the shift varible or number of work hours per day.

		//shift is a temp that holds the number of hours worked per day that needs to be moved into users table
		int shift = 8;

		//number of pixels in height the bar should be
		timebarIB.Height = 3;

		//if we have worked less than our shift time display the bar in black, otherwise display it in red
		if (shift - time >= 0)
		{
			timebarIB.Width = Unit.Percentage((double)time / shift * 100);
			timebarIB.ImageUrl = "images/black_dot.gif";
			timebarIB.ToolTip = shift - time + " hrs left today!";
		}
		else
		{
			timebarIB.Width = Unit.Percentage(100);
			timebarIB.ImageUrl = "images/red_dot.gif";
			timebarIB.ToolTip = "You have worked " + (time - shift) + " extra hours today so far";
		}
	}

	/// <summary>
	/// Submits a new task to the DB.  Once complete will transfer the user back to the page to begin another task entry.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void SubmitButton_Click(object sender, EventArgs e)
	{
		TasksBLL task = new TasksBLL();

		//to clean up the add task call we define everything here first
		DateTime date = Convert.ToDateTime(DateTextBox.SelectedDate);
		decimal time = Convert.ToDecimal(TimeTextBox.Text);
		int effort = Convert.ToInt32(ServiceDropDownList.SelectedValue);
		int project = Convert.ToInt32(ProjectDropDownList.SelectedIndex) == 0 ? -1 : Convert.ToInt32(ProjectDropDownList.SelectedValue);
		int user = (int)Session["userID"];
		int phase = Convert.ToInt32(PhaseDropDown.SelectedIndex) == 0 ? -1 : Convert.ToInt32(PhaseDropDown.SelectedValue);
		int asset = Convert.ToInt32(AssetDropDown.SelectedIndex) == 0 ? -1 : Convert.ToInt32(AssetDropDown.SelectedValue);
		//int asset = Convert.ToInt32(AssetDropDown.SelectedValue);
		int category = Convert.ToInt32(CategoryDropDown.SelectedValue);

		if (task.AddTask(date, time, WorkDoneTextBox.Text, null, WOTextBox.Text, RFCTextBox.Text, effort, project, user, phase, asset, category))
		{
			TodayLB_Click(TodayLB, new System.EventArgs());
			TasksGridView.DataBind();
			SetInfoBar();
			StatsLabel.Text = "Task Added! " + StatsLabel.Text;
			ResetControls();
			ScriptManager.SetFocus(TimeTextBox);
		}
		else
			StatsLabel.Text = "Error: Task not added.";
	}

	protected void TasksGridView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
	{
		if (e.Exception != null)
		{
			// Display a user-friendly message
			ExceptionDetails.Visible = true;
			ExceptionDetails.Text = "There was a problem updating the task. ";

			if (e.Exception.InnerException != null)
			{
				Exception inner = e.Exception.InnerException;

				if (inner is System.Data.Common.DbException)
					ExceptionDetails.Text += "Our database is currently experiencing problems. Please try again later.";
				else if (inner is NoNullAllowedException)
					ExceptionDetails.Text += "There are one or more required fields that are missing.";
				else if (inner is ArgumentException)
				{
					string paramName = ((ArgumentException)inner).ParamName;
					ExceptionDetails.Text += string.Concat("The ", paramName, " value is illegal.");
				}
				else if (inner is ApplicationException) ExceptionDetails.Text += inner.Message;
			}

			// Indicate that the exception has been handled
			e.ExceptionHandled = true;

			// Keep the row in edit mode
			e.KeepInEditMode = true;
		}
	}

	protected void TasksGridView_SelectedIndexChanged(object sender, EventArgs e)
	{
		GridViewRow row = TasksGridView.SelectedRow;

		StatsLabel.Text = "You selected " + row.Cells[1].Text + ", " + row.Cells[2].Text;

		Server.Transfer("EditTask.aspx?t=" + row.Cells[1].Text);
	}

	protected void ProjectDropDownList_DataBound(object sender, EventArgs e)
	{
		//uncomment below link to test blank project dropdown
		//ProjectDropDownList.Items.Clear();

		//check to make sure user has projects to enter time on, inform them about the Project page
		if (ProjectDropDownList.Items.Count == 0)
		{
			DataEntryPanel.Visible = false;
			ViewPanel.Visible = false;
			StatsLabel.Text = "You don't have any projects to enter time on, add one or more on the <a href=\"Projects.aspx\">Projects</a> page.";
		}
	}

	protected void TodayLB_Click(object sender, EventArgs e)
	{
		TasksBLL task = new TasksBLL();
		decimal time = task.TotalTimeByUserIDByDate((int)Session["userID"], DateTime.Today);;
        decimal prjtime = task.WeeklyProjectTimeByUserID((int)Session["userID"]);
             
		TasksDataSource.SelectParameters.Clear();
		TasksDataSource.SelectMethod = "GetTasksByUserIDByDate";
		TasksDataSource.SelectParameters.Add(new Parameter("userID", TypeCode.String, Session["userID"].ToString()));
		TasksDataSource.SelectParameters.Add(new Parameter("date", TypeCode.DateTime, DateTime.Now.ToShortDateString()));

        if (prjtime > 0)
        {
            StatsLabel.Text = "You have worked for " + time + " hours today and " + prjtime.ToString() + " project hours this week.";
        }
        else
        {
            StatsLabel.Text = "You have worked for " + time + " hours today.";
        }

		ViewHelper("Today");
	}
	protected void YesterdayLB_Click(object sender, EventArgs e)
	{
		TasksBLL task = new TasksBLL();
		decimal time = task.TotalTimeByUserIDByDate((int)Session["userID"], DateTime.Today.AddDays(-1));

		TasksDataSource.SelectParameters.Clear();
		TasksDataSource.SelectMethod = "GetTasksByUserIDByDate";
		TasksDataSource.SelectParameters.Add(new Parameter("userID", TypeCode.String, Session["userID"].ToString()));
		TasksDataSource.SelectParameters.Add(new Parameter("date", TypeCode.DateTime, DateTime.Now.AddDays(-1).ToShortDateString()));

		StatsLabel.Text = "You worked " + time + " hrs yesterday";

		ViewHelper("Yesterday");
	}

	protected void PickDayLB_Click(object sender, EventArgs e)
	{
		StatsLabel.Text = "Select a date to view...";
		ViewHelper("PickDay");
	}

	protected void AllTasksLB_Click(object sender, EventArgs e)
	{
		TasksDataSource.SelectParameters.Clear();
		TasksDataSource.SelectMethod = "GetTasksByUserID";
		TasksDataSource.SelectParameters.Add(new Parameter("userID", TypeCode.String, Session["userID"].ToString()));

		StatsLabel.Text = "All your tasks...";

		ViewHelper("All");
	}

	protected void ViewHelper(string view)
	{
		TodayLB.Enabled = YesterdayLB.Enabled = PickDayLB.Enabled = AllTasksLB.Enabled = true;
		PickDayPanel.Visible = false;

		switch (view)
		{
			case "Today":
				TodayLB.Enabled = false;
				ShowNote(DateTime.Today);
				break;
			case "Yesterday":
				YesterdayLB.Enabled = false;
				ShowNote(DateTime.Today.AddDays(-1));
				break;
			case "PickDay":
				PickDayLB.Enabled = false;
				PickDayPanel.Visible = true;
				//ShowNote(DateTime.Today.AddDays(-1));
				break;
			case "All":
				AllTasksLB.Enabled = false;

				//don't display the notebox in All Tasks view
				NotesLabel.Visible = false;
				break;
		}
	}

	protected void CategoryDropDown_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (CategoryDropDown.SelectedItem.Text != "Project" && CategoryDropDown.SelectedItem.Text != "Maintenance" && CategoryDropDown.SelectedItem.Text != "Break/Fix")
		{
			EnableProject(false, false);				//disable the control so they can't update it
			ProjectDropDownList.SelectedIndex = 0;		//incase they already changed it, reset it back initial state
		}
		else if (CategoryDropDown.SelectedItem.Text == "Project")
		{
			EnableProject(true, true);
		}
		else
		{
			EnableProject(true, false);					// project is optional here
		}

		ScriptManager.SetFocus(CategoryDropDown);
	}
	protected void ProjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (ProjectDropDownList.SelectedIndex == 0)
		{
			EnablePhase(false);
			PhaseDropDown.SelectedIndex = 0;
		}
		else
		{
			ProjectsBLL project = new ProjectsBLL();

			TimeKeeper.ProjectsDataTable prj = project.GetProjectByProjectID(Convert.ToInt32(ProjectDropDownList.SelectedValue));
			TimeKeeper.ProjectsRow prjRow = prj[0];

			if (prjRow.PhaseID >= 0)
				PhaseDropDown.SelectedValue = prjRow.PhaseID.ToString();
			else
				PhaseDropDown.SelectedValue = "None";

			EnablePhase(true);
		}

		ScriptManager.SetFocus(ProjectDropDownList);
	}

	/// <summary>
	/// Clears all the controls after submitting a new task
	/// </summary>
	protected void ResetControls()
	{
		//clear all the controls
		DateTextBox.SelectedDate = DateTime.Now.Date;
		TimeTextBox.Text = String.Empty;
		WorkDoneTextBox.Text = String.Empty;
		CategoryDropDown.SelectedIndex = 0;
		ProjectDropDownList.SelectedIndex = 0;
		PhaseDropDown.SelectedIndex = 0;
		ServiceDropDownList.SelectedIndex = 0;
		AssetDropDown.SelectedIndex = 0;
		WOTextBox.Text = String.Empty;
		RFCTextBox.Text = String.Empty;

		//disable Phase until a project is selected and enable Project
		EnablePhase(false);
		EnableProject(true, true);
	}

	protected void EnablePhase(bool enable)
	{
		PhaseDropDown.Enabled = enable;

		foreach (BaseValidator valCtl in Page.Validators)
		{
			if (valCtl.ID == "PhaseRequiredFieldValidator")
				valCtl.Enabled = enable;
		}
	}

	protected void EnableProject(bool enableField, bool enableValidation)
	{
		ProjectDropDownList.Enabled = enableField;

		foreach (BaseValidator valCtl in Page.Validators)
		{
			if (valCtl.ID == "ProjectRequiredFieldValidator")
				valCtl.Enabled = enableValidation;
		}
	}

	public void ShowNote(DateTime selectedDate)
	{
		NotesBLL notes = new NotesBLL();
		TimeKeeper.NotesDataTable note = notes.GetNoteByUserID((int)Session["UserID"]);

		string noteTitle = "Your Notes:";

		if (note.Count > 0)
		{
			NotesLabel.Visible = true;

			TimeKeeper.NotesRow row = note[0];

			Session["NoteID"] = row.NotesId;

			if (note.NoteTextColumn.ToString().Length > 0)
				NotesLabel.Text = "<div id=\"notebox\"><h4>" + noteTitle + "</h4>" + row.NoteText.Replace("\n", "<br />") + "</div>";

		}
		else
		{
			NotesLabel.Visible = false;
		}
	}

	protected void PickDayCalendar_SelectionChanged(object sender, EventArgs e)
	{
		DateTime start = PickDayCalendar.SelectedDates[0];
		DateTime end = PickDayCalendar.SelectedDates[PickDayCalendar.SelectedDates.Count - 1];

		DisplayTaskByDate(start, end);
	}

	protected void PickDayCalendar_DayRender(object sender, DayRenderEventArgs e)
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

	protected void DisplayTaskByDate(DateTime start, DateTime end)
	{
		TasksBLL tasks = new TasksBLL();
		//TimeKeeper.TasksDataTable task = tasks.GetTasksByUserIDByDateRange((int)Session["userID"], start, end);

		TasksDataSource.SelectParameters.Clear();
		TasksDataSource.SelectMethod = "GetTasksByUserIDByDateRange";
		TasksDataSource.SelectParameters.Add(new Parameter("userID", TypeCode.String, Session["userID"].ToString()));
		TasksDataSource.SelectParameters.Add(new Parameter("start", TypeCode.DateTime, start.ToString()));
		TasksDataSource.SelectParameters.Add(new Parameter("end", TypeCode.DateTime, end.ToString()));

		decimal TotalTime = tasks.TotalTimeByUserIDByDateRange((int)Session["userID"], start, end);

		//TasksGridView.DataBind();

		if (start == end)
			StatsLabel.Text = "You worked " + TotalTime + " hrs on " + start.ToShortDateString(); 
		else
			StatsLabel.Text = "You worked " + TotalTime + " hrs between " + start.ToShortDateString() + " and " + end.ToShortDateString();

		//ViewHelper("PickDay");
	}
}
