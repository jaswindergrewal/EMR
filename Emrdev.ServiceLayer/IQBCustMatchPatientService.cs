using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQBCustMatchPatientService" in both code and config file together.
    [ServiceContract]
    public interface IQBCustMatchPatientService
    {

        [OperationContract]
        List<QBCustMatchPatientViewModel> GetQBCustomerList();

        [OperationContract]
        void DeleteMatch(string QBid);

        [OperationContract]
        void InsertQBMatch(QB_MatchViewModel viewModelQB);

        [OperationContract]
        void UpdateQBMatch(PatientViewModel viewModelQB);

        [OperationContract]
        PatientViewModel GetPatientDetailById(int PatientId);

        [OperationContract]
        List<PatientQuickBookViewModel> GetPatientQuickBookList();

        [OperationContract]
        List<QBCustMatchPatientViewModel> GetQBMatchListByPatientId(int PatientId);

        [OperationContract]
        QBMatchEmrAddressViewModel GetQBMatchAddressByPatientId(int PatientId);

        [OperationContract]
        void InsertQBMatch(int PatientID, string QBCustomer);
        [OperationContract]
        void DoWork();

        

    }
}
