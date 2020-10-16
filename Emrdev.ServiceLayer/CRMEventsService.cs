using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRMEventsService" in both code and config file together.
    public class CRMEventsService : ICRMEventsService
    {
        CRMEventsBAL objCRMEventsBAL = new CRMEventsBAL();
        public void DoWork()
        {
        }

        public List<CRMEventsViewModel> GetCRMEventsDetails()
        {
            List<CRMEventsViewModel> lstObj = objCRMEventsBAL.GetCRMEventsDetails();
            return lstObj;
        }

        public List<ProspectViewmodel> GetProspectDetails()
        {
            List<ProspectViewmodel> lstObj = objCRMEventsBAL.GetProspectDetails();
            return lstObj;
        }

        public string AddProspectData(int ProspectID, int ChkEventTrue, DateTime EventDate, string EventName, int EventID, int chkMarketSource, string MarketSourceName, int MarketSourceID, string Email)
        {
           return objCRMEventsBAL.AddProspectData(ProspectID, ChkEventTrue, EventDate, EventName, EventID, chkMarketSource, MarketSourceName,MarketSourceID,Email);
           
        }

        public ProspectViewmodel GetProspectDetailsByID(int ProspectID)
        {
            ProspectViewmodel lstObj = objCRMEventsBAL.GetProspectDetailsByID(ProspectID);
            return lstObj;
        }
    }
}
