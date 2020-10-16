using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ManageService : IManageService
    {
        ManageBAL _ManageBAL = new ManageBAL();
        public List<CRM_Campaigns_ViewModel> GetAllCrmCampaign()
        {
            List<CRM_Campaigns_ViewModel> objCrmCampaign = _ManageBAL.GetAllCrmCampaign();

            return objCrmCampaign;
        }

        public dynamic GetAllapt_recs()
        {
            var objAppointments = _ManageBAL.GetAllapt_recs();
            return objAppointments;
        }

        public List<CRM_Status_ViewModel> GetAllactiveStatus()
        {
            List<CRM_Status_ViewModel> objstatus = _ManageBAL.GetAllactiveStatus();
            return objstatus;
        }

        public List<CRM_Events_ViewModel> GetAllEvents()
        {
            List<CRM_Events_ViewModel> objEvents = _ManageBAL.GetAllEvents();
            return objEvents;
        }

        public List<CRM_MarketingSources_ViewModel> GetAllMarketingSource()
        {
            List<CRM_MarketingSources_ViewModel> objEvents = _ManageBAL.GetAllMarketingSource();
            return objEvents;
        }

        public List<ManageGrdProspectViewModel> GetAllProspect()
        {
            List<ManageGrdProspectViewModel> objprospect = _ManageBAL.GetAllProspect();
            return objprospect;
        }

        public List<ClinicsViewModel> GetAllClinic()
        {
            List<ClinicsViewModel> objClinics = _ManageBAL.GetAllClinic();
            return objClinics;
        }

        public List<ManageCampaignViewModel> GetAllCampaign()
        {
            List<ManageCampaignViewModel> objCampaign = _ManageBAL.GetAllCampaign();
            return objCampaign;
        }
        public void DeleteEvent(int EventId)
        {
            _ManageBAL.DeleteEvent(EventId);
        }

        public void DeleteCampaign(int CampaignID)
        {
            _ManageBAL.DeleteCampaign(CampaignID);
        }

        public void DeleteProspect(int ProspectID)
        {
            _ManageBAL.DeleteProspect(ProspectID);
        }

        public void InsertUpdateProspect(int ProspectID, string Address, string AltPhone, string City, string ContactMethod,
                                        string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                        string Notes, string State, int StatusID, string Zip, int UserName, int EventId)
        {
            _ManageBAL.InsertUpdateProspect(ProspectID, Address, AltPhone, City, ContactMethod,
                                         Email, FirstName, Flagged, LastName, MainPhone, MarketingSources,
                                         Notes, State, StatusID, Zip, UserName, EventId);
        }

        public void InsertUpdateStatus(int StatusID, bool Active_YN, string StatusName)
        {
            _ManageBAL.InsertUpdateStatus(StatusID, Active_YN, StatusName);
        }

        public void InsertUpdateMSource(int MarketingSourceID, bool Active_YN, string MarketingSourceName)
        {
            _ManageBAL.InsertUpdateMSource(MarketingSourceID, Active_YN, MarketingSourceName);
        }

        public List<MarketingActivityViewModel> GetMarketingActivity(int CapmpaignID)
        {
            List<MarketingActivityViewModel> objMarketingActivity = _ManageBAL.GetMarketingActivity(CapmpaignID);
            return objMarketingActivity;
        }

        public void InsertUpdateCampaign(int CampaignID, string CampaignName, string CampaignType, string MarketingBudget, string StartDate,
                                        string EndDate, string MarketingSources)
        {
            _ManageBAL.InsertUpdateCampaign(CampaignID, CampaignName, CampaignType, MarketingBudget, StartDate,
                                        EndDate, MarketingSources);
        }

        public void InsertUpdateActivity(int MarketingActivityID, string CampaignId, string EndDate, string MoneySpent, string Notes,
                                        string SourceID, string SourceType, string StartDate)
        {
            _ManageBAL.InsertUpdateActivity(MarketingActivityID, CampaignId, EndDate, MoneySpent, Notes,
                                         SourceID, SourceType, StartDate);
        }

        public dynamic GetAllAttend(int EventID)
        {
            var objAttend = _ManageBAL.GetAllAttend(EventID);
            return objAttend;
        }

        public void AddRecordAttendee(int EventID, int ProspectId)
        {
            _ManageBAL.AddRecordAttendee(EventID, ProspectId);
        }

        //
        //For Deleting Prospect Attendees
        public string DeleteRecordAttendee(int EventID, int ProspectId)
        {
            return _ManageBAL.DeleteRecordAttendee(EventID, ProspectId);
        }
        public void InsertUpdateEvent(int EventId, string EventDate, string EventName, string Notes, string Venue, string Appointments,
                                      string AudienceQuality, string AudienceReaction, string Callbacks, string EventLength, string FacilityInteriorExterior,
                                      string Location, string OverallPerformance, string Parking, string VenueQuality, string Walkins, int CampaignID)
        {
            _ManageBAL.InsertUpdateEvent(EventId, EventDate, EventName, Notes, Venue, Appointments,
                                       AudienceQuality, AudienceReaction, Callbacks, EventLength, FacilityInteriorExterior,
                                       Location, OverallPerformance, Parking, VenueQuality, Walkins, CampaignID);
        }

        public List<MarketingSourceViewModel> GetSelectedMarketingSource(int prospectId, int TabId)
        {
            return _ManageBAL.GetSelectedMarketingSource(prospectId, TabId);
        }

       
        public bool CheckDuplicateProspect(int ProspectID, string Email)
        {
            return _ManageBAL.CheckDuplicateProspect(ProspectID, Email);
        }
        public int CheckDuplicateFbImporter(string Event, string Email)
        {
            return _ManageBAL.CheckDuplicateFbImporter(Event, Email);
        }

        public bool CheckDuplicateStatus(int StatusID, string StatusName)
        {
            return _ManageBAL.CheckDuplicateStatus(StatusID, StatusName);
        }

        public bool CheckDuplicateMarketingSource(int MarketingSourceID, string MarketingSourceName)
        {
            return _ManageBAL.CheckDuplicateMarketingSource(MarketingSourceID, MarketingSourceName);
        }

        public bool CheckDuplicateCampaignName(int CampaignID, string CampaignName)
        {
            return _ManageBAL.CheckDuplicateCampaignName(CampaignID, CampaignName);
        }

        public bool CheckDuplicateEventName(int CampaignID, int EventID, string EventName, DateTime eventDate)
        {
            return _ManageBAL.CheckDuplicateEventName(CampaignID,EventID, EventName,eventDate);
        }

        public bool CheckDuplicateMarketingActivity(int CampaignID, int MarketingActivityID, string SourceType, DateTime StartDate, DateTime EndDate, int SourceID)
        {
            return _ManageBAL.CheckDuplicateMarketingActivity(CampaignID,MarketingActivityID,SourceType, StartDate,EndDate,SourceID);
        }


        public void DeleteStatusMgmt(int Id)
        {
            _ManageBAL.DeleteStatusMgmt(Id);
        }

        public void DeleteMarketingSourceMgmt(int Id)
        {
            _ManageBAL.DeleteMarketingSourceMgmt(Id);
        }

        public bool ReCordAttendee(int PatientId, int ProspectID, int AptID, int StaffID, string Clinic, int EventID)
        {
            return _ManageBAL.ReCordAttendee(PatientId, ProspectID, AptID, StaffID, Clinic,EventID);
        }

        public List<CRM_Events_ViewModel> GetAllEventOnDate(DateTime EventDate)
        {
            List<CRM_Events_ViewModel> objEvents = _ManageBAL.GetAllEventOnDate(EventDate);
            return objEvents;
        }

        public List<CRM_CampaignType_ViewModel> GetAllactiveCampaignType(bool Active)
        {
            return _ManageBAL.GetAllactiveCampaignType(Active);
        }

        public void InsertUpdateCampaignType(CRM_CampaignType_ViewModel CampaignModel)
        {
            _ManageBAL.InsertUpdateCampaignType(CampaignModel);
        }

        public ManageGrdProspectViewModel GetProspectById(int ProspectId)
        {
            return _ManageBAL.GetProspectById(ProspectId);
        }

        public bool CheckDuplicateCampaignType(int CampaignID, string CampaignType)
        {
            return _ManageBAL.CheckDuplicateCampaignType(CampaignID, CampaignType);
        }

        public void DeleteProspectAll(string ProspectID)
        {
            _ManageBAL.DeleteProspectAll(ProspectID);
        }

        public List<AppointmentTypeModel> GetAppointmentTypes()
        {
           return _ManageBAL.GetAppointmentTypes();
        }

        public bool SaveAppoint_Patient(int PatientID, int ApptID,int ProspectID, string Clinic, int StaffID, int EventID,int ProviderID, DateTime StartDate, DateTime EndDate)
        {
            return _ManageBAL.SaveAppoint_Patient(PatientID, ApptID, ProspectID, Clinic, StaffID, EventID,ProviderID, StartDate, EndDate);
        }

        public string CheckDuplicateAppointment(int ProviderID, DateTime StartDate, DateTime EndDate)
        {
            return _ManageBAL.CheckDuplicateAppointment(ProviderID, StartDate, EndDate);
        }

        public bool SaveFollowup_Patient(int ProspectID, string Clinic, int StaffID, int EventID)
        {
            return _ManageBAL.SaveFollowup_Patient(ProspectID, Clinic, StaffID, EventID);
        }
    }
}
