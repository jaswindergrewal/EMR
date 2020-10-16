using AutoMapper;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class SeminarScheduleDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }//test

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
            return ObjectEntity1.Set<T>();
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
            throw new NotImplementedException();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public List<PostSeminarAppointment> GetPostSeminarAppointment(DateTime StartDate,string Clinic)
        {
            var objResult = ObjectEntityPart2.ssp_GetPostSeminarAppointments(StartDate,Clinic).ToList();
            var objIList = new List<PostSeminarAppointment>();
            Mapper.CreateMap<ssp_GetPostSeminarAppointments_Result, PostSeminarAppointment>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
