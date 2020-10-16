using Emrdev.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.GeneralClasses
{
    public class AutoshipProductBAL
    {
        #region Global

        Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL objAutoShipDAL;

        #endregion

        #region Add New Product

        public void AddNewProduct(Emrdev.ViewModelLayer.AutoshipProductViewModel objModel)
        {
            objAutoShipDAL = new Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL();
            /*Create object of Entity and set properties from Model*/
            AutoshipProduct objEntity = new Emrdev.DataLayer.AutoshipProduct();
            objEntity.Active = objModel.Active;
            objEntity.AutoshipPrice = objModel.AutoshipPrice;
            objEntity.ProductName = objModel.ProductName;
            objEntity.Reviewed = objModel.Reviewed;
            objEntity.Viewable = objModel.Viewable;
            objEntity.QBId = objModel.QBId;
            objEntity.Sku = objModel.Sku;
            objEntity.Weight = objModel.Weight;
            objEntity.Bundle = objModel.Bundle;
            objEntity.GroupID = 0;
            objAutoShipDAL.CreateProduct<AutoshipProduct>(objEntity);
        }

        #endregion


        #region Update Product

        public void UpdateProduct(Emrdev.ViewModelLayer.AutoshipProductViewModel objModel)
        {
            objAutoShipDAL = new Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL();
            objAutoShipDAL.UpdateProduct(objModel);
        }

        #endregion

        
        public string CheckOneTimeOrder(int PatientID, int ProductID, string ShipDate)
        {
            DateTime ShippingDate = Convert.ToDateTime(ShipDate);
            objAutoShipDAL = new AutoshipProductDAL();
            ProfileItem profs = new ProfileItem();
            profs = objAutoShipDAL.Get<ProfileItem>(o => o.PatientID == PatientID && o.ProductID == ProductID && o.EndDate == null);
            bool foundOne = false;
            if (profs == null)
            {
                foundOne = false;
            }
            else if (profs.EndDate < ShippingDate)
            {
                foundOne = false;
            }
            else if (profs.EndDate == null && profs.NextShipDate.AddDays(-3) <= ShippingDate && ShippingDate < profs.NextShipDate)
            {
                foundOne = true;
            }
            else if (profs.EndDate < ShippingDate && profs.NextShipDate.AddDays(-3) <= ShippingDate && ShippingDate < profs.NextShipDate)
            {
                foundOne = true;
            }

            if (foundOne)
            {
                return "This item is already due to be shipped on " + profs.NextShipDate.ToShortDateString();
            }
            else
            {
                return "";
            }
            
        }

        public List<ViewModelLayer.AutoshipProductViewModel> GetBundleGridData(int BundleId)
        {
            objAutoShipDAL = new Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL();

            return objAutoShipDAL.GetBundleGridData(BundleId);
        }

        public void AddBundledata(int BundleId, int ProductId)
        {
            objAutoShipDAL = new Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL();
            objAutoShipDAL.AddBundledata(BundleId, ProductId);
        }

        public ViewModelLayer.ShippingValues GetShippingValues()
        {
            objAutoShipDAL = new Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL();
            var _objList = new ShippingValues();
            var ShippingEntity = new Admin_ShippingValues();
            ShippingEntity = objAutoShipDAL.GetEntity<Admin_ShippingValues>(o => o.Id>0);

            Mapper.CreateMap<Admin_ShippingValues, ShippingValues>();
            _objList = Mapper.Map(ShippingEntity, _objList);
            return _objList;
        }

        public void AddShippingValues(decimal ShippingFee, decimal OrderLimit)
        {
            objAutoShipDAL = new Emrdev.DataLayer.GeneralClasses.AutoshipProductDAL();
            objAutoShipDAL.AddShippingValues(ShippingFee, OrderLimit);
        }
    }
}
