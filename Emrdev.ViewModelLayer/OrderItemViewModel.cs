using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class OrderItemViewModel
    {
        public int OrderItemID { get; set; }
        public int OrderId { get; set; }
        public int ProfileItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public int DiscountID { get; set; }
    }
}
