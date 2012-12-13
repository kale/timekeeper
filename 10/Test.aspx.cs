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

public partial class Test : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		// Expose the __doPostBack function to programmatically call it from javascript
		//Page.ClientScript.GetPostBackEventReference(this, String.Empty);

		YesButton.OnClientClick = String.Format("postbackFromJS('{0}', '{1}')", YesButton.UniqueID.ToString(), "");

		if (!Page.IsPostBack)
		{
			OutputLabel.Text = "Page Load";
		}
	}

	protected void YesButton_Click(object sender, EventArgs e)
	{
		// Code to delete the item goes here...

		OutputLabel.Text = "Item deleted";
		Server.Transfer("Tasks.aspx");
	}
}
