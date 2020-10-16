using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class EndMedicalBAL
    {
        EndMedicalDAL objEndMedicalDAL = new EndMedicalDAL();

        public List<EndMedicalViewModel> GetEndMedicalDetails(string ClinicName)
        {
            List<EndMedicalViewModel> lstObject = objEndMedicalDAL.GetEndMedicalDetails(ClinicName);
            return lstObject;
        }

        public void InsertEndMedical(PatientViewModel clsContact)
        {
            //PatientViewModel clsContact = new PatientViewModel();            
            //clsContact.EndMedical = EndMedicalDate;
            //clsContact.PatientID = PatientId;
            Patient cls = new Patient();
            //cls.EndMedical = clsContact.EndMedical;
            AutoMapper.Mapper.CreateMap<PatientViewModel, Patient>();
            cls = AutoMapper.Mapper.Map(clsContact, cls);

            objEndMedicalDAL.Edit(cls);
        }

        public void CloseAppointments(int ID, int PatientID, string Text, int StaffID, int AptID, bool CboAutoShip, DateTime DateEntered,string ReasonToClose)
        {
         
            objEndMedicalDAL.CloseAppointments(ID, PatientID, Text, StaffID, AptID,CboAutoShip,DateEntered);
            //Close Appointments
            List<apt_rec> apts = objEndMedicalDAL.GetAll<apt_rec>(o => o.patient_id == PatientID).ToList();
            string EmployeeName;
            foreach (apt_rec a in apts)
            {
                a.closed_yn = true;
                a.closed_time = DateTime.Now;
                a.ActionNeeded = "Delete";
                Staff objStaff = objEndMedicalDAL.Get<Staff>(o => o.EmployeeID == StaffID);
                EmployeeName = objStaff.EmployeeName;
                a.ReasonToClose = ReasonToClose;
                objEndMedicalDAL.AddContactDetails(57, a.patient_id.Value, "Appointment deleted. Date/Time" + a.ApptStart + ". Deleted by " + EmployeeName + "." +" Reason "+ ReasonToClose +".", StaffID, a.apt_id);
            }
            

        }
    }
}
