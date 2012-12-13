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

public partial class MasterPage : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
			SetUserWelcome();
	}

	protected void SetUserWelcome()
	{

		string welcomeMsg;

		int time = Convert.ToInt32(DateTime.Now.ToString("HH"));
		if (time < 12)
			welcomeMsg = "Good Morning ";
		else if (time >= 12 && time <= 18)
			welcomeMsg = "Good Afternoon ";
		else
			welcomeMsg = "Good Night ";

		if (Session["userDisplayName"] != null)
			WelcomeLabel.Text = welcomeMsg + (string)Session["userDisplayName"];
		else
			WelcomeLabel.Text = welcomeMsg;
	}
}
