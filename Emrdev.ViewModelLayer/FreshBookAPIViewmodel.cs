using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    class FreshBookAPIViewmodel
    {
    }

    public partial class ssp_FreshBookGetOrdersnotMatchFB_Viewmodel
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

    public partial class ssp_FreshBookGetpatientsnotMatchFB_ViewModel
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string ClientId { get; set; }
        public int RecordCount { get; set; }
    }
    public partial class FreshBookOrdersViewModel
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

    public partial class FreshBookInvoiceItemsViewModel
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

    public partial class FreshBookPatientViewModel
    {
         public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingStreet { get; set; }
        public string BillingZip { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingZip { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string FaxPone { get; set; }
        public string Email { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyPhone { get; set; }
        public string emergencyContact { get; set; }
        public Nullable<long> ClientId { get; set; }
}

    public partial class FreshBookClient_ViewModel
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

    public partial class FreshBook_Patients_Match_ViewModel
    {
        public int PatientID { get; set; }
        public long ClientId { get; set; }
        public string Note { get; set; }
        public int IsRecUpdate { get; set; }
    }

    public partial class ssp_FreshBook_MatchedPatients_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> NoOfContactId { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class ssp_FreshBook_NotMatchedPatients_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public long ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }


    public partial class ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel
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

    public partial class ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel
    {
        public Nullable<long> Row { get; set; }
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> RecordCount { get; set; }
    }

    public partial class FreshBook_Item_Match_ViewModel
    {
        public int FbItemID { get; set; }
        public int ProductId { get; set; }
        public int StockInHand { get; set; }
    }

    public partial class TaxFreshbook_ViewModel
    {
        public string NameTax { get; set; }
        public string TaxCode { get; set; }
        public double TaxP { get; set; }
    }
    public partial class GetAllCreatedInvoice_ViewModel
    {
        public int Id { get; set; }
        public double InvoiceId { get; set; }
    }
    public partial class ssp_GetFBCreatedInvoice_ViewModel
    {
        public float CreatedInvoiceId { get; set; }
        public float FailedInvoiceId { get; set; }
        public float InvoiceId { get; set; }
    }
}
