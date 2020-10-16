using System;
using System.Collections.Generic;
using System.Linq;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using AutoMapper;
using Emrdev.DataLayer;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI;
using System.Net;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class ManageBAL
    {
        ManageDAL obj = new ManageDAL();

        public dynamic GetAllapt_recs()
        {
            var objAppointments = obj.GetAllAppointments();
            return objAppointments;
        }

        public List<CRM_Campaigns_ViewModel> GetAllCrmCampaign()
        {
            CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
            return objCampaignTypeDAL.GetAllCrmCampaign();
            //var objCrmCampaign = new List<CRM_Campaigns_ViewModel>();
            //List<CRM_Campaigns> CrmCampaignEntity = obj.GetAll<CRM_Campaigns>(o => o.Enabled).OrderBy(o => o.CampaignName).ToList();
            //Mapper.CreateMap<CRM_Campaigns, CRM_Campaigns_ViewModel>();
            //objCrmCampaign = Mapper.Map(CrmCampaignEntity, objCrmCampaign);
            //return objCrmCampaign;
        }


        public List<CRM_Status_ViewModel> GetAllactiveStatus()
        {
            var objstatus = new List<CRM_Status_ViewModel>();
            //List<CRM_Status> statusEntity = obj.GetAll<CRM_Status>(o => o.Active_YN == true).ToList();

            // modify the code as per the client's comments(WB-08/08/2013) that
            // all list should be displayed instead of only active records
            // modified by: Deepak Thakur[12.April.2013]
            List<CRM_Status> statusEntity = obj.GetAll<CRM_Status>(o => o.StatusName != "").ToList();
            Mapper.CreateMap<CRM_Status, CRM_Status_ViewModel>();
            objstatus = Mapper.Map(statusEntity, objstatus);
            return objstatus;
        }

        public List<CRM_Events_ViewModel> GetAllEvents(int CampaignId)
        {
            var objEvents = new List<CRM_Events_ViewModel>();
            List<CRM_Events> EventEntity = obj.GetAll<CRM_Events>(o => o.Enabled == true && o.CampaignID == CampaignId).ToList();
            Mapper.CreateMap<CRM_Status, CRM_Events_ViewModel>();
            objEvents = Mapper.Map(EventEntity, objEvents);
            return objEvents;
        }

        public List<CRM_MarketingSources_ViewModel> GetAllMarketingSource()
        {
            var objMarketingSource = new List<CRM_MarketingSources_ViewModel>();
            //List<CRM_MarketingSources> MarketingSourceEntity = obj.GetAll<CRM_MarketingSources>(o => o.Active_YN == true).ToList();

            // modify the code as per the client's comments(WB-08/08/2013) that
            // all list should be displayed instead of only active records
            // modified by: Deepak Thakur[12.April.2013]
            List<CRM_MarketingSources> MarketingSourceEntity = obj.GetAll<CRM_MarketingSources>(o => o.MarketingSourceName != "").ToList();
            Mapper.CreateMap<CRM_MarketingSources, CRM_MarketingSources_ViewModel>();
            objMarketingSource = Mapper.Map(MarketingSourceEntity, objMarketingSource);
            return objMarketingSource;
        }

        public List<MarketingSourceViewModel> GetSelectedMarketingSource(int prospectId, int TabId)
        {
            List<MarketingSourceViewModel> mktScr = new List<MarketingSourceViewModel>();
            var CRM_MarketsourceID = obj.List<Emrdev.DataLayer.CRM_MarketsourceID>();

            if (TabId == 1)
            {
                mktScr = (from a in CRM_MarketsourceID
                          where a.Prospect_CampID == prospectId && a.IsProspect == 1
                          select new MarketingSourceViewModel
                          {
                              MarketingSourceID = a.MarketSourceID
                          }).ToList();
            }
            else if (TabId == 2)
            {


                mktScr = (from a in CRM_MarketsourceID
                          where a.Prospect_CampID == prospectId && a.IsProspect == 0
                          select new MarketingSourceViewModel
                          {
                              MarketingSourceID = a.MarketSourceID


                          }).ToList();

            }
            return mktScr;
        }

        public List<CRM_Events_ViewModel> GetAllEvents()
        {
            var objEvents = new List<CRM_Events_ViewModel>();
            var CRM_Events = obj.List<CRM_Events>();
            var CRM_Campaigns = obj.List<CRM_Campaigns>();
            objEvents = (from e in CRM_Events
                         join c in CRM_Campaigns on e.CampaignID equals c.CampaignID
                         where e.Enabled == true && c.Enabled == true

                         select new CRM_Events_ViewModel
                         {
                             EventName = e.EventName,
                             EventID = e.EventID,
                             EventDate = e.EventDate
                         }).Distinct().ToList();

            //List<CRM_Events> EventEntity = obj.GetAll<CRM_Events>(o => o.Enabled == true).OrderByDescending(o => o.EventDate).ToList();
            //Mapper.CreateMap<CRM_Events, CRM_Events_ViewModel>();
            //objEvents = Mapper.Map(EventEntity, objEvents);
            return objEvents.OrderByDescending(o => o.EventDate).ToList();
        }

        public ManageGrdProspectViewModel GetProspectById(int ProspectId)
        {
            var objEvents = new ManageGrdProspectViewModel();
            var CRM_Prospects = obj.List<CRM_Prospects>();
            objEvents = (from e in CRM_Prospects

                         where e.ProspectID == ProspectId

                         select new ManageGrdProspectViewModel
                         {
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             Email = e.Email
                         }).FirstOrDefault();


            return objEvents;
        }

        /// <summary>
        /// Get the Manage Prospect list data
        /// </summary>
        /// <returns></returns>
        public List<ManageGrdProspectViewModel> GetAllProspect()
        {

            CampaignTypeDAL objDal = new CampaignTypeDAL();
            return objDal.GetAllProspect();
            //var CRM_Prospects = obj.List<Emrdev.DataLayer.CRM_Prospects>();
            //var CRM_Registrants = obj.List<Emrdev.DataLayer.CRM_Registrants>();
            //var CRM_Events = obj.List<CRM_Events>();
            //var Staff = obj.List<Staff>();
            //List<ManageGrdProspectViewModel> objPGetAllProspectDetail = new List<ManageGrdProspectViewModel>();
            //List<ManageGrdProspectViewModel> objPGetAllProspect = (from p in CRM_Prospects
            //                                                       join r in CRM_Registrants on p.ProspectID equals r.ProspectID
            //                                                       join e in CRM_Events on r.EventID equals e.EventID

            //                                                       select new ManageGrdProspectViewModel
            //                                                       {
            //                                                           ProspectID = p.ProspectID,
            //                                                           FirstName = p.FirstName,
            //                                                           LastName = p.LastName,
            //                                                           City = p.City,
            //                                                           State = p.State,
            //                                                           StatusID = p.StatusID,
            //                                                           MainPhone = p.MainPhone,
            //                                                           ContactMethod = p.ContactMethod,
            //                                                           CreatedBy = p.CreatedBy,
            //                                                           StaffName="",
            //                                                           Address = p.Address,
            //                                                           AltPhone = p.AltPhone,
            //                                                           AppointmentID = p.AppointmentID,
            //                                                           Email = p.Email,
            //                                                           Flagged = p.Flagged,
            //                                                           //MarketingSources = p.MarketingSources,
            //                                                           Notes = p.Notes,
            //                                                           Zip = p.Zip,
            //                                                           EventID = e.EventID,
            //                                                           EventName = e.EventName
            //                                                       }).ToList();

            //foreach (var data in objPGetAllProspect)
            //{

            //    ManageGrdProspectViewModel oDiagplan = new ManageGrdProspectViewModel();
            //    oDiagplan.Notes = data.Notes;
            //    oDiagplan.Zip = data.Zip;
            //    oDiagplan.EventID = data.EventID;
            //    oDiagplan.EventName = data.EventName;
            //    oDiagplan.AltPhone = data.AltPhone;
            //    oDiagplan.AppointmentID = data.AppointmentID;
            //    oDiagplan.Email = data.Email;
            //    oDiagplan.Flagged = data.Flagged;
            //    oDiagplan.MarketingSources = getMarketSource(data.ProspectID, 1);
            //    oDiagplan.ProspectID =data.ProspectID;
            //    oDiagplan.FirstName = data.FirstName;
            //    oDiagplan.LastName = data.LastName;
            //    oDiagplan.City = data.City;
            //    oDiagplan.State = data.State;
            //    oDiagplan.StatusID = data.StatusID;
            //    oDiagplan.MainPhone = data.MainPhone;
            //    oDiagplan.ContactMethod = data.ContactMethod;
            //    oDiagplan.StaffName = getStaffName(data.CreatedBy);
            //    oDiagplan.CreatedBy = data.CreatedBy;
            //    oDiagplan.Address = data.Address;
            //    objPGetAllProspectDetail.Add(oDiagplan);

            //}
            // return objPGetAllProspectDetail;
        }

        public string getMarketSource(int ProspectId, int TabID)
        {
            CampaignTypeDAL objmrksrc = new CampaignTypeDAL();
            return objmrksrc.getMarketSource(ProspectId, TabID);
        }

        public string getStaffName(int staffID)
        {
            string name = string.Empty;
            var Staff = obj.List<Staff>();
            ManageGrdProspectViewModel objPGetAllProspect = (from p in Staff
                                                             where p.EmployeeID == staffID

                                                             select new ManageGrdProspectViewModel
                                                             {
                                                                 StaffName = p.EmployeeName
                                                             }).FirstOrDefault();
            if (objPGetAllProspect != null)
            {
                name = objPGetAllProspect.StaffName;
            }

            return name;

        }

        /// <summary>
        /// Get the list of all campaigns to fill the grid
        /// </summary>
        /// <returns></returns>
        public List<ManageCampaignViewModel> GetAllCampaign()
        {
            var CRM_Campaigns = obj.List<CRM_Campaigns>();
            List<ManageCampaignViewModel> objPGetAllCampaign = (from c in CRM_Campaigns
                                                                where c.Enabled == true
                                                                select new ManageCampaignViewModel
                                                                {
                                                                    CampaignID = c.CampaignID,
                                                                    CampaignName = c.CampaignName

                                                                }).OrderBy(o => o.CampaignName).ToList();
            return objPGetAllCampaign;
        }

        /// <summary>
        /// Delete Events from the database
        /// </summary>
        /// <param name="EventId"></param>
        public void DeleteEvent(int EventId)
        {
            CRM_Events objEvents = obj.Get<CRM_Events>(o => o.EventID == EventId);
            if (objEvents != null)
            {
                if (objEvents.Enabled == true)
                {
                    objEvents.Enabled = false;
                }
                else
                {
                    objEvents.Enabled = true;
                }

                obj.Edit(objEvents);

            }
        }

        /// <summary>
        /// Delete Campaign values
        /// </summary>
        /// <param name="CampaignID"></param>
        public void DeleteCampaign(int CampaignID)
        {
            CRM_Campaigns objCampaign = obj.Get<CRM_Campaigns>(o => o.CampaignID == CampaignID);
            if (objCampaign != null)
            {
                if (objCampaign.Enabled == true)
                {
                    objCampaign.Enabled = false;
                }
                else
                {
                    objCampaign.Enabled = true;
                }
                obj.Edit(objCampaign);

            }
        }

        /// <summary>
        /// Delete Manage prospect values 
        /// </summary>
        /// <param name="ProspectID"></param>
        public void DeleteProspect(int ProspectID)
        {
            obj.DeleteProspect(ProspectID);
        }

        public void InsertUpdateProspect(int ProspectID, string Address, string AltPhone, string City, string ContactMethod,
                                        string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                        string Notes, string State, int StatusID, string Zip, int UserName, int EventId)
        {

            Emrdev.DataLayer.CRM_Prospects pros;
            CRM_Registrants res;

            if (ProspectID == 0)
            {
                pros = new Emrdev.DataLayer.CRM_Prospects();
            }
            else
            {
                pros = obj.Get<Emrdev.DataLayer.CRM_Prospects>(o => o.ProspectID == ProspectID);
            }
            pros.Address = Address;
            pros.AltPhone = AltPhone;
            pros.City = City;
            pros.ContactMethod = ContactMethod;
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


            CampaignTypeDAL objCRMprospectinsert = new CampaignTypeDAL();
            if (ProspectID == 0)
            {
                pros.CreatedBy = UserName;

                obj.Create(pros);
                //Added Condition by Jaswinder to not make event mandatory
                if (EventId > 0)
                {
                    res = new CRM_Registrants();
                    res.EventID = EventId;
                    res.ProspectID = pros.ProspectID;
                    obj.Create(res);
                }

                objCRMprospectinsert.InserMarketSource(pros.ProspectID, MarketingSources, false, 1);

            }
            else
            {
                obj.Edit(pros);
                res = obj.Get<Emrdev.DataLayer.CRM_Registrants>(o => o.ProspectID == ProspectID);
                //added by jaswinder for CRM to add event if eventid>0 and it not exists in the event table and delete if eventid=0 
                if (res != null && EventId > 0)
                {
                    res.EventID = EventId;
                    obj.Edit(res);
                }
                else if (res == null && EventId > 0)
                {
                    res = new CRM_Registrants();
                    res.EventID = EventId;
                    res.ProspectID = pros.ProspectID;
                    obj.Create(res);
                }
                else if (res != null && EventId <= 0)
                {
                    obj.Delete(res);
                }
                objCRMprospectinsert.InserMarketSource(ProspectID, MarketingSources, true, 1);


            }


            //Added by jaswinder to add the atendent stauts in Crm_attendee table
            if (StatusID > 0)
            {
                if (StatusID == Convert.ToInt32(crmstatus.Attendant))
                {
                    CRM_Attendees objAttendant;
                    objAttendant = obj.Get<Emrdev.DataLayer.CRM_Attendees>(o => o.ProspectID == ProspectID && o.EventID == EventId);
                    if (objAttendant != null)
                    {
                        objAttendant.EventID = EventId;
                        objAttendant.ProspectID = pros.ProspectID;
                        obj.Edit(objAttendant);
                    }
                    else
                    {
                        objAttendant = new CRM_Attendees();
                        objAttendant.EventID = EventId;
                        objAttendant.ProspectID = pros.ProspectID;
                        obj.Create(objAttendant);
                    }

                }

            }


        }

        /// <summary>
        /// method for check the duplicate records in CRM_Prospects table
        /// during add/update the data
        /// used CRM/Manage.aspx.cs
        /// </summary>
        /// <param name="ProspectID"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool CheckDuplicateProspect(int ProspectID, string Email)
        {

            bool isExist = false;
            if (ProspectID == 0)
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Prospects>(o => o.Email == Email);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Prospects>(o => o.Email == Email && o.ProspectID != ProspectID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        public int CheckDuplicateFbImporter(string Event, string Email)
        {

            int isExist = 0;

            var objfirst = obj.Get<Emrdev.DataLayer.CRM_Prospects>(o => o.Email == Email);
            if (objfirst != null)
            {
                List<CRM_Registrants> event1 = obj.GetAll<Emrdev.DataLayer.CRM_Registrants>(o => o.ProspectID == objfirst.ProspectID).ToList();
                if (event1.Count > 0)
                {
                    foreach (var item in event1)
                    {
                        var objEventName = obj.Get<Emrdev.DataLayer.CRM_Events>(o => o.EventID == item.EventID && o.EventName.ToLower() == Event.ToLower());
                        if (objEventName != null)
                        {
                            isExist = -1;
                        }
                    }
                }
                if (isExist == 0)
                {
                    isExist = objfirst.ProspectID;
                }
            }

            return isExist;

        }

        public void InsertUpdateStatus(int StatusID, bool Active_YN, string StatusName)
        {
            CRM_Status Status;
            if (StatusID == 0)
            {
                Status = new CRM_Status();
            }
            else
            {
                Status = obj.Get<CRM_Status>(o => o.StatusID == StatusID);
            }
            Status.Active_YN = Active_YN;
            Status.StatusName = StatusName;
            if (StatusID == 0)
            {

                obj.Create(Status);
            }
            else
            {
                obj.Edit(Status);
            }
        }

        /// <summary>
        /// method for check the duplicate records in CRM_Prospects table
        /// during add/update the data
        /// used CRM/Manage.aspx.cs under Status & Sources
        /// created by: deepakt
        /// </summary>
        /// <param name="StatusID"></param>
        /// <param name="StatusName"></param>
        /// <returns>isExist</returns>
        public bool CheckDuplicateStatus(int StatusID, string StatusName)
        {
            bool isExist = false;
            if (StatusID == 0)
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Status>(o => o.StatusName == StatusName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Status>(o => o.StatusName == StatusName && o.StatusID != StatusID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        /// <summary>
        /// method for check the duplicate records in CRM_MarketingSources table
        /// during add/update the data
        /// used CRM/Manage.aspx.cs under Status & Sources
        /// created by: Rakesh kumar
        /// Created date : 5-aug-2013
        /// </summary>
        /// <param name="MarketingSourceID"></param>
        /// <param name="MarketingSourceName"></param>
        /// <returns>isExist</returns>
        public bool CheckDuplicateMarketingSource(int MarketingSourceID, string MarketingSourceName)
        {
            bool isExist = false;
            if (MarketingSourceID == 0)
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_MarketingSources>(o => o.MarketingSourceName == MarketingSourceName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_MarketingSources>(o => o.MarketingSourceName == MarketingSourceName && o.MarketingSourceID != MarketingSourceID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }


        /// <summary>
        /// method for check the duplicate records in CRM_Campaigns table
        /// during add/update the data
        /// used CRM/Manage.aspx.cs under Compaigns & Events
        /// created by: Rakesh kumar
        /// Created date : 5-aug-2013
        /// </summary>
        /// <param name="CampaignID"></param>
        /// <param name="CampaignName"></param>
        /// <returns>isExist</returns>
        public bool CheckDuplicateCampaignName(int CampaignID, string CampaignName)
        {
            bool isExist = false;
            if (CampaignID == 0)
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Campaigns>(o => o.CampaignName == CampaignName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Campaigns>(o => o.CampaignName == CampaignName && o.CampaignID != CampaignID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }


        /// <summary>
        /// method for check the duplicate records in CRM_Events table
        /// during add/update the data
        /// used CRM/Manage.aspx.cs under Compaigns & Events
        /// created by: Rakesh kumar
        /// Created date : 5-aug-2013
        /// </summary>
        /// <param name="CampaignID"></param>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns>isExist</returns>
        public bool CheckDuplicateEventName(int CampaignID, int EventID, string EventName, DateTime eventDate)
        {
            bool isExist = false;
            if (EventID == 0)
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Events>(o => o.EventName == EventName && o.CampaignID == CampaignID && o.EventDate == eventDate);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_Events>(o => o.EventName == EventName && o.CampaignID == CampaignID && o.EventID != EventID && o.EventDate == eventDate);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }

        /// <summary>
        /// method for check the duplicate records in CRM_Events table
        /// during add/update the data
        /// used CRM/Manage.aspx.cs under Compaigns & Events
        /// created by: Rakesh kumar
        /// Created date : 5-aug-2013
        /// </summary>
        /// <param name="CampaignID"></param>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns>isExist</returns>
        public bool CheckDuplicateMarketingActivity(int CampaignID, int MarketingActivityID, string SourceType, DateTime StartDate, DateTime EndDate, int SourceID)
        {
            bool isExist = false;
            if (MarketingActivityID == 0)
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_MarketingActivity>(o => o.CapmpaignID == CampaignID && o.SourceType == SourceType && (o.EndDate <= StartDate || o.EndDate <= EndDate) && o.SourceID == SourceID);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = obj.Get<Emrdev.DataLayer.CRM_MarketingActivity>(o => o.CapmpaignID == CampaignID && o.MarketingActivityID != MarketingActivityID && o.SourceType == SourceType && o.EndDate < StartDate && o.EndDate < EndDate && o.SourceID == SourceID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }

        //   CheckDuplicateMarketingActivity(CampaignID,MarketingActivityID,SourceType, StartDate,EndDate,SourceID)


        public void InsertUpdateMSource(int MarketingSourceID, bool Active_YN, string MarketingSourceName)
        {
            CRM_MarketingSources MarketingSources;
            if (MarketingSourceID == 0)
            {
                MarketingSources = new CRM_MarketingSources();
            }
            else
            {
                MarketingSources = obj.Get<CRM_MarketingSources>(o => o.MarketingSourceID == MarketingSourceID);
            }
            MarketingSources.Active_YN = Active_YN;
            MarketingSources.MarketingSourceName = MarketingSourceName;
            if (MarketingSourceID == 0)
            {

                obj.Create(MarketingSources);
            }
            else
            {
                obj.Edit(MarketingSources);
            }
        }

        /// <summary>
        /// Get marketingActivity values on the basis of campaignid
        /// </summary>
        /// <param name="CapmpaignID"></param>
        /// <returns></returns>
        public List<MarketingActivityViewModel> GetMarketingActivity(int CapmpaignID)
        {
            var CRM_MarketingActivity = obj.List<Emrdev.DataLayer.CRM_MarketingActivity>();
            List<MarketingActivityViewModel> objMarketingActivity = (from M in CRM_MarketingActivity
                                                                     where M.CapmpaignID == CapmpaignID
                                                                     select new MarketingActivityViewModel
                                                                     {
                                                                         CapmpaignID = M.CapmpaignID,
                                                                         EndDate = M.EndDate,
                                                                         MarketingActivityID = M.MarketingActivityID,
                                                                         Money_Spent = M.Money_Spent,
                                                                         Notes = M.Notes,
                                                                         SourceID = M.SourceID,
                                                                         SourceType = M.SourceType,
                                                                         StartDate = M.StartDate,

                                                                     }).ToList();
            return objMarketingActivity;
        }

        /// <summary>
        /// Insert or update campaign values
        /// </summary>
        /// <param name="CampaignID"></param>
        /// <param name="CampaignName"></param>
        /// <param name="CampaignType"></param>
        /// <param name="MarketingBudget"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="MarketingSources"></param>
        public void InsertUpdateCampaign(int CampaignID, string CampaignName, string CampaignType, string MarketingBudget, string StartDate,
                                        string EndDate, string MarketingSources)
        {
            CRM_Campaigns Campaigns;
            if (CampaignID == 0)
            {
                Campaigns = new CRM_Campaigns();
            }
            else
            {
                Campaigns = obj.Get<CRM_Campaigns>(o => o.CampaignID == CampaignID);
            }
            Campaigns.CampaignName = CampaignName;
            Campaigns.CampaignType = int.Parse(CampaignType);
            Campaigns.MarketingBudget = MarketingBudget.ToString() != "" ? decimal.Parse(MarketingBudget.ToString().Replace("$", "")) : (decimal?)null;
            Campaigns.StartDate = StartDate.ToString() != "" ? DateTime.Parse(StartDate.ToString()) : (DateTime?)null;
            Campaigns.EndDate = EndDate.ToString() != "" ? DateTime.Parse(EndDate.ToString()) : (DateTime?)null;
            //Campaigns.MarketingSources = MarketingSources;
            CampaignTypeDAL objCRMprospectinsert = new CampaignTypeDAL();
            if (CampaignID == 0)
            {

                Campaigns.Enabled = true;
                obj.Create(Campaigns);

                CRM_Events eve = new CRM_Events();
                eve.CampaignID = Campaigns.CampaignID;
                eve.EventDate = DateTime.Now;
                eve.EventName = "New Event";
                eve.Venue = "Venue";
                eve.Enabled = true;
                obj.Create(eve);



                objCRMprospectinsert.InserMarketSource(Campaigns.CampaignID, MarketingSources, false, 0);

            }
            else
            {
                obj.Edit(Campaigns);
                objCRMprospectinsert.InserMarketSource(Campaigns.CampaignID, MarketingSources, true, 0);
            }
        }


        /// <summary>
        /// Insert or update Activity tab values
        /// </summary>
        /// <param name="MarketingActivityID"></param>
        /// <param name="CampaignId"></param>
        /// <param name="EndDate"></param>
        /// <param name="MoneySpent"></param>
        /// <param name="Notes"></param>
        /// <param name="SourceID"></param>
        /// <param name="SourceType"></param>
        /// <param name="StartDate"></param>
        public void InsertUpdateActivity(int MarketingActivityID, string CampaignId, string EndDate, string MoneySpent, string Notes,
                                        string SourceID, string SourceType, string StartDate)
        {
            Emrdev.DataLayer.CRM_MarketingActivity activity;
            if (MarketingActivityID == 0)
            {
                activity = new Emrdev.DataLayer.CRM_MarketingActivity();
            }
            else
            {
                activity = obj.Get<Emrdev.DataLayer.CRM_MarketingActivity>(o => o.MarketingActivityID == MarketingActivityID);
            }
            activity.CapmpaignID = int.Parse(CampaignId);
            activity.EndDate = DateTime.Parse(EndDate.ToString());
            activity.Money_Spent = MoneySpent != "" ? decimal.Parse(MoneySpent.ToString()) : 0;
            activity.Notes = Notes;
            activity.SourceID = SourceID != "" ? int.Parse(SourceID.ToString()) : 0;
            activity.SourceType = SourceType;
            activity.StartDate = DateTime.Parse(StartDate.ToString());
            if (MarketingActivityID == 0)
            {

                obj.Create(activity);
            }
            else
            {
                obj.Edit(activity);
            }
        }

        /// <summary>
        /// Get all the clinic names
        /// </summary>
        /// <returns></returns>
        public List<ClinicsViewModel> GetAllClinic()
        {

            var Clinic = obj.List<Emrdev.DataLayer.Clinic>();
            List<ClinicsViewModel> objPGetAllClinic = (from c in Clinic
                                                       select new ClinicsViewModel
                                                       {
                                                           ClinicID = c.ClinicID,
                                                           ClinicName = c.ClinicName
                                                       }).OrderBy(o => o.ClinicName).ToList();
            return objPGetAllClinic;
        }

        public dynamic GetAllAttend(int EventID)
        {

            CampaignTypeDAL objDAL = new CampaignTypeDAL();
            var objGetAllAttend = objDAL.GetAllAttend(EventID);
            return objGetAllAttend;
        }

        public static void SendCRMSurveyMessage(int ProspectId)
        {
            var IfromAddress = new MailAddress("contactus@LongevityMedicalClinic.com", "Tickets");
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            string toName = string.Empty;
            string toAddress = string.Empty;

            const string fromPassword = "StarFish44!";
            string subject = "Longevity Patient Survey";
            string body = string.Empty;
            CRMWufooLink objTemplate = null;
            objTemplate = objDAL.Get<CRMWufooLink>(o => o.IsActive == true);
            if (objTemplate != null)
            {


                string FormURL = objTemplate.WufooFormLink;
                //CRM_Prospects objProspect = null;
                ManageDAL obj = new ManageDAL();
                CRM_Prospects objProspect = obj.Get<CRM_Prospects>(o => o.ProspectID == ProspectId);
                {


                    if (objProspect != null)
                    {
                        toName = objProspect.FirstName + " " + objProspect.LastName;
                        toAddress = objProspect.Email;
                        var ItoAddress = new MailAddress(toAddress, toName);

                        body = objTemplate.EmailDescription;
                        body = body.Replace("{UserName}", toName);
                        body = body.Replace("{Url}", FormURL);
                        if (!string.IsNullOrEmpty(toAddress))
                        {
                            var smtp = new SmtpClient
                            {
                                Host = "pod51019.outlook.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(IfromAddress.Address, fromPassword)
                            };
                            using (var message = new MailMessage(IfromAddress.Address, toAddress)
                            {
                                Subject = subject,
                                Body = body,
                                IsBodyHtml = true
                            })
                            {
                                smtp.Send(message);
                            }
                        }
                    }


                }
            }


        }

        public void AddRecordAttendee(int EventID, int ProspectId)
        {

            obj.AddRecordAttendee(EventID, ProspectId);
            SendCRMSurveyMessage(ProspectId);


        }

        //
        //For Deleting Prospect Attendees
        public string DeleteRecordAttendee(int EventID, int ProspectId)
        {
            return obj.DeleteRecordAttendee(EventID, ProspectId);
        }

        public void InsertUpdateEvent(int EventId, string EventDate, string EventName, string Notes, string Venue, string Appointments,
                                      string AudienceQuality, string AudienceReaction, string Callbacks, string EventLength, string FacilityInteriorExterior,
                                      string Location, string OverallPerformance, string Parking, string VenueQuality, string Walkins, int CampaignID)
        {
            CRM_Events Events;
            if (EventId == 0)
            {
                Events = new CRM_Events();
            }
            else
            {
                Events = obj.Get<CRM_Events>(o => o.EventID == EventId);
            }
            Events.EventDate = DateTime.Parse(EventDate.ToString());
            Events.EventName = EventName;
            Events.Notes = Notes;
            Events.Venue = Venue;
            Events.Appointments = Appointments.ToString() != "" ? int.Parse(Appointments.ToString()) : (int?)null;
            Events.AudienceQuality = AudienceQuality != "" ? int.Parse(AudienceQuality) : (int?)null;
            Events.AudienceReaction = AudienceReaction != "" ? int.Parse(AudienceReaction) : (int?)null;
            Events.Callbacks = Callbacks != "" ? int.Parse(Callbacks) : (int?)null;
            Events.EventLength = EventLength;
            Events.FacilityInteriorExterior = FacilityInteriorExterior != "" ? int.Parse(FacilityInteriorExterior) : (int?)null;
            Events.Location = Location;
            Events.OverallPerformance = OverallPerformance != "" ? int.Parse(OverallPerformance) : (int?)null;
            Events.Parking = Parking != "" ? int.Parse(Parking) : (int?)null;
            Events.VenueQuality = VenueQuality != "" ? int.Parse(VenueQuality) : (int?)null;
            Events.Walkins = Walkins;
            Events.CampaignID = CampaignID;
            if (EventId == 0)
            {
                Events.Enabled = true;
                obj.Create(Events);

                // eve.CampaignID = int.Parse(sqlEvent.SelectParameters[0].DefaultValue);

            }
            else
            {
                obj.Edit(Events);
            }
        }

        public void DeleteStatusMgmt(int Id)
        {
            obj.DeleteStatusMgmt(Id);
        }

        public void DeleteMarketingSourceMgmt(int Id)
        {
            obj.DeleteMarketingSourceMgmt(Id);
        }

        public bool ReCordAttendee(int PatientId, int ProspectID, int AptID, int StaffID, string Clinic, int EventID)
        {
            try
            {
                int PatientID = PatientId;
                CRM_Prospects pros = obj.Get<CRM_Prospects>(o => o.ProspectID == ProspectID);
                //add to patioents if needed
                if (pros.PatientID == null)
                {
                    Patient pat = new Patient();
                    pat.BillingCity = pros.City;
                    pat.BillingState = pros.State;
                    pat.BillingStreet = pros.Address;
                    pat.BillingZip = pros.Zip;
                    pat.FirstName = pros.FirstName;
                    pat.LastName = pros.LastName;
                    pat.HomePhone = pros.MainPhone;
                    pat.Email = pros.Email;
                    pat.ContactPreference = pros.ContactMethod;
                    pat.EmergencyContact = "";
                    pat.AllowApptReassign = false;
                    pat.Medical = false;
                    pat.Aesthetics = false;
                    pat.Autoship = false;
                    pat.Retail = false;
                    pat.Affiliate = false;
                    pat.DiabetesSOC = false;
                    pat.HeartSOC = false;
                    pat.AutoShipAlerts = "";
                    pat.AutoshipNote = "";
                    pat.MedicareOptOut_YN = false;
                    pat.EatingPlanReceived_YN = false;
                    pat.AutoshipEmail = false;
                    pat.AutoshipCancelReasonID = -1;
                    pat.AutoshipCancelOther = "";
                    pat.Marketing_source = EventID;
                    pat.Birthday = DateTime.Now;
                    pat.Clinic = Clinic;
                    pat.Inactive = false;
                    //Added fields by jaswinder on 19th nov 2013 to show the crm patients on calendar
                    pat.ConciergeID = StaffID.ToString();
                    pat.Cancel_NoShow_frm_signed = false;
                    pat.HIPPA_signed = false;
                    pat.NameAlert = false;
                    pat.Home_detailed_info = false;
                    pat.Home_CB_only = false;
                    pat.Work_Detailed_info = false;
                    pat.Work_CB_only = false;
                    pat.Cell_CB_Only = false;
                    pat.Cell_Detailed_info = false;
                    pat.Email_auth_detailed_info = false;
                    pat.Fax_auth_detailed_info = false;


                    obj.Create(pat);

                    pros.PatientID = pat.PatientID;
                    PatientID = pat.PatientID;
                }
                else
                {
                    PatientID = (int)pros.PatientID;
                    pros.StatusID = (int)crmstatus.Scheduled;
                    obj.Edit(pros);
                }

                //return  old appointment to pool if needed
                int?[] aptTypes = { 1, 82, 94 };
                List<apt_rec> oldApts = obj.GetAll<apt_rec>(o => o.patient_id == PatientID && aptTypes.Contains(o.AppointmentTypeID)).ToList();

                foreach (apt_rec a in oldApts)
                {
                    a.patient_id = 7447;
                    obj.Edit(a);
                }


                //update appointment

                apt_rec newApt = obj.Get<apt_rec>(o => o.apt_id == AptID);

                newApt.patient_id = PatientID;
                obj.Edit(newApt);
                pros.AppointmentID = newApt.apt_id;
                pros.StatusID = (int)crmstatus.Scheduled;
                obj.Edit(pros);

                //create a post blooddraw appointment 
                apt_rec createblooddraw = new apt_rec();
                createblooddraw.ActionNeeded = newApt.ActionNeeded;
                createblooddraw.AppointmentTypeID = 29;
                createblooddraw.ApptStart = newApt.ApptEnd;
                createblooddraw.ApptEnd = newApt.ApptEnd.Value.AddMinutes(30);
                createblooddraw.patient_id = PatientID;
                createblooddraw.date_entered = DateTime.Now;
                createblooddraw.closed_yn = false;
                createblooddraw.StatusID = (int)crmstatus.LrCompletedJoined;
                createblooddraw.AllDay = false;
                createblooddraw.EmailOnChange = false;
                createblooddraw.Results = 0;
                if (Clinic == Clicnic.Kirkland.ToString())

                    createblooddraw.ProviderID = (int)(ProvidersForCalendar.KirklandMA);

                else if (Clinic == Clicnic.Lynnwood.ToString())

                    createblooddraw.ProviderID = (int)(ProvidersForCalendar.LynnwoodMA);
                else if (Clinic == Clicnic.South.ToString())

                    createblooddraw.ProviderID = (int)(ProvidersForCalendar.TacomaMA);

                obj.Create(createblooddraw);


                CRM_Log log = new CRM_Log();
                log.DateEntered = DateTime.Now;
                log.EnteredBy = StaffID;
                log.OldStatus = pros.StatusID;
                log.NewStatus = (int)crmstatus.Scheduled;
                log.ProspectID = pros.ProspectID;
                obj.Create(log);


                //create Contact Entries
                //Contact_tbl clsEntityContact = new Contact_tbl();
                //clsEntityContact.ContactDateEntered = DateTime.Now;
                //clsEntityContact.EnteredBy = StaffID;
                //clsEntityContact.MessageBody = "Assigned the prospect to the already scheduled appointment from CRM ";
                //clsEntityContact.PatientID = PatientID;
                //clsEntityContact.Apt_ID = newApt.apt_id;
                //clsEntityContact.AptType = newApt.AppointmentTypeID;
                //obj.Create(clsEntityContact);

                CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
                objCampaignTypeDAL.InsertCRMContactsDetails(StaffID, 0, "Assigned the prospect to the already scheduled appointment from CRM ", PatientID, 0, newApt.apt_id, newApt.AppointmentTypeID, 1);


                return true;

            }

            catch (System.Exception ex)
            {
                return false;
            }
        }



        public List<CRM_Events_ViewModel> GetAllEventOnDate(DateTime EventDate)
        {

            var objEvents = new List<CRM_Events_ViewModel>();
            List<CRM_Events> EventEntity = obj.GetAll<CRM_Events>(o => o.Enabled == true && o.EventDate >= EventDate).OrderBy(o => o.EventName).ToList();
            Mapper.CreateMap<CRM_Events, CRM_Events_ViewModel>();
            objEvents = Mapper.Map(EventEntity, objEvents);
            return objEvents;

        }

        public List<CRM_CampaignType_ViewModel> GetAllactiveCampaignType(bool Active)
        {
            CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
            var objCampaign = new List<CRM_CampaignType_ViewModel>();
            List<CRM_CampaignType> EventEntity = new List<CRM_CampaignType>();

            if (Active == false)
                EventEntity = objCampaignTypeDAL.GetAll<CRM_CampaignType>(o => o.CampaignType != "").OrderBy(o => o.CampaignType).ToList();
            else
                EventEntity = objCampaignTypeDAL.GetAll<CRM_CampaignType>(o => o.CampaignType != "" && o.IsActive == true).OrderBy(o => o.CampaignType).ToList();
            Mapper.CreateMap<CRM_CampaignType, CRM_CampaignType_ViewModel>();
            objCampaign = Mapper.Map(EventEntity, objCampaign);
            return objCampaign;
        }

        public void InsertUpdateCampaignType(CRM_CampaignType_ViewModel CampaignModel)
        {
            CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
            Emrdev.DataLayer.CRM_CampaignType CampaignType;
            if (CampaignModel.CampaignID == 0)
            {
                CampaignType = new Emrdev.DataLayer.CRM_CampaignType();

            }
            else
            {
                CampaignType = objCampaignTypeDAL.Get<Emrdev.DataLayer.CRM_CampaignType>(o => o.CampaignID == CampaignModel.CampaignID);
            }
            CampaignType.CampaignType = CampaignModel.CampaignType.Trim();
            CampaignType.IsActive = CampaignModel.IsActive;

            if (CampaignModel.CampaignID == 0)
            {

                objCampaignTypeDAL.Create(CampaignType);
            }
            else
            {
                objCampaignTypeDAL.Edit(CampaignType);
            }
        }

        public bool CheckDuplicateCampaignType(int CampaignID, string CampaignType)
        {
            CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
            bool isExist = false;
            if (CampaignID == 0)
            {
                var objfirst = objCampaignTypeDAL.Get<Emrdev.DataLayer.CRM_CampaignType>(o => o.CampaignType.ToLower() == CampaignType.ToLower().Trim());
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objCampaignTypeDAL.Get<Emrdev.DataLayer.CRM_CampaignType>(o => o.CampaignType.ToLower() == CampaignType.ToLower().Trim() && o.CampaignID != CampaignID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }

        public void DeleteProspectAll(string ProspectID)
        {
            CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
            objCampaignTypeDAL.DeleteProspectAll(ProspectID);
        }

        public List<AppointmentTypeModel> GetAppointmentTypes()
        {
            CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
            return objCampaignTypeDAL.GetAppointmentTypes();
        }

        //Added by jaswinder for creating new appointment and prospect from patient
        public bool SaveAppoint_Patient(int PatientId, int ApptID, int ProspectID, string Clinic, int StaffID, int EventID, int ProviderID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                int PatientID = PatientId;
                CRM_Prospects pros = obj.Get<CRM_Prospects>(o => o.ProspectID == ProspectID);
                //add to patients if needed
                if (pros.PatientID == null)
                {
                    Patient pat = new Patient();
                    pat.BillingCity = pros.City;
                    pat.BillingState = pros.State;
                    pat.BillingStreet = pros.Address;
                    pat.BillingZip = pros.Zip;
                    pat.FirstName = pros.FirstName;
                    pat.LastName = pros.LastName;
                    pat.HomePhone = pros.MainPhone;
                    pat.Email = pros.Email;
                    pat.ContactPreference = pros.ContactMethod;
                    pat.EmergencyContact = "";
                    pat.AllowApptReassign = false;
                    pat.Medical = false;
                    pat.Aesthetics = false;
                    pat.Autoship = false;
                    pat.Retail = false;
                    pat.Affiliate = false;
                    pat.DiabetesSOC = false;
                    pat.HeartSOC = false;
                    pat.AutoShipAlerts = "";
                    pat.AutoshipNote = "";
                    pat.MedicareOptOut_YN = false;
                    pat.EatingPlanReceived_YN = false;
                    pat.AutoshipEmail = false;
                    pat.AutoshipCancelReasonID = -1;
                    pat.AutoshipCancelOther = "";
                    pat.Marketing_source = EventID;
                    pat.Birthday = DateTime.Now;
                    pat.Clinic = Clinic;
                    pat.Inactive = false;
                    pat.ConciergeID = StaffID.ToString();
                    pat.Cancel_NoShow_frm_signed = false;
                    pat.HIPPA_signed = false;
                    pat.NameAlert = false;
                    pat.Home_detailed_info = false;
                    pat.Home_CB_only = false;
                    pat.Work_Detailed_info = false;
                    pat.Work_CB_only = false;
                    pat.Cell_CB_Only = false;
                    pat.Cell_Detailed_info = false;
                    pat.Email_auth_detailed_info = false;
                    pat.Fax_auth_detailed_info = false;


                    obj.Create(pat);

                    pros.PatientID = pat.PatientID;
                    PatientID = pat.PatientID;
                    HttpContext.Current.Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + PatientID.ToString(), false);
                }
                else
                {
                    PatientID = (int)pros.PatientID;
                }

                //return  old appointment to pool if needed
                int?[] aptTypes = { 1, 82, 94 };
                List<apt_rec> oldApts = obj.GetAll<apt_rec>(o => o.patient_id == PatientID && aptTypes.Contains(o.AppointmentTypeID)).ToList();

                foreach (apt_rec a in oldApts)
                {
                    a.patient_id = 7447;
                    obj.Edit(a);
                }

                int aptid = ApptID;

                //create a post blooddraw appointment 
                apt_rec createblooddraw = new apt_rec();
                createblooddraw.ActionNeeded = "No";
                createblooddraw.AppointmentTypeID = 29;
                createblooddraw.ApptStart = StartDate;
                createblooddraw.ApptEnd = EndDate;
                createblooddraw.patient_id = PatientID;
                createblooddraw.date_entered = DateTime.Now;
                createblooddraw.closed_yn = false;
                createblooddraw.StatusID = (int)crmstatus.LrCompletedJoined;
                createblooddraw.AllDay = false;
                createblooddraw.EmailOnChange = false;
                createblooddraw.Results = 0;
                createblooddraw.ProviderID = ProviderID;

                obj.Create(createblooddraw);


                //create Contact Entries
                CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
                objCampaignTypeDAL.InsertCRMContactsDetails(StaffID, 0, "Created New appointment from CRM ", PatientID, 0, createblooddraw.apt_id, createblooddraw.AppointmentTypeID, 2);



                pros.AppointmentID = createblooddraw.apt_id;
                pros.StatusID = (int)crmstatus.Scheduled;
                obj.Edit(pros);

                //Enter data in log tables
                CRM_Log log = new CRM_Log();
                log.DateEntered = DateTime.Now;
                log.EnteredBy = StaffID;
                log.OldStatus = pros.StatusID;
                log.NewStatus = (int)crmstatus.Scheduled;
                log.ProspectID = pros.ProspectID;
                obj.Create(log);
                return true;

            }

            catch (System.Exception ex)
            {
                return false;
            }
        }


        public string CheckDuplicateAppointment(int ProviderID, DateTime StartDate, DateTime EndDate)
        {
            string strMessage = string.Empty;
            apt_rec checkprevAppointment = obj.Get<apt_rec>(o => o.ApptStart == StartDate && o.ProviderID == ProviderID);
            if (checkprevAppointment != null)
            {
                strMessage = "appoint for provider is already exits do you continue";

            }
            return strMessage;
        }

        //save follow up while creating new patient.
        //added by jaswinder
        public bool SaveFollowup_Patient(int ProspectID, string Clinic, int StaffID, int EventID)
        {
            try
            {
                int PatientID = 0;
                CRM_Prospects pros = obj.Get<CRM_Prospects>(o => o.ProspectID == ProspectID);
                //add to patients if needed
                if (pros.PatientID == null)
                {
                    Patient pat = new Patient();
                    pat.BillingCity = pros.City;
                    pat.BillingState = pros.State;
                    pat.BillingStreet = pros.Address;
                    pat.BillingZip = pros.Zip;
                    pat.FirstName = pros.FirstName;
                    pat.LastName = pros.LastName;
                    pat.HomePhone = pros.MainPhone;
                    pat.Email = pros.Email;
                    pat.ContactPreference = pros.ContactMethod;
                    pat.EmergencyContact = "";
                    pat.AllowApptReassign = false;
                    pat.Medical = false;
                    pat.Aesthetics = false;
                    pat.Autoship = false;
                    pat.Retail = false;
                    pat.Affiliate = false;
                    pat.DiabetesSOC = false;
                    pat.HeartSOC = false;
                    pat.AutoShipAlerts = "";
                    pat.AutoshipNote = "";
                    pat.MedicareOptOut_YN = false;
                    pat.EatingPlanReceived_YN = false;
                    pat.AutoshipEmail = false;
                    pat.AutoshipCancelReasonID = -1;
                    pat.AutoshipCancelOther = "";
                    pat.Marketing_source = EventID;
                    pat.Birthday = DateTime.Now;
                    pat.Clinic = Clinic;
                    pat.Inactive = false;
                    pat.ConciergeID = StaffID.ToString();
                    pat.Cancel_NoShow_frm_signed = false;
                    pat.HIPPA_signed = false;
                    pat.NameAlert = false;
                    pat.Home_detailed_info = false;
                    pat.Home_CB_only = false;
                    pat.Work_Detailed_info = false;
                    pat.Work_CB_only = false;
                    pat.Cell_CB_Only = false;
                    pat.Cell_Detailed_info = false;
                    pat.Email_auth_detailed_info = false;
                    pat.Fax_auth_detailed_info = false;


                    obj.Create(pat);

                    pros.PatientID = pat.PatientID;
                    PatientID = pat.PatientID;
                    HttpContext.Current.Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + PatientID.ToString(), false);

                }
                else
                {
                    PatientID = (int)pros.PatientID;

                }

                pros.StatusID = (int)crmstatus.LrCompletedJoined;
                obj.Edit(pros);

                //Create followup
                string EventName = string.Empty;
                DateTime EventDate = DateTime.MinValue;
                CRM_Events Events = obj.Get<CRM_Events>(o => o.EventID == EventID);
                if (Events.EventID > 0)
                {
                    EventName = Events.EventName;
                    EventDate = Events.EventDate;

                }

                apt_FollowUpsViewModel clsViewModelFollowUp = new apt_FollowUpsViewModel();
                clsViewModelFollowUp.FollowUp_Body = "Call back to manually schedule post seminar appointment.<br>" + "Eventname :" + EventName + " EventDate :" + EventDate;
                clsViewModelFollowUp.Range_Start = DateTime.Now;
                clsViewModelFollowUp.FirstCall = false;
                clsViewModelFollowUp.FirstCallNote = "";
                clsViewModelFollowUp.SecondCall = false;
                clsViewModelFollowUp.SeconCallNote = "";
                clsViewModelFollowUp.FinalCall = false;

                clsViewModelFollowUp.FinalCallNote = "";
                clsViewModelFollowUp.Letter = false;
                clsViewModelFollowUp.LetterNote = "";
                clsViewModelFollowUp.Range_End = DateTime.Now.AddDays(30);
                clsViewModelFollowUp.FollowUp_Cat = (int)Followups.GeneralFollowUp;
                clsViewModelFollowUp.Entered_By = StaffID;
                clsViewModelFollowUp.DateEntered = DateTime.Now;

                clsViewModelFollowUp.Apt_ID = null;
                clsViewModelFollowUp.PatientID = PatientID;

                apt_FollowUps clsEntityFollowUp = new apt_FollowUps();
                AutoMapper.Mapper.CreateMap<apt_FollowUpsViewModel, apt_FollowUps>();
                clsEntityFollowUp = AutoMapper.Mapper.Map(clsViewModelFollowUp, clsEntityFollowUp);
                obj.Create(clsEntityFollowUp);

                //create Contact Entries

                CampaignTypeDAL objCampaignTypeDAL = new CampaignTypeDAL();
                objCampaignTypeDAL.InsertCRMContactsDetails(StaffID, clsEntityFollowUp.FollowUp_ID, "Call back to manually schedule post seminar appointment \r\n" + "Eventname :" + EventName + " EventDate :" + EventDate, PatientID, (int)ContactType.Followup, 0, 0, 3);


                CRM_Log log = new CRM_Log();
                log.DateEntered = DateTime.Now;
                log.EnteredBy = StaffID;
                log.OldStatus = pros.StatusID;
                log.NewStatus = (int)crmstatus.LrCompletedJoined;
                log.ProspectID = pros.ProspectID;
                obj.Create(log);
                return true;

            }

            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
