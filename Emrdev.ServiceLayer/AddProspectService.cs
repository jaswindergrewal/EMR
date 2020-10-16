using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddProspectService" in both code and config file together.
    public class AddProspectService : IAddProspectService
    {
        AddProspectBAL obj = new AddProspectBAL();
        
        public List<CRM_Events_ViewModel> GetAllEvents()
        {
            List<CRM_Events_ViewModel> _ProspectBAL = obj.GetAllEvents();
            return _ProspectBAL;
        }

        public List<MarketingSourceViewModel> GetHearFromProspect()
        {
            List<MarketingSourceViewModel> _ProspectHearBAL = obj.GetHearFromProspect();
            return _ProspectHearBAL;
        }

        public void InsertProspect(string Address, string City, string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                     string Notes, string State, int StatusID, string Zip, int UserName, int EventId)
        {
            obj.InsertProspect( Address,City,  Email, FirstName, Flagged,  LastName,  MainPhone,  MarketingSources,
                                     Notes,  State,  StatusID,  Zip, UserName,  EventId);
        }
    }
}
