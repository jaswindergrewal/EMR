using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class MailChimpCampaignDAL : ObjectEntity, IRepositary
    {
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }

        public void Create1<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart2.Set<T>().Add(entityToCreate);
            ObjectEntityPart2.SaveChanges();
        }

        public void Edit1<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart2.SaveChanges();
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

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public T Get1<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
        }


        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public MailChimpCampaignViewModel GetMalChimpCampaign()
        {
            var objResult = ObjectEntityPart2.ssp_GetMailChimpCampaign().FirstOrDefault();
            var objIList = new MailChimpCampaignViewModel();
            Mapper.CreateMap<ssp_GetMailChimpCampaign_Result, MailChimpCampaignViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void SaveMailChimpCampaign(MailChimpCampaignViewModel MailChimpCampaign)
        {
            ObjectEntityPart2.ssp_SaveMailChimpCampaign(MailChimpCampaign.MailChimpCampaignId,MailChimpCampaign.MailChimpCampaignName,MailChimpCampaign.MailChimpCampaignListId);
        }

        

    }
}
