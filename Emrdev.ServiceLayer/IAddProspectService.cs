using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;



namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAddProspectService" in both code and config file together.
    [ServiceContract]
    public interface IAddProspectService
    {
        [OperationContract]
        List<CRM_Events_ViewModel> GetAllEvents();
        [OperationContract]
        List<MarketingSourceViewModel> GetHearFromProspect();
        [OperationContract]
        void InsertProspect(string Address, string City, string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                       string Notes, string State, int StatusID, string Zip, int UserName, int EventId);
    }
}
