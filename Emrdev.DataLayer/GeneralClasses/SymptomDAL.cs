using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class SymptomDAL : ObjectEntity, IRepositary
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<AutoshipProductsForSyymptomViewModel> PopulateSymptomList(int SymptomId)
        {
            var objResult = ObjectEntity1.ssp_PopulateSymptomList(SymptomId).ToList();
            var objIList = new List<AutoshipProductsForSyymptomViewModel>();
            Mapper.CreateMap<ssp_PopulateSymptomList_Result, AutoshipProductsForSyymptomViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<GroupRangeViewModel> GetGroupRangeId()
        {
            var objResult = GetDetails<GroupRange>();
            var objIList = new List<GroupRangeViewModel>();
            Mapper.CreateMap<GroupRange, GroupRangeViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateDiagList(int DiagnosisId)
        {
            var objResult = ObjectEntity1.ssp_PopulateDiagList(DiagnosisId).ToList();
            var objIList = new List<AutoshipProductsForSyymptomViewModel>();
            Mapper.CreateMap<ssp_PopulateDiagList_Result, AutoshipProductsForSyymptomViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateLabList(int GroupRangeId)
        {
            var objResult = ObjectEntity1.ssp_PopulateLabsList(GroupRangeId).ToList();
            var objIList = new List<AutoshipProductsForSyymptomViewModel>();
            Mapper.CreateMap<ssp_PopulateLabsList_Result, AutoshipProductsForSyymptomViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public dynamic BindRangeListBox()
        {
            var obj = ObjectEntity1.ssp_BindRangeListBox().ToList();
            return obj;
        }

        public void DeleteSymptomSupplement(int SymptomId)
        {
            ObjectEntity1.ssp_DeleteSymptomSupplement(SymptomId);
        }

        public void DeleteDiagnosisSupplements(int Diagnosis_ID)
        {
            ObjectEntity1.ssp_DeleteDiagnosisSupplements(Diagnosis_ID);
        }

        public void DeleteGroupRangeSupplement(int GroupRangeID)
        {
            ObjectEntity1.ssp_DeleteGroupRangeSupplement(GroupRangeID);
        }

        public int GetSymptomId()
        {
            int SymptomID = 0;
            SymptomID = (from s in ObjectEntity1.Symptoms
                         where s.viewable_yn == true
                         && s.SymptomName != string.Empty
                         orderby s.SymptomName
                         select s.SymptomID).First();
            return SymptomID;
        }

        public int GetDiagnosisID()
        {
            int DiagnosisID = 0;
            DiagnosisID = (from s in ObjectEntity1.Diagnosis_tbl
                           where s.Viewable_YN == true
                           && s.Diag_Title != string.Empty
                           orderby s.Diag_Title
                           select s.Diagnosis_ID).First();
            return DiagnosisID;
        }

        public int GetRangeID()
        {
            int RangeID = 0;

            RangeID = (from s in ObjectEntity1.GroupRanges
                       orderby s.GroupRangeID
                       select s.GroupRangeID).FirstOrDefault();

            return RangeID;
        }
    }
}
