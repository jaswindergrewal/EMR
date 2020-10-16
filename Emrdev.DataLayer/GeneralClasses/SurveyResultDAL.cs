using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class SurveyResultDAL : ObjectEntity,IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            throw new NotImplementedException();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Get the list of surveyed patients list from the database
        /// jaswinder 5th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<SurveyResultViewModel> GetSurveyDetails(int PageIndex, int PageSize)
        {//emr2017
            //var objResult = ObjectEntity1.ssp_Patients_Sat_SurveyResults( PageIndex, PageSize).ToList();
            var objIList = new List<SurveyResultViewModel>();
            //Mapper.CreateMap<ssp_Patients_Sat_SurveyResults_Result, SurveyResultViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Get the list of patients that have entry in contact_tbl table with todays date
        /// Jaswinder on 5th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<TodaysContactViewModel> GetTodaysContactDetails(int PageIndex, int PageSize)
        {//Emr2017
            //var objResult = ObjectEntity1.ssp_PatientswithContactEntriesToday(PageIndex, PageSize).ToList();
            var objIList = new List<TodaysContactViewModel>();
            //Mapper.CreateMap<ssp_PatientswithContactEntriesToday_Result, TodaysContactViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
