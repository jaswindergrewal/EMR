using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAllergieService" in both code and config file together.
    [ServiceContract]
    public interface IAllergieService
    {
        [OperationContract]
        PatientViewModel GetPatientByID(int PatientId);

        [OperationContract]
        void UpdateAllergies(PatientViewModel pat);

        [OperationContract]
        List<AestheticNotesViewModel> GetAestheticNotes(int PatientID);

        [OperationContract]
        List<AnestheticFollowupViewModel> GetAestheticFollowups(int PatientID);

        [OperationContract]
        List<AestheticNotesViewModel> GetAestheticNotesALL(int PatientID);


    }
}
