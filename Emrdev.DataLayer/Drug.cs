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
    
    public partial class Drug
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Viewable_yn { get; set; }
        public string Gender { get; set; }
        public string DrugType { get; set; }
        public string DrugCategory { get; set; }
        public Nullable<bool> Supplement_yn { get; set; }
        public Nullable<bool> Reviewed { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}
