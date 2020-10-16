using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminSuppListViewModel
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
        
    }
}
