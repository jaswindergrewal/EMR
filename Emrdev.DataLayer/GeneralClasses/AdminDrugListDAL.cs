using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Emrdev.DataLayer.GeneralClasses
{
    //implement interface IRepositary where common methods are defined.
    public class AdminDrugListDAL : ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public void DeleteDrug(int Id)
        {
            ObjectEntity1.ssp_DeleteDrug(Id);
        }

        /// <summary>
        /// Get Drug List 
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="reviewed"></param>
        /// <returns></returns>

        public List<Emrdev.ViewModelLayer.sp_GetDrugList_Result> SelectAllDrugList(string sortExpression, int startRowIndex, int maximumRows, bool reviewed)
        {
            string[] myParam;
            string filter=string.Empty;
            string sortingBy=string.Empty;
            if (!string.IsNullOrEmpty(sortExpression) && sortExpression.Contains("__ob_sep__"))
            {
                myParam = sortExpression.Split(new string[] { "__ob_sep__" }, StringSplitOptions.None);//Extract sortingParameter and Filter
                //Code Review:myParam.Length > 1 & myParam[0] != ""
                if (myParam.Length > 1 & !string.IsNullOrEmpty(myParam[0]))
                {
                    filter = myParam[1];
                    sortingBy = myParam[0];
                    //Both parameter are passed Sorting and Filter
                }
                else if (sortExpression.StartsWith("__ob_sep__"))
                {
                    //Only Filter Parameter
                    filter = sortExpression.Substring(sortExpression.IndexOf('(') + 1, (sortExpression.LastIndexOf(')') - sortExpression.IndexOf('(')) - 1);
                    sortingBy = "DrugName ASC";
                }
                else
                {
                    // Sorting Parameter
                    sortingBy = sortExpression;
                    filter = "No Filter";
                }
            }
            else if (!string.IsNullOrEmpty(sortExpression))
            {
                //Sorting Parameter
                sortingBy = sortExpression;
                filter = "No Filter";
            }
            else
            {
                /* If parameter are empty pass default values */
                sortingBy = "DrugName ASC";
                filter = "No Filter";
            }
            HttpContext.Current.Request.Cookies.Add(new HttpCookie("Filter", filter));
            return ObjectEntity1.sp_GetDrugList(!reviewed, filter, sortingBy).Select(i => new Emrdev.ViewModelLayer.sp_GetDrugList_Result { DrugID = i.DrugID, DrugName = i.DrugName, Description = i.Description, Viewable_yn = i.Viewable_yn, Gender = i.Gender, DrugType = i.DrugType, DrugCategory = i.DrugCategory, Supplement_yn = i.Supplement_yn, Reviewed = i.Reviewed, DateEntered = i.DateEntered, ProductID = i.ProductID }).Skip(startRowIndex).Take(maximumRows).ToList();
        }


        /// <summary>
        /// Get Drug List Count
        /// </summary>
        /// <param name="reviewed"></param>
        /// <returns></returns>
        public int SelectAllDrugListCount(string sortExpression,bool reviewed)
        {
            string filter=string.Empty;
            string strsortExpression = sortExpression;
            if (HttpContext.Current.Request.Cookies["Filter"] != null)
            {
                filter = HttpContext.Current.Request.Cookies["Filter"].Value;
            }
            if (string.IsNullOrEmpty(filter)) filter = "No Filter";
            return ObjectEntity1.sp_GetDrugListCount(!reviewed, filter).First().Value;
        }
    }
}
