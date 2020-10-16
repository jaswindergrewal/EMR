using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOneTimeSaleService" in both code and config file together.
    [ServiceContract]
    public interface IOneTimeSaleService
    {
        [OperationContract]
        List<OneTimeSaleViewModel> GetAutoshipOneTimeOrderDetails(int PatientId);

        [OperationContract]
        void AddUpdateOneTimeSaleData(int OneTimeSaleID, int DiscountID, int PatientID, int ProductID, int Quantity, bool Affiliate);

        [OperationContract]
        void DeleteOneTimeSaleData(int OneTimeSaleID);

        [OperationContract]
        void DoWork();
    }
}
