using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class PatientVitalsBAL
    {
        PatientVitalsDAL objDAL = new PatientVitalsDAL();

        /// <summary>
        /// Method get the vital details for the patient by vitalid
        /// </summary>
        /// <param name="VitalID"></param>
        /// <returns></returns>
        public PatientVitalsViewModel GetPatientVitalsByVitalId(int VitalID)
        {
            var _objpatientList = new PatientVitalsViewModel();
            var PatientEntity = new Patient_Vitals();
            PatientEntity = objDAL.Get<Patient_Vitals>(o => o.Vital_ID == VitalID);
            Mapper.CreateMap<Patient_Vitals, PatientVitalsViewModel>();
            _objpatientList = Mapper.Map(PatientEntity, _objpatientList);
            return _objpatientList;
        }
        
        /// <summary>
        /// Update the selected Vital details
        /// </summary>
        /// <param name="theVital"></param>
        /// <param name="VitalID"></param>
        /// <returns></returns>
        public int UpdatePatientVitals(PatientVitalsViewModel theVital, int VitalID)
        {
            try
            {
                Patient_Vitals PatientEntity = objDAL.Get<Patient_Vitals>(o => o.Vital_ID == VitalID);
                theVital.Patient_ID = PatientEntity.Patient_ID;
                theVital.active = true;
                theVital.DateEntered = PatientEntity.DateEntered;
                Mapper.CreateMap<PatientVitalsViewModel, Patient_Vitals>();
                PatientEntity = Mapper.Map(theVital, PatientEntity);
                objDAL.Edit(PatientEntity);
                return 1;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the list of all the active vitals for the patients by patientid
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<PatientVitalsViewModel> GetPatientVitalsByPatientId(int PatientID)
        {
            return objDAL.GetPatientVitalsByPatientId(PatientID);
        }

        /// <summary>
        /// Insert the vital details .
        /// 14th aug 2013 -Jaswinder
        /// </summary>
        /// <param name="vitalViewModel"></param>
        /// <returns></returns>
        public int InsertPatientVitalDetails(PatientVitalsViewModel vitalViewModel)
        {
            try
            {
                Patient_Vitals PatientEntity = new Patient_Vitals();               
                vitalViewModel.active = true;
                Mapper.CreateMap<PatientVitalsViewModel, Patient_Vitals>();
                PatientEntity = Mapper.Map(vitalViewModel, PatientEntity);
                objDAL.Create(PatientEntity);
                return 1;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Method to inActive patient vital by vital id
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteVitalsByID(int ID)
        {           
            Patient_Vitals PatientEntity = objDAL.Get<Patient_Vitals>(o => o.Vital_ID == ID);           
            PatientEntity.active = false;                      
            objDAL.Edit(PatientEntity);
        }
    }
}
