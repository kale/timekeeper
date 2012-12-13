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
public class PhasesBLL
{
	private PhasesTableAdapter _PhasesAdaptor = null;

	protected PhasesTableAdapter Adaptor
	{
		get
		{
			if (_PhasesAdaptor == null)
				_PhasesAdaptor = new PhasesTableAdapter();

			return _PhasesAdaptor;
		}
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.PhasesDataTable GetPhases()
	{
		return Adaptor.GetPhases();
	}

	/*
	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
	public TimeKeeper.PhasesDataTable GetPhaseByPhaseID(int phaseID)
	{
		return Adaptor.GetPhaseByPhaseID(phaseID);
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
	public bool AddPhase(int userID, DateTime dateTime, string phaseText)
	{
		//Create a new PhaseRow instance
		TimeKeeper.PhasesDataTable Phases = new TimeKeeper.PhasesDataTable();
		TimeKeeper.PhasesRow phase = Phases.NewPhasesRow();

		phase.UserId = userID;
		phase.Date = dateTime;
		phase.PhaseText = phaseText;

		//Add the new phase
		Phases.AddPhasesRow(phase);
		int rowsAffected = Adaptor.Update(Phases);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
	public bool UpdatePhase(int userID, DateTime dateTime, string phaseText, int phaseID)
	{
		TimeKeeper.PhasesDataTable Phases = Adaptor.GetPhaseByPhaseID(phaseID);
		if (Phases.Count == 0)
			return false;

		TimeKeeper.PhasesRow phase = Phases[0];

		phase.UserId = userID;
		phase.Date = dateTime;
		phase.PhaseText = phaseText;

		//Add the new phase
		int rowsAffected = Adaptor.Update(phase);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
	public bool DeletePhase(int phaseID)
	{
		int rowsAffected = Adaptor.Delete(phaseID);

		//Return true if precisely one row was inserted, otherwise false
		return rowsAffected == 1;
	}
	*/
}