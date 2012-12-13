using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TimeKeeperTableAdapters;

[System.ComponentModel.DataObject]
public class UsersBLL
{
    private UsersTableAdapter _usersAdaptor = null;

    protected UsersTableAdapter Adaptor
    {
        get
        {
            if (_usersAdaptor == null)
                _usersAdaptor = new UsersTableAdapter();

            return _usersAdaptor;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public TimeKeeper.UsersDataTable GetUsers()
    {
        return Adaptor.GetUsers();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public TimeKeeper.UsersDataTable GetUserByUserID(int userID)
    {
        return Adaptor.GetUserByUserID(userID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public TimeKeeper.UsersDataTable GetUserByAccountName(string AccountName)
    {
        return Adaptor.GetUserByAccountName(AccountName);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public TimeKeeper.UsersDataTable GetUsersByDirectReportsOf(int userID)
    {
        return Adaptor.GetUsersByDirectReportsOf(userID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddUser(string accountName, string displayName, string email, int defaultView, string role, bool active, int managerID, int sectionID)
    {
        //Create a new UserRow instance
        TimeKeeper.UsersDataTable users = new TimeKeeper.UsersDataTable();
        TimeKeeper.UsersRow user = users.NewUsersRow();

        user.AccountName = accountName;
        user.DisplayName = displayName;
        user.Email = email;
        user.DefaultView = defaultView;
        user.Role = role;
        user.Active = active;
        user.ManagerID = managerID;
		user.SectionID = sectionID;

        //Add the new user
        users.AddUsersRow(user);
        int rowsAffected = Adaptor.Update(users);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateUser(string accountName, string displayName, string email, int defaultView, string role, bool active, int managerID, int sectionID, int userID)
    {
        TimeKeeper.UsersDataTable users = Adaptor.GetUserByUserID(userID);
        if (users.Count == 0)
            return false;

        TimeKeeper.UsersRow user = users[0];

        user.AccountName = accountName;
        user.DisplayName = displayName;
        user.Email = email;
        user.DefaultView = defaultView;
        user.Role = role;
        user.Active = active;
        user.ManagerID = managerID;
		user.SectionID = sectionID;

        int rowsAffected = Adaptor.Update(user);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteUser(int userID)
    {
        int rowsAffected = Adaptor.Delete(userID);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }
}