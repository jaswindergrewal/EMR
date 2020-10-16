using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RecurringApptService" in both code and config file together.
    public class RecurringApptService : IRecurringApptService
    {
        RecurringAppointmentBAL objRecurringAppointmentBAL = new RecurringAppointmentBAL();
        public void DoWork()
        {
        }

        public List<ViewModelLayer.RecurringAppointmentViewModel> PreviewRecurringAppointment(int ProviderID, int ApTID, DateTime ApptStart, DateTime ApptEND, string ApptStartH, string ApptEndH, string ApptStartM, string ApptEndM)
        {
            List<RecurringAppointmentViewModel> objLst = objRecurringAppointmentBAL.PreviewRecurringAppointment(ProviderID, ApTID, ApptStart, ApptEND, ApptStartH, ApptEndH, ApptStartM, ApptEndM);
            return objLst;
        }


        public void AddUpdateRecurringAppointment(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID)
        {
            objRecurringAppointmentBAL.AddUpdateRecurringAppointment(AptType, PatientID, MessageBody, EmployeeID, Apt_ID);
        }


        public Apt_Rec_ViewModel GetApt_Rec(int apt_id)
        {
            Apt_Rec_ViewModel objLst = objRecurringAppointmentBAL.GetApt_Rec(apt_id);
            return objLst;
        }
    }
}
