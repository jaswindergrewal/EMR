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
    
    public partial class Orders_GetOrderbystatusAutoship_Result
    {
        public int OrderID { get; set; }
        public string ShipName { get; set; }
        public System.DateTime DatePrep { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public int PatientID { get; set; }
        public string Note { get; set; }
        public System.DateTime ShipDate { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string AutoshipNote { get; set; }
        public string Status { get; set; }
        public bool Invoiced { get; set; }
        public bool Paid { get; set; }
        public Nullable<System.Guid> XeropatientId { get; set; }
        public Nullable<bool> CallBeforeShip { get; set; }
        public string HotNotes { get; set; }
    }
}
