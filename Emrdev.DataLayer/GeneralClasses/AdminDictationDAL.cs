using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AdminDictationDAL : ObjectEntity, IRepositary
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
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
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
            throw new NotImplementedException();
        }

        #endregion

        #region Methods
        //Method to delete Diagnosis by using sp
        public void DeleteDictationDiagnosis(int DiagnosisID)
        {
            ObjectEntity1.ssp_DeleteDictationDiagnosis(DiagnosisID);
        }

        //Method to delete plan by using sp
        public void DeleteDictationPlan(int DiagnosisID, int PlanID, bool IsDiagnosis)
        {
            ObjectEntity1.ssp_DeleteDictationPlan(DiagnosisID, PlanID, IsDiagnosis);
        }

        public List<Dictation_Plan_ViewModel> GetDictationPlanByID(int PlanID)
        {
            var objDiagplan = ObjectEntity1.ssp_GetDictationPlanByID(PlanID);
            List<Dictation_Plan_ViewModel> _listDiagplan = new List<Dictation_Plan_ViewModel>();

            foreach (var data in objDiagplan)
            {
                Dictation_Plan_ViewModel oDiagplan = new Dictation_Plan_ViewModel();
                oDiagplan.PlanID = data.PlanID;
                oDiagplan.PlanName = data.PlanName;
                oDiagplan.PlanDescrip = data.PlanDescrip;
                oDiagplan.KeyWords = data.KeyWords;
                oDiagplan.DiagnosisID = data.DiagnosisID;
                oDiagplan.Category = data.Category;
                oDiagplan.Viewable_YN = data.Viewable_YN;
                _listDiagplan.Add(oDiagplan);
            }

            return _listDiagplan;
        }

        public string InsertDictation_Diagnosis(string DiagnosisDescrip, string DiagnosisName, string ICDCode, string KeyWords, bool Viewable_YN)
        {
            return ObjectEntity1.ssp_InsertDictation_Diagnosis(DiagnosisDescrip, DiagnosisName, ICDCode, KeyWords, Viewable_YN).FirstOrDefault().ToString();
        }

        public string InsertDictation_Plan(string Category, string PlanDesc, string PlanName, string KeyWords, bool Viewable_YN)
        {
            return ObjectEntity1.ssp_InsertDictation_Plan(Category, PlanDesc, PlanName, KeyWords, Viewable_YN).FirstOrDefault().ToString();
        }

        public string CheckExistDictation_Plan(string Category, string PlanName, int PlanId)
        {
            var variable = ObjectEntity1.ssp_CheckExistDictation_Plan(Category, PlanName, PlanId).FirstOrDefault();
            if (variable != null)
                return variable.ToString();
            else
                return string.Empty;
        }

        public string ValidateAndUpdateDictation_Diagnosis(int DiagnosisID, string DiagnosisName, string ICDCode)
        {
            var variable = ObjectEntityPart1.ssp_ValidateAndUpdateDictation_Diagnosis(DiagnosisID, DiagnosisName, ICDCode).FirstOrDefault();
            if (variable != null)
                return variable.ToString();
            else
                return string.Empty;
        }

        #endregion
    }
}
