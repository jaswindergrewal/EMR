using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminLabRemindersService" in both code and config file together.
    [ServiceContract]
    public interface IAdminLabRemindersService
    {
        [OperationContract]
       
        List<LabSymptomViewModel> GetLabDiagnosis(int DiagnosisID);

        [OperationContract]
        List<LabSymptomViewModel> GetLabSymptoms(int SymptomID);

        [OperationContract]
        List<SymptomViewModel> getAllSymptoms();

        [OperationContract]
        List<DiagnosistblViewModel> getAllDiagnosis();

        [OperationContract]
        void InsertDiagnosisLab(DiagnosisLabViewModel sysup);

        [OperationContract]
        void DeleteDiagnosisLab(int DiagnosisID);

        [OperationContract]
        void DeleteSymptomLabs(int SymptomID);

        [OperationContract]
        void InsertSymptomLab(SymptomLabViewModel sysup);
       
    }
}
