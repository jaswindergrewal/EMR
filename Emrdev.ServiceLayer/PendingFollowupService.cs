using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PendingFollowupService" in both code and config file together.
    public class PendingFollowupService : IPendingFollowupService
    {
        PendingFollowupBAL objBAL = new PendingFollowupBAL();
        public PendingFollowupViewModel GetPendingFollowUpDetail(int FollowUp_ID, int PatientID)
        {
            return objBAL.GetPendingFollowUpDetail(FollowUp_ID, PatientID);
        }

        public List<ContactTypeViewModel> GetContactTypeList()
        {
            return objBAL.GetContactTypeList();
        }

        public List<ContactListViewModel> GetContactList(int FollowUpID)
        {
            return objBAL.GetContactList(FollowUpID);
        }


        public List<PendingConsultRequestViewModel> GetPendingFollowups()
        {
            return objBAL.GetPendingFollowups();
        }

        public void CloseFollowup(int FollowUpID)
        {
            objBAL.CloseFollowup(FollowUpID);
        }
    }
}
