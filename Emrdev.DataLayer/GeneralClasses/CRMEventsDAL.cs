﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class CRMEventsDAL: ObjectEntity, IRepositary
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
            throw new NotImplementedException();
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
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<CRMEventsViewModel> GetCRMEventsDetails()
        {
            var drugDetails = GetDetails<CRM_Events>().ToList();
            var objIList = new List<CRMEventsViewModel>();
            Mapper.CreateMap<CRM_Events, CRMEventsViewModel>();
            objIList = Mapper.Map(drugDetails, objIList);
            return objIList;
        }
    }
}