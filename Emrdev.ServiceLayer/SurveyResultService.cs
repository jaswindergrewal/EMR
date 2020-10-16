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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SurveyResultService" in both code and config file together.
    public class SurveyResultService : ISurveyResultService
    {
        SurveyResultBAL objBAL = new SurveyResultBAL();

        public List<SurveyResultViewModel> GetSurveyDetails(int PageIndex, int PageSize)
        {
            return objBAL.GetSurveyDetails( PageIndex,  PageSize);
        }

        public List<TodaysContactViewModel> GetTodaysContactDetails(int PageIndex, int PageSize)
        {
            return objBAL.GetTodaysContactDetails( PageIndex,  PageSize);
        }
    }
}
