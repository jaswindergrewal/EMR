using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;

namespace Emrdev.GeneralClasses
{
    public class ProblemListBAL
    {
        ProblemListDAL objDAL = new ProblemListDAL();
        public ProblemSymptEditViewModel GetProblemSymtomsList(int ProbSymptID)
        {
            return objDAL.GetProblemSymtomsList(ProbSymptID);
        }

        public void UpdateProblemSymtomsList(int ProbSymptID, decimal Priority, decimal Severity, string Trend)
        {
            Problem_Symptom_join _objEdit = objDAL.Get<Problem_Symptom_join>(o => o.ProbSymptID == ProbSymptID);
            _objEdit.Severity_num = Severity;
            _objEdit.Priority_num = Priority;
            _objEdit.Dir = Trend;
            objDAL.Edit(_objEdit);
        }


        public List<ProblemSymptListViewModel> GetProblemSymtomsListByPatientID(int PatientId)
        {
            return objDAL.GetProblemSymtomsListByPatientID(PatientId);
        }

        public List<MisDiagnosisListViewModel> GetMiscDiagListByPatientID(int PatientId)
        {
            return objDAL.GetMiscDiagListByPatientID(PatientId);
        }


        public List<DiagnosisListViewModel> GetProblemSDiagnosisListByPatientID(int PatientId,int AptID)
        {
            return objDAL.GetProblemSDiagnosisListByPatientID(PatientId,AptID);
        }

        public void DeleteProblemListElements(int ElementListID, int ElementID)
        {
            if (ElementListID == 1)
            {
                Problem_Diagnosis_join delOne = objDAL.Get<Problem_Diagnosis_join>(o => o.ProbDiagID == ElementID);
                objDAL.Delete(delOne);

            }
            else if (ElementListID == 2)
            {
                Problem_Symptom_join delOne = objDAL.Get<Problem_Symptom_join>(o => o.ProbSymptID == ElementID);
                objDAL.Delete(delOne);

            }
            else if (ElementListID == 3)
            {
                Problem_MiscDiagnosis_join delOne = objDAL.Get<Problem_MiscDiagnosis_join>(o => o.ProbDiagID == ElementID);
                objDAL.Delete(delOne);

            }
        }

        public List<SymptomProblemListViewModel> GetSymptomList()
        {
            var Symptoms = objDAL.List<Symptom>();
            List<SymptomProblemListViewModel> _objResult = (from s in Symptoms
                                                            where s.viewable_yn == true
                                                            orderby s.SymptomName
                                                            select new SymptomProblemListViewModel { Text = s.SymptomName, Value = s.SymptomID, }).ToList();
            return _objResult;

        }

        public List<SymptomProblemListViewModel> GetDiagnosisList()
        {
            var Diagnosis_tbl = objDAL.List<Diagnosis_tbl>();

            List<SymptomProblemListViewModel> _objResult = (from d in Diagnosis_tbl
                                                            orderby d.Diag_Title
                                                            select new SymptomProblemListViewModel
                                                            {
                                                                Text = d.Diag_Title + " - " + d.ICD9_Code,
                                                                Value = d.Diagnosis_ID
                                                            }).ToList();
            return _objResult;

        }

