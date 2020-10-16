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
    public class PendingPrescriptionAproveDAL:ObjectEntity, IRepositary
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion

        public PendingPrescriptionAproveViewModel GetPescriptionDetail(int StaffId, int PrescriptionID)
        {
            var objResult = ObjectEntity1.ssp_GetPrescriptionAprove(StaffId, PrescriptionID);
            PendingPrescriptionAproveViewModel PendingPrescriptionList = new PendingPrescriptionAproveViewModel();
            if (objResult != null)
            {
                foreach (var data in objResult)
                {
                    PendingPrescriptionList.DrugName = data.DrugName;
                    PendingPrescriptionList.Drug_NumbRefills = data.Drug_NumbRefills;
                    PendingPrescriptionList.Drug_EndDate = data.Drug_EndDate;
                    PendingPrescriptionList.Drug_Dispenses = data.Drug_Dispenses;
                    PendingPrescriptionList.Drug_DatePrescibed = data.Drug_DatePrescibed;
                    PendingPrescriptionList.AccessLevel = data.AccessLevel;
                    PendingPrescriptionList.Approved_Date = data.Approved_Date;
                    PendingPrescriptionList.Approved_YN = data.Approved_YN;
                    PendingPrescriptionList.DateEntered = data.DateEntered;
                    PendingPrescriptionList.Drug_Dose = data.Drug_Dose;
                    PendingPrescriptionList.EmployeeName = data.EmployeeName;
                    PendingPrescriptionList.FirstName = data.FirstName;
                    PendingPrescriptionList.LastName = data.LastName;
                    PendingPrescriptionList.Notes = data.Notes;
                    PendingPrescriptionList.PatientID = data.PatientID;
                    PendingPrescriptionList.PrescriptionID = data.PrescriptionID;
                    PendingPrescriptionList.ThirdParty_YN = data.ThirdParty_YN;
                    PendingPrescriptionList.viewable_yn = data.viewable_yn;
                }

            }
            return PendingPrescriptionList;
           
        }

        public PendingPrescriptionAproveViewModel GetPescriptionSupDetail(int StaffId, int PrescriptionID)
        {
            var objResult = ObjectEntity1.ssp_GetPrescriptionSuppAprove(StaffId, PrescriptionID);
            PendingPrescriptionAproveViewModel PendingPrescriptionList = new PendingPrescriptionAproveViewModel();
            if (objResult != null)
            {
                foreach (var data in objResult)
                {
                    PendingPrescriptionList.DrugName = data.DrugName;
                    PendingPrescriptionList.Drug_NumbRefills = data.Drug_NumbRefills;
                    PendingPrescriptionList.Drug_EndDate = data.Drug_EndDate;
                    PendingPrescriptionList.Drug_Dispenses = data.Drug_Dispenses;
                    PendingPrescriptionList.Drug_DatePrescibed = data.Drug_DatePrescibed;
                    PendingPrescriptionList.AccessLevel = data.AccessLevel;
                    PendingPrescriptionList.Approved_Date = data.Approved_Date;
                    PendingPrescriptionList.Approved_YN = data.Approved_YN;
                    PendingPrescriptionList.DateEntered = data.DateEntered;
                    PendingPrescriptionList.Drug_Dose = data.Drug_Dose;
                    PendingPrescriptionList.EmployeeName = data.EmployeeName;
                    PendingPrescriptionList.FirstName = data.FirstName;
                    PendingPrescriptionList.LastName = data.LastName;
                    PendingPrescriptionList.Notes = data.Notes;
                    PendingPrescriptionList.PatientID = data.PatientID;
                    PendingPrescriptionList.PrescriptionID = data.PrescriptionID;
                    PendingPrescriptionList.viewable_yn = data.viewable_yn;
                }

            }
            return PendingPrescriptionList;

        }
    }
}
