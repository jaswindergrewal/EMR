using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class PatientInfoService : IPatientInfoService
    {
        #region Global

        Emrdev.GeneralClasses.PatientInfoBAL objBAL;

        #endregion

        public ViewModelLayer.PatientViewModel GetPatientInfoById(int patientId)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            return objBAL.GetPatientInfoById(patientId);
        }


        public string GetQBCustomerFullNameByPatientId(int patientId)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            return objBAL.GetQBCustomerFullNameByPatientID(patientId);
        }


        public List<Tuple<string, decimal?, DateTime?, string, string>> GetInvoiceDetailByPatientId(int patientId, string[] typeIDs)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            return objBAL.GetInvoiceDetailByPatientId(patientId, typeIDs);
        }


        public void UpdatePatientInformation(ViewModelLayer.PatientInfoViewModel objModel)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            objBAL.UpdatePatientInformation(objModel);
        }


        public DateTime? GetInvoiceDateByDateOrder(int patientId)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            return objBAL.GetInvoiceDateByDateOrder(patientId);
        }


        public DateTime? GetInvoiceDateByDateOrder(int patientId, string[] typeIDs)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            return objBAL.GetInvoiceDateByDateOrder(patientId, typeIDs);
        }

        public int ProfileItemCount(int patientId)
        {
            objBAL = new GeneralClasses.PatientInfoBAL();
            return objBAL.ProfileItemCount(patientId);
        }
    }
}
