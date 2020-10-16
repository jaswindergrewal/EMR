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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "QBCustMatchPatientService" in both code and config file together.
    public class QBCustMatchPatientService : IQBCustMatchPatientService
    {
        QBCustMatchPatientBAL objQBCustMatchPatientBAL = new QBCustMatchPatientBAL();
        public void DoWork()
        {
        }

        public List<QBCustMatchPatientViewModel> GetQBCustomerList()
        {
            List<QBCustMatchPatientViewModel> lstObj = objQBCustMatchPatientBAL.GetQBCustomerList();
            return lstObj;
        }


        public void DeleteMatch(string QBid)
        {
            objQBCustMatchPatientBAL.DeleteMatch(QBid);
        }


        public void InsertQBMatch(QB_MatchViewModel viewModelQB)
        {
            objQBCustMatchPatientBAL.InsertQBMatch(viewModelQB);
        }

        public void UpdateQBMatch(PatientViewModel viewModelQB)
        {
            objQBCustMatchPatientBAL.UpdateQBMatch(viewModelQB);
        }

        public PatientViewModel GetPatientDetailById(int PatientId)
        {
            PatientViewModel objLst = objQBCustMatchPatientBAL.GetPatientDetailById(PatientId);
            return objLst;
        }


        public List<PatientQuickBookViewModel> GetPatientQuickBookList()
        {
            List<PatientQuickBookViewModel> objLst = objQBCustMatchPatientBAL.GetPatientQuickBookList();
            return objLst;
        }

        public List<QBCustMatchPatientViewModel> GetQBMatchListByPatientId(int PatientId)
        {
            return objQBCustMatchPatientBAL.GetQBMatchListByPatientId(PatientId);

        }

        public QBMatchEmrAddressViewModel GetQBMatchAddressByPatientId(int PatientId)
        {
            return objQBCustMatchPatientBAL.GetQBMatchAddressByPatientId(PatientId);
        }

        public void InsertQBMatch(int PatientID, string QBCustomer)
        {
            objQBCustMatchPatientBAL.InsertQBMatch(PatientID, QBCustomer);
        }
    }
}
