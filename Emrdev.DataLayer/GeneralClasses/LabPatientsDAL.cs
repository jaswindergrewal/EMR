using AutoMapper;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Emrdev.DataLayer
{
    public class LabPatientsDAL : ObjectEntity, IRepositary
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

        /// <summary>
        ///this function used for get unassigned clinic patient details using Labs_UnmatchedLabs store procedure.
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns>PatientViewModel</returns>
        /// Created By : Rakesh Pareek
        /// Cteated Date : 4-sep-2013
        public List<LabPatientsViewModel> GetUnmatchedLabsPatientDataBy()
        {
            var objResult = ObjectEntity1.Labs_UnmatchedLabs().ToList();
            var objIList = new List<LabPatientsViewModel>();
            if (objResult != null)
            {
                foreach (var recd in objResult)
                {
                    LabPatientsViewModel objLabPatientsViewModel = new LabPatientsViewModel();
                    objLabPatientsViewModel.PatientNameFirstName = recd.PatientNameFirstName.ToString();
                    objLabPatientsViewModel.PatientNameLastName = recd.PatientNameLastName.ToString();
                    objLabPatientsViewModel.ID = recd.ID;
                    objLabPatientsViewModel.Sex = recd.Sex;
                    objLabPatientsViewModel.DateOfBirth = recd.DateOfBirth;
                    objIList.Add(objLabPatientsViewModel);
                }
            }
            return objIList;

        }

        /// <summary>
        /// this function used for get lab_Patients details according the labID
        /// </summary>
        /// <param name="LabID"></param>
        /// <returns>LabPatientsViewModel</returns>
        /// Created Date : Rakesh Kumar
        /// Created Date : 4-Sep-2013
        public LabPatientsViewModel LabQuestPatientMatchByLabID(int LabID)
        {
            Mapper.CreateMap<lab_QuestPatient_Match_Result, LabPatientsViewModel>();
            var objResult = ObjectEntity1.lab_QuestPatient_Match(LabID).FirstOrDefault();
            var objIList = new LabPatientsViewModel();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


       
        /// <summary>
        /// this function used for update the lab_patients details in the database lab_patients table.
        /// </summary>
        /// <returns>id</returns>
        /// Created By : Rakesh Kumar
        /// Created Date : 4-Sep-2013
        public int UpdaterLabPatientDetails(LabPatientsViewModel objLabPatientsViewModel)
        {
            int id = 0;

            var labPatient = ObjectEntity1.lab_Patients.Where(lp => lp.ID == objLabPatientsViewModel.ID).FirstOrDefault();
            if (labPatient != null)
            {
                labPatient.CorrespondingPatientID = objLabPatientsViewModel.PatientId;
                ObjectEntity1.SaveChanges();
                id = objLabPatientsViewModel.ID;
            }

            return id;
        }
  
    }
}
