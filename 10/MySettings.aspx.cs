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

public partial class MySettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UsersBLL users = new UsersBLL();
            TimeKeeper.UsersDataTable user = users.GetUserByAccountName(User.Identity.Name);
            TimeKeeper.UsersRow row = user[0];

            AccountNameTextBox.Text = row.AccountName.ToString();
            DisplayNameTextBox.Text = row.DisplayName.ToString();
            EmailTextBox.Text = row.Email.ToString();
        }
    }
 /*
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        UsersBLL users = new UsersBLL();
        TimeKeeper.UsersDataTable user = users.GetUserByAccountName(User.Identity.Name);
        TimeKeeper.UsersRow row = user[0];

        if (users.UpdateUser(AccountNameTextBox.Text, DisplayNameTextBox.Text, EmailTextBox.Text, 0, null, row.Active, row.ManagerID, row.SectionID, (int)Session["userID"]))
        {
            MessageTextBox.Text = "Your user settings have been saved.";           
        }
        else
            MessageTextBox.Text = "Error: Not able to update your user settings.";
    }
  */
}
