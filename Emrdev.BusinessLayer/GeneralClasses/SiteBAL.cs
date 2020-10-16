using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class SiteBAL
    {
        SiteDAL obj = new SiteDAL();
        public long Count(int StaffID)
        {
            long ApptFollowUpCount;
            ApptFollowUpCount = obj.Count<apt_FollowUps>(o => o.Assigned == StaffID
                                                         && o.FollowUp_Completed_YN == false
                                                         && o.DueDate <= DateTime.Now);
            return ApptFollowUpCount;
        }

        public dynamic GetAllPatients(string prefixText)
        {

            var objGetPatients = obj.GetAllPatients(prefixText);
            return objGetPatients;
        }

        
    }
}
