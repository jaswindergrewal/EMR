using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class LabLaunchDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }//test

        public void Edit<T>(T entityToEdit) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Get By Patient Id

        public Emrdev.ViewModelLayer.LabLaunchViewModel GetByPatientId(int patientId)
        {
            Emrdev.ViewModelLayer.LabLaunchViewModel objModel=new ViewModelLayer.LabLaunchViewModel();
            //ObjectEntity1.Lab_LabList(patientId);
            objModel = ObjectEntity1.Patients.Select(i => new Emrdev.ViewModelLayer.LabLaunchViewModel { PatientId = i.PatientID, FirstName = i.FirstName, LastName = i.LastName }).SingleOrDefault(i => i.PatientId == patientId);
            objModel.JoinCollection = ObjectEntity1.Lab_LabList(patientId).Select(i => new Emrdev.ViewModelLayer.NavigationProp { MessageID = i.MessageID, ObservationDateTime = i.ObservationDateTime == null ? "--" : i.ObservationDateTime.Value.ToString("MM-dd-yyyy"), LastChanged = i.LastChanged.ToString("MM-dd-yyyy") }).ToList();            
            return objModel;
        }

        #endregion


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
