using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AppointmentsBAL
    {
        AppointmentsDAL objDAL = new AppointmentsDAL();
        
        /// <summary>
        /// Get all the appoints that the patient have with different providers
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public List<AppointmentViewModel> GetAppointsbyPatientID(int PatientId)
        {
            return objDAL.GetAppointsbyPatientID(PatientId);
        }

        /// <summary>
        /// Set the appointment as accurred by changing the result to 3
        /// </summary>
        /// <param name="AppointmentId"></param>
        public void UpdateAppointmentOccured(int AppointmentId)
        {
            apt_rec _objEdit = objDAL.Get<apt_rec>(o => o.apt_id == AppointmentId);
            _objEdit.Results = 3;
            objDAL.Edit(_objEdit);
        }

        /// <summary>
        /// To get the open appointments by clinic name
        /// jaswinder 13th aug 2013
        /// </summary>
        /// <param name="Clinic"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<OpenAppointmentViewModel> GetAppointmentsByClinic(string Clinic, int PageIndex, int PageSize)
        {
            return objDAL.GetAppointmentsByClinic(Clinic, PageIndex, PageSize);
        }

        /// <summary>
        /// Get the appointment gaps that are 120 or 90 days old 
        /// 'A' greater than 120 and b=90
        /// Jaswinder 13th aug 2013
        /// </summary>
        /// <param name="Clinic"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<OpenAppointmentViewModel> GetAppointmentsGap(string SearchType, int PageIndex, int PageSize)
        {
            return objDAL.GetAppointmentsGap(SearchType, PageIndex, PageSize);
        }

        /// <summary>
        /// Get the list of pending appoints by clincic 
        /// Jaswinder 13th aug 2013
        /// </summary>
        /// <param name="Clinic"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<PendingFollowupClinicViewModel> GetPendingFollowupsByClinic(string Clinic, string OrderBy, int PageIndex,int  PageSize)
        {
            return objDAL.GetPendingFollowupsByClinic(Clinic, OrderBy, PageIndex, PageSize);
        }

        /// <summary>
        /// Get the list of last five days prescription provided to patients
        /// jaswinder 9th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<PrescriptionFor5Days> GetLastFiveDayPrescription(int PageIndex, int PageSize)
        {
            return objDAL.GetLastFiveDayPrescription(PageIndex, PageSize);
        }

        /// <summary>
        /// Get the special AppointmentList
        /// jaswinder 14th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<SpecialAptViewModel> GetSpecialAppointment(int PageIndex, int PageSize)
        {
            return objDAL.GetSpecialAppointment(PageIndex, PageSize);
        }


    }
}
