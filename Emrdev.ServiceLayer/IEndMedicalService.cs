using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEndMedical" in both code and config file together.
    [ServiceContract]
    public interface IEndMedicalService
    {
        [OperationContract]
        List<EndMedicalViewModel> GetEndMedicalDetails(string ClinicName);

        [OperationContract]
        void InsertEndMedical(PatientViewModel clsContact);

        [OperationContract]
        void DoWork();

        [OperationContract]
        void CloseAppointments(int ID, int PatientID, string Text, int StaffID, int AptID, bool CboAutoShip, DateTime DateEntered, string ReasonToClose);
       
    }
}
