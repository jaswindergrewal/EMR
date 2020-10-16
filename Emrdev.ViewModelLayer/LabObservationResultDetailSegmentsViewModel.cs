using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabObservationResultDetailSegmentsViewModel
    {
        public int TableRowID { get; set; }
        public string SetID { get; set; }
        public string ResultID { get; set; }
        public string Value { get; set; }
        public string ReferenceRange { get; set; }
        public string Units { get; set; }
        public string AbnormalFlag { get; set; }
        public string LabDetails { get; set; }
    }
}
