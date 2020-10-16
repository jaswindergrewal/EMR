using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISpecialAttentionService" in both code and config file together.
    [ServiceContract]
    public interface ISpecialAttentionService
    {
        [OperationContract]
        List<SpecialAttentionViewModel >GetSpecialAttentionByPatientId(int PatientID);

        [OperationContract]
        void DeleteSpecialAttentionFlag(int SpecialAttentionID);

        [OperationContract]
        void AddSpecialAttentionByPatientId(int PatientID,string Content, int StaffId);

        [OperationContract]
        long GetSpecialAttentionCount(int PatientID);
    }
}
