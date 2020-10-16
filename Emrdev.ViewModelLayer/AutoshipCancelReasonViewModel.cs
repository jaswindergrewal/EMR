using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public partial class AutoshipCancelReasonViewModel
    {
        public int ReasonID { get; set; }
        public string ReasonName { get; set; }
        public bool Active { get; set; }
    }
}
