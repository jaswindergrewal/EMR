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
    
    public partial class ssp_XEROGetOrderItemsbyOrderID_Result
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
}
