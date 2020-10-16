using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public interface IAutoshipProductService
    {
        [OperationContract]
        void AddNewProduct(Emrdev.ViewModelLayer.AutoshipProductViewModel objModel);

        [OperationContract]
        void UpdateProduct(Emrdev.ViewModelLayer.AutoshipProductViewModel objModel);

        [OperationContract]
        string CheckOneTimeOrder(int PatientID, int ProductID, string ShipDate);

        [OperationContract]
        List<ViewModelLayer.AutoshipProductViewModel> GetBundleGridData(int BundleId);

        [OperationContract]
        void AddBundledata(int BundleId, int ProductId);

        [OperationContract]
        ViewModelLayer.ShippingValues GetShippingValues();

        [OperationContract]
        void AddShippingValues(decimal ShippingFee,decimal OrderLimit);
    }
}
