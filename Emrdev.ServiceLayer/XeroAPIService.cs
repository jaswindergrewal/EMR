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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "XeroAPIService" in both code and config file together.
    public class XeroAPIService : IXeroAPIService
    {
        XeroAPIBAL _objBAL = new XeroAPIBAL();



        public XeroCredentialViewModel GetXeroCredential()
        {
            return _objBAL.GetXeroCerdential();
        }

        public string EditXeroCredential(XeroCredentialViewModel XeroCredentialVM)
        {
            return _objBAL.EditXeroCredential(XeroCredentialVM);
        }

        public void InsertXeroMatch(QB_MatchViewModel QbMatch)
        {
            _objBAL.InsertXeroMatch(QbMatch);
        }


        public List<XeroNotMatchedContacts> GetXeroPatientsNotMathed(int page, int rows, string sord, string sidx)
        {
            return _objBAL.GetXeroPatientsNotMathed(page, rows, sord, sidx);
        }

        public List<XEROpatientsMatchedSearchModel> GetXeroPatientsMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName)
        {
            return _objBAL.GetXeroPatientsMathedSearch(page, rows, sord, sidx, FirstName, LastName);
        }

        public DateTime? GetLastContactFatchedDate()
        {
            return _objBAL.GetLastContactFatchedDate();
        }

        public void EditLastContactFatchedDate()
        {
            _objBAL.EditLastContactFatchedDate();
        }

        public void InsertXeroNotMatch(XeroPatientViewModel QbMatch)
        {
            _objBAL.InsertXeroNotMatch(QbMatch);
        }

        public string MatchAppPatientsWithXeroContacts(string PatientId, string ContactId)
        {
            return _objBAL.MatchAppPatientsWithXeroContacts(PatientId, ContactId);
        }

        public List<XeroOrders> GetXeroInvoiceByID(string OrderIDs)
        {
            return _objBAL.GetXeroInvoiceByID(OrderIDs);
        }

        public List<XeroInvoiceItems> GetXeroInvoiceItemsByID(int OrderID)
        {
            return _objBAL.GetXeroInvoiceItemsByID(OrderID);
        }

        public void UpdateInvoiceXeroId(int InvoiceNumber, Guid InvoiceId)
        {
             _objBAL.UpdateInvoiceXeroId(InvoiceNumber, InvoiceId);
        }

        public void  InsertXeroAccounts(string Code,string Name)
        {
            _objBAL.InsertXeroAccounts(Code, Name);
        }

        public List<XeroAccounts> GetXeroAccounts()
        {
            return _objBAL.GetXeroAccounts();
        }

        public string UpdatePayment(int OrderId, string AccountCode, decimal Amount, DateTime PaymentDate,string PaymentReference)
        { //return _objBAL.UpdatePayment(OrderId, AccountCode, Amount, PaymentDate, PaymentReference); 
            return "";
        }

        public void  UpdateOrderPaid(int OrderId)
        {

             _objBAL.UpdateOrderPaid(OrderId); 
        }

        public void UpdateFreeShippingStatus(int OrderId)
        {

            _objBAL.UpdateFreeShippingStatus(OrderId);
        }

    }
}
