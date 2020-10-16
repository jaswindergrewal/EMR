using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class OldVisitViewModel
    {
        public int VisitID { get; set; }
        public string TypeofVisit { get; set; }
        public string Subjective { get; set; }
        public Nullable<System.DateTime> VisitDate { get; set; }
       
    }

    public class CallbacksoldViewModel
    {
        public int CallID { get; set; }
        public string ReasonForCallBack { get; set; }
        public Nullable<System.DateTime> CallDate { get; set; }
        public string VisitDate { get; set; }
       
    }
}
