using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class RecurringAppointmentBAL
    {
        RecurringAppointmentDAL objRecurringAppointmentDAL = new RecurringAppointmentDAL();

        public List<RecurringAppointmentViewModel> PreviewRecurringAppointment(int ProviderID, int ApTID, DateTime ApptStart, DateTime ApptEND, string ApptStartH,
             string ApptEndH, string ApptStartM, string ApptEndM)
        {
            List<RecurringAppointmentViewModel> objLst = objRecurringAppointmentDAL.PreviewRecurringAppointment(ProviderID, ApTID, ApptStart, ApptEND, ApptStartH, ApptEndH, ApptStartM, ApptEndM);
            return objLst;
        }

        public void AddUpdateRecurringAppointment(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID)
        {
            objRecurringAppointmentDAL.AddUpdateRecurringAppointment(AptType, PatientID, MessageBody, EmployeeID, Apt_ID);
        }

        public Apt_Rec_ViewModel GetApt_Rec(int apt_id)
        {
            var _objpatientList = new Apt_Rec_ViewModel();
            var PatientEntity = new apt_rec();
            PatientEntity = objRecurringAppointmentDAL.Get<apt_rec>(o => o.apt_id == apt_id);

            Mapper.CreateMap<apt_rec, Apt_Rec_ViewModel>();
            _objpatientList = Mapper.Map(PatientEntity, _objpatientList);
            return _objpatientList;
        }
    }
}
