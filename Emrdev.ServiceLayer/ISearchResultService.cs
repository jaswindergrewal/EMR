using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISearchResultService" in both code and config file together.
    [ServiceContract]
    public interface ISearchResultService
    {
        [OperationContract]
        List<Patient_Details_ViewModel> SearchResult(string FirstName, string LastName, string MiddleName, string Phone, string Clinic, bool InActive, int PageIndex, int PageSize);
    }
}
