using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class SearchResultDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            throw new NotImplementedException();
        }

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
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
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

        public List<Patient_Details_ViewModel> SearchResult(string FirstName, string LastName, string MiddleName, string Phone, string Clinic, bool InActive, int PageIndex, int PageSize)
        {
            var objPatient = ObjectEntity1.ssp_GetPatientSearchResult(FirstName, LastName, MiddleName, Phone, Clinic, InActive, PageIndex, PageSize);
            List<Patient_Details_ViewModel> _listPatient = new List<Patient_Details_ViewModel>();

            foreach (var data in objPatient)
            {
                Patient_Details_ViewModel oPatient = new Patient_Details_ViewModel();
                oPatient.PatientID = Convert.ToInt16(data.PatientID);
                oPatient.FirstName = data.FirstName;
                oPatient.LastName = data.LastName;
                oPatient.RecordCount = data.RecordCount;
                oPatient.MiddleInitial = data.MiddleInitial;
                oPatient.HomePhone = data.HomePhone;
                _listPatient.Add(oPatient);
            }

            return _listPatient;
        }
    }
}
