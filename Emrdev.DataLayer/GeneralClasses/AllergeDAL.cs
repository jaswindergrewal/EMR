using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AllergeDAL : ObjectEntity, IRepositary
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

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<AestheticNotesViewModel> GetAestheticNotes(int PatientID)
        {

            var objAestheticNotes = ObjectEntity1.ssp_AnestheticDetail(PatientID);
            List<AestheticNotesViewModel> _listAestheticNotes = new List<AestheticNotesViewModel>();

            foreach (var data in objAestheticNotes)
            {
                AestheticNotesViewModel oAestheticNotes = new AestheticNotesViewModel();
                oAestheticNotes.PatientID = data.PatientID;
                oAestheticNotes.AptTypeDesc = data.AptTypeDesc;
                oAestheticNotes.ContactDateEntered = data.ContactDateEntered;
                oAestheticNotes.ContactID = data.ContactID;
                oAestheticNotes.MessageBody = data.MessageBody;

                _listAestheticNotes.Add(oAestheticNotes);
            }
            return _listAestheticNotes;
        }

        public List<AnestheticFollowupViewModel> GetAestheticFollowups(int PatientID)
        {

            var objResult = ObjectEntity1.ssp_GetAnestheticFollowup(PatientID).ToList();
            var objIList = new List<AnestheticFollowupViewModel>();
            Mapper.CreateMap<ssp_GetAnestheticFollowup_Result, AnestheticFollowupViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Get all the details for anesthetic notes
        /// jaswinder on 4th sept 2013
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<AestheticNotesViewModel> GetAestheticNotesALL(int PatientID)
        {
            var objAestheticNotes = ObjectEntity1.AestheticNotes_All(PatientID);
            List<AestheticNotesViewModel> _listAestheticNotes = new List<AestheticNotesViewModel>();

            foreach (var data in objAestheticNotes)
            {
                AestheticNotesViewModel oAestheticNotes = new AestheticNotesViewModel();
                oAestheticNotes.EnteredBy = data.EnteredBy;
                oAestheticNotes.AptTypeDesc = data.AptTypeDesc;
                oAestheticNotes.ContactDateEntered = data.ContactDateEntered;
                oAestheticNotes.FirstName = data.FirstName;
                oAestheticNotes.LastName = data.LastName;
                oAestheticNotes.MessageBody = data.MessageBody;
                oAestheticNotes.PatientID = data.patientid;
                

                _listAestheticNotes.Add(oAestheticNotes);
            }
            return _listAestheticNotes;
        }


    }
}
