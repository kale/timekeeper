using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TimeKeeperTableAdapters;

[System.ComponentModel.DataObject]
public class TasksBLL
{
	private TasksTableAdapter _TasksAdaptor = null;
    private QueriesTableAdapter _QueriesAdaptor = null;

	protected TasksTableAdapter Adaptor
	{
		get
		{
			if (_TasksAdaptor == null)
				_TasksAdaptor = new TasksTableAdapter();

			return _TasksAdaptor;
		}
	}
    
    protected QueriesTableAdapter QAdaptor
    {
        get
        {
            if (_QueriesAdaptor == null)
                _QueriesAdaptor = new QueriesTableAdapter();

            return _QueriesAdaptor;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public decimal WeeklyProjectTimeByUserID(int userID)
    {
        return Convert.ToDecimal(QAdaptor.WeeklyProjectTime(userID));
    } 

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.TasksDataTable GetTasks()
	{
		return Adaptor.GetTasks();
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTaskByTaskID(int taskID)
	{
		return Adaptor.GetTaskByTaskID(taskID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByProjectID(int projectID)
	{
		return Adaptor.GetTasksByProjectID(projectID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetProjectsForUser(int userID)
	{
		return Adaptor.GetProjectsForUser(userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByServiceID(int serviceID)
	{
		return Adaptor.GetTasksByServiceID(serviceID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByProjectIDByUserID(int projectID, int userID)
	{
		return Adaptor.GetTasksByProjectIDByUserID(projectID, userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByServiceIDByUserID(int serviceID, int userID)
	{
		return Adaptor.GetTasksByServiceIDByUserID(serviceID, userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByProjectIDByUserIDByDate(int projectID, int userID, DateTime date)
	{
		return Adaptor.GetTasksByProjectIDByUserID(projectID, userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByUserID(int userID)
	{
		return Adaptor.GetTasksByUserID(userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByUserIDByDate(int userID, DateTime date)
	{
		return Adaptor.GetTasksByUserIDByDate(userID, date);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.TasksDataTable GetTasksByUserIDByDateRange(int userID, DateTime start, DateTime end)
	{
		return Adaptor.GetTasksByUserIDByDateRange(userID, start, end);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddTask(DateTime date, decimal time, string workDone, string notes, string woNum, string rfcNum, int serviceID, int projectID, int userID, int phaseID, int assetID, int categoryID)
	{
		//Create a new TaskRow instance
		TimeKeeper.TasksDataTable Tasks = new TimeKeeper.TasksDataTable();
		TimeKeeper.TasksRow task = Tasks.NewTasksRow();

		task.Date = date;
		task.Time = time;
		task.WorkDone = workDone;
		task.Notes = notes;
		task.WONum = woNum;
		task.RFCNum = rfcNum;
		task.ServiceID = serviceID;
		task.ProjectID = projectID;
		task.UserID = userID;
		task.PhaseID = phaseID;
		task.AssetID = assetID;
		task.CategoryID = categoryID;

		//Add the new task
		Tasks.AddTasksRow(task);
		int rowsAffected = Adaptor.Update(Tasks);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdateTask(DateTime date, decimal time, string workDone, string notes, string woNum, string rfcNum, int serviceID, int projectID, int userID, int phaseID, int assetID, int categoryID, int taskID)
	{
		TimeKeeper.TasksDataTable tasks = Adaptor.GetTaskByTaskID(taskID);
		if (tasks.Count == 0)
			return false;

		TimeKeeper.TasksRow task = tasks[0];

		task.Date = date;
		task.Time = time;
		task.WorkDone = workDone;
		task.Notes = notes;
		task.WONum = woNum;
		task.RFCNum = rfcNum;
		task.ServiceID = serviceID;
		task.ProjectID = projectID;
		task.UserID = userID;
		task.PhaseID = phaseID;
		task.AssetID = assetID;
		task.CategoryID = categoryID;

		//Add the new task
		int rowsAffected = Adaptor.Update(task);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeleteTask(int taskID)
	{
		int rowsAffected = Adaptor.Delete(taskID);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}
	/*
		[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
		public decimal TotalTimeByUserIDByDate(int userID, DateTime date)
		{
			return Convert.ToDecimal(Adaptor.TotalTimeByUserIDByDate(userID, date));
		}
	*/
	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public decimal TotalTimeByUserIDByDate(int userID, DateTime date)
	{
		return Convert.ToDecimal(Adaptor.TotalTimeByUserIDByDate(userID, date));
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public decimal TotalTimeByUserIDByDateRange(int userID, DateTime start, DateTime end)
	{
		return Convert.ToDecimal(Adaptor.TotalTimeByUserIDByDateRange(userID, start, end));
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public decimal TotalTimeByProjectID(int projectID)
	{
		return Convert.ToDecimal(Adaptor.TotalTimeByProjectID(projectID));
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public decimal TotalTimeByUserIDByProjectID(int userID, int projectID)
	{
		return Convert.ToDecimal(Adaptor.TotalTimeByUserIDByProjectID(userID, projectID));
	}
}