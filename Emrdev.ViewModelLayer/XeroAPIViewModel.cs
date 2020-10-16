using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class QbMatch
    {
        public int PatientID { get; set; }
        public int QBid { get; set; }

    }

    public class XeroOrders
    {

        public int orderid { get; set; }
        public string Note { get; set; }
        public System.DateTime DatePrep { get; set; }
        public int PatientID { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public System.DateTime ShipDate { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public Nullable<System.Guid> QBInvID { get; set; }
        public System.Guid QBid { get; set; }
        public System.Guid XeropatientId { get; set; }
        public Nullable<decimal> ShippingFee { get; set; }
        public Nullable<decimal> OrderLimit { get; set; }
        public int SalesAccountCode { get; set; }
    }
    public class XeroInvoiceItems
    {
        public int OrderItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int DiscountID { get; set; }
        public string DiscountName { get; set; }
        public Nullable<decimal> Dollar { get; set; }
        public Nullable<decimal> Percent { get; set; }
        public string Sku { get; set; }
    }

    public class GetPatientDetailsViewModel
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string FaxPone { get; set; }

    }

    public partial class XeroPatientViewModel
    {
        public System.Guid ContactId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public string PostalStreet { get; set; }
        public string PostalCity { get; set; }
        public string PostalState { get; set; }
        public string PostalZip { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
    }

    public partial class XeroCredentialViewModel
    {
        public int Id { get; set; }
        public string XeroConsumerKey { get; set; }
        public string XeroConsumerSecret { get; set; }
    }
    public partial class XeroNotMatchedContacts
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZip { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string FaxPone { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }
    public partial class XEROpatientsMatchedSearchModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZip { get; set; }
        public System.Guid ContactId { get; set; }
        public string MiddleInitial { get; set; }
       
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }
    public partial class ssp_XERO_MatchedPatientsViewModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> NoOfContactId { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class ssp_XERO_MatchedPatientsWithXeroPatients_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public System.Guid QBid { get; set; }
        public string Note { get; set; }
        public int IsRecUpdate { get; set; }
        public Nullable<System.Guid> ContactId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public string PostalStreet { get; set; }
        public string PostalCity { get; set; }
        public string PostalState { get; set; }
        public string PostalZip { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class ssp_XeroGetOrdersnotMatchxero_Viewmodel
    {

        public int OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public System.DateTime DatePrep { get; set; }
        public string FbInvId { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class ssp_XeroGetpatientsnotMatchxero_ViewModel
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string ClientId { get; set; }
        public int RecordCount { get; set; }
    }
    public partial class XeroOrdersViewModel
    {
        public int orderid { get; set; }
        public string Note { get; set; }
        public System.DateTime DatePrep { get; set; }
        public int PatientID { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public System.DateTime ShipDate { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public Nullable<int> FbInvID { get; set; }
        public Nullable<long> ClientId { get; set; }

    }

    public partial class XeroInvoiceItemsViewModel
    {
        public int OrderItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int DiscountID { get; set; }
        public string DiscountName { get; set; }
        public Nullable<decimal> Dollar { get; set; }
        public Nullable<decimal> Percent { get; set; }
        public int productId { get; set; }
        public Nullable<int> FbItemId { get; set; }

    }



    public partial class XeroClient_ViewModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }
        public string LastName { get; set; }
        public string PostalStreet { get; set; }
        public string PostalCity { get; set; }
        public string PostalState { get; set; }
        public string PostalCountry { get; set; }
        public string PostalZip { get; set; }
        public bool IsDeleted { get; set; }
        public long ClientId { get; set; }

        public string UserName { get; set; }
        public string SecStreet { get; set; }
        public string SecCity { get; set; }
        public string SecState { get; set; }
        public string SecCountry { get; set; }
        public string SecZip { get; set; }
    }

    public partial class Xero_Patients_Match_ViewModel
    {
        public int PatientID { get; set; }
        public long ClientId { get; set; }
        public string Note { get; set; }
        public int IsRecUpdate { get; set; }
    }

    public partial class ssp_Xero_MatchedPatients_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> NoOfContactId { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class ssp_Xero_NotMatchedPatients_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }


    public partial class ssp_Xero_MatchedPatientsWithXeroClientByPatientId_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public Nullable<long> ClientId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PostalStreet { get; set; }
        public string PostalCity { get; set; }
        public string PostalState { get; set; }
        public string PostalCountry { get; set; }
        public string PostalZip { get; set; }
        public string SecStreet { get; set; }
        public string SecCity { get; set; }
        public string SecState { get; set; }
        public string SecCountry { get; set; }
        public string SecZip { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public int PatientID { get; set; }
        public string Note { get; set; }
        public int IsRecUpdate { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class ssp_Xero_SearchPatientToMatchXeroClient_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class Xero_Item_Match_ViewModel
    {
        public int FbItemID { get; set; }
        public int ProductId { get; set; }
        public int StockInHand { get; set; }
    }

    public partial class TaxXero_ViewModel
    {
        public string NameTax { get; set; }
        public string TaxCode { get; set; }
        public double TaxP { get; set; }
    }
    //public partial class GetAllCreatedInvoice_ViewModel
    //{
    //    public int Id { get; set; }
    //    public double InvoiceId { get; set; }
    //}
    public partial class ssp_GetXeroCreatedInvoice_ViewModel
    {
        public float CreatedInvoiceId { get; set; }
        public float FailedInvoiceId { get; set; }
        public float InvoiceId { get; set; }
        public DateTime OrderDate { get; set; }

    }

    public partial class ssp_GetXeroCreatedClient_ViewModel
    {
        public float ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string EMAIL { get; set; }
    }

    public partial class ssp_XeroInsertViewModel
    {
        public int ID { get; set; }
        public Guid InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<DateTime> UpdatedDateUTC { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public bool Matched { get; set; }
    }

    public class Insert_CSV
    {
        public Nullable<Guid> InvoiceId { get; set; }
        public Nullable<Guid> CreditNoteID { get; set; }
        public string CSVType { get; set; }
        public Nullable<DateTime> CSVDate { get; set; }
        public int Num { get; set; }
        public string Memo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Item { get; set; }
        //public int QTY { get; set; }
        //public Nullable<decimal> SalePrice { get; set; }
        //public decimal Discount { get; set; }
        //public Nullable<decimal> Amount { get; set; }
        //public Nullable<decimal> Balance { get; set; }
        public bool XEROUpdated { get; set; }

    }
    public class Insert_CSVItem
    {
        public int Id { get; set; }
        public System.Guid Xero_CSV_ID { get; set; }
        public string Num { get; set; }
        public string Item { get; set; }
        public Nullable<int> QTY { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        //public Nullable<bool> XEROUpdated { get; set; }
        public Nullable<System.DateTime> XeroUpdated { get; set; }
    }

    public class XeroLocalPOViewModelLayer
    {
        public int ID { get; set; }
        public Nullable<System.Guid> InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.Guid> CreditNoteID { get; set; }
        public string CreditNoteNumber { get; set; }
        public string Status { get; set; }
        public string Item { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string AccountCode { get; set; }
        public string ItemCode { get; set; }
        public string Action { get; set; }
        public Nullable<decimal> SalesPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<bool> IsMatch { get; set; }
    }

    public class XeroInventoryviewmodel
    {
        public int ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Completed { get; set; }
        public string Action { get; set; }
    }

    public class XeroPOViewMLayer
    {
        public int ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ContactName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<bool> Completed { get; set; }
        public string Action { get; set; }
    }

    public class XeroPOIDViewModelLayer
    {
        public int ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ContactName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }

    public class XeroPOITEMViewModelLayer
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string AccountCode { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    }

    public class XeroPOViewModelLayer
    {
        public int ID { get; set; }
        public Nullable<System.Guid> PurchaseOrderID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ContactName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<bool> Completed { get; set; }
    }

    public partial class XeroPOItemViewModelLayer
    {
        public int ID { get; set; }
        public Nullable<System.Guid> XeroPurcahseOrderID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string AccountCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> ActualQuantity { get; set; }
        public Nullable<int> DifferenceQuantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<bool> Completed { get; set; }
    }
    public class reconcilinginventoryViewmodel
    {
        public int ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string itemcode { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> PurchaseQuantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Completed { get; set; }
        public string Action { get; set; }
        public Nullable<int> SalesQuantity { get; set; }
        public Nullable<int> QuantityInStock { get; set; }
    }

    public class XeroAccounts
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
    }
}

