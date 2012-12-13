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
public class ProjectMembersBLL
{
	private ProjectMembersTableAdapter _projectmembersAdaptor = null;

	protected ProjectMembersTableAdapter Adaptor
	{
		get
		{
			if (_projectmembersAdaptor == null)
				_projectmembersAdaptor = new ProjectMembersTableAdapter();

			return _projectmembersAdaptor;
		}
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.ProjectMembersDataTable GetProjectMembers()
	{
		return Adaptor.GetProjectMembers();
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.ProjectMembersDataTable GetProjectMemberByProjectMemberID(int projectMemberID)
	{
		return Adaptor.GetProjectMemberByProjectMemberID(projectMemberID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.ProjectMembersDataTable GetProjectMembersByUserID(int userID)
	{
		return Adaptor.GetProjectMembersByUserID(userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.ProjectMembersDataTable GetProjectMembersByProjectID(int projectID)
	{
		return Adaptor.GetProjectMembersByProjectID(projectID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddProjectMember(int projectID, int userID, string position)
	{
		//Create a new ProjectRow instance
		TimeKeeper.ProjectMembersDataTable projectmembers = new TimeKeeper.ProjectMembersDataTable();
		TimeKeeper.ProjectMembersRow projectmember = projectmembers.NewProjectMembersRow();

		projectmember.ProjectID = projectID;
		projectmember.UserID = userID;
		projectmember.Position = position;

		//Add the new projectmember
		projectmembers.AddProjectMembersRow(projectmember);
		int rowsAffected = Adaptor.Update(projectmembers);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdateProjectMember(int projectID, int userID, string position, int projectMemberID)
	{
		TimeKeeper.ProjectMembersDataTable projectmembers = Adaptor.GetProjectMemberByProjectMemberID(projectMemberID);
		if (projectmembers.Count == 0)
			return false;

		TimeKeeper.ProjectMembersRow projectmember = projectmembers[0];

		projectmember.ProjectID = projectID;
		projectmember.UserID = userID;
		projectmember.Position = position;

		int rowsAffected = Adaptor.Update(projectmember);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeleteProjectMember(int projectMemberID)
	{
		int rowsAffected = Adaptor.Delete(projectMemberID);

		//Return true if precisely one row was deleted, otherwise false
		return rowsAffected == 1;
	}
	
	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
	public int DeleteProjectMembersByProjectID(int projectID)
	{
		int rowsAffected = Adaptor.DeleteProjectMembersByProjectID(projectID);

		//Return number of rows that were deleted
		return rowsAffected;
	}
}