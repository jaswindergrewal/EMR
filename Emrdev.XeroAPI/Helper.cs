using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.XeroAPI
{
    //class Helper
    //{
    //}

    public class SessionData
    {
        public string VerificationCode { get; set; }
        public string oauth_token { get; set; }
        public string oauth_verifier { get; set; }
        public string org { get; set; }
    }
    public class ConsumerSessionData
    {
        public string AunthoticateUrl { get; set; }
        public object CunsumerSession { get; set; }
    }
    public class AddInvoice
    {
        public AddInvoice()
        {
            InitializecLass();
        }

        private void InitializecLass()
        {
            ItemList = new List<AddItem>();

        }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public Guid InvoiceID { get; set; }
        public decimal SubTotal { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalDiscount { get; set; }
        public List<AddItem> ItemList { get; set; }
        public Guid ContactID { get; set; }
        public Nullable<decimal> ShippingFee { get; set; }
        public Nullable<decimal> OrderLimit { get; set; }
        public int SalesAccountCode { get; set; }
        public bool Medical { get; set; }
        public string ProviderName { get; set; }
        public string ShippingAddress { get; set; }
    }
    public class AddItem
    {
        public string Description { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineAmount { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public string ItemCode { get; set; }
    }
    public class Addcustomer
    {
        public Addcustomer()
        {
            InitializecLass();
        }

        private void InitializecLass()
        {
            Address = new List<AddAddress>();

        }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AddAddress> Address { get; set; }
        public Guid ContactID { get; set; }
        public string ContactStatus { get; set; }
        public string ContactPerson { get; set; }
        public List<AddPhone> Phone { get; set; }
        public string ProviderName { get; set; }
    }
    public class AddAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string AddressType { get; set; }
    }

    public class AddPhone
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneCountryCode { get; set; }
    }

    public class AddTax
    {

        public string Name { get; set; }
        public string TaxType { get; set; }
        public List<AddTaxComponent> TaxComponent { get; set; }

    }

    public class AddTaxComponent
    {
        public string Name { get; set; }
        public decimal? Rate { get; set; }
        public bool IsCompound { get; set; }
    }

    //Declaring the class for csv Invoice
    public class CSVInvoice
    {
        public CSVInvoice()
        {
            Initializeclass();
        }
        private void Initializeclass()
        {
            ItemList = new List<AddCsvItem>();
        }
        public Guid InvoiceID { get; set; }
        public string Type { get; set; }  ////New Type
        public string NameContact { get; set; }
        public DateTime Date { get; set; } //DueDate
        //public int Num { get; set; }
        public string Num { get; set; }
        public string Memo { get; set; }
        public string Name { get; set; } //Contact     
        public string ItemType { get; set; } //New Type
        public List<AddCsvItem> ItemList { get; set; } //ItemList LineItem
        public double Amount { get; set; } // for paymet time use
        public Guid ContactID { get; set; }
    }
    //Add the Item for csvInvoice class
    public class AddCsvItem
    {
        public string Type { get; set; }
        public string NameContact { get; set; }
        // public int Num { get; set; }
        public string Num { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Item { get; set; }
        //public int Qty { get; set; }
        public double Qty { get; set; }
        public double SalesPrice { get; set; }
        public string ItemType { get; set; }
        public double Discount { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string Paid { get; set; }
    }
}
