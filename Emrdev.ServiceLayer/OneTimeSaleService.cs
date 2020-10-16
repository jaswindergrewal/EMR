using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OneTimeSaleService" in both code and config file together.
    public class OneTimeSaleService : IOneTimeSaleService
    {
        OneTimeSaleBAL objOneTimeSaleBAL = new OneTimeSaleBAL();
        public void DoWork()
        {
        }

        public List<OneTimeSaleViewModel> GetAutoshipOneTimeOrderDetails(int PatientId)
        {
            List<OneTimeSaleViewModel> objList = objOneTimeSaleBAL.GetAutoshipOneTimeOrderDetails(PatientId);
            return objList;
        }


        public void AddUpdateOneTimeSaleData(int OneTimeSaleID, int DiscountID, int PatientID, int ProductID, int Quantity, bool Affiliate)
        {
            objOneTimeSaleBAL.AddUpdateOneTimeSaleData(OneTimeSaleID, DiscountID, PatientID, ProductID, Quantity, Affiliate);
        }


        public void DeleteOneTimeSaleData(int OneTimeSaleID)
        {
            objOneTimeSaleBAL.DeleteOneTimeSaleData(OneTimeSaleID);
        }
    }
}
