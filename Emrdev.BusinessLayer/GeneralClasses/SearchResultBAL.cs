using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class SearchResultBAL
    {
        SearchResultDAL objDAL = new SearchResultDAL();
        
       /// <summary>
       ///  Show the patients details 
       /// </summary>
       /// <param name="SearchText"></param>
       /// <param name="Clinic"></param>
       /// <param name="InActive"></param>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
        public List<Patient_Details_ViewModel> SearchResult(string FirstName, string LastName, string MiddleName, string Phone, string Clinic, bool InActive, int PageIndex, int PageSize)
        {
            return objDAL.SearchResult(FirstName, LastName, MiddleName, Phone, Clinic, InActive, PageIndex, PageSize);
        }
    }
}
