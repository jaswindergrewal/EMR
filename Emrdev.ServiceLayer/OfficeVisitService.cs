using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
   // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OfficeVisitUpdate_Short" in both code and config file together.
    public class OfficeVisitService : IOfficeVisitService
    {
        OfficeVisitUpdate_ShortBAL obj = new OfficeVisitUpdate_ShortBAL();


        public List<Sympt> GetSymptom(int AptID)
        {
            return obj.GetSymptom(AptID);
        }

        public List<Sympt> GetGoalItem(int AptID)
        {
            return obj.GetGoalItem(AptID);
        }

        public List<Sympt> GetApt_Symptoms(int AptID)
        {
            return obj.GetApt_Symptoms(AptID);
        }
        public List<Sympt> GetApt_Goals(int AptID)
        {
            return obj.GetApt_Goals(AptID);
        }
        public List<OfficeVisitViewModel> GetOfficeVisitDetails(int AptID)
        {
            return obj.GetOfficeVisitDetails(AptID);
        }

        public void InsetOfficeVisit(apt_BloodWork bloodWork, apt_LisfeStyle life, apt_Misc misc)
        { 
            obj.InsetOfficeVisit(bloodWork, life, misc);
        }

        public void InsertSymptoms(List<apt_Symtpoms> symptom, int AptID)
        {
            obj.InsertSymptoms(symptom, AptID);
        }

        public void InsertGoals(List<apt_Goals> Goals, int AptID)
        {
            obj.InsertGoals(Goals, AptID);
        }
    }
   
}
