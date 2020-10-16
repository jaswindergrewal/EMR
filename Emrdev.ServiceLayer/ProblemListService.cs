using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProblemListService" in both code and config file together.
    public class ProblemListService : IProblemListService
    {
        ProblemListBAL objBAL = new ProblemListBAL();
        public ProblemSymptEditViewModel GetProblemSymtomsList(int ProbSymptID)
        {
            return objBAL.GetProblemSymtomsList(ProbSymptID);
        }

        public void UpdateProblemSymtomsList(int ProbSymptID, decimal Priority, decimal Severity, string Trend)
        {
             objBAL.UpdateProblemSymtomsList(ProbSymptID, Priority, Severity,Trend);
        }

        public List<ProblemSymptListViewModel> GetProblemSymtomsListByPatientID(int PatientId)
        {
            return objBAL.GetProblemSymtomsListByPatientID(PatientId);
        }

        public List<MisDiagnosisListViewModel> GetMiscDiagListByPatientID(int PatientId)
        {
            return objBAL.GetMiscDiagListByPatientID(PatientId);
        }


        public List<DiagnosisListViewModel> GetProblemSDiagnosisListByPatientID(int PatientId,int AptID)
        {
            return objBAL.GetProblemSDiagnosisListByPatientID(PatientId,AptID);
        }

        public void DeleteProblemListElements(int ElementListID, int ElementID)
        {
            objBAL.DeleteProblemListElements(ElementListID, ElementID);
        }

        public List<SymptomProblemListViewModel> GetSymptomList()
        {
            return objBAL.GetSymptomList();
        }

        public List<SymptomProblemListViewModel> GetDiagnosisList()
        {
            return objBAL.GetDiagnosisList();
        }

        public int InsertProblemDiagnosisElements(int ElementListID, MisDiagnosisListViewModel ViewModelInsert,int AptID)
        {
            return objBAL.InsertProblemDiagnosisElements(ElementListID, ViewModelInsert, AptID);
        }

        public int InsertProblemSymptoms(ProblemSymptInsertListViewModel ViewModelInsert)
        {
            return objBAL.InsertProblemSymptoms(ViewModelInsert);
        }

        public void UpdateProblemListStatus(int ElementListID, int ElementID, int Status)
        {
            objBAL.UpdateProblemListStatus(ElementListID, ElementID,Status);
        }

        public void UpdateProblemListAddress(int ElementListID, int ElementID)
        {
            objBAL.UpdateProblemListAddress(ElementListID, ElementID);
        }

        public DiagnosisListViewModel GetProblemDiagnosisList(int ProbDiagID)
        {
            return objBAL.GetProblemDiagnosisList(ProbDiagID);
        }

        public void UpdateProblemDiagList(int ProbDiagID, decimal Priority, decimal Severity)
        {
            objBAL.UpdateProblemDiagList(ProbDiagID,Priority,Severity);
        }

        public DiagnosisListViewModel GetProblemMiscDiagnosisList(int ProbDiagID)
        {
            return objBAL.GetProblemMiscDiagnosisList(ProbDiagID);
        }

        public void UpdateProblemMiscDiagList(int ProbDiagID, decimal Priority, decimal Severity)
        {
            objBAL.UpdateProblemMiscDiagList(ProbDiagID,Priority,Severity);
        }

        public void InsertProblemAppointment(int PatientID, int DiagnosisID, int AptID)
        {
            objBAL.InsertProblemAppointment(PatientID, DiagnosisID, AptID);
        }

        public void DeleteProblemAppointment(int PatientID, int DiagnosisID, int AptID)
        {
            objBAL.DeleteProblemAppointment(PatientID, DiagnosisID, AptID);
        }

        public List<DiagnosistblViewModel> GetDiagnosisPropemApt(int PatientID, int ApptID)
        {
            return objBAL.GetDiagnosisPropemApt(PatientID, ApptID);
        }
        
    }
}
