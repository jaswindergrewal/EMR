using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PendingMedRecordsService" in both code and config file together.
    public class PendingMedRecordsService : IPendingMedRecordsService
    {
        PendingMedRecordsBAL objBAL = new PendingMedRecordsBAL();
        public List<PendingMedRecordsViewModel> GetPendingMedRegords(string ClinicData)
        {
            return objBAL.GetPendingMedRegords(ClinicData);
        
        }
    }
}
