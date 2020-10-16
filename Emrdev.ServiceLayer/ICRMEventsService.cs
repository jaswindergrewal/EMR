using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICRMEventsService" in both code and config file together.
    [ServiceContract]
    public interface ICRMEventsService
    {
        [OperationContract]
        List<CRMEventsViewModel> GetCRMEventsDetails();

        [OperationContract]
        List<ProspectViewmodel> GetProspectDetails();

        [OperationContract]
        string AddProspectData(int ProspectID, int ChkEventTrue, DateTime EventDate, string EventName, int EventID, int chkMarketSource, string MarketSourceName, int MarketSourceID, string Email);

        [OperationContract]
        void DoWork();

        [OperationContract]
        ProspectViewmodel GetProspectDetailsByID(int ProspectID);
       
    }
}
