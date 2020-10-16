using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILMCBaseService" in both code and config file together.
    [ServiceContract]
    public interface ILMCBaseService
    {
        [OperationContract]
        StaffViewModel Get(string UserName);
        [OperationContract]
        long Count(int StaffID);

    }
}
