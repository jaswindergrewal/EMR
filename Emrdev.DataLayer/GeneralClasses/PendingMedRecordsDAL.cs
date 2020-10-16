using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class PendingMedRecordsDAL : ObjectEntity, IRepositary
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

        public List<PendingMedRecordsViewModel> GetPendingMedRegords(string ClinicData)
        {

            var objPendingMedRegords = ObjectEntity1.ssp_GetPendingMedRecords(ClinicData);
            List<PendingMedRecordsViewModel> _listPendingMedRegords = new List<PendingMedRecordsViewModel>();

            foreach (var data in objPendingMedRegords)
            {
                PendingMedRecordsViewModel oPendingMedRegords = new PendingMedRecordsViewModel();
                oPendingMedRegords.PatientID = data.PatientID;
                oPendingMedRegords.apt_id = data.Apt_ID;
                oPendingMedRegords.Clinic = data.Clinic;
                oPendingMedRegords.dateentered = data.DateEntered;
                oPendingMedRegords.EmployeeName = data.EmployeeName;
                oPendingMedRegords.Firstname = data.FirstName;
                oPendingMedRegords.followup_id = data.FollowUp_ID;
                oPendingMedRegords.followup_type_desc = data.FollowUp_Type_Desc;
                oPendingMedRegords.Lastname = data.LastName;
                oPendingMedRegords.Range_End = data.Range_End;
                oPendingMedRegords.Range_Start = data.Range_Start;


                _listPendingMedRegords.Add(oPendingMedRegords);
            }
            return _listPendingMedRegords;
        }    
    }
}
