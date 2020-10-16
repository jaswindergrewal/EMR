using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public partial class OneTimeSaleViewModel
    {
        public int OneTimeSaleID { get; set; }
        public int ProductID { get; set; }
        public int DiscountID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Discount { get; set; }
        public bool Affiliate { get; set; }
    }
}
