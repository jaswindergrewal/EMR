using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppointmentsService" in both code and config file together.
    [ServiceContract]
    public interface IAppointmentsService
    {
        [OperationContract]
        List<AppointmentViewModel> GetAppointsbyPatientID(int PatientId);

        [OperationContract]
        void UpdateAppointmentOccured(int AppointmentId);

        [OperationContract]
        List<OpenAppointmentViewModel> GetAppointmentsByClinic(string Clinic,int PageIndex,int PageSize);

        [OperationContract]
        List<OpenAppointmentViewModel> GetAppointmentsGap(string SearchType, int PageIndex, int PageSize);

        [OperationContract]
        List<PendingFollowupClinicViewModel> GetPendingFollowupsByClinic(string Clinic, string OrderBy, int PageIndex, int PageSize);

        [OperationContract]
        List<PrescriptionFor5Days> GetLastFiveDayPrescription(int PageIndex, int PageSize);

        [OperationContract]
        List<SpecialAptViewModel> GetSpecialAppointment(int PageIndex, int PageSize);
        
    }
}
