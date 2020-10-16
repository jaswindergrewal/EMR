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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LabAddService" in both code and config file together.
    public class LabAddService : ILabAddService
    {
        LabAddBAL objBAL = new LabAddBAL();
        public int InsertintoAptFollowup(apt_FollowUpsViewModel ViewModelfup, int PatientID)
        {
            return objBAL.InsertintoAptFollowup(ViewModelfup, PatientID);
        }

        public void UpdateLabAdd(int LabID, DateTime RangeStart, DateTime RangeEnd, int StaffID,string Content)
        {
            objBAL.UpdateLabAdd(LabID, RangeStart, RangeEnd, StaffID, Content);
        }

        public apt_FollowUpsViewModel GetFollowupDetails(int FollowupId)
        {
            return objBAL.GetFollowupDetails(FollowupId);
        }

        public int InsertAptFollowup(apt_FollowUpsViewModel ViewModelfup)
        {
            return objBAL.InsertAptFollowup(ViewModelfup);
        }
    }
}
