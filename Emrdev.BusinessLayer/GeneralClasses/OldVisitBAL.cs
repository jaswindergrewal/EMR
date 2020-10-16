using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using AutoMapper;

namespace Emrdev.GeneralClasses
{
    public class OldVisitBAL
    {
        OldVisitDAL objDAL=new OldVisitDAL();
        public List<OldVisitViewModel> GetOldVisits(int PatientID)
        {
            var _objOldVisitList = new List<OldVisitViewModel>();
            var OldVisitEntity = new List<Visit>();
            OldVisitEntity = objDAL.GetAll<Visit>(o => o.PatientID == PatientID).ToList();
            Mapper.CreateMap<Visit, OldVisitViewModel>();
            _objOldVisitList = Mapper.Map(OldVisitEntity, _objOldVisitList);
            return _objOldVisitList;
        }

        public List<CallbacksoldViewModel> GetOldNotes(int PatientID)
        {
            var _objOldNotesList = new List<CallbacksoldViewModel>();
            var OldNotesEntity = new List<Callbacks_old>();
            OldNotesEntity = objDAL.GetAll<Callbacks_old>(o => o.PatientID == PatientID).ToList(); 
            Mapper.CreateMap<Callbacks_old, CallbacksoldViewModel>();
            _objOldNotesList = Mapper.Map(OldNotesEntity, _objOldNotesList);
            return _objOldNotesList;
        }
    
    }
}
