using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminSuppListService" in both code and config file together.
    public class AdminSuppListService : IAdminSuppListService
    {
        AdminSuppListBAL objBAL = new AdminSuppListBAL();

        //Method to get all Supplier list
        public List<AdminSuppListViewModel> GetAllSuppList()
        {
            List<AdminSuppListViewModel> _objSuppList = objBAL.GetAllSuppList();
            return _objSuppList;
        }


        //Method to update Supplier list
        public void UpdateAdminSuppList(int ProductId, string ProductName, string Viewable, string Reviewed)
        {

            objBAL.UpdateAdminSuppList(ProductId, ProductName, Viewable, Reviewed);
        }


        public void DeleteSupplimentList(int Id)
        {
            objBAL.DeleteSupplimentList(Id);
        }
    }
}
