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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SearchResultService" in both code and config file together.
    public class SearchResultService : ISearchResultService
    {
        SearchResultBAL objBAL = new SearchResultBAL();
        public List<Patient_Details_ViewModel> SearchResult(string FirstName, string LastName, string MiddleName, string Phone, string Clinic, bool InActive, int PageIndex, int PageSize)
        {
            return objBAL.SearchResult(FirstName, LastName, MiddleName, Phone, Clinic, InActive, PageIndex,  PageSize);
        }
    }
}
