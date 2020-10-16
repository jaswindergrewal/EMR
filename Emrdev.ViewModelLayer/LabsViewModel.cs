using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabsViewModel
    {
        public int ExternalLabListID { get; set; }
        public string ExternaLabName { get; set; }
        public Nullable<int> PanelID { get; set; }
    }

    public  class LabReportPanelViewModel
    {
        public int PanelID { get; set; }
        public string PanelName { get; set; }
    }
}
