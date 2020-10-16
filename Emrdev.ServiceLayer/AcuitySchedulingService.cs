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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class AcuitySchedulingService : IAcuitySchedulingService
    {
        AcuitySchedulingBAL _ManageBAL = new AcuitySchedulingBAL();

        public void SaveAcuityAppointment(List<AcuityAppointment> acuityStream, int staffId, string APIKey, string mailChimpCampaignId)
        {
            _ManageBAL.SaveAcuityAppointment(acuityStream, staffId, APIKey, mailChimpCampaignId);
        }

        public List<SharePointPatientViewModel> ListSharePointPatients()
        {
            return _ManageBAL.ListSharePointPatients();
        }

        public SharePointPatientViewModel GetSharePointPatientsById(int Id)
        {
            return _ManageBAL.GetSharePointPatientsById(Id);
        }
        public void SaveUpdateSharePointPatients(SharePointPatientViewModel PatientDetails)
        {
            _ManageBAL.SaveUpdateSharePointPatients(PatientDetails);
        }
        public  void DeleteSharePointPatient(int PatientId)
        {
            _ManageBAL.DeleteSharePointPatient(PatientId);
        }
    }
}


