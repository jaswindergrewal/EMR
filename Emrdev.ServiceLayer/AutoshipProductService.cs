using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class AutoshipProductService:IAutoshipProductService
    {
        #region Global

        Emrdev.GeneralClasses.AutoshipProductBAL objAutoshipProductBAL;

        #endregion


        #region Add New Product

        public void AddNewProduct(ViewModelLayer.AutoshipProductViewModel objModel)
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            objAutoshipProductBAL.AddNewProduct(objModel);
        }

        #endregion


        public void UpdateProduct(ViewModelLayer.AutoshipProductViewModel objModel)
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            objAutoshipProductBAL.UpdateProduct(objModel);
        }

        //Function to check one time order
        //By Jaswinder
        public string CheckOneTimeOrder(int PatientID, int ProductID, string ShipDate)
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            return objAutoshipProductBAL.CheckOneTimeOrder(PatientID, ProductID, ShipDate);
        }

        public List<ViewModelLayer.AutoshipProductViewModel>GetBundleGridData(int BundleId)
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            return objAutoshipProductBAL.GetBundleGridData(BundleId);
        }

        public void AddBundledata(int BundleId, int ProductId)
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            objAutoshipProductBAL.AddBundledata(BundleId, ProductId);
        }
        public  ViewModelLayer.ShippingValues GetShippingValues()
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            return objAutoshipProductBAL.GetShippingValues();
        }

        public void AddShippingValues(decimal ShippingFee, decimal OrderLimit)
        {
            objAutoshipProductBAL = new GeneralClasses.AutoshipProductBAL();
            objAutoshipProductBAL.AddShippingValues(ShippingFee, OrderLimit);
        }
       
    }
}
