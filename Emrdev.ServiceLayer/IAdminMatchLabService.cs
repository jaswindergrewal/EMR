using System.Collections.Generic;
using System.ServiceModel;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminMatchLabService" in both code and config file together.
    [ServiceContract]
    public interface IAdminMatchLabService
    {
        [OperationContract]
        List<AdminMatchLabViewModel> GetMatchLabsList( string year);

        void EditMactLab(int FollowupID, int AppointmentID);
    }
}
