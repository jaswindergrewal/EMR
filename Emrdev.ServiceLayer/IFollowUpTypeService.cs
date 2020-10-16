using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFollowUpTypeService" in both code and config file together.
    [ServiceContract]
    public interface IFollowUpTypeService
    {
        [OperationContract]
        void InsertAptFollowUpType(string FollowUp_Type_Desc, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool Appointment, bool Viewable_YN, bool StaffTicketType_YN, int DepartmentID);

        [OperationContract]
        void UpdateAptFollowUpType(string FollowUp_Type_Desc, bool Viewable_YN, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool StaffTicketType_YN, bool Appointment, int DepartmentID, int FollowUp_Type_ID);

        [OperationContract]
        List<FollowupTypesViewModel> GetFollowupTypeList();

        [OperationContract]
        int GetPatientIdByAptFollowUps(int TicketID);

        [OperationContract]
        List<FollowupViewModel> GetFollowupListDetails(int PatientId);

        [OperationContract]
        FollowupViewModel GetFollowupListByFollowupId(int FollowupId);

        [OperationContract]
        FollowupViewModel GetFirstRecordForFollowUpType(int FollowUp_ID);

        [OperationContract]
        void UpdateFollowupTicket(FollowupViewModel VeiwModel);

        [OperationContract]
        void UpdateFollowupTicketDept(int FollowupID, int Assigned, int Severity);

        [OperationContract]
        void UpdateFollowUp_Completed_YN(FollowupViewModel viewModel);

        [OperationContract]
        int CountAptFollowUpRecords(int StaffId);

        [OperationContract]
        List<PendingFollowUpsViewModel> GetPendingFollowUpListByPatient(int PatientId);

        [OperationContract]
        List<PendingFollowUpsViewModel> GetPendingFollowUpList(int PatientId);

        [OperationContract]
        void DeleteAptFollowUptypes(int Id);

        [OperationContract]
        void DoWork();
    }
}
