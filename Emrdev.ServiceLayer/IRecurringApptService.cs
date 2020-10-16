using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRecurringApptService" in both code and config file together.
    [ServiceContract]
    public interface IRecurringApptService
    {
        [OperationContract]
        List<RecurringAppointmentViewModel> PreviewRecurringAppointment(int ProviderID, int ApTID, DateTime ApptStart, DateTime ApptEND, string ApptStartH,
             string ApptEndH, string ApptStartM, string ApptEndM);

        [OperationContract]
        void AddUpdateRecurringAppointment(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID);

        [OperationContract]
        Apt_Rec_ViewModel GetApt_Rec(int apt_id);

        [OperationContract]
        void DoWork();
    }
}
