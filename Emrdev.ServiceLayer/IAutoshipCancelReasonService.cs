using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAutoshipCancelReasonService" in both code and config file together.
    [ServiceContract]
    public interface IAutoshipCancelReasonService
    {
        [OperationContract]
        List<AutoshipCancelReasonViewModel> GetAutoshipCancelReasonList();

        [OperationContract]
        void DeleteExcepProfileItemsByPatient(int PatientId);

        [OperationContract]
        void AddContactTbl(int AptType, int PatientId, string MessageBody, int EmployeeID);

        [OperationContract]
        List<AutoshipDiscountViewModel> GetAutoshipDiscount();

        [OperationContract]
        List<AutoshipProductsViewModel> GetAutoshipProductList();

        [OperationContract]
        void DoWork();

        [OperationContract]
        void InsertUpdateAutoShipCancelReason(string Reason, bool Active, int ReasonId);

        [OperationContract]
        void DeleteAutoshipCancelReasons(int Id);

        [OperationContract]
        List<AutoshipProductsViewModel> GetAutoshipProductDropDownList();
       
    }
}
