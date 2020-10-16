using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;
using System.Globalization;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProfileItemService" in both code and config file together.
    public class ProfileItemService : IProfileItemService
    {
        ProfileItemBAL objProfileItemBAL = new ProfileItemBAL();
        public void DoWork()
        {
        }

        public ProfileItemViewModel GetProfitItemList(int ProfileItemID)
        {
            ProfileItemViewModel objList = objProfileItemBAL.GetProfitItemList(ProfileItemID);
            return objList;
        }

        public void UpdateAffliliateInProfileItems(int ProfileItemId, bool Affliliate)
        {
            objProfileItemBAL.UpdateAffliliateInProfileItems(ProfileItemId, Affliliate);
        }


        public void AddRecordsInProfileItem(int PatientId, int ProductID, int Quantity, DateTime StartDate, DateTime EndDate, DateTime NextShipDate, DateTime LastShipped, int DayToShip, int DiscountID)
        {
            objProfileItemBAL.AddRecordsInProfileItem(PatientId, ProductID, Quantity, StartDate, EndDate, NextShipDate, LastShipped, DayToShip, DiscountID);
        }


        public List<ProfileItemsGetTree> ProfileItemGetTree(int PatientId)
        {
            List<ProfileItemsGetTree> lstObj = objProfileItemBAL.ProfileItemGetTree(PatientId);
            return lstObj;
        }


        public System.Data.DataTable ProfileItemGetTree1(int patientId, string connection)
        {
            System.Data.DataTable dt1 = new System.Data.DataTable();
            dt1.Locale = CultureInfo.InvariantCulture;
            dt1 = objProfileItemBAL.ProfileItemGetTree1(patientId, connection);
            return dt1;
        }


        public ProfileItem_GetByID_ViewModel GetProfileItemById(int profielItemId)
        {
           return objProfileItemBAL.GetProfileItemById(profielItemId);
        }


        public void UpdateNextShipDateByProfileItemId(int profileItemId, DateTime nextShipDate)
        {
            objProfileItemBAL = new ProfileItemBAL();
            objProfileItemBAL.UpdateNextShipDateByProfileItemId(profileItemId, nextShipDate);
        }
    }
}
