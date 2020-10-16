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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AppointmentsService" in both code and config file together.
    public class AppointmentsService : IAppointmentsService
    {
        AppointmentsBAL objBAL = new AppointmentsBAL();
       public List<AppointmentViewModel> GetAppointsbyPatientID(int PatientId)
       {

           return objBAL.GetAppointsbyPatientID(PatientId);
        }

       public void UpdateAppointmentOccured(int AppointmentId)
       {
           objBAL.UpdateAppointmentOccured(AppointmentId);
       }

       public List<OpenAppointmentViewModel> GetAppointmentsByClinic(string Clinic, int PageIndex, int PageSize)
       {
           return objBAL.GetAppointmentsByClinic(Clinic, PageIndex, PageSize);
       }

       public List<OpenAppointmentViewModel> GetAppointmentsGap(string SearchType, int PageIndex, int PageSize)
       {
           return objBAL.GetAppointmentsGap(SearchType, PageIndex, PageSize);
       }

       public List<PendingFollowupClinicViewModel> GetPendingFollowupsByClinic(string Clinic,string OrderBy, int PageIndex,int PageSize)
       {
           return objBAL.GetPendingFollowupsByClinic(Clinic, OrderBy, PageIndex, PageSize);
       }

       public List<PrescriptionFor5Days> GetLastFiveDayPrescription(int PageIndex, int PageSize)
       {
           return objBAL.GetLastFiveDayPrescription(PageIndex,PageSize);
       }

       public List<SpecialAptViewModel> GetSpecialAppointment(int PageIndex, int PageSize)
       {
           return objBAL.GetSpecialAppointment(PageIndex, PageSize);
       }
    }
}
