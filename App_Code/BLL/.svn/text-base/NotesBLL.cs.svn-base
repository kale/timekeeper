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
public class NotesBLL
{
	private NotesTableAdapter _NotesAdaptor = null;

	protected NotesTableAdapter Adaptor
	{
		get
		{
			if (_NotesAdaptor == null)
				_NotesAdaptor = new NotesTableAdapter();

			return _NotesAdaptor;
		}
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.NotesDataTable GetNotes()
	{
		return Adaptor.GetNotes();
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.NotesDataTable GetNoteByNoteID(int noteID)
	{
		return Adaptor.GetNoteByNoteID(noteID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.NotesDataTable GetNoteByUserID(int userID)
	{
		return Adaptor.GetNoteByUserID(userID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddNote(int userID, string noteText)
	{
		//Create a new NoteRow instance
		TimeKeeper.NotesDataTable Notes = new TimeKeeper.NotesDataTable();
		TimeKeeper.NotesRow note = Notes.NewNotesRow();

		note.UserId = userID;
		note.NoteText = noteText;

		//Add the new note
		Notes.AddNotesRow(note);
		int rowsAffected = Adaptor.Update(Notes);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdateNote(int userID, string noteText, int noteID)
	{
		TimeKeeper.NotesDataTable notes = Adaptor.GetNoteByNoteID(noteID);
		if (notes.Count == 0)
			return false;

		TimeKeeper.NotesRow note = notes[0];

		note.UserId = userID;
		note.NoteText = noteText;

		//Add the new note
		int rowsAffected = Adaptor.Update(note);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeleteNote(int noteID)
	{
		int rowsAffected = Adaptor.Delete(noteID);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}
}