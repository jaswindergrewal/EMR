using AutoMapper;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class CRMdashboardDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntityPart1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntityPart1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<PlottGraphViewModel> GetCampaignGraph(int CampaignID)
        {
            var objResult = ObjectEntityPart1.ssp_ReportGraphCRmProspect(CampaignID).ToList();
            var objIList = new List<PlottGraphViewModel>();
            Mapper.CreateMap<ssp_ReportGraphCRmProspect_Result, PlottGraphViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public CRMStatisticViewModel GetCRMStatisticData()
        {
            var objResult = ObjectEntityPart1.ssp_CRMDashBoardStatistic().FirstOrDefault();
            var objIList = new CRMStatisticViewModel();
            Mapper.CreateMap<ssp_CRMDashBoardStatistic_Result, CRMStatisticViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<CRMStatisticViewModel> GetEventGraph(int EventID)
        {
            var objResult = ObjectEntityPart1.ssp_ReportGraphCRmEventProspect(EventID).ToList();
            var objIList = new List<CRMStatisticViewModel>();
            Mapper.CreateMap<ssp_ReportGraphCRmEventProspect_Result, CRMStatisticViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

       
    }
}
