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

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		try
		{
			if (Session["errorType"] != null)
			{
				if ((string)Session["errorType"] == "UnknownUser")
				{
                    ErrorMessage.Text = "You have not been setup to access TimeKeeper.";
				}
				else
				{					
                    ErrorMessage.Text = "TimeKeeper was not able to handled the exception that was just generated.<br /><br />Error Type: " + Session["errorType"];
					
					//Display the actual exception blurp from the system
					ErrorExceptionMessage.Text = (string)Session["errorException"];
				}
			}
		} catch (Exception ex)
		{
			ErrorMessage.Text = "Error: " + ex.ToString();
		}
    }
}
