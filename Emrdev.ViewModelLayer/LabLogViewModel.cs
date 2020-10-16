using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabLogViewModel
    {
        public int LabLogID { get; set; }
        public string LabName { get; set; }
        public System.DateTime DateImported { get; set; }
        public string PatientName { get; set; }
        public string Birthday { get; set; }
    }

    public class LabRequestTestViewModel
    {
        public int LabRequest_TestID { get; set; }
        public string TestName { get; set; }
        public bool Active_YN { get; set; }
    }

    public class LabRequestPanelViewModel
    {
        public int LabRequest_PanelID { get; set; }
        public string PanelName { get; set; }
        public Nullable<bool> Active_YN { get; set; }
    }
}
