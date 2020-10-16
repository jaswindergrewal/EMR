using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IManageService
    {
        [OperationContract]
        List<CRM_Campaigns_ViewModel> GetAllCrmCampaign();
        [OperationContract]
        dynamic GetAllapt_recs();
        [OperationContract]
        List<CRM_Status_ViewModel> GetAllactiveStatus();
        [OperationContract]
        List<CRM_Events_ViewModel> GetAllEvents();
        [OperationContract]
        List<CRM_MarketingSources_ViewModel> GetAllMarketingSource();
        [OperationContract]
        List<ManageGrdProspectViewModel> GetAllProspect();
        [OperationContract]
        List<ClinicsViewModel> GetAllClinic();
        [OperationContract]
        List<ManageCampaignViewModel> GetAllCampaign();
        [OperationContract]
        void DeleteEvent(int EventId);
        [OperationContract]
        void DeleteCampaign(int CampaignID);
        [OperationContract]
        ManageGrdProspectViewModel GetProspectById(int ProspectId);

        [OperationContract]
        void InsertUpdateProspect(int ProspectID, string Address, string AltPhone, string City, string ContactMethod,
                                        string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                        string Notes, string State, int StatusID, string Zip, int UserName, int EventId);
        [OperationContract]
        void DeleteProspect(int ProspectID);
        [OperationContract]
        void InsertUpdateStatus(int StatusID, bool Active_YN, string StatusName);
        [OperationContract]
        void InsertUpdateMSource(int MarketingSourceID, bool Active_YN, string MarketingSourceName);
        [OperationContract]
        List<MarketingActivityViewModel> GetMarketingActivity(int CapmpaignID);
        [OperationContract]
        void InsertUpdateCampaign(int CampaignID, string CampaignName, string CampaignType, string MarketingBudget, string StartDate,
                                        string EndDate, string MarketingSources);
        [OperationContract]
        void InsertUpdateActivity(int MarketingActivityID, string CampaignId, string EndDate, string MoneySpent, string Notes,
                                        string SourceID, string SourceType, string StartDate);
        [OperationContract]
        dynamic GetAllAttend(int EventID);
        [OperationContract]
        void AddRecordAttendee(int EventID, int ProspectId);
        [OperationContract]
        List<MarketingSourceViewModel> GetSelectedMarketingSource(int prospectId, int TabId);

        [OperationContract]
        void InsertUpdateEvent(int EventId, string EventDate, string EventName, string Notes, string Venue, string Appointments,
                                      string AudienceQuality, string AudienceReaction, string Callbacks, string EventLength, string FacilityInteriorExterior,
                                      string Location, string OverallPerformance, string Parking, string VenueQuality, string Walkins, int CampaignID);

        [OperationContract]
        bool CheckDuplicateProspect(int ProspectID, string Email);
        [OperationContract]
        int CheckDuplicateFbImporter(string Event, string Email);

        [OperationContract]
        bool CheckDuplicateStatus(int StatusID, string StatusName);

        [OperationContract]
        bool CheckDuplicateMarketingSource(int MarketingSourceID, string MarketingSourceName);

        [OperationContract]
        bool CheckDuplicateCampaignName(int CampaignID, string CampaignName);

        [OperationContract]
        bool CheckDuplicateEventName(int CampaignID,int EventID, string EventName,DateTime eventDate);

        [OperationContract]
        bool CheckDuplicateMarketingActivity(int CampaignID, int MarketingActivityID, string SourceType, DateTime StartDate, DateTime EndDate, int SourceID);

        [OperationContract]
        void DeleteStatusMgmt(int Id);

        [OperationContract]
        void DeleteMarketingSourceMgmt(int Id);

        [OperationContract]
        bool ReCordAttendee(int PatientId, int ProspectID, int AptID, int StaffID, string Clinic,int EventID);

        [OperationContract]
        List<CRM_Events_ViewModel> GetAllEventOnDate(DateTime EventDate);

        [OperationContract]
        List<CRM_CampaignType_ViewModel> GetAllactiveCampaignType(bool Active);

        [OperationContract]
        void InsertUpdateCampaignType(CRM_CampaignType_ViewModel CampaignModel);

        [OperationContract]
        bool CheckDuplicateCampaignType(int CampaignID, string CampaignType);

        [OperationContract]
        void DeleteProspectAll(string ProspectID);

        [OperationContract]
        List<AppointmentTypeModel> GetAppointmentTypes();

        [OperationContract]
        bool SaveAppoint_Patient(int PatientID, int ApptID,int ProspectID, string Clinic, int StaffID, int EventID,int ProviderID, DateTime StartDate, DateTime EndDate);

        [OperationContract]
        string CheckDuplicateAppointment(int ProviderID, DateTime StartDate, DateTime EndDate);

        [OperationContract]
        bool SaveFollowup_Patient(int ProspectID, string Clinic, int StaffID, int EventID);
    }

    //Emr2017

    //// Use a data contract as illustrated in the sample below to add composite types to service operations.
    //// You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Emrdev.ServiceLayer.ContractType".
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
