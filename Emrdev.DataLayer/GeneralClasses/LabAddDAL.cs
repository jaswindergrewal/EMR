using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class LabAddDAL : ObjectEntity, IRepositary
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

        public int AssignAppts(int PatientID, int FolloupID, bool SaveChanges)
        {
            int? patientID = PatientID;
            int? folloupID = FolloupID;
            bool? saveChanges = SaveChanges;
            ObjectParameter aptIDOUT = new ObjectParameter("aptIDOUT", typeof(global::System.Int32));
            aptIDOUT.Value = DBNull.Value;
            //ObjectParameter rowsCount = null;//new ObjectParameter("RowsCount", typeof(Int32));
            //Code review points: int result remove
           ObjectEntity1.ssp_AssignAppointments(folloupID, saveChanges, patientID, aptIDOUT);
            return Convert.ToInt32(aptIDOUT.Value);
        }

        public void UpdateLabAdd(int LabID, DateTime RangeStart, DateTime RangeEnd, int StaffID, string Content)
        {
            ObjectEntity1.ssp_UpdateLabAdd(LabID, RangeStart, RangeEnd, StaffID,Content);
        }
    }
}
