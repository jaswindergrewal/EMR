using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class OneTimeSaleBAL
    {
        OneTimeSaleDAL objOneTimeSaleDAL = new OneTimeSaleDAL();

        public List<OneTimeSaleViewModel> GetAutoshipOneTimeOrderDetails(int PatientId)
        {
            List<OneTimeSaleViewModel> objList = objOneTimeSaleDAL.GetAutoshipOneTimeOrderDetails(PatientId);
            return objList;
        }

        public void AddUpdateOneTimeSaleData(int OneTimeSaleID, int DiscountID, int PatientID, int ProductID, int Quantity, bool Affiliate)
        {
            objOneTimeSaleDAL.AddUpdateOneTimeSaleData(OneTimeSaleID, DiscountID, PatientID, ProductID, Quantity, Affiliate);
        }

        public void DeleteOneTimeSaleData(int OneTimeSaleID)
        {
            objOneTimeSaleDAL.DeleteOneTimeSaleData(OneTimeSaleID);
        }
    }
}
