using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class AdminSuppListBAL
    {
        AdminSuppListDAL objDAL = new AdminSuppListDAL();

        /// <summary>
        /// Method to get all Supliers list name where Viewable=false and productname is not blank
        /// </summary>
        /// <returns></returns>
        public List<AdminSuppListViewModel> GetAllSuppList()
        {
            var _objSuppList = new List<AdminSuppListViewModel>();
            var AutoshipProductEntity = new List<AutoshipProduct>();
            //AutoshipProductEntity = objDAL.GetAll<AutoshipProduct>(o => o.Viewable == false && o.ProductName != "" && o.ProductName != "-1").OrderBy(o => o.ProductName).ToList();

            // modify the code as per the client's comments(WB-08/08/2013) that
            // all list should be displayed instead of only active records
            // modified by: Deepak Thakur[12.April.2013]
            AutoshipProductEntity = objDAL.GetAll<AutoshipProduct>(o => o.ProductName != "" && o.ProductName != "-1").OrderBy(o => o.ProductName).ToList();

            Mapper.CreateMap<AutoshipProduct, AdminSuppListViewModel>();
            _objSuppList = Mapper.Map(AutoshipProductEntity, _objSuppList);
            return _objSuppList;
        }

        /// <summary>
        /// Method to update Supplier list
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductName"></param>
        /// <param name="Viewable"></param>
        /// <param name="Reviewed"></param>
        public void UpdateAdminSuppList(int ProductId, string ProductName, string Viewable, string Reviewed)
        {
            AutoshipProduct _objAutoShip;
            _objAutoShip = objDAL.Get<AutoshipProduct>(o => o.ProductID == ProductId);

            _objAutoShip.ProductName = ProductName;
            _objAutoShip.Viewable = bool.Parse(Viewable);
            _objAutoShip.Reviewed = bool.Parse(Reviewed);

            objDAL.Edit(_objAutoShip);
        }

        public void DeleteSupplimentList(int Id)
        {
            objDAL.DeleteSupplimentList(Id);
        }
    }
}
