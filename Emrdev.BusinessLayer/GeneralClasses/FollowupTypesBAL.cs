using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class FollowupTypesBAL
    {
        FollowupTypesDAL objFollowupTypesDAL = new FollowupTypesDAL();

        public List<FollowupTypesViewModel> GetFollowupTypeList()
        {
            List<FollowupTypesViewModel> lstObj = objFollowupTypesDAL.GetFollowupTypeList();
            return lstObj;
        }
        public void InsertAptFollowUpType(string FollowUp_Type_Desc, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool Appointment, bool Viewable_YN, bool StaffTicketType_YN, int DepartmentID)
        {
            objFollowupTypesDAL.InsertAptFollowUpType(FollowUp_Type_Desc, ConsultType_YN, FollowUpType_YN, TicketType_YN, Appointment, Viewable_YN, StaffTicketType_YN, DepartmentID);
        }

        public void UpdateAptFollowUpType(string FollowUp_Type_Desc, bool Viewable_YN, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool StaffTicketType_YN, bool Appointment, int DepartmentID, int FollowUp_Type_ID)
        {
            objFollowupTypesDAL.UpdateAptFollowUpType(FollowUp_Type_Desc, Viewable_YN, ConsultType_YN, FollowUpType_YN, TicketType_YN, StaffTicketType_YN, Appointment, DepartmentID, FollowUp_Type_ID);
        }

        public int GetPatientIdByAptFollowUps(int TicketID)
        {
            return objFollowupTypesDAL.GetPatientIdByAptFollowUps(TicketID);
        }

        public List<FollowupViewModel> GetFollowupListDetails(int PatientId)
        {
          
            var objIList = new List<FollowupViewModel>();
            var objEntity = new List<apt_FollowUps>();
            objEntity = objFollowupTypesDAL.GetAll<apt_FollowUps>(p => p.PatientID == PatientId && p.FollowUp_Subject != null).OrderByDescending(p => p.DateEntered).ToList();
            Mapper.CreateMap<apt_FollowUps, FollowupViewModel>();
            objIList = Mapper.Map(objEntity, objIList);
            return objIList;
           
        }

        public FollowupViewModel GetFollowupListByFollowupId(int FollowupId)
        {

            var objFollowup = new FollowupViewModel();
            var apt_FollowUps = objFollowupTypesDAL.List<apt_FollowUps>();
            var apt_FollowUp_types = objFollowupTypesDAL.List<apt_FollowUp_types>();
            objFollowup = (from e in apt_FollowUps
                           join c in apt_FollowUp_types on e.FollowUp_Cat equals c.FollowUp_Type_ID
                           where e.FollowUp_ID == FollowupId

                           select new FollowupViewModel
                         {
                             FollowUp_ID = e.FollowUp_ID,
                             FollowUp_Body = e.FollowUp_Body,
                             Apt_ID = e.Apt_ID,
                             AptAssigned=e.AptAssigned,
                             FollowUp_Cat=e.FollowUp_Cat,
                             FollowUp_Subject=e.FollowUp_Subject,
                             DateEntered=e.DateEntered,
                             Entered_By=e.Entered_By,
                             PatientID=e.PatientID,
                             FollowUp_Completed_YN=e.FollowUp_Completed_YN,
                             FollowUp_Assigned_YN=e.FollowUp_Assigned_YN,
                             Severity=e.Severity,
                             DueDate=e.DueDate,
                             DepartmentAssign=e.DepartmentAssign,
                             Assigned=e.Assigned,
                             CustomHeader=c.CustomHeader,
                             CustomPage=c.CustomPage,
                             CustomParams=c.CustomParams,

                         }).SingleOrDefault();

         
         
            
            
            //var objIList = new FollowupViewModel();
            //var objEntity = new apt_FollowUps();
            //objEntity = objFollowupTypesDAL.Get<apt_FollowUps>(p => p.FollowUp_ID == FollowupId);
            //Mapper.CreateMap<apt_FollowUps, FollowupViewModel>();
            //objIList = Mapper.Map(objEntity, objIList);
            return objFollowup;
        }

        public FollowupViewModel GetFirstRecordForFollowUpType(int FollowUp_ID)
        {
            FollowupViewModel objLst = objFollowupTypesDAL.GetFirstRecordForFollowUpType(FollowUp_ID);
            return objLst;
        }

        public void UpdateFollowupTicket(FollowupViewModel viewModel)
        {
            apt_FollowUps clsGroup = new apt_FollowUps();
            AutoMapper.Mapper.CreateMap<FollowupViewModel, apt_FollowUps>();
            clsGroup = AutoMapper.Mapper.Map(viewModel, clsGroup);
            objFollowupTypesDAL.Edit(clsGroup);
            //objFollowupTypesDAL.UpdateFollowupTicket(FollowupID, Assigned, Severity);
        }

        public void UpdateFollowupTicketDept(int FollowupID, int Assigned, int Severity)
        {
            objFollowupTypesDAL.UpdateFollowupTicketDept(FollowupID, Assigned, Severity);
        }

        public void UpdateFollowUp_Completed_YN(FollowupViewModel viewModel)
        {
            apt_FollowUps clsGroup = new apt_FollowUps();
            AutoMapper.Mapper.CreateMap<FollowupViewModel, apt_FollowUps>();
            clsGroup = AutoMapper.Mapper.Map(viewModel, clsGroup);
            objFollowupTypesDAL.Edit(clsGroup);
        }

        public int CountAptFollowUpRecords(int StaffId)
        {
            return objFollowupTypesDAL.CountAptFollowUpRecords(StaffId);
        }

        public List<PendingFollowUpsViewModel> GetPendingFollowUpListByPatient(int PatientId)
        {
            List<PendingFollowUpsViewModel> lstObj = objFollowupTypesDAL.GetPendingFollowUpListByPatient(PatientId);
            return lstObj;
        }

        public List<PendingFollowUpsViewModel> GetPendingFollowUpList(int PatientId)
        {
            List<PendingFollowUpsViewModel> lstObj = objFollowupTypesDAL.GetPendingFollowUpList(PatientId);
            return lstObj;
        }

        public void DeleteAptFollowUptypes(int Id)
        {
            objFollowupTypesDAL.DeleteAptFollowUptypes(Id);
        }
    }
}
