using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabDrilldownRecordsViewModel
    {
        public int patientID { get; set; }
        public string ObservationValue { get; set; }
        public Nullable<DateTime> ObservationDateTime { get; set; }
        public string LabName { get; set; }
        public int OrderSegmentID { get; set; }
    }
}
