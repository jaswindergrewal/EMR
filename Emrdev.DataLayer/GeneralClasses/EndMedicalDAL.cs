using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
  public  class EndMedicalDAL: ObjectEntity, IRepositary
  {
      #region IRepositary Members
      public void Create<T>(T entityToCreate) where T : class
        {
            throw new NotImplementedException();
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

        public List<EndMedicalViewModel> GetEndMedicalDetails(string ClinicName)
        {
            var objResult = ObjectEntity1.ssp_GetEndMedicalList(ClinicName).ToList();
            var objIList = new List<EndMedicalViewModel>();
            Mapper.CreateMap<ssp_GetEndMedicalList_Result, EndMedicalViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void CloseAppointments(int ? ID, int ? PatientID, string Text, int ? StaffID, int ? AptID, bool ? CboAutoShip,DateTime ? DateEntered)
        {
            ObjectEntity1.ssp_CloseAppointments(ID, PatientID,Text, StaffID, AptID, CboAutoShip, DateEntered);

        }

        public void AddContactDetails(int ID, int PatientID, string Text, int StaffID, int AptID)
        {
            ObjectEntity1.contact_tbl_EMR_Insert(ID, PatientID, Text, StaffID, AptID);

        }
  }
}
