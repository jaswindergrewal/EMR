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
    using System.Collections.Generic;
    
    public partial class Exception
    {
        public int ExceptionID { get; set; }
        public int PatientID { get; set; }
        public int ProfileItemID { get; set; }
        public int Quantity { get; set; }
        public int Frequency { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateEnd { get; set; }
        public Nullable<int> DayToShip { get; set; }
        public int DiscountID { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> NextShipDate { get; set; }
        public Nullable<System.DateTime> OriginalShipDate { get; set; }
    
        public virtual ProfileItem ProfileItem { get; set; }
    }
}
