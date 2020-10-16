using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRenewalExceptionService" in both code and config file together.
    [ServiceContract]
    public interface IRenewalExceptionService
    {
        [OperationContract]
        PatientViewModel GEtPatientByID(int PatientID);

        [OperationContract]
        StaffViewModel GetStaffByStaffID(int StaffID);

        [OperationContract]
        void UpdatePatientRenewalException(PatientViewModel pat);
    }
}
