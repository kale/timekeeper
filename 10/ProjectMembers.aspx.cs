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

public partial class ProjectMembers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["RemoveMember"] != null)
            {
                ProjectMembersBLL projectMember = new ProjectMembersBLL();

                if (projectMember.DeleteProjectMember(Convert.ToInt32(Request.QueryString["RemoveMember"])))
                    Server.Transfer("projects.aspx");
                else
                    MessageTextBox.Text = "Error: Could not remove you as a member of this project: " + Request.QueryString["RemoveMember"].ToString();
            }
            else if (Request.QueryString["AddPrj"] != null)
            {
                ProjectMembersBLL projectMember = new ProjectMembersBLL();

                if (projectMember.AddProjectMember(Convert.ToInt32(Request.QueryString["AddPrj"]), (int)Session["userID"], "Member"))
                    Server.Transfer("projects.aspx");
                else
                    MessageTextBox.Text = "Error: Could not add you as a member of this project.";
            }
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        ProjectMembersBLL projectMember = new ProjectMembersBLL();

        if (projectMember.AddProjectMember(Convert.ToInt32(ProjectNameDropDownList.SelectedValue), Convert.ToInt32(UserNameDropDownList.SelectedValue), PositionTextBox.Text))
        {
            MessageTextBox.Text = "Project Member Added!!";
            ProjectMembersGridView.DataBind();
        }
        else
            MessageTextBox.Text = "Error: Project Member not added.";
    }
}
