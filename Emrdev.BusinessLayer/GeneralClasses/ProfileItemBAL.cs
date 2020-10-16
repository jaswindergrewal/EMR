using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class ProfileItemBAL
    {
        ProfileItemDAL objProfileItemDAL = new ProfileItemDAL();

        public ProfileItemViewModel GetProfitItemList(int ProfileItemID)
        {

            var objResult = new ProfileItem();
            var objIList = new ProfileItemViewModel();
            objResult = objProfileItemDAL.Get<ProfileItem>(p => p.ProfileItemID == ProfileItemID);
            Mapper.CreateMap<ProfileItem, ProfileItemViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
            //ProfileItemViewModel objList = objProfileItemDAL.GetProfitItemList().Where(p => p.ProfileItemID == ProfileItemID).FirstOrDefault();
            //return objList;
        }

        public void UpdateAffliliateInProfileItems(int ProfileItemId, bool Affliliate)
        {
            objProfileItemDAL.UpdateAffliliateInProfileItems(ProfileItemId, Affliliate);
        }

        public void AddRecordsInProfileItem(int PatientId, int ProductID, int Quantity, DateTime StartDate, DateTime EndDate, DateTime NextShipDate, DateTime LastShipped, int DayToShip, int DiscountID)
        {
            objProfileItemDAL.AddRecordsInProfileItem(PatientId, ProductID, Quantity, StartDate, EndDate, NextShipDate, LastShipped, DayToShip, DiscountID);
        }

        public List<ProfileItemsGetTree> ProfileItemGetTree(int PatientId)
        {
            List<ProfileItemsGetTree> lstObj = objProfileItemDAL.ProfileItemGetTree(PatientId);
            return lstObj;
        }

        public System.Data.DataTable ProfileItemGetTree1(int patientId, string connection)
        {
            System.Data.DataTable dt1 = new System.Data.DataTable();
            dt1 = objProfileItemDAL.ProfileItemGetTree1(patientId, connection);
            return dt1;
        }

        public Emrdev.ViewModelLayer.ProfileItem_GetByID_ViewModel GetProfileItemById(int profielItemId)
        {
            return objProfileItemDAL.GetProfileItemById(profielItemId);
        }

        public void UpdateNextShipDateByProfileItemId(int profileItemId,DateTime nextShipDate)
        {
            objProfileItemDAL=new ProfileItemDAL();
            objProfileItemDAL.UpdateNextShipDateByProfileItemId(profileItemId, nextShipDate);            
        }
    }
}
