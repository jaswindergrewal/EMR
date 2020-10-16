using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class SharepointPrescriptionDetailDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }//test

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart1.SaveChanges();

        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntityPart1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }


        public List<SharepointPrescriptionDetailViewModel> GetAllSharepointPrescriptionDetailList(int ID)
        {
            var objResult = ObjectEntityPart1.ssp_GetAllSharepointPrescriptionDetailList(ID).ToList();
            var objIList = new List<SharepointPrescriptionDetailViewModel>();
            Mapper.CreateMap<ssp_GetAllSharepointPrescriptionDetailList_Result, SharepointPrescriptionDetailViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        public List<SharepointPrescriptionDetailViewModel> GetAllSharepointPrescriptionDetailReport(string PatientName, string Clinic, string Physician, DateTime? LastRefill, DateTime? MedStartDate, bool IsDiet, bool IsMedical)
        {
            var objResult = ObjectEntityPart1.ssp_GetAllSharepointPrescriptionDetailReport(PatientName, Clinic, Physician, LastRefill, MedStartDate, IsDiet, IsMedical).ToList();
            var objIList = new List<SharepointPrescriptionDetailViewModel>();
            Mapper.CreateMap<ssp_GetAllSharepointPrescriptionDetailReport_Result, SharepointPrescriptionDetailViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertUpdateSharePointPrescriptionDetail(int PresciptionId, string PatientName, string Clinic, string Vials, DateTime? LastRefill, DateTime? MedStartDate, string Physician, string Comments, string Diet, string Medical)
        {//emr2017
            //ObjectEntityPart1.ssp_InsertUpdateSharePointPrescriptionDetail(PresciptionId, PatientName, Clinic, Vials, LastRefill, MedStartDate, Physician, Comments, Diet, Medical);
            ObjectEntityPart1.SaveChanges();
        }
    }
}
