using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using System.Data;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class OfficeVisitUpdate_ShortBAL
    {
     OfficeVisitUpdate_ShortDAL obj = new OfficeVisitUpdate_ShortDAL();

        //public dynamic GetSymptom(int AptID)
        //{
        //    var objSymptom = obj.GetSymptom(AptID);
        //    return objSymptom;
        //}

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


        public void InsetOfficeVisit( apt_BloodWork bloodWork,apt_LisfeStyle life,apt_Misc misc)
        {

            apt_BloodWork objBloodWork = obj.Get<apt_BloodWork>(o => o.AptID == bloodWork.AptID);
            if (objBloodWork != null)
            {
                obj.Delete(objBloodWork);

            }
            obj.Create(bloodWork);

            apt_LisfeStyle objLisfeStyle = obj.Get<apt_LisfeStyle>(o => o.AptID == life.AptID);
            if (objLisfeStyle != null)
            {
                obj.Delete(objLisfeStyle);

            }
            obj.Create(life);

            apt_Misc objmisc = obj.Get<apt_Misc>(o => o.AptID == misc.AptID);
            if (objmisc != null)
            {
                obj.Delete(objmisc);

            }
            obj.Create(misc);
        }

        public void InsertSymptoms(List<apt_Symtpoms> symptom, int AptID)
        {
           List<apt_Symtpoms> objsymptom = obj.GetAll<apt_Symtpoms>(o => o.AptID == AptID).ToList();
           foreach (var item in objsymptom)
           {
               obj.Delete(item);
           }
            //if (objsymptom != null)
            //{
            //    obj.Delete(objsymptom);

            //}
            foreach (var item in symptom)
            {
              
                obj.Create(item);
            }
        }

        public void InsertGoals(List<apt_Goals> Goals, int AptID)
        {
            List<apt_Goals> objGoals = obj.GetAll<apt_Goals>(o => o.AptID == AptID).ToList();
            foreach (var item in objGoals)
            {
                obj.Delete(item);
            }
            //apt_Goals objGoals = obj.Get<apt_Goals>(o => o.AptID == AptID);
            //if (objGoals != null)
            //{
            //    obj.Delete(objGoals);

            //}
            foreach (var item in Goals)
            {
               
                obj.Create(item);
            }
        }
       



    }
}
