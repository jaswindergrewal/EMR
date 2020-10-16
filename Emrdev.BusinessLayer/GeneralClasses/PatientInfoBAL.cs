using Emrdev.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.GeneralClasses
{
    public class PatientInfoBAL
    {
        #region Global

        Emrdev.DataLayer.GeneralClasses.PatientInfoDAL objDAL;

        #endregion

        #region Get Patient Info By Id

        public Emrdev.ViewModelLayer.PatientViewModel GetPatientInfoById(int pateintId)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            return objDAL.GetPatientInfoById(pateintId);
        }

        #endregion

        #region Get QBCustomerFullName By PatientId

        public string GetQBCustomerFullNameByPatientID(int patientId)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            return objDAL.GetQBFullNameByPatientId(patientId);
        }

        #endregion

        #region Get Invoice Detail By PatientId

        public List<Tuple<string, decimal?, DateTime?, string, string>> GetInvoiceDetailByPatientId(int patientId, string[] typeIDs)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            return objDAL.GetInvoiceByPatientId(patientId, typeIDs);
        }

        #endregion


        #region Update Patient Information

        public void UpdatePatientInformation(Emrdev.ViewModelLayer.PatientInfoViewModel objModel)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            Patient objPatient=objDAL.Get<Patient>(i => i.PatientID == objModel.PatientId);
            objPatient.PatientID = objModel.PatientId;
            objPatient.NameAlert = objModel.NameAlert;
            objPatient.Birthday = objModel.BirthDay;
            objPatient.Inactive = objModel.InActive;
            objPatient.Allergies = objModel.Allergies;
            objPatient.Nickname = objModel.NickName;
            objPatient.InvoiceDue = objModel.InvoiceDue;
            objPatient.ExpirationDate = objModel.ExpirationDate;
            objPatient.InvoiceDueDate = objModel.InvoiceDueDate;
            objPatient.InvoicePaid = objModel.InvoicePaid;
            objDAL.Edit<Patient>(objPatient);
        }

        #endregion


        #region Get Invoice Date By Date Order

        public DateTime? GetInvoiceDateByDateOrder(int patientId)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            return objDAL.GetInvoiceDateByDateOrder(patientId);
        }


        public DateTime? GetInvoiceDateByDateOrder(int patientId, string[] typeIDs)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            return objDAL.GetInvoiceDateByDateOrder(patientId, typeIDs);
        }

        #endregion

        #region Count ProfileItems by Patient Id

        public int ProfileItemCount(int patientId)
        {
            objDAL = new DataLayer.GeneralClasses.PatientInfoDAL();
            return objDAL.ProfileItemCount(patientId);
        }

        #endregion




    }
}
