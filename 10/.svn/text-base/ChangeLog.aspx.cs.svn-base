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

public partial class ChangeLog : System.Web.UI.Page
{
	// TODO rename configfilePath
	protected string changelogFileName = ConfigurationManager.AppSettings["configfilePath"];

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			SaveButton.Visible = false;

			//if user is not an admin then hid button completely
			if (!User.IsInRole("Administrator"))
				UpdateButton.Visible = false;

			if (System.IO.File.Exists(Server.MapPath(changelogFileName)))
			{
				System.IO.StreamReader StreamReader = new System.IO.StreamReader(Server.MapPath(changelogFileName));
				ChangeLogTextBox.Text = StreamReader.ReadToEnd();
				StreamReader.Close();
			}
		}
	}
	protected void SaveButton_Click(object sender, EventArgs e)
	{
		System.IO.StreamWriter StreamWriter = new System.IO.StreamWriter(Server.MapPath(changelogFileName));
		StreamWriter.WriteLine(ChangeLogTextBox.Text);
		StreamWriter.Close();

		UpdateButton.Visible = true;
		ChangeLogTextBox.ReadOnly = true;

		SaveButton.Visible = false;
	}
	protected void UpdateButton_Click(object sender, EventArgs e)
	{
		//In update mode now, so first hide the update button and show the Save button
		UpdateButton.Visible = false;
		SaveButton.Visible = true;

		//Next make the textbox not readonly so the admin can update
		ChangeLogTextBox.ReadOnly = false;
	}
}
