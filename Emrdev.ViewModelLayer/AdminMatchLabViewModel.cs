using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminMatchLabViewModel
    {
        public string Patient { get; set; }
        public string Followup { get; set; }
        public string AppointmentType { get; set; }
        public string ApptStart { get; set; }
        public string Range { get; set; }
        public int Followup_ID { get; set; }
        public int AppointmentID { get; set; }
    
    }
}
