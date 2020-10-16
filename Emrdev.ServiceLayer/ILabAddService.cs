using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILabAddService" in both code and config file together.
    [ServiceContract]
    public interface ILabAddService
    {
        [OperationContract]
        int InsertintoAptFollowup(apt_FollowUpsViewModel ViewModelfup, int PatientID);

        [OperationContract]
        void UpdateLabAdd(int LabID, DateTime RangeStart, DateTime RangeEnd, int StaffID,string Content);

        [OperationContract]
        apt_FollowUpsViewModel GetFollowupDetails(int FollowupId);

        [OperationContract]
        int InsertAptFollowup(apt_FollowUpsViewModel ViewModelfup);
    }

    
}
