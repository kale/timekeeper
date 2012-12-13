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
public class TodosBLL
{
    private TodosTableAdapter _TodosAdaptor = null;

    protected TodosTableAdapter Adaptor
    {
        get
        {
            if (_TodosAdaptor == null)
                _TodosAdaptor = new TodosTableAdapter();

            return _TodosAdaptor;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public TimeKeeper.TodosDataTable GetTodos()
    {
        return Adaptor.GetTodos();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public TimeKeeper.TodosDataTable GetTodoByTodoID(int todoID)
    {
        return Adaptor.GetTodoByTodoID(todoID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public TimeKeeper.TodosDataTable GetTodosByUserID(int userID)
    {
        return Adaptor.GetTodosByUserID(userID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public TimeKeeper.TodosDataTable GetTodosByUserIDByProjectID(int userID, int projectID)
    {
        return Adaptor.GetTodosByUserIDByProjectID(userID, projectID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddTodo(string todoItem, DateTime dueDate, bool complete, DateTime completeDate, int userID, int projectID)
    {
        //Create a new TodoRow instance
        TimeKeeper.TodosDataTable Todos = new TimeKeeper.TodosDataTable();
        TimeKeeper.TodosRow todo = Todos.NewTodosRow();

        todo.TodoItem = todoItem;
        todo.DueDate = dueDate;
        todo.Complete = complete;
        todo.CompletedDate = completeDate;
        todo.UserID = userID;
        todo.ProjectID = projectID;

        //Add the new todo
        Todos.AddTodosRow(todo);
        int rowsAffected = Adaptor.Update(Todos);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateTodo(string todoItem, DateTime dueDate, bool complete, DateTime completeDate, int userID, int projectID, int todoID)
    {
        TimeKeeper.TodosDataTable todos = Adaptor.GetTodoByTodoID(todoID);
        if (todos.Count == 0)
            return false;

        TimeKeeper.TodosRow todo = todos[0];

        todo.TodoItem = todoItem;
        todo.DueDate = dueDate;
        todo.Complete = complete;
        todo.CompletedDate = completeDate;
        todo.UserID = userID;
        todo.ProjectID = projectID;

        //Add the new todo
        int rowsAffected = Adaptor.Update(todo);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteTodo(int todoID)
    {
        int rowsAffected = Adaptor.Delete(todoID);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }
}