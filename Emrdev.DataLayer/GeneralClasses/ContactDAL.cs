using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class ContactDAL : ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>().Where(whereCondition).Count();
        }


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Method
        public ContactRecordCloseViewModel GetContactRecordCloseDetails(int ContactID)
        {
            var objResult = ObjectEntity1.Contact_Record_details(ContactID);
            ContactRecordCloseViewModel objContactRecordCloseViewModel = new ContactRecordCloseViewModel();

            if (objResult != null)
            {
                foreach (var entity in objResult.ToList())
                {
                    objContactRecordCloseViewModel.ContactID = entity.ContactID;
                    objContactRecordCloseViewModel.AptTypeDesc = entity.AptTypeDesc;
                    objContactRecordCloseViewModel.PatientID = entity.PatientID;
                    objContactRecordCloseViewModel.FirstName = entity.FirstName;
                    objContactRecordCloseViewModel.LastName = entity.LastName;
                    objContactRecordCloseViewModel.EnteredBy = entity.EnteredBy;
                    objContactRecordCloseViewModel.ContactDateEntered = entity.ContactDateEntered;
                    objContactRecordCloseViewModel.FollowUP_Completed = entity.FollowUP_Completed;
                    objContactRecordCloseViewModel.FollowUp_ActualDate = entity.FollowUp_ActualDate;
                    objContactRecordCloseViewModel.FollowUp_Date = entity.FollowUp_Date;
                    objContactRecordCloseViewModel.MessageBody = entity.MessageBody;
                    objContactRecordCloseViewModel.ReqsFollow_YN = entity.ReqsFollow_YN;
                    objContactRecordCloseViewModel.FollowUpBody = entity.FollowUpBody;
                    objContactRecordCloseViewModel.Cat_Desc = entity.Cat_Desc;

                }
            }
            return objContactRecordCloseViewModel;
        }
        #endregion


        #region Save New Contact Detail

        public void InsertContactDetail(Contact_tbl objEntity)
        {
            ObjectEntity1.contact_tbl_EMR_Insert(objEntity.AptType, objEntity.PatientID, objEntity.MessageBody, objEntity.EnteredBy, objEntity.Apt_ID);
        }

        #endregion


        #region Select All Contact Types

        public List<Emrdev.ViewModelLayer.Contact_Type_tblViewModel> SelectAllContactType()
        {
            return ObjectEntity1.Contact_Type_tbl.Where(i => i.Viewable_yn.Value).OrderBy(i => i.AptTypeDesc).Select(i => new Emrdev.ViewModelLayer.Contact_Type_tblViewModel { AptTypeID = i.AptTypeID, AptTypeDesc = i.AptTypeDesc, Viewable_yn = i.Viewable_yn }).ToList();
        }

        #endregion


    }
}
