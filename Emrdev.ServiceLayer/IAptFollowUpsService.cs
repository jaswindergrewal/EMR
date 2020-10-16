using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAptFollowUpsService" in both code and config file together.
    [ServiceContract]
    public interface IAptFollowUpsService
    {
        [OperationContract]
        void InsertAptFollowUps(AptFollowUpsViewModel objAptFollowUpsViewModel);
    }
}
