using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AdminDrugListBAL
    {
        AdminDrugListDAL objDAL = new AdminDrugListDAL();

        //Method to get all drugs name where reviewd=false
        public List<AdminDrugListViewModel> GetAllDrugList(bool Reviewed)
        {
            var _objDrugList=new List<AdminDrugListViewModel>();
            var DrugEntity = new List<Drug>();
            if (Reviewed == false)
            {
                DrugEntity = objDAL.GetAll<Drug>(o => o.DrugName != "" && o.Reviewed == false).OrderBy(o => o.DrugName).ToList();

            }
            else
            {

                DrugEntity = objDAL.GetAll<Drug>(o => o.DrugID > 0 && o.DrugName != "").OrderBy(o => o.DrugName).ToList();
            }
            Mapper.CreateMap<Drug, AdminDrugListViewModel>();
            _objDrugList = Mapper.Map(DrugEntity, _objDrugList);
            return _objDrugList;
        }

        //Method to update Drug list
        public void UpdateAdminDrugList(int DrugId, string DrugName, string Viewable, string Gender, string Supplement_yn, string Reviewed)
        {
            Drug DrugEntity = new Drug();
            DrugEntity = objDAL.Get<Drug>(o => o.DrugID == DrugId);
            DrugEntity.DrugName = DrugName.Trim();
            DrugEntity.Viewable_yn = bool.Parse(Viewable);
            DrugEntity.Reviewed = bool.Parse(Reviewed);
            DrugEntity.Supplement_yn = bool.Parse(Supplement_yn);
            DrugEntity.Gender = Gender;
            objDAL.Edit(DrugEntity);

        }

        /// <summary>
        /// delete the drug name by id
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteDrug(int Id)
        {
            objDAL.DeleteDrug(Id);
        }


       /// <summary>
       /// Get the drug list using sort expression
       /// </summary>
       /// <param name="sortExpression"></param>
       /// <param name="startRwoIndex"></param>
       /// <param name="maximumRows"></param>
       /// <param name="reviewed"></param>
       /// <returns></returns>
        public List<Emrdev.ViewModelLayer.sp_GetDrugList_Result> SelectAllDrugList(string sortExpression, int startRwoIndex, int maximumRows, bool reviewed)
        {
            return objDAL.SelectAllDrugList(sortExpression, startRwoIndex, maximumRows, reviewed);

        }

        /// <summary>
        /// Get the drug list count
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="reviewed"></param>
        /// <returns></returns>
        public int SelectAllDrugListCount(string sortExpression, bool reviewed)
        {
            return objDAL.SelectAllDrugListCount(sortExpression,reviewed);
        }

    }
}
