using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EndMedical" in both code and config file together.
    public class EndMedicalService : IEndMedicalService
    {
        EndMedicalBAL objEndMedicalBAL = new EndMedicalBAL();
        public void DoWork()
        {
        }

        public List<EndMedicalViewModel> GetEndMedicalDetails(string ClinicName)
        {
            List<EndMedicalViewModel> lstObject = objEndMedicalBAL.GetEndMedicalDetails(ClinicName);
            return lstObject;
        }


        public void InsertEndMedical(PatientViewModel clsContact)
        {
            objEndMedicalBAL.InsertEndMedical(clsContact);
        }

        public void CloseAppointments(int ID, int PatientID, string Text, int StaffID, int AptID, bool CboAutoShip, DateTime DateEntered, string ReasonToClose)
        {

            objEndMedicalBAL.CloseAppointments(ID, PatientID, Text, StaffID, AptID, CboAutoShip, DateEntered, ReasonToClose);
        }
    }
}
