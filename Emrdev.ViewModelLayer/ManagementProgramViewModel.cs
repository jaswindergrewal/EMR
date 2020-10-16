using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class ManagementProgramViewModel
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<System.DateTime> DateEdited { get; set; }
    }
}
