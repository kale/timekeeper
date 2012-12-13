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

public partial class EditTask : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			if (Request.QueryString["t"] != null)
			{
				int taskID = Convert.ToInt32(Request.QueryString["t"]);
				PopulateForm(taskID);
			}
			else
				Server.Transfer("Tasks.aspx");
		}

		//set focus on form to time field and submit button
		Page.Form.DefaultFocus = TimeTextBox.ClientID;
		Page.Form.DefaultButton = UpdateButton.UniqueID;
	}

	protected void PopulateForm(int taskID)
	{
		TasksBLL tasks = new TasksBLL();
		TimeKeeper.TasksDataTable task = tasks.GetTaskByTaskID(taskID);

		if (task[0].UserID.ToString() == Session["userID"].ToString())
		{
			//StatsLabel.Text = "Editing Task ID = " + taskID.ToString();
			StatsLabel.Text = "Edit/view your task below...";

			DateTextBox.SelectedDate = Convert.ToDateTime(task[0].Date);
			TimeTextBox.Text = task[0].Time.ToString();
			WorkDoneTextBox.Text = task[0].WorkDone;

			CategoryDropDown.SelectedValue = task[0].CategoryID.ToString();

			if (task[0].ProjectID > 0)
			{
				EnablePhase(true);
				EnableProject(true, true);
				ProjectDropDownList.SelectedValue = task[0].ProjectID.ToString();
				PhaseDropDown.SelectedValue = task[0].PhaseID.ToString();
			}
			else
			{
				EnablePhase(false);
				EnableProject(true, false);
				ProjectDropDownList.SelectedValue = "None";
				PhaseDropDown.SelectedValue = "None";
			}

			ServiceDropDownList.SelectedValue = task[0].ServiceID > 0 ? task[0].ServiceID.ToString() : "None";
			AssetDropDown.SelectedValue = task[0].AssetID > 0 ? task[0].AssetID.ToString() : "None";

			WOTextBox.Text = task[0].WONum;
			RFCTextBox.Text = task[0].RFCNum;
		}
		else
		{
			StatsLabel.Text = "Incorrect user!";
			Server.Transfer("Tasks.aspx");
		}
	}

	/// <summary>
	/// Updates the task.  Once complete will transfer the user back to the Tasks.aspx page to begin another task entry.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void UpdateButton_Click(object sender, EventArgs e)
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
		int category = Convert.ToInt32(CategoryDropDown.SelectedValue);

		if (task.UpdateTask(date, time, WorkDoneTextBox.Text, null, WOTextBox.Text, RFCTextBox.Text, effort, project, user, phase, asset, category, Convert.ToInt32(Request.QueryString["t"])))
		{
			StatsLabel.Text = "Task Updated!";
			Server.Transfer("Tasks.aspx");
		}
		else
			StatsLabel.Text = "Error: Task could not be updated.";
	}

	protected void CancelButton_Click(object sender, EventArgs e)
	{
		Server.Transfer("Tasks.aspx");
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
}
