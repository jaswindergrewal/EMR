using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminDrugListService" in both code and config file together.
    [ServiceContract]
    public interface IAdminDrugListService
    {
        [OperationContract]
        List<AdminDrugListViewModel> GetAllDrugList(bool Reviewed);

        [OperationContract]
        void UpdateAdminDrugList(int DrugId, string DrugName, string Viewable, string Gender, string Supplement_yn, string Reviewed);

        [OperationContract]
        void DeleteDrug(int Id);

        [OperationContract]
        List<Emrdev.ViewModelLayer.sp_GetDrugList_Result> SelectAllDrugList(string sortExpression, int startRwoIndex, int maximumRows, bool reviewed);


        [OperationContract]
        int SelectAllDrugListCount(string sortExpression, bool reviewed);
    }
}
