using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class LabLaunchViewModel
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<NavigationProp> JoinCollection{ get; set; }

    }

    //1:M
    public class NavigationProp
    {
        public int MessageID { get; set; }
        public string ObservationDateTime { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastChanged { get; set; }
        public string PlacerOrderNumber { get; set; }
    }
}
