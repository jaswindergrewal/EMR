using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class SurveyResultBAL
    {
        SurveyResultDAL objDAL = new SurveyResultDAL();

        /// <summary>
        /// Get the list of patients whoes survey is taken and have entry in sat_survey table
        /// Jaswinder 5th aug 2013
        /// </summary>
        /// <returns></returns>
        public List<SurveyResultViewModel> GetSurveyDetails(int PageIndex, int PageSize)
        {
            return objDAL.GetSurveyDetails( PageIndex,  PageSize);
        }

        #region "admin_todayscontacts Page"

        /// <summary>
        /// Method to get all the patients that are contacted today
        /// jaswinder 5th aug 2013
        /// </summary>
        /// <returns></returns>
        public List<TodaysContactViewModel> GetTodaysContactDetails(int PageIndex, int PageSize)
        {
            return objDAL.GetTodaysContactDetails( PageIndex,  PageSize);
        }
        #endregion
    }
}
