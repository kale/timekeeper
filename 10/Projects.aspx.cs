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
using System.Xml;
using com.domian.subdomain;

public partial class Projects : System.Web.UI.Page
{
	//TODO: change these GUID's to match your project site
    protected const string PROJECT_BOARD_GUID = "{AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA}";
    protected const string PROJECT_BOARD_VIEW_GUID = "{BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB}";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			//hide the pull output
			PullOutputLabel.Visible = false;
		}
	}

	protected int PullPrjBoard(bool PullActiveOnly, bool UpdateProjects)
	{
		string output = "";     //holds label output
		int count = 0;          //count of number of projects pulled, will return this value

		Lists listService = new Lists();
		ProjectsBLL project = new ProjectsBLL();

        listService.PreAuthenticate = true;
		listService.Credentials = System.Net.CredentialCache.DefaultCredentials;

		XmlDocument xmlDoc = new XmlDocument();
		XmlNode ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "");
		XmlNode ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "");
		XmlNode ndQueryOptions = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "");

        ndQueryOptions.InnerXml = "";// "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>" + "<DateInUtc>TRUE</DateInUtc>";
        ndViewFields.InnerXml = "";// "<FieldRef Name='Title'/><FieldRef Name='Column5'/><FieldRef Name='Column4'/><FieldRef Name='Completed'/><FieldRef Name='Column12'/><FieldRef Name='Project_x0020_Site_x0020_URL'/><FieldRef Name='Project_x0020_Manager2'/><FieldRef Name='IT_x0020_Technical_x0020_Lead'/><FieldRef Name='Project_x0020_Phase'/>";

		//query XML with only completed items or all items?
		if (PullActiveOnly)
			ndQuery.InnerXml = "<Where><Eq><FieldRef Name='Completed'/><Value Type='Number'>0</Value></Eq></Where>";
		else
			ndQuery.InnerXml = "";

		try
		{
			XmlNode ndListItems;
			Hashtable prjHash = new Hashtable();

			//call web service to get all the projects from the PMO Project Board
			ndListItems = listService.GetListItems(PROJECT_BOARD_GUID, PROJECT_BOARD_VIEW_GUID, ndQuery, ndViewFields, null, ndQueryOptions);

			//create xml document so we can process
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(ndListItems.OuterXml);

			foreach (XmlNode element in doc.ChildNodes[0].ChildNodes[0].ChildNodes)
			{
				if (element.Attributes != null)
				{
					foreach (XmlAttribute attr in element.Attributes)
					{
						switch (attr.Name)
						{
							//TODO: This will be different based on the fields in your SharePoint list.
							case "ows_Title":
								prjHash.Add("ProjectNumber", attr.Value);
								break;
                            case "ows_NSR_x002f_Project_x0020_Name":
								prjHash.Add("Name", attr.Value);
								break;
							case "ows_Completed":
								//list contains 0 if not compeleted and -1 if completed
								if (Convert.ToInt32(attr.Value) == 0)
									prjHash.Add("Active", true);
								else
									prjHash.Add("Active", false);
								break;
                            case "ows_Approved":
								prjHash.Add("Approved", attr.Value);
								break;
                            case "ows_Current_x0020_Status":
								prjHash.Add("Status", attr.Value);
								break;
                            case "ows_Project_x0020_Site_x0020_URL":
								//list returns value as url,url.  we only need it once, so using split we take the first one
								prjHash.Add("SiteURL", attr.Value.Split(',')[0]);
								break;
                            case "ows_Project_x0020_Manager":
                                prjHash.Add("ProjectManager", attr.Value.Split('#')[1]);
                                break;
							case "ows_Project_x0020_Phase":
								prjHash.Add("PhaseID", attr.Value);
								break;
							case "ows_ID":
								//once the attribute equals "ows_ID" we know we can process

								//setup some temp holders
								int projectID = Convert.ToInt32(attr.Value);
								string number = prjHash["ProjectNumber"].ToString();
								string name = prjHash["Name"].ToString();
                                
                                string projectManager;
                                if (prjHash.ContainsKey("ProjectManager"))
                                    projectManager = prjHash["ProjectManager"].ToString();
                                else
                                    projectManager = "";

								string siteURL;
								if (prjHash.ContainsKey("SiteURL"))
									siteURL = prjHash["SiteURL"].ToString();
								else
									siteURL = null;

								int phaseID;
								if (prjHash.ContainsKey("PhaseID"))
								{
									switch (prjHash["PhaseID"].ToString())
									{
										case "Initiating":
											phaseID = 0;
											break;
										case "Planning":
											phaseID = 1;
											break;
										case "Execution":
											phaseID = 2;
											break;
										case "Monitoring":
											phaseID = 3;
											break;
										case "Closing":
											phaseID = 4;
											break;
										case "Proposals":
											phaseID = 5;
											break;
										case "Startups/Turndowns":
											phaseID = 6;
											break;
                                        case "Startup":
                                            phaseID = 7;
                                            break;
                                        case "Turndown":
                                            phaseID = 8;
                                            break;
                                        case "Win-awaiting NTP":
                                            phaseID = 9;
                                            break;
										default:
											phaseID = -1;
											break;
									}
								}
								else
									phaseID = -1;

								bool active;
                                if (Convert.ToBoolean(prjHash["Active"]) && (prjHash["Approved"].ToString() == "Approved as Project" || prjHash["Approved"].ToString() == "NSR Analysis"))
									active = true;
								else
									active = false;

                                //DEBUG PRINT
                                //output += "<tr><td>Debug: " + number + " - " + name + "</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";

								//first we check to see if this project already exists
								if (project.ProjectIDExists(projectID) < 1)
								{
									output += "<tr><td>Adding: " + number + " - " + name + "</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";

									//add the project to the db, first checking to see if SiteURL exists and is not empty
									if (prjHash.ContainsKey("SiteURL") && (string)prjHash["SiteURL"] != String.Empty)
										project.AddProject(projectID, number, name, siteURL, active, phaseID, projectManager);
									else
										project.AddProject(projectID, number, name, null, active, phaseID, projectManager);

									output += "<td>Complete!</td></tr>";

									count++;
								}
								else
								{
									//otherwise the project already exists and we can update it
									if (UpdateProjects)
									{
										TimeKeeper.ProjectsDataTable prj = project.GetProjectByProjectID(projectID);
										TimeKeeper.ProjectsRow prjRow = prj[0];

										
										//output += "<tr><td>Update - " + number + " - " + name + "</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";
										//output += "<tr><td>active = " + active.ToString() + "</tr></td>";
										//count++;
										

										if (prjRow.Active != active)
										{
											//RUN DEACTIVATE PROJECT CODE HERE
											if (active == false)
											{
												ProjectMembersBLL projectMembers = new ProjectMembersBLL();

												int membersDeleted = projectMembers.DeleteProjectMembersByProjectID(projectID);

												output += "<tr><td>Deactivating: " + number + " - " + name + ", Removed " + membersDeleted.ToString() + " member(s)</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";
												project.UpdateProject(projectID, number, name, null, false, phaseID, projectManager);
												output += "<td>Complete!</td></tr>";
											}
											else
											{
												output += "<tr><td>Activating: " + number + " - " + name + "</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";
												project.UpdateProject(projectID, number, name, null, true, phaseID, projectManager);
												output += "<td>Complete!</td></tr>";
											}
											count++;
										}

										if (prjRow.ProjectNumber != number || prjRow.Name != name || prjRow.PhaseID != phaseID || prjRow.ProjectManager.ToString() != projectManager)
										{
											//it does exist, print message
											output += "<tr><td>Updating: " + number + " - " + name + "</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";

											if (prjHash.ContainsKey("SiteURL") && (string)prjHash["SiteURL"] != String.Empty)
											{
												project.UpdateProject(projectID, number, name, siteURL, active, phaseID, projectManager);
											}
											else
											{
												project.UpdateProject(projectID, number, name, null, active, phaseID, projectManager);
											}
											output += "<td>Complete!</td></tr>";

											count++;
										}
										if (prjRow.IsTeamSiteURLNull() && prjHash.ContainsKey("SiteURL") && (string)prjHash["SiteURL"] != String.Empty)
										{
											//there is a new team site that we need to add
											output += "<tr><td>Team Site added: " + number + " - " + name + "</td><td style=\"color: #FFFFE0;\"> (ID = " + attr.Value + ")</td>";
											project.UpdateProject(projectID, number, name, siteURL, active, phaseID, projectManager);
											output += "<td>Complete!</td></tr>";

											count++;
										}
									}
								}
								break;
						}
					}
				}

				//clear the hash for the next element
				prjHash.Clear();
			}
		}
		catch (System.Web.Services.Protocols.SoapException ex)
		{
			output = ex.StackTrace;
		}

		//set label
		PullOutputLabel.Text = output;

		return count;
	}

	protected void ProjectsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			TimeKeeper.ProjectsRow project = (TimeKeeper.ProjectsRow)((System.Data.DataRowView)e.Row.DataItem).Row;

			if (!project.IsMemberIDNull())
			{
				//user is associated with project, let them know they are and allow them to delete
				HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

				hl.NavigateUrl = "ProjectMembers.aspx?RemoveMember=" + project.MemberID.ToString();
				hl.Text = @"<img src='images/remove.png'>";
			}
		}
	}

	protected void PullLinkButton_Click(object sender, EventArgs e)
	{
		int count = 0;

		count = PullPrjBoard(false, true);

		if (count > 0)
		{
			InfoLabel.Text = "Pulled/updated " + count + " projects from the PMO Project Board (<a href=\"#Details\">Details</a>):";
			ImportLogLabel.Text += "<br /><br /><div id=\"informationbar\"><h3><a name=\"Details\">Details:</a></h3><table>" + PullOutputLabel.Text + "</table></div>";

			ProjectsGridView.DataBind();
		}
		else
			InfoLabel.Text = "No projects to pull or update!";
	}
}
