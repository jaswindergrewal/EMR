using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AutoshipCancelReasonBAL
    {
        AutoshipCancelReasonDAL objAutoshipCancelReasonDAL = new AutoshipCancelReasonDAL();

        public List<AutoshipCancelReasonViewModel> GetAutoshipCancelReasonList()
        {
            //List<AutoshipCancelReasonViewModel> lstModel = objAutoshipCancelReasonDAL.GetAutoshipCancelReasonList().Where(p => p.Active == true).OrderBy(p => p.ReasonName).ToList();

            // modify the code as per the client's comments(WB-08/08/2013) that
            // all list should be displayed instead of only active records
            // modified by: Deepak Thakur[12.April.2013]
            List<AutoshipCancelReasonViewModel> lstModel = objAutoshipCancelReasonDAL.GetAutoshipCancelReasonList().Where(p => p.ReasonName != "").OrderBy(p => p.ReasonName).ToList();
            return lstModel;
        }

        public void DeleteExcepProfileItemsByPatient(int PatientId)
        {
            objAutoshipCancelReasonDAL.DeleteExcepProfileItemsByPatient(PatientId);
        }

        public void AddContactTbl(int AptType, int PatientId, string MessageBody, int EmployeeID)
        {
            objAutoshipCancelReasonDAL.AddContactTbl(AptType, PatientId, MessageBody, EmployeeID);
        }

        public List<AutoshipDiscountViewModel> GetAutoshipDiscount()
        {
            List<AutoshipDiscountViewModel> lstModel = objAutoshipCancelReasonDAL.GetAutoshipDiscount().OrderByDescending(p => p.DiscountName).ToList();
            return lstModel;
        }

        public List<AutoshipProductsViewModel> GetAutoshipProductList()
        {
            List<AutoshipProductsViewModel> lstModel = objAutoshipCancelReasonDAL.GetAutoshipProductList().Where(p => p.Active == true).OrderBy(p => p.ProductName).ToList();
            return lstModel;
        }

        public void InsertUpdateAutoShipCancelReason(string Reason, bool Active, int ReasonId)
        {
            objAutoshipCancelReasonDAL.InsertAutoShipCancelReason(Reason, Active, ReasonId);
        }

        public void DeleteAutoshipCancelReasons(int Id)
        {
            objAutoshipCancelReasonDAL.DeleteAutoshipCancelReasons(Id);
        }

        public List<AutoshipProductsViewModel> GetAutoshipProductDropDownList()
        {
            List<AutoshipProductsViewModel> lstModel = objAutoshipCancelReasonDAL.GetAutoshipProductDropDownList().OrderBy(p => p.ProductName).ToList(); ;
            return lstModel;
        }
    }
}
