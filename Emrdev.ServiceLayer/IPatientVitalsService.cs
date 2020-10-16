using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPatientVitalsService" in both code and config file together.
    [ServiceContract]
    public interface IPatientVitalsService
    {
        [OperationContract]
        PatientVitalsViewModel GetPatientVitalsByVitalId(int VitalID);

        [OperationContract]
        int UpdatePatientVitals(PatientVitalsViewModel theVital, int VitalID);

        [OperationContract]
        List<PatientVitalsViewModel> GetPatientVitalsByPatientId(int PatientID);

        [OperationContract]
        int InsertPatientVitalDetails(PatientVitalsViewModel vitalViewModel);

        [OperationContract]
        void DeleteVitalsByID(int ID);
    }
}
