using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    
    public partial class AutoshipProductsForSyymptomViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Assigned { get; set; }
    }

    public partial class AutoshipDiscountViewModel
    {
        public int DiscountID { get; set; }
        public string DiscountName { get; set; }
        public Nullable<decimal> Dollar { get; set; }
        public Nullable<decimal> Percent { get; set; }
    }

    public partial class AutoshipProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal AutoshipPrice { get; set; }
        public bool Active { get; set; }
        public string QBId { get; set; }
        public int GroupID { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<bool> Viewable { get; set; }
        public bool Reviewed { get; set; }
        public string Weight { get; set; }
        public Nullable<decimal> Length { get; set; }
        public Nullable<decimal> Width { get; set; }
        public Nullable<decimal> Height { get; set; }
        public string Sku { get; set; }
        public Nullable<bool> Bundle { get; set; }
    }

    public class ItemS
    {
        public string sku { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double unitPrice { get; set; }
    }

    public class Container
    {
        public string orderNumber { get; set; }
        public DateTime orderDate { get; set; }
        public string orderKey { get; set; }
        public string orderStatus { get; set; }
        public billto billto { get; set; }
        public shipto shipto { get; set; }

        //  public item[] item { get; set; }
        public List<ItemS> items { get; set; }

    }
    public partial class billto
    {
        public string name { get; set; }
    }
    public partial class shipto
    {
        public string name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public bool residential { get; set; }
    }

    public partial class orders
    {
        public string orderNumber { get; set; }
    }
    public partial class Orders1
    {
        public orders[] orders { get; set; }
    }

    public partial class ShippingValues
    {
        public decimal ShippingFee { get; set; }
        public decimal OrderTotalLimit { get; set; }
    }
}
