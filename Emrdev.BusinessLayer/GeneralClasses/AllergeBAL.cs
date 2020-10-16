using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
   
    public class AllergeBAL
    {
        AllergeDAL objDAL = new AllergeDAL();
        public PatientViewModel GetPatientByID(int PatientId)
        {
            var _objpatientList = new PatientViewModel();
            var PatientEntity = new Patient();
            PatientEntity = objDAL.Get<Patient>(o => o.PatientID == PatientId);

            Mapper.CreateMap<Patient, PatientViewModel>();
            _objpatientList = Mapper.Map(PatientEntity, _objpatientList);
            return _objpatientList;
        }

        public void UpdateAllergies(PatientViewModel pat)
        {
            Patient PatientEntity = new Patient();
            Mapper.CreateMap<PatientViewModel,Patient>();
            PatientEntity = Mapper.Map(pat, PatientEntity);
            objDAL.Edit(PatientEntity);
        }

        /// <summary>
        /// Get the anethetic followup for the patient in manage page
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<AestheticNotesViewModel> GetAestheticNotes(int PatientID)
        {
            return objDAL.GetAestheticNotes(PatientID);
        }

        /// <summary>
        /// Anethetic notes for the patient in manage page
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<AnestheticFollowupViewModel> GetAestheticFollowups(int PatientID)
        {
            return objDAL.GetAestheticFollowups(PatientID);
        }

        /// <summary>
        /// Get all the details for anesthetic notes
        /// jaswinder on 4th sept 2013
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<AestheticNotesViewModel> GetAestheticNotesALL(int PatientID)
        {
            return objDAL.GetAestheticNotesALL(PatientID);
        }
    }
}
