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
public class ServicesBLL
{
    private ServicesTableAdapter _ServicesAdaptor = null;

    protected ServicesTableAdapter Adaptor
    {
        get
        {
            if (_ServicesAdaptor == null)
                _ServicesAdaptor = new ServicesTableAdapter();

            return _ServicesAdaptor;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public TimeKeeper.ServicesDataTable GetServices()
    {
        return Adaptor.GetServices();
    }

	[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
	public TimeKeeper.ServicesDataTable GetServicesBySectionID(int sectionID)
	{
		return Adaptor.GetServicesBySectionID(sectionID);
	}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public TimeKeeper.ServicesDataTable GetServicesByServiceID(int serviceID)
    {
        return Adaptor.GetServiceByServiceID(serviceID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddService(string name, string description, string examples, int sectionID)
    {
        //Create a new ServiceRow instance
        TimeKeeper.ServicesDataTable Services = new TimeKeeper.ServicesDataTable();
        TimeKeeper.ServicesRow service = Services.NewServicesRow();

        service.Name = name;
        service.Description = description;
		service.Examples = examples;
		service.SectionID = sectionID;
		service.Core = false;				//all efforts added by the UI will not be core, so we hardcode that here

        //Add the new service
        Services.AddServicesRow(service);
        int rowsAffected = Adaptor.Update(Services);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateService(string name, string description, string examples, int serviceID)
    {
        TimeKeeper.ServicesDataTable services = Adaptor.GetServiceByServiceID( serviceID );
        if (services.Count == 0)
            //no matching record found, return false
            return false;

        TimeKeeper.ServicesRow service = services[0];

        service.Name = name;
        service.Description = description;
		service.Examples = examples;

        //Update the service record
        int rowsAffected = Adaptor.Update(services);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteService(int serviceID)
    {
        int rowsAffected = Adaptor.Delete(serviceID);

        //Return true if precisely one row was inserted, otherwise false
        return rowsAffected == 1;
    }
}

