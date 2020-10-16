using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    public interface IPatientInfoService
    {
        [OperationContract]
        Emrdev.ViewModelLayer.PatientViewModel GetPatientInfoById(int patientId);

        [OperationContract]
        string GetQBCustomerFullNameByPatientId(int patientId);

        [OperationContract]
        List<Tuple<string, decimal?, DateTime?, string, string>> GetInvoiceDetailByPatientId(int patientId, string[] typeIDs);

        [OperationContract]
        void UpdatePatientInformation(PatientInfoViewModel objModel);

        [OperationContract]
        DateTime? GetInvoiceDateByDateOrder(int patientId);

        [OperationContract]
        DateTime? GetInvoiceDateByDateOrder(int patientId, string[] typeIDs);

        [OperationContract]
        int ProfileItemCount(int patientId);
    }
}
