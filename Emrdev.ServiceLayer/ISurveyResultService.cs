using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISurveyResultService" in both code and config file together.
    [ServiceContract]
    public interface ISurveyResultService
    {
        [OperationContract]
        List<SurveyResultViewModel> GetSurveyDetails(int PageIndex, int PageSize);

        [OperationContract]
        List<TodaysContactViewModel> GetTodaysContactDetails(int PageIndex, int PageSize);
    }
}
