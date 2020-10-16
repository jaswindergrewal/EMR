using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data;
using System.Configuration;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class ProfileItemDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<ProfileItemViewModel> GetProfitItemList()
        {
            var objResult = GetDetails<ProfileItem>().ToList();
            var objIList = new List<ProfileItemViewModel>();
            Mapper.CreateMap<ProfileItem, ProfileItemViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void UpdateAffliliateInProfileItems(int ProfileItemId, bool Affliliate)
        {
            ObjectEntity1.ssp_UpdateAffliliateInProfileItems(ProfileItemId, Affliliate);
        }

        public void AddRecordsInProfileItem(int PatientId, int ProductID, int Quantity, DateTime StartDate, DateTime EndDate, DateTime NextShipDate, DateTime LastShipped, int DayToShip, int DiscountID)
        {
            ObjectEntity1.ssp_AddRecordsInProfileItem(PatientId, ProductID, Quantity, StartDate, EndDate, NextShipDate, LastShipped, DayToShip, DiscountID);
        }

        public List<ProfileItemsGetTree> ProfileItemGetTree(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_ProfileItems_GetTree(PatientId).ToList();
            var objIList = new List<ProfileItemsGetTree>();
            Mapper.CreateMap<ssp_ProfileItems_GetTree_Result, ProfileItemsGetTree>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public DataTable ProfileItemGetTree1(int patientId, string connection)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connection))
            {
                using (System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("ssp_ProfileItems_GetTree", conn))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@PatientID", patientId);
                    System.Data.SqlClient.SqlDataAdapter da1 = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    return dt1;
                }
            }
        }


        /// <summary>
        /// Get ProfileItem By ProfileItemId
        /// </summary>
        /// <param name="profileItemId"></param>
        /// <returns>Returns a collection of properties by joining with other tables</returns>
        public Emrdev.ViewModelLayer.ProfileItem_GetByID_ViewModel GetProfileItemById(int profileItemId)
        {
            return ObjectEntity1.ProfileItem_GetByID(profileItemId).Select(i => new Emrdev.ViewModelLayer.ProfileItem_GetByID_ViewModel { ProfileItemID = i.ProfileItemID, ProductName = i.ProductName, Quantity = i.Quantity, Frequency = i.Frequency, StartDate = i.StartDate, EndDate = i.EndDate, DayToShip = i.DayToShip, LastShipped = i.LastShipped, ProductID = i.ProductID, DiscountID = i.DiscountID, NextShipDate = i.NextShipDate, FrequencyValue = i.FrequencyValue }).First();
        }


        /// <summary>
        /// Update NextShipDate by ProfileItemId
        /// </summary>
        /// <param name="profileItemId"></param>
        /// <param name="nextShipDate"></param>
        public void UpdateNextShipDateByProfileItemId(int profileItemId, DateTime nextShipDate)
        {
            ProfileItem objProfile=ObjectEntity1.ProfileItems.SingleOrDefault(i => i.ProfileItemID == profileItemId && i.EndDate==null);
            if (objProfile != null)
            {
                objProfile.NextShipDate = nextShipDate;
                Edit<ProfileItem>(objProfile);
            }
            
        }
    }
}
