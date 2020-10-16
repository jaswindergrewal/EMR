using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICriticalTaskService" in both code and config file together.
    [ServiceContract]
    public interface ICriticalTaskService
    {
        [OperationContract]
        List<CriticalTaskViewModel> GetCriticalTaskListByPatientID(int PatientId);

        [OperationContract]
        void UpdatePatientsCriticalTasks(int TaskId, bool cboRequest);

        [OperationContract]
        List<uploadtblViewModelCritical> GetUploadDocsByPatientID(int PatientId);

        [OperationContract]
        void UpdatePatientsCriticalTasksUploads(int TaskId, bool cboRequest, DateTime ? ReceivedDate,int ? UploadID);
    }
}
