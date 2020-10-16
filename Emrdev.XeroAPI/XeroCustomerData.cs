
using System.Collections.Generic;
namespace Emrdev.XeroAPI
{
    public class XeroCustomerData
    {
        public string ConsumerKey;
        public string ConsumerSecret;

        public void AddCustomers(Addcustomer _Addcustomer)
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.CreatingAndUpdatingContacts(_Addcustomer);

        }

        public void AddInvoices(AddInvoice _AddInvoice)
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.CreatingInvoiceWithValidationErrors(_AddInvoice);
        }

        public void AddPayment(AddInvoice _AddInvoice, string Amount, string PaymentDate, string AccountCode, string PaymentReference)
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.CreatingInvoicAddPayment(_AddInvoice, Amount, PaymentDate, AccountCode, PaymentReference);
        }
        public ConsumerSessionData CreateRepository()
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            return __objXeroApiGateway.CreateRepository();
        }

        public void AddTaxes(AddTax _AddTax)
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.CreatingTaxWithValidationErrors(_AddTax);
        }

        public void SaveTaxData(List<AddTax> _csvdata)
        {
            XeroApiGateway _objXeroApiGateway = new XeroApiGateway();
            _objXeroApiGateway.CreatingTaxListWithValidationErrors(_csvdata);
        }
      
        public void GetOrg()
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.GetOrg();
        }
        public void GetUser()
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.GetUser();
        }

        public void GetAccounts()
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.GetAccounts();
        }

       
        public void SaveCSVData(List<CSVInvoice> _csvdata)
        {
            XeroApiGateway _objXeroApiGateway = new XeroApiGateway();
            _objXeroApiGateway.CreatingCSVInvoiceWithValidationErrorsList(_csvdata);
        }

        public void GetContacts()
        {
            XeroApiGateway __objXeroApiGateway = new XeroApiGateway();
            __objXeroApiGateway.GetContacts();
        }

    }

}
