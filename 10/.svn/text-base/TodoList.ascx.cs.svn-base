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

public partial class TodoList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TodosBLL todos = new TodosBLL();
        todos.GetTodosByUserID((int)Session["userID"]);

        TodosGridView.DataBind();
    }
    protected void SubmitLinkButton_Click(object sender, EventArgs e)
    {
        TodosBLL todos = new TodosBLL();
        TimeKeeper.TodosDataTable todo = todos.GetTodosByUserID((int)Session["userID"]);
        TimeKeeper.TodosRow row = todo[0];

        if (todos.AddTodo(TodoItemTextBox.Text, Convert.ToDateTime(DueDateTextBox.Text), false, Convert.ToDateTime(DueDateTextBox.Text), (int)Session["userID"], 0))
        {
            EntryViewLabel.Text = "Todo Item has been added.";
        }
        else
            EntryViewLabel.Text = "Error: Not able to add todo item.";
    }
}
