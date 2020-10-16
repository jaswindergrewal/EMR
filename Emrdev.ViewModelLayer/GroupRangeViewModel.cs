using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
   public partial class GroupRangeViewModel
    {
        public int GroupRangeID { get; set; }
        public int GroupID { get; set; }
        public double HighRange { get; set; }
        public double LowRange { get; set; }
    }
}
