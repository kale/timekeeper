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

public partial class Users : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		PopulateRoleList(Session["userID"].ToString());
		InfoBarLabel.Text = String.Empty;
		UsersGridView.DataBind();
	}

	protected void SubmitButton_Click(object sender, EventArgs e)
	{
		UsersBLL user = new UsersBLL();

		if (user.AddUser(AccountNameTextBox.Text, DisplayNameTextBox.Text, EmailTextBox.Text, Convert.ToInt32(DefaultViewDropDownList.SelectedValue), null, ActiveCheckBox.Checked, Convert.ToInt32(ManagerIDDropDownList.SelectedValue), Convert.ToInt32(SectionIDDropDownList.SelectedValue)))
		{
			InfoBarLabel.Text = "User Added!!";
			UsersGridView.DataBind();
		}
		else
			InfoBarLabel.Text = "Error: User not added.";
	}

	protected void PopulateRoleList(string userName)
	{
		RoleList.Items.Clear();

		string[] roleNames = Roles.GetAllRoles();

		foreach (string roleName in roleNames)
		{
			ListItem roleListItem = new ListItem();
			roleListItem.Text = roleName;
			roleListItem.Selected = Roles.IsUserInRole(userName, roleName);

			RoleList.Items.Add(roleListItem);
		}
	}

	protected void UsersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType.ToString() == "DataRow")
		{
			if (Roles.IsUserInRole(e.Row.Cells[4].Text.ToString(), "Manager"))
			{
				ImageButton b1 = (ImageButton)e.Row.Cells[9].Controls[0];

				b1.CommandName = "RemoveManagerRole";
				b1.ImageUrl = "~/10/images/remove.png";
			}
		}
	}

	protected void UsersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);

		GridViewRow selectedRow = UsersGridView.Rows[index];
		TableCell accountName = selectedRow.Cells[4];
		string account = accountName.Text;

		InfoBarLabel.Text += Roles.IsUserInRole(account, "Manager").ToString();

		if (e.CommandName == "AddManagerRole")
		{
			if (Roles.IsUserInRole(account, "Manager") == false)
				Roles.AddUserToRole(account, "Manager");
		}
		else if (e.CommandName == "RemoveManagerRole")
		{
			if (Roles.IsUserInRole(account, "Manager") == true)
				Roles.RemoveUserFromRole(account, "Manager");
		}

		InfoBarLabel.Text += e.CommandName.ToString();
	}
}