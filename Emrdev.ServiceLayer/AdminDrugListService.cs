using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using System.Collections.Generic;

namespace Emrdev.ServiceLayer
{
    public class AdminDrugListService : IAdminDrugListService
    {
        AdminDrugListBAL objBAL = new AdminDrugListBAL();

        //Method to get all drugs name
        public List<AdminDrugListViewModel> GetAllDrugList(bool Reviewed)
        {
            List<AdminDrugListViewModel> _objDrugList = objBAL.GetAllDrugList(Reviewed);
            return _objDrugList;
        }

        public void UpdateAdminDrugList(int DrugId, string DrugName, string Viewable, string Gender, string Supplement_yn, string Reviewed)
        {
            objBAL.UpdateAdminDrugList(DrugId, DrugName, Viewable, Gender, Supplement_yn, Reviewed);
        }


        public void DeleteDrug(int Id)
        {
            objBAL.DeleteDrug(Id);
        }



        #region Get Drug List

        public List<sp_GetDrugList_Result> SelectAllDrugList(string sortExpression, int startRowIndex, int maximumRows, bool reviewed)
        {
            return objBAL.SelectAllDrugList(sortExpression, startRowIndex, maximumRows, reviewed);
        }

        public int SelectAllDrugListCount(string sortExpression, bool reviewed)
        {
            return objBAL.SelectAllDrugListCount(sortExpression, reviewed);
        }

        #endregion
    }
}
