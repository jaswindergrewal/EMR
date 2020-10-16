using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IXeroAPIService" in both code and config file together.
    [ServiceContract]
    public interface IXeroAPIService
    {

        [OperationContract]
        XeroCredentialViewModel GetXeroCredential();

        [OperationContract]
        string EditXeroCredential(XeroCredentialViewModel XeroCredentialVM);

        [OperationContract]
        void InsertXeroMatch(QB_MatchViewModel QbMatch);

        [OperationContract]
        List<XeroNotMatchedContacts> GetXeroPatientsNotMathed(int page, int rows, string sord, string sidx);

        [OperationContract]
        List<XEROpatientsMatchedSearchModel> GetXeroPatientsMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName);

         [OperationContract]
        DateTime? GetLastContactFatchedDate();

         [OperationContract]
         void EditLastContactFatchedDate();

         [OperationContract]
         void InsertXeroNotMatch(XeroPatientViewModel QbMatch);

         [OperationContract]
         string MatchAppPatientsWithXeroContacts(string PatientId, string ContactId);

        [OperationContract]
         List<XeroOrders> GetXeroInvoiceByID(string OrderIDs);

        [OperationContract]
        List<XeroInvoiceItems> GetXeroInvoiceItemsByID(int OrderID);

        [OperationContract]
        void UpdateInvoiceXeroId(int InvoiceNumber, Guid InvoiceId);

        [OperationContract]
        void  InsertXeroAccounts(string Code,string Name);

        [OperationContract]
        List<XeroAccounts> GetXeroAccounts();

        [OperationContract]
        string UpdatePayment(int OrderId, string AccountCode, decimal Amount, DateTime PaymentDate,string PaymentReference);

        [OperationContract]
        void UpdateOrderPaid(int OrderId);

        [OperationContract]
        void UpdateFreeShippingStatus(int OrderId);
        

    }
}
