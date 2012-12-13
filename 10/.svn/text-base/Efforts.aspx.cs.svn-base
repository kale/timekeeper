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

public partial class Efforts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ServicesGridView.Columns[1].Visible = false;	//hide delete button for everyone...

        if (!Page.IsPostBack)
        {
            //if the user is not a Manager or Administrator we will only display the services for review
            if (!Roles.IsUserInRole("Manager") && !Roles.IsUserInRole("Administrator"))
            {
                ServicesFormView.Visible = false;				//hide data entry form
                ServicesGridView.Columns[0].Visible = false;	//hide edit button
                

                //MessageLabel.Text = "Please review the list of effort types that are availible to use.  If you would like to suggest one please see your manager.";
            }
        }

		//set section label to display to user based on session variable
		SectionLabel.Text = (string)Session["sectionDisplayName"];
    }
	protected void ServicesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			TimeKeeper.ServicesRow effort = (TimeKeeper.ServicesRow)((System.Data.DataRowView)e.Row.DataItem).Row;

			if (effort.Core == true)
			{
				//core effort, label accordingly
				e.Row.Font.Bold = true;

				e.Row.Cells[0].Text = String.Empty;
				e.Row.Cells[1].Text = String.Empty;
			}
		}
	}
}
