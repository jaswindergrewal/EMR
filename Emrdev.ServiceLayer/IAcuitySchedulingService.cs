using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAcuitySchedulingService
    {

        [OperationContract]
        void SaveAcuityAppointment(List<AcuityAppointment> acuityStream,int staffId, string APIKey, string mailChimpCampaignId);
        List<SharePointPatientViewModel> ListSharePointPatients();
        SharePointPatientViewModel GetSharePointPatientsById(int Id);

        void SaveUpdateSharePointPatients(SharePointPatientViewModel PatientDetails);
        void DeleteSharePointPatient(int PatientId);

    }



}
