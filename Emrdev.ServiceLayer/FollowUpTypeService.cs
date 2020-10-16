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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FollowUpTypeService" in both code and config file together.
    public class FollowUpTypeService : IFollowUpTypeService
    {
        FollowupTypesBAL objFollowupTypesBAL = new FollowupTypesBAL();
        public void DoWork()
        {
        }

        public void InsertAptFollowUpType(string FollowUp_Type_Desc, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool Appointment, bool Viewable_YN, bool StaffTicketType_YN, int DepartmentID)
        {
            objFollowupTypesBAL.InsertAptFollowUpType(FollowUp_Type_Desc, ConsultType_YN, FollowUpType_YN, TicketType_YN, Appointment, Viewable_YN, StaffTicketType_YN, DepartmentID);
        }


        public void UpdateAptFollowUpType(string FollowUp_Type_Desc, bool Viewable_YN, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool StaffTicketType_YN, bool Appointment, int DepartmentID, int FollowUp_Type_ID)
        {
            objFollowupTypesBAL.UpdateAptFollowUpType(FollowUp_Type_Desc, Viewable_YN, ConsultType_YN, FollowUpType_YN, TicketType_YN, StaffTicketType_YN, Appointment, DepartmentID, FollowUp_Type_ID);
        }


        public List<FollowupTypesViewModel> GetFollowupTypeList()
        {
            List<FollowupTypesViewModel> lstObj = objFollowupTypesBAL.GetFollowupTypeList();
            return lstObj;
        }


        public int GetPatientIdByAptFollowUps(int TicketID)
        {
            return objFollowupTypesBAL.GetPatientIdByAptFollowUps(TicketID);
        }


        public List<FollowupViewModel> GetFollowupListDetails(int PatientId)
        {
            List<FollowupViewModel> objLst = objFollowupTypesBAL.GetFollowupListDetails(PatientId);
            return objLst;
        }


        public FollowupViewModel GetFollowupListByFollowupId(int FollowupId)
        {
            FollowupViewModel objLst = objFollowupTypesBAL.GetFollowupListByFollowupId(FollowupId);
            return objLst;
        }


        public FollowupViewModel GetFirstRecordForFollowUpType(int FollowUp_ID)
        {
            FollowupViewModel objLst = objFollowupTypesBAL.GetFirstRecordForFollowUpType(FollowUp_ID);
            return objLst;
        }


        public void UpdateFollowupTicket(FollowupViewModel viewModel)
        {
            objFollowupTypesBAL.UpdateFollowupTicket(viewModel);
        }

        public void UpdateFollowupTicketDept(int FollowupID, int Assigned, int Severity)
        {
            objFollowupTypesBAL.UpdateFollowupTicketDept(FollowupID, Assigned, Severity);
        }


        public void UpdateFollowUp_Completed_YN(FollowupViewModel viewModel)
        {
            objFollowupTypesBAL.UpdateFollowUp_Completed_YN(viewModel);
        }


        public int CountAptFollowUpRecords(int StaffId)
        {
            return objFollowupTypesBAL.CountAptFollowUpRecords(StaffId);
        }


        public List<PendingFollowUpsViewModel> GetPendingFollowUpListByPatient(int PatientId)
        {
            List<PendingFollowUpsViewModel> lstObj = objFollowupTypesBAL.GetPendingFollowUpListByPatient(PatientId);
            return lstObj;
        }


        public List<PendingFollowUpsViewModel> GetPendingFollowUpList(int PatientId)
        {
            List<PendingFollowUpsViewModel> lstObj = objFollowupTypesBAL.GetPendingFollowUpList(PatientId);
            return lstObj;
        }


        public void DeleteAptFollowUptypes(int Id)
        {
            objFollowupTypesBAL.DeleteAptFollowUptypes(Id);
        }
    }
}
