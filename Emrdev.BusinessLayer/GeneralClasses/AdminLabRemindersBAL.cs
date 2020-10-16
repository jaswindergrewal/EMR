using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using AutoMapper;

namespace Emrdev.GeneralClasses
{
    public class AdminLabRemindersBAL
    {
        AdminLabRemindersDAL objDAL = new AdminLabRemindersDAL();
        public List<LabSymptomViewModel> GetLabDiagnosis(int DiagnosisID)
        {
            return objDAL.GetLabDiagnosis(DiagnosisID);
        }

        public List<LabSymptomViewModel> GetLabSymptoms(int SymptomID)
        {
            return objDAL.GetLabSymptoms(SymptomID);
        }

        public List<SymptomViewModel> getAllSymptoms()
        {
            var Symptoms = objDAL.List<Symptom>();
            List<SymptomViewModel> _objSymptoms = (from s in Symptoms
                                                  where s.viewable_yn==true
                                                  && s.SymptomName!=""
                                                  select new SymptomViewModel
                                                    {
                                                        SymptomName = s.SymptomName,
                                                        SymptomID = s.SymptomID
                                                    }).ToList();
            return _objSymptoms;
        }

        public List<DiagnosistblViewModel> getAllDiagnosis()
        {
            var Diagnosis = objDAL.List<Diagnosis_tbl>();
            List<DiagnosistblViewModel> _objDiagnosis = (from d in Diagnosis
                                                   where d.Viewable_YN == true
                                                   && d.Diag_Title != ""
                                                    select new DiagnosistblViewModel
                                                   {
                                                       Diagnosis_ID = d.Diagnosis_ID,
                                                       Diag_Title = d.Diag_Title
                                                   }).ToList();
            return _objDiagnosis;
        }


        public void InsertDiagnosisLab(DiagnosisLabViewModel sysup)
        {
            DiagnosisLab DiagnosisLabEntity = new DiagnosisLab();
            Mapper.CreateMap<DiagnosisLabViewModel, DiagnosisLab>();
            DiagnosisLabEntity = Mapper.Map(sysup, DiagnosisLabEntity);
            objDAL.Edit(DiagnosisLabEntity);
        }

        public void DeleteDiagnosisLab(int DiagnosisID)
        {
            
             List<DiagnosisLab> _objDiagnosis=objDAL.GetAll<DiagnosisLab>(o=>o.DiagnosisID==DiagnosisID).ToList();
             objDAL.Delete(_objDiagnosis);
        }

        public void DeleteSymptomLabs(int SymptomID)
        {
            List<SymptomLab> _objSymptom = objDAL.GetAll<SymptomLab>(o => o.SymptomID == SymptomID).ToList();
            objDAL.Delete(_objSymptom);
        }

        public void InsertSymptomLab(SymptomLabViewModel sysup)
        {
            SymptomLab SymptomLabEntity = new SymptomLab();
            Mapper.CreateMap<SymptomLabViewModel, SymptomLab>();
            SymptomLabEntity = Mapper.Map(sysup, SymptomLabEntity);
            objDAL.Edit(SymptomLabEntity);
        }
    
    }
}
