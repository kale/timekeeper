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

public partial class DailyNotes : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			ShowNote();

			NotesTextBox.BackColor = System.Drawing.Color.AliceBlue;
		}
	}

	public void ShowNote()
	{
		NotesBLL notes = new NotesBLL();
		TimeKeeper.NotesDataTable note = notes.GetNoteByUserID((int)Session["UserID"]);

		if (note.Count > 0)
		{
			TimeKeeper.NotesRow row = note[0];

			Session["NoteID"] = row.NotesId;

			if (note.NoteTextColumn.ToString().Length > 0)
				NotesTextBox.Text = row.NoteText.ToString();
		}
		else
		{
			NotesTextBox.Text = String.Empty;
			NoteLabel.Text = "Empty";
		}
	}

	protected void SubmitButton_Click(object sender, EventArgs e)
	{
		NotesBLL notes = new NotesBLL();
		TimeKeeper.NotesDataTable note = notes.GetNoteByUserID((int)Session["UserID"]);

		if (NoteLabel.Text == "Empty")
		{
			if (notes.AddNote((int)Session["UserID"], NotesTextBox.Text))
			{
				NoteLabel.Text = "Why? " + NoteLabel.Text.ToString();
				NotesTextBox.BackColor = System.Drawing.Color.White;
			}
			else
				NoteLabel.Text = "Could not create a new note for today!";
		}
		else
		{
			if (NotesTextBox.Text.ToString().Length == 0)
			{
				notes.DeleteNote((int)Session["NoteID"]);
			}
			else if (notes.UpdateNote((int)Session["UserID"], NotesTextBox.Text, (int)Session["NoteID"]))
			{
				NoteLabel.Text = "Note updated!";
				NotesTextBox.BackColor = System.Drawing.Color.White;
			}
			else
				NoteLabel.Text = "Could not update the note!";
		}
	}
}
