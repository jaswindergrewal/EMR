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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminLabRemindersService" in both code and config file together.
    public class AdminLabRemindersService : IAdminLabRemindersService
    {
        AdminLabRemindersBAL objBAL = new AdminLabRemindersBAL();
        public List<LabSymptomViewModel> GetLabDiagnosis(int DiagnosisID)
        {
            return objBAL.GetLabDiagnosis(DiagnosisID);
        }

        public List<LabSymptomViewModel> GetLabSymptoms(int SymptomID)
        {
            return objBAL.GetLabSymptoms(SymptomID);
        }

        public List<SymptomViewModel> getAllSymptoms()
        {
            return objBAL.getAllSymptoms();
        }
        public List<DiagnosistblViewModel> getAllDiagnosis()
        {
            return objBAL.getAllDiagnosis();
        }

        public void InsertDiagnosisLab(DiagnosisLabViewModel sysup)
        {
            objBAL.InsertDiagnosisLab(sysup);
        }

        public void DeleteDiagnosisLab(int DiagnosisID)
        {
            objBAL.DeleteDiagnosisLab(DiagnosisID);
        }

        public void DeleteSymptomLabs(int SymptomID)
        {
            objBAL.DeleteSymptomLabs(SymptomID);
        }

        public void InsertSymptomLab(SymptomLabViewModel sysup)
        {
            objBAL.InsertSymptomLab(sysup);
        }
    }
}
