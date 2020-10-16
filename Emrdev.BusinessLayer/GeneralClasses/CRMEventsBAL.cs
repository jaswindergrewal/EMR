using System.Collections.Generic;
using System.Linq;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System;

namespace Emrdev.BusinessLayer.GeneralClasses
{
   public class CRMEventsBAL
    {
       CRMEventsDAL objCRMEventsDAL = new CRMEventsDAL();

       public List<CRMEventsViewModel> GetCRMEventsDetails()
       {
           List<CRMEventsViewModel> lstObj = objCRMEventsDAL.GetCRMEventsDetails();
           return lstObj;
       }

       public List<ProspectViewmodel> GetProspectDetails()
       {
           var _objList = new List<ProspectViewmodel>();
           var ProspectEntity = new List<Prospect>();
           ProspectEntity = objCRMEventsDAL.GetAll<Prospect>(o=>o.ProspectID>0).ToList();

           Mapper.CreateMap<Prospect, ProspectViewmodel>();
           _objList = Mapper.Map(ProspectEntity, _objList);
           return _objList;
       }

       public string AddProspectData(int ProspectID, int ChkEventTrue, DateTime EventDate, string EventName, int EventID, int chkMarketSource, string MarketSourceName, int MarketSourceID,string Email)
       {
           CampaignTypeDAL ObjDAL = new CampaignTypeDAL();
           return ObjDAL.AddProspectData(ProspectID, ChkEventTrue, EventDate, EventName, EventID, chkMarketSource, MarketSourceName, MarketSourceID,Email);

       }

       public ProspectViewmodel GetProspectDetailsByID(int ProspectID)
       {
           var _objList = new ProspectViewmodel();
           var ProspectEntity = new Prospect();
           ProspectEntity = objCRMEventsDAL.Get<Prospect>(o => o.ProspectID ==ProspectID);

           Mapper.CreateMap<Prospect, ProspectViewmodel>();
           _objList = Mapper.Map(ProspectEntity, _objList);
           return _objList;
       }
    }
}
