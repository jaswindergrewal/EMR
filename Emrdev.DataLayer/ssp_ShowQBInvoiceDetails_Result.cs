//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emrdev.DataLayer
{
    using System;
    
    public partial class ssp_ShowQBInvoiceDetails_Result
    {
        public int QBInvoiceID { get; set; }
        public string TxnID { get; set; }
        public string CustomerRefListID { get; set; }
        public string CustomerRefFullName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Num { get; set; }
        public string PONumber { get; set; }
        public string Terms { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string OpenBalance { get; set; }
        public string IsPaid { get; set; }
        public string InvoiceLineDesc { get; set; }
        public string InvoiceLineItemRefFullName { get; set; }
        public string SalesRepRefListID { get; set; }
        public string SalesRepRefFullName { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> InvoiceLineQuantity { get; set; }
        public Nullable<decimal> InvoiceLineRate { get; set; }
        public Nullable<decimal> InvoiceLineAmount { get; set; }
        public Nullable<System.DateTime> InvoiceLineServiceDate { get; set; }
        public string InvoiceLineItemRefListID { get; set; }
    }
}
