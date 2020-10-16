using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using AutoMapper;
using Emrdev.DataLayer.GeneralClasses;

namespace Emrdev.BusinessLayer.GeneralClasses
{
   public class RenewalExceptionBAL
    {
        RenewalExceptionDAL objDAL = new RenewalExceptionDAL();
        public PatientViewModel GEtPatientByID(int PatientID)
        {

            var _objpatientList = new PatientViewModel();
            var PatientEntity = new Patient();
            PatientEntity = objDAL.Get<Patient>(o => o.PatientID == PatientID);

            Mapper.CreateMap<Patient, PatientViewModel>();
            _objpatientList = Mapper.Map(PatientEntity, _objpatientList);
            return _objpatientList;
        }


        public StaffViewModel GetStaffByStaffID(int StaffID)
        {
            var _objStaffList = new StaffViewModel();
            var StaffEntity = new Staff();
            StaffEntity = objDAL.Get<Staff>(o => o.EmployeeID == StaffID);

            Mapper.CreateMap<Staff, StaffViewModel>();
            _objStaffList = Mapper.Map(StaffEntity, _objStaffList);
            return _objStaffList;
        }


        public void UpdatePatientRenewalException(PatientViewModel pat)
        {
            Mapper.CreateMap<PatientViewModel, Patient>();
            var PatientEntity = new Patient();
            PatientEntity = Mapper.Map(pat, PatientEntity);
            objDAL.Edit(PatientEntity);
        }
   
   }
}
