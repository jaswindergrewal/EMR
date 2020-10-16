using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using System.Data;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class LabScehduleGroupsDAL : ObjectEntity, IRepositary
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

        public List<LabScehduleGroupsViewModel> GetLabScehduleGroupsDetails()
        {
            var objTable = GetDetails<LabScehdule_Groups>();
            var objIList = new List<LabScehduleGroupsViewModel>();
            Mapper.CreateMap<LabScehdule_Groups, LabScehduleGroupsViewModel>();
            objIList = Mapper.Map(objTable, objIList);
            return objIList;
        }

        public List<LabScheduleTestsViewModel> GetLabScehduleTestDetails()
        {
            var objTable = GetDetails<LabSchedule_Tests>();
            var objIList = new List<LabScheduleTestsViewModel>();
            Mapper.CreateMap<LabSchedule_Tests, LabScheduleTestsViewModel>();
            objIList = Mapper.Map(objTable, objIList);
            return objIList;
        }   
     
         public LabScehduleGroupsViewModel GetLabScehduleGroupListByGroupId(int groupId)
        {
            LabScehduleGroupsViewModel objIList = new LabScehduleGroupsViewModel();
            LabScehdule_Groups objLst = Get<LabScehdule_Groups>(p => p.GroupID == groupId);
            Mapper.CreateMap<LabScehdule_Groups, LabScehduleGroupsViewModel>();
            objIList = Mapper.Map(objLst, objIList);
            return objIList;
        }

         public LabScheduleTestsViewModel GetLabScehduleTestListByTestId(int testId)
         {
             LabScheduleTestsViewModel objIList = new LabScheduleTestsViewModel();
             LabSchedule_Tests objLst = Get<LabSchedule_Tests>(p => p.TestID == testId);
             Mapper.CreateMap<LabSchedule_Tests, LabScheduleTestsViewModel>();
             objIList = Mapper.Map(objLst, objIList);
             return objIList;
         }

         public void DeleteLabScehduleGroups(int Id)
         {
             ObjectEntity1.ssp_DeleteLabScehduleGroups(Id);
         }

         public void DeleteLabScheduleTests(int Id)
         {
             ObjectEntity1.ssp_DeleteLabScheduleTests(Id);
         }
    }
}
