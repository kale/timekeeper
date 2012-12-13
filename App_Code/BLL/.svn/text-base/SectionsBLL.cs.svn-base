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
public class SectionsBLL
{
	private SectionsTableAdapter _SectionsAdaptor = null;

	protected SectionsTableAdapter Adaptor
	{
		get
		{
			if (_SectionsAdaptor == null)
				_SectionsAdaptor = new SectionsTableAdapter();

			return _SectionsAdaptor;
		}
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.SectionsDataTable GetSections()
	{
		return Adaptor.GetSections();
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.SectionsDataTable GetSectionBySectionID(int sectionID)
	{
		return Adaptor.GetSectionBySectionID(sectionID);
	}

	/*
	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddSection(int userID, DateTime dateTime, string sectionText)
	{
		//Create a new SectionRow instance
		TimeKeeper.SectionsDataTable Sections = new TimeKeeper.SectionsDataTable();
		TimeKeeper.SectionsRow section = Sections.NewSectionsRow();

		section.UserId = userID;
		section.Date = dateTime;
		section.SectionText = sectionText;

		//Add the new section
		Sections.AddSectionsRow(section);
		int rowsAffected = Adaptor.Update(Sections);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdateSection(int userID, DateTime dateTime, string sectionText, int sectionID)
	{
		TimeKeeper.SectionsDataTable Sections = Adaptor.GetSectionBySectionID(sectionID);
		if (Sections.Count == 0)
			return false;

		TimeKeeper.SectionsRow section = Sections[0];

		section.UserId = userID;
		section.Date = dateTime;
		section.SectionText = sectionText;

		//Add the new section
		int rowsAffected = Adaptor.Update(section);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeleteSection(int sectionID)
	{
		int rowsAffected = Adaptor.Delete(sectionID);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}
	*/
}