using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AddProspectBAL
    {
        AddProspectDAL obj = new AddProspectDAL();
        
        
        /// <summary>
        /// Methods to get the list of all events
        /// Changes by jaswinder to show only enabled=true events 19 nov 2013
        /// </summary>
        /// <returns></returns>
        public List<CRM_Events_ViewModel> GetAllEvents()
        {
            var crm_Events = obj.List<CRM_Events>();
            List<CRM_Events_ViewModel> objEvents = (from p in crm_Events where p.Enabled==true

                                                    select new CRM_Events_ViewModel
                                          {
                                              EventName = p.EventName,
                                              EventID=p.EventID
                                          }).Distinct().ToList();
            return objEvents;
        }


        /// <summary>
        /// Methods to get the list of all marketing sources
        /// </summary>
        /// <returns></returns>
        public List<MarketingSourceViewModel> GetHearFromProspect()
        {
            var CRM_MarketingSources = obj.List<Emrdev.DataLayer.CRM_MarketingSources>();
            List<MarketingSourceViewModel> objProspect = (from p in CRM_MarketingSources
                                                          where p.Active_YN==true
                                                          select new MarketingSourceViewModel
                                                      {
                                                        MarketingSourceName=p.MarketingSourceName,
                                                        MarketingSourceID=p.MarketingSourceID
                                                      }).Distinct().ToList();
            return objProspect;
        }


        /// <summary>
        /// Methods to insert crm prospect
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="City"></param>
        /// <param name="Email"></param>
        /// <param name="FirstName"></param>
        /// <param name="Flagged"></param>
        /// <param name="LastName"></param>
        /// <param name="MainPhone"></param>
        /// <param name="MarketingSources"></param>
        /// <param name="Notes"></param>
        /// <param name="State"></param>
        /// <param name="StatusID"></param>
        /// <param name="Zip"></param>
        /// <param name="UserName"></param>
        /// <param name="EventId"></param>
        public void InsertProspect( string Address, string City,string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                       string Notes, string State, int StatusID, string Zip, int UserName,int EventId)
        {

            CRM_Prospects pros = new CRM_Prospects();
           
            pros.Address = Address;
            pros.City = City;
            pros.Email = Email;
            pros.FirstName = FirstName;
            pros.Flagged = Flagged;
            pros.LastName = LastName;
            pros.MainPhone = MainPhone;
            pros.MarketingSources = MarketingSources;
            pros.Notes = Notes;
            pros.State = State;
            pros.StatusID = StatusID;
            pros.Zip = Zip;
            pros.CreatedBy = UserName;
            obj.Create(pros);

            //Added by jaswinder to insert events only if they are selected in relation table
            if (EventId > 0)
            {

                CRM_Registrants res = new CRM_Registrants();
                res.EventID = EventId;
                res.ProspectID = pros.ProspectID;
                obj.Create(res);
            }

            CRM_Log log = new CRM_Log();
		    log.DateEntered = DateTime.Now;
		    log.EnteredBy = UserName;
		    log.NewStatus = 4;
		    log.ProspectID = pros.ProspectID;
		    obj.Create(log);

            CampaignTypeDAL objCRMprospectinsert = new CampaignTypeDAL();
            objCRMprospectinsert.InserMarketSource(pros.ProspectID, MarketingSources, false,1);

        }
    
    }
}
