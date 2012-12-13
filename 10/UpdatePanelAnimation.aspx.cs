// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePanelAnimation_UpdatePanelAnimation : System.Web.UI.Page
{
    /// <summary>
    /// Change the label everytime we update
    /// </summary>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
		lblUpdate.Text = "Hello World!";
    }
}