using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPendingFollowupService" in both code and config file together.
    [ServiceContract]
    public interface IPendingFollowupService
    {
        [OperationContract]
        PendingFollowupViewModel GetPendingFollowUpDetail(int FollowUp_ID, int PatientID);

        [OperationContract]
        List<ContactTypeViewModel> GetContactTypeList();

        [OperationContract]
        List<ContactListViewModel> GetContactList(int FollowUpID);

        [OperationContract]
        List<PendingConsultRequestViewModel> GetPendingFollowups();

        [OperationContract]
        void CloseFollowup(int FollowUpID);
       
    }
}