        public int InsertProblemDiagnosisElements(int ElementListID, MisDiagnosisListViewModel ViewModelInsert,int AptID)
        {
            int Result = 0;
            if (ElementListID == 1)
            {
                Problem_Diagnosis_join _objDuplicateDiag = objDAL.Get<Problem_Diagnosis_join>(o =>( o.DiagnosisID == ViewModelInsert.DiagnosisID && o.PatientID==ViewModelInsert.PatientID));
                if (_objDuplicateDiag == null)
                {
                    Problem_Diagnosis_join cls = new Problem_Diagnosis_join();
                    AutoMapper.Mapper.CreateMap<MisDiagnosisListViewModel, Problem_Diagnosis_join>();
                    cls = AutoMapper.Mapper.Map(ViewModelInsert, cls);
                    objDAL.Create(cls);

                    objDAL.InsertProblemAppointment(Convert.ToInt16(ViewModelInsert.PatientID), Convert.ToInt16(ViewModelInsert.DiagnosisID), AptID);
                 
                }

                else
                {
                    Result = 1;
                }
            }
            else if (ElementListID == 2)
            {
                Problem_MiscDiagnosis_join _objDuplicateMiscDiag = objDAL.Get<Problem_MiscDiagnosis_join>(o => (o.DiagnosisID == ViewModelInsert.DiagnosisID && o.PatientID == ViewModelInsert.PatientID));
                if (_objDuplicateMiscDiag == null)
                {

                    Problem_MiscDiagnosis_join cls = new Problem_MiscDiagnosis_join();
                    AutoMapper.Mapper.CreateMap<MisDiagnosisListViewModel, Problem_MiscDiagnosis_join>();
                    cls = AutoMapper.Mapper.Map(ViewModelInsert, cls);
                    objDAL.Create(cls);
                }
                else
                {
                    Result = 1;
                }
            }
            return Result;
        }

        public int InsertProblemSymptoms(ProblemSymptInsertListViewModel ViewModelInsert)
        {
            int Result = 0;
            Problem_Symptom_join _objDuplicateSympt = objDAL.Get<Problem_Symptom_join>(o => (o.SymptomID == ViewModelInsert.SymptomID && o.PatientID == ViewModelInsert.PatientID));
            if (_objDuplicateSympt == null)
            {
                Problem_Symptom_join cls = new Problem_Symptom_join();
                AutoMapper.Mapper.CreateMap<ProblemSymptInsertListViewModel, Problem_Symptom_join>();
                cls = AutoMapper.Mapper.Map(ViewModelInsert, cls);
                objDAL.Create(cls);
            }
            else
            {
                Result = 1;
            }
            return Result;
        }

        public void UpdateProblemListStatus(int ElementListID, int ElementID, int Status)
        {
            objDAL.UpdateProblemListStatus(ElementListID, ElementID, Status);
        }

        public void UpdateProblemListAddress(int ElementListID, int ElementID)
        {
            objDAL.UpdateProblemListAddress(ElementListID, ElementID);
        }

        public DiagnosisListViewModel GetProblemDiagnosisList(int ProbDiagID)
        {
            return objDAL.GetProblemDiagnosisList(ProbDiagID);
        }


        public void UpdateProblemDiagList(int ProbDiagID, decimal Priority, decimal Severity)
        {
            Problem_Diagnosis_join _objEdit = objDAL.Get<Problem_Diagnosis_join>(o => o.ProbDiagID == ProbDiagID);
            _objEdit.Severity_num = Severity;
            _objEdit.Priority_num = Priority;
            objDAL.Edit(_objEdit);
        }

        public DiagnosisListViewModel GetProblemMiscDiagnosisList(int ProbDiagID)
        {
            return objDAL.GetProblemMiscDiagnosisList(ProbDiagID);
        }

        public void UpdateProblemMiscDiagList(int ProbDiagID, decimal Priority, decimal Severity)
        {
            Problem_MiscDiagnosis_join _objEdit = objDAL.Get<Problem_MiscDiagnosis_join>(o => o.ProbDiagID == ProbDiagID);
            _objEdit.Severity_num = Severity;
            _objEdit.Priority_num = Priority;
            objDAL.Edit(_objEdit);
        }

        public void InsertProblemAppointment(int PatientID, int DiagnosisID, int AptID)
        {
            objDAL.InsertProblemAppointment(PatientID, DiagnosisID, AptID);
        }

        public void DeleteProblemAppointment(int PatientID, int DiagnosisID, int AptID)
        {
            objDAL.DeleteProblemAppointment(PatientID, DiagnosisID, AptID);
        }

        public List<DiagnosistblViewModel> GetDiagnosisPropemApt(int PatientID, int ApptID)
        {
            return objDAL.GetDiagnosisPropemApt(PatientID, ApptID);
        }
    }
    
}
