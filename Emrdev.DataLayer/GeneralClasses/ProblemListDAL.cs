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
    public class ProblemListDAL : ObjectEntity, IRepositary
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
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public void CreatePart1<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }

        public ProblemSymptEditViewModel GetProblemSymtomsList(int ProbSymptID)
        {
            var objResult = ObjectEntity1.ssp_GetProblemSymtoms(ProbSymptID);

            ProblemSymptEditViewModel ProblemSymtList = new ProblemSymptEditViewModel();
            if (objResult != null)
            {
                foreach (var data in objResult)
                {
                    ProblemSymtList.SymptomName = data.SymptomName;
                    ProblemSymtList.Dir = data.Dir;
                    ProblemSymtList.Priority_num = (int)(data.Priority_num);
                    ProblemSymtList.Severity_num = (int)data.Severity_num;
                  
                }

            }
            return ProblemSymtList;
        }


        public List<ProblemSymptListViewModel> GetProblemSymtomsListByPatientID(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetPatient_ProblemSymptoms(PatientId).ToList();
            var objIList = new List<ProblemSymptListViewModel>();
            Mapper.CreateMap<ssp_GetPatient_ProblemSymptoms_Result, ProblemSymptListViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<MisDiagnosisListViewModel> GetMiscDiagListByPatientID(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetPatient_MiscDiag(PatientId).ToList();
            var objIList = new List<MisDiagnosisListViewModel>();
            Mapper.CreateMap<ssp_GetPatient_MiscDiag_Result, MisDiagnosisListViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        public List<DiagnosisListViewModel> GetProblemSDiagnosisListByPatientID(int PatientId ,int AptID)
        {
            var objResult = ObjectEntity1.ssp_GetProblemDiagnosis(PatientId, AptID).ToList();
            var objIList = new List<DiagnosisListViewModel>();
            Mapper.CreateMap<ssp_GetProblemDiagnosis_Result, DiagnosisListViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void UpdateProblemListStatus(int ElementListID, int ElementID, int Status)
        {
            ObjectEntity1.ssp_UpdateProblemListStatus(ElementID, Status, ElementListID);
        }

        public void UpdateProblemListAddress(int ElementListID, int ElementID)
        {
            ObjectEntity1.ssp_UpdateProblemListAddress(ElementID, ElementListID);
        }

        public DiagnosisListViewModel GetProblemDiagnosisList(int ProbDiagID)
        {
            var objResult = ObjectEntity1.ssp_GetProblemDiagByID(ProbDiagID);
            DiagnosisListViewModel ProblemDiagList = new DiagnosisListViewModel();
            if (objResult != null)
            {
                foreach (var data in objResult)
                {
                    ProblemDiagList.Diag_Title = data.Diag_Title;
                    ProblemDiagList.Priority_num = (int)(data.Priority_num);
                    ProblemDiagList.Severity_num = (int)data.Severity_num;

                }

            }
            return ProblemDiagList;
        }

        public DiagnosisListViewModel GetProblemMiscDiagnosisList(int ProbDiagID)
        {
            var objResult = ObjectEntity1.ssp_GetProblemMiscDiagByID(ProbDiagID);
            DiagnosisListViewModel ProblemDiagList = new DiagnosisListViewModel();
            if (objResult != null)
            {
                foreach (var data in objResult)
                {
                    ProblemDiagList.Diag_Title = data.Diag_Title;
                    ProblemDiagList.Priority_num = (int)(data.Priority_num);
                    ProblemDiagList.Severity_num = (int)data.Severity_num;

                }

            }
            return ProblemDiagList;
        }

        public void InsertProblemAppointment(int PatientID, int DiagnosisID,int AptID)
        {
             ObjectEntityPart1.ssp_InsertProblem_Appointment(PatientID, DiagnosisID, AptID);
        }

        public void DeleteProblemAppointment(int PatientID, int DiagnosisID, int AptID)
        {
            ObjectEntityPart1.ssp_DeleteProblem_Appointment(PatientID, DiagnosisID, AptID);
        }

        public List<DiagnosistblViewModel> GetDiagnosisPropemApt(int PatientID, int ApptID)
        {
            var objResult = ObjectEntityPart1.ssp_GetProblem_Appointment_Diagnosis(PatientID, ApptID).ToList();
            var objIList = new List<DiagnosistblViewModel>();
            Mapper.CreateMap<ssp_GetProblem_Appointment_Diagnosis_Result, DiagnosistblViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        
    }
}
