using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AdminLabRemindersDAL : ObjectEntity, IRepositary
    {
        public List<LabSymptomViewModel> GetLabSymptoms(int SymptomID)
        {
            var objSymp = ObjectEntity1.ssp_GetLabReminderSymptoms(SymptomID);
            List<LabSymptomViewModel> _listSymp = new List<LabSymptomViewModel>();

            foreach (var data in objSymp)
            {
                LabSymptomViewModel oSymp = new LabSymptomViewModel();
                oSymp.GroupID = data.GroupID;
                oSymp.GroupName = data.GroupName;
                oSymp.Assigned = data.Assigned;
               
                _listSymp.Add(oSymp);
            }

            return _listSymp;
        }

        public List<LabSymptomViewModel> GetLabDiagnosis(int DiagnosisID)
        {
            var objDiag = ObjectEntity1.ssp_GetLabReminderDiagnosis(DiagnosisID);
            List<LabSymptomViewModel> _listDiag = new List<LabSymptomViewModel>();

            foreach (var data in objDiag)
            {
                LabSymptomViewModel oDiag = new LabSymptomViewModel();
                oDiag.GroupID = data.GroupID;
                oDiag.GroupName = data.GroupName;
                oDiag.Assigned = data.Assigned;

                _listDiag.Add(oDiag);
            }

            return _listDiag;
        }


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
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
