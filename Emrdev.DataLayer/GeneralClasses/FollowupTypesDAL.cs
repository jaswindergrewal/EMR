using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using System.Data;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class FollowupTypesDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).Count();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<FollowupTypesViewModel> GetFollowupTypeList()
        {
            var objResult = ObjectEntity1.ssp_GetFollowupTypesList().ToList();
            var objIList = new List<FollowupTypesViewModel>();
            Mapper.CreateMap<ssp_GetFollowupTypesList_Result, FollowupTypesViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertAptFollowUpType(string FollowUp_Type_Desc, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool Appointment, bool Viewable_YN, bool StaffTicketType_YN, int DepartmentID)
        {
            ObjectEntity1.ssp_AddFollowUpTypeDetails(FollowUp_Type_Desc, ConsultType_YN, FollowUpType_YN, TicketType_YN, Appointment, Viewable_YN, StaffTicketType_YN, DepartmentID);
        }

        public void UpdateAptFollowUpType(string FollowUp_Type_Desc, bool Viewable_YN, bool ConsultType_YN, bool FollowUpType_YN, bool TicketType_YN, bool StaffTicketType_YN, bool Appointment, int DepartmentID, int FollowUp_Type_ID)
        {
            ObjectEntity1.ssp_UpdateFollowUpTypeDetails(FollowUp_Type_Desc, Viewable_YN, ConsultType_YN, FollowUpType_YN, TicketType_YN, StaffTicketType_YN, Appointment, DepartmentID, FollowUp_Type_ID);
        }

        public int GetPatientIdByAptFollowUps(int TicketID)
        {
            int PatientID = 0;
            //Code review point Commented below code

            //var tester = (from f in ObjectEntity1.apt_FollowUps
            //              where f.FollowUp_ID == TicketID//int.Parse(Request.QueryString["TicketID"])
            //              select f).FirstOrDefault();

            var patient = (from f in ObjectEntity1.apt_FollowUps
                           where f.FollowUp_ID == TicketID//int.Parse(Request.QueryString["TicketID"])
                           select f).FirstOrDefault();

            if (patient.PatientID != null)
            {
                PatientID = patient.PatientID.Value;
            }
            return PatientID;

        }

        public int CountAptFollowUpRecords(int StaffId)
        {
            int i = 0;
            i = (from t in ObjectEntity1.apt_FollowUps
                 where t.Assigned == StaffId
                 && t.FollowUp_Completed_YN == false
                 && t.DueDate <= DateTime.Now
                 select t).Count();
            return i;
        }

        public List<FollowupViewModel> GetFollowupListDetails()
        {
            var objEntity = GetDetails<apt_FollowUps>().ToList();
            var objIList = new List<FollowupViewModel>();
            Mapper.CreateMap<apt_FollowUps, FollowupViewModel>();
            objIList = Mapper.Map(objEntity, objIList);
            return objIList;
        }

        public FollowupViewModel GetFirstRecordForFollowUpType(int FollowUp_ID)
        {
            var _objpatientList = new FollowupViewModel();
            var PatientEntity = new apt_FollowUps();
            PatientEntity = Get<apt_FollowUps>(o => o.FollowUp_ID == FollowUp_ID);
            Mapper.CreateMap<apt_FollowUps, FollowupViewModel>();
            _objpatientList = Mapper.Map(PatientEntity, _objpatientList);
            return _objpatientList;
        }

        public void UpdateFollowupTicket(int FollowupID, int Assigned, int Severity)
        {
            ObjectEntity1.apt_followUp_UpdateTicket(FollowupID, Assigned, Severity);
        }

        public void UpdateFollowupTicketDept(int FollowupID, int Assigned, int Severity)
        {
            ObjectEntity1.apt_followUp_UpdateTicketDept(FollowupID, Assigned, Severity);
        }

        public List<PendingFollowUpsViewModel> GetPendingFollowUpListByPatient(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetPendingFollowUpListByPatient(PatientId).ToList();
            var objIList = new List<PendingFollowUpsViewModel>();
            Mapper.CreateMap<ssp_GetPendingFollowUpListByPatient_Result, PendingFollowUpsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingFollowUpsViewModel> GetPendingFollowUpList(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetPendingFollowUpList(PatientId).ToList();
            var objIList = new List<PendingFollowUpsViewModel>();
            Mapper.CreateMap<ssp_GetPendingFollowUpList_Result, PendingFollowUpsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void DeleteAptFollowUptypes(int Id)
        {
            ObjectEntity1.ssp_DeleteAptFollowUptypes(Id);
        }
    }
}
