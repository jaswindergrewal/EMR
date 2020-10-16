using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.BusinessLayer.GeneralClasses
{
   public class QBCustMatchPatientBAL
    {
       QBCustMatchPatientDAL objQBCustMatchPatientDAL = new QBCustMatchPatientDAL();
       public List<QBCustMatchPatientViewModel> GetQBCustomerList()
       {
           List<QBCustMatchPatientViewModel> objLst = objQBCustMatchPatientDAL.GetQBCustomerList();
           return objLst;
       }

       public void InsertQBMatch(QB_MatchViewModel viewModelQB)
       {
           QB_Match cls = new QB_Match();
           cls = objQBCustMatchPatientDAL.Get<QB_Match>(o => o.PatientID == viewModelQB.PatientID && o.QBid == viewModelQB.QBid);
           if (cls != null)
           {
               cls = new QB_Match();
               AutoMapper.Mapper.CreateMap<QB_MatchViewModel, QB_Match>();
               cls = AutoMapper.Mapper.Map(viewModelQB, cls);
               objQBCustMatchPatientDAL.Create(cls);
           }
       }

       public void UpdateQBMatch(PatientViewModel viewModelQB)
       {
           Patient cls = new Patient();
           AutoMapper.Mapper.CreateMap<PatientViewModel, Patient>();
           cls = AutoMapper.Mapper.Map(viewModelQB, cls);
           objQBCustMatchPatientDAL.Edit(cls);
       }

       public void DeleteMatch(string QBid)
       {
           objQBCustMatchPatientDAL.DeleteMatch(QBid);
       }

       public PatientViewModel GetPatientDetailById(int PatientId)
       {
           PatientViewModel objLst = objQBCustMatchPatientDAL.GetPatientDetailById(PatientId);
           return objLst;
       }

       public List<PatientQuickBookViewModel> GetPatientQuickBookList()
       {
           List<PatientQuickBookViewModel> lstObj = objQBCustMatchPatientDAL.GetPatientQuickBookList();
           return lstObj;
       }

       #region QB_Match page Methods

       /// <summary>
       /// Method to QBMatch by PatientId For QB_Match page
       /// </summary>
       /// <param name="PatientId"></param>
                  
       public List<QBCustMatchPatientViewModel> GetQBMatchListByPatientId(int PatientId)
       {
           return objQBCustMatchPatientDAL.GetQBMatchListByPatientId(PatientId);
       }

       /// <summary>
       /// Method to QBMatch Address for the Patient for QB_Match page
       /// </summary>
       /// <param name="PatientId"></param>
      
       public QBMatchEmrAddressViewModel GetQBMatchAddressByPatientId(int PatientId)
       {
            var Patient = objQBCustMatchPatientDAL.List<Patient>();

           QBMatchEmrAddressViewModel _objGetEmrAddress = (from p in Patient
                                                                 where p.PatientID == PatientId
                                                                 select new QBMatchEmrAddressViewModel
                                                                 {
                                                                     EmrAddress = "EMR Address: " + p.ShippingStreet + " " + p.ShippingCity,
                                                                 }).FirstOrDefault();
           return _objGetEmrAddress;
       }

       /// <summary>
       /// Method to Insert a new record in a QB_MAtch table for the 
       /// Correspondig Patient
       /// </summary>
       /// <param name="PatientId"></param>
       /// <param name="QBCustomer"></param>
       
       public void InsertQBMatch(int PatientID, string QBCustomer)
       {
           QB_Match theMatch = new QB_Match();
           theMatch.PatientID = PatientID;
           theMatch.QBid = QBCustomer;
           objQBCustMatchPatientDAL.Create(theMatch);
       }

       #endregion

    }
}
