using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class CriticalTaskBAL
    {
        CriticalTaskDAL objDAL = new CriticalTaskDAL();
        public List<CriticalTaskViewModel> GetCriticalTaskListByPatientID(int PatientId)
        {
            return objDAL.GetCriticalTaskListByPatientID(PatientId);
        }

        public void UpdatePatientsCriticalTasks(int TaskId,bool cboRequest)
        {

            Patients_CriticalTasks _objEdit = objDAL.Get<Patients_CriticalTasks>(o => o.TaskID == TaskId);
            if (cboRequest == true)
            {
                _objEdit.RequestedDate = DateTime.Now;
            }
            else
            {
                _objEdit.RequestedDate =null;
            }
            _objEdit.Requested = cboRequest;
             objDAL.Edit(_objEdit);
        }

        public List<uploadtblViewModelCritical> GetUploadDocsByPatientID(int PatientId)
        {
            return objDAL.GetUploadDocsByPatientID(PatientId);
        }

        public void UpdatePatientsCriticalTasksUploads(int TaskId, bool cboRequest,DateTime ? ReceivedDate ,int ? UploadID)
        {

            Patients_CriticalTasks _objEdit = objDAL.Get<Patients_CriticalTasks>(o => o.TaskID == TaskId);

            _objEdit.Received = cboRequest;
            _objEdit.ReceivedDate = ReceivedDate;
            _objEdit.UploadID = UploadID;   
           
            objDAL.Edit(_objEdit);
        }

    }
}
