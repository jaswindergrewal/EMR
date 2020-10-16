using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILabPatientsService" in both code and config file together.
    [ServiceContract]
    public interface ILabPatientsService
    {
        [OperationContract]
        List<LabPatientsViewModel> GetUnmatchedLabsPatientData();

        [OperationContract]
        LabPatientsViewModel LabQuestPatientMatchByLabID(int LabID);

        [OperationContract]
        int UpdaterLabPatientDetails(LabPatientsViewModel objLabPatientsViewModel);
       

        [OperationContract]
        void DoWork();
    }
}
