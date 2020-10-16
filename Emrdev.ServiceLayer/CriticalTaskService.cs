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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CriticalTaskService" in both code and config file together.
    public class CriticalTaskService : ICriticalTaskService
    {
        CriticalTaskBAL objBAL = new CriticalTaskBAL();
        public List<CriticalTaskViewModel> GetCriticalTaskListByPatientID(int PatientId)
        {
            return objBAL.GetCriticalTaskListByPatientID(PatientId);
        }

        public void UpdatePatientsCriticalTasks(int TaskId, bool cboRequest)
        {
            objBAL.UpdatePatientsCriticalTasks(TaskId, cboRequest);
        }
        public List<uploadtblViewModelCritical> GetUploadDocsByPatientID(int PatientId)
        {
            return objBAL.GetUploadDocsByPatientID(PatientId);
        }

         public void UpdatePatientsCriticalTasksUploads(int TaskId, bool cboRequest,DateTime ? ReceivedDate,int ? UploadID)
        {
            objBAL.UpdatePatientsCriticalTasksUploads(TaskId, cboRequest, ReceivedDate,UploadID);
        }
        
    }
}
