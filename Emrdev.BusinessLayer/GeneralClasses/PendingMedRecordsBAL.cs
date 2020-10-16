using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class PendingMedRecordsBAL
    {
        PendingMedRecordsDAL objDAL = new PendingMedRecordsDAL();
        public List<PendingMedRecordsViewModel> GetPendingMedRegords(string ClinicData)
        {
            return objDAL.GetPendingMedRegords(ClinicData);

        }
    
    }
}
