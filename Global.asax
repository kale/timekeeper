<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(Object sender, EventArgs e)
    {
        // Code that runs on application startup
		// Need to create Roles if they do not exist yet
        if (Roles.Enabled)
        {
            if (!Roles.RoleExists("Member"))
            {
                Roles.CreateRole("Member");
            }
            if (!Roles.RoleExists("Manager"))
            {
                Roles.CreateRole("Manager");
            }
            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
		Exception ex = Server.GetLastError();
        Session["errorType"] = "Unhandled Exception";
		Session["errorException"] = ex.ToString();
		//Server.Transfer("~/10/Error.aspx");
        Response.Redirect("~/10/Error.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
		try
		{
			UsersBLL users = new UsersBLL();
			TimeKeeper.UsersDataTable user = users.GetUserByAccountName(User.Identity.Name);
			TimeKeeper.UsersRow userRow = user[0];

			//setup user session values
            Session["userID"] = userRow.UserID;
            Session["accountName"] = userRow.AccountName.ToString();
            Session["userDisplayName"] = userRow.DisplayName.ToString();
            Session["sectionID"] = userRow.SectionID;

            SectionsBLL sections = new SectionsBLL();
            TimeKeeper.SectionsDataTable section = sections.GetSectionBySectionID(userRow.SectionID);
            TimeKeeper.SectionsRow sectionRow = section[0];

            Session["sectionDisplayName"] = sectionRow.Name.ToString();
		}
		catch (IndexOutOfRangeException ex)
		{
			Session["errorType"] = "UnknownUser";
			Session["errorException"] = ex.ToString();
			Server.Transfer("~/10/Error.aspx");
		}
		catch (Exception ex)
		{
			Session["errorType"] = "UnknownError";
            Session["errorException"] = ex.ToString();
			Server.Transfer("~/10/Error.aspx");
			
		}			

        if (!Roles.IsUserInRole("Member"))
            Roles.AddUserToRole(User.Identity.Name, "Member");
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>

