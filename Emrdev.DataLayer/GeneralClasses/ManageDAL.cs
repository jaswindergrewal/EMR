using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using System.Data;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class ManageDAL : ObjectEntity, IRepositary
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
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        
        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Methods Members

        public void DeleteProspect(int ProspectID)
        {
             ObjectEntity1.ssp_Delete_Prospect(ProspectID);
        }

        public dynamic GetAllAttend(int EventID) 
        {

            var objAttend= ObjectEntity1.ssp_Get_Attendent(EventID);
            return objAttend;
             
        }

        public void AddRecordAttendee(int EventID, int ProspectId)
        {

           ObjectEntity1.ssp_RecordAttendee(EventID, ProspectId);

        }

        public dynamic GetAllAppointments()
        {

            var objAppointments = ObjectEntity1.ssp_GetAppointments();
            return objAppointments;

        }

        //
        //For Deleting Prospect Attendees
        public string DeleteRecordAttendee(int EventID, int ProspectId)
        {
            ManageDAL obj = new ManageDAL();
            //
            //Getting the Prospective details according to Prospectiveid
            List<CRM_Prospects> objProspectives = obj.GetAll<CRM_Prospects>(x => x.ProspectID == ProspectId).ToList();

            //
            //Checking if the record present or not and if present does it contain one item
            if (objProspectives != null && objProspectives.Count == 1)
            {
                if (objProspectives.FirstOrDefault().PatientID == null)
                {
                    //
                    //Deleting Prospective record
                    ObjectEntityPart1.ssp_DeleteRecordAttendee(EventID, ProspectId);
                    return "No Patient";
                }
                else
                {
                    return "Patient Exist";
                }
            }
            else
            {
                return "";
            }

        }
        

        public void DeleteStatusMgmt(int Id)
        {
            ObjectEntity1.ssp_DeleteStatusMgmt(Id);
        }

        public void DeleteMarketingSourceMgmt(int Id)
        {
            ObjectEntity1.ssp_DeleteMarketingSourceMgmt(Id);
        }

       
        #endregion
       
    }
}
