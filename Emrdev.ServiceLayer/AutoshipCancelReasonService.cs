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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AutoshipCancelReasonService" in both code and config file together.
    public class AutoshipCancelReasonService : IAutoshipCancelReasonService
    {
        AutoshipCancelReasonBAL objAutoshipCancelReasonBAL = new AutoshipCancelReasonBAL();
        public void DoWork()
        {
        }

        public List<AutoshipCancelReasonViewModel> GetAutoshipCancelReasonList()
        {
            List<AutoshipCancelReasonViewModel> lstModel = objAutoshipCancelReasonBAL.GetAutoshipCancelReasonList();
            return lstModel;
        }


        public void DeleteExcepProfileItemsByPatient(int PatientId)
        {
            objAutoshipCancelReasonBAL.DeleteExcepProfileItemsByPatient(PatientId);
        }


        public void AddContactTbl(int AptType, int PatientId, string MessageBody, int EmployeeID)
        {
            objAutoshipCancelReasonBAL.AddContactTbl(AptType, PatientId, MessageBody, EmployeeID);
        }


        public List<AutoshipDiscountViewModel> GetAutoshipDiscount()
        {
            List<AutoshipDiscountViewModel> lstModel = objAutoshipCancelReasonBAL.GetAutoshipDiscount();
            return lstModel;
        }


        public List<AutoshipProductsViewModel> GetAutoshipProductList()
        {
            List<AutoshipProductsViewModel> lstModel = objAutoshipCancelReasonBAL.GetAutoshipProductList();
            return lstModel;
        }

        public void InsertUpdateAutoShipCancelReason(string Reason,bool Active,int ReasonId)
        {
            objAutoshipCancelReasonBAL.InsertUpdateAutoShipCancelReason(Reason, Active, ReasonId);
        }


        public void DeleteAutoshipCancelReasons(int Id)
        {
            objAutoshipCancelReasonBAL.DeleteAutoshipCancelReasons(Id);
        }

        public List<AutoshipProductsViewModel> GetAutoshipProductDropDownList()
        {
            List<AutoshipProductsViewModel> lstModel = objAutoshipCancelReasonBAL.GetAutoshipProductDropDownList();
            return lstModel;
        }
       
    }
}
