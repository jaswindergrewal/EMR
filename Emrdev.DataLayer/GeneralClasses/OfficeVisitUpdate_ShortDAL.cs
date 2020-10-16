using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class OfficeVisitUpdate_ShortDAL : ObjectEntity, IRepositary
    {

        public List<Sympt> GetSymptom(int AptID)
        {
            var objSymptom = ObjectEntity1.ssp_lstSymptoms(AptID);
            List<Sympt> _listSymptom = new List<Sympt>();

            foreach (var a in objSymptom)
            {
                Sympt oSymptom = new Sympt();
                oSymptom.Symptom = a.Symptom.ToString();
                oSymptom.SymptomID = Convert.ToInt16(a.SymptomID);
                _listSymptom.Add(oSymptom);
            }

            return _listSymptom;
        }

        public List<Sympt> GetGoalItem(int AptID)
        {
            var objGoalItem = ObjectEntity1.ssp_lstGoals(AptID);
            List<Sympt> _listGoalItem = new List<Sympt>();

            foreach (var oItem in objGoalItem)
            {
                Sympt oGoalItem = new Sympt();
                oGoalItem.Symptom = oItem.DisplayName.ToString();
                //oGoalItem.DisplayOrder = Convert.ToInt16(oItem.DisplayOrder);
                oGoalItem.SymptomID = Convert.ToInt16(oItem.GoalItemID);

                _listGoalItem.Add(oGoalItem);
            }
            return _listGoalItem;
        }

        public List<Sympt> GetApt_Symptoms(int AptID)
        {
            var objApt_SymptomsItem = ObjectEntity1.ssp_lstApt_Symptoms(AptID);
            List<Sympt> _listSymptomsItem = new List<Sympt>();

            foreach (var oItem in objApt_SymptomsItem)
            {
                Sympt oSymItem = new Sympt();
                oSymItem.dir = oItem.dir;
                oSymItem.resolved = Convert.ToBoolean(oItem.Resolved);
                oSymItem.RowPosition = Convert.ToInt16(oItem.RowPosition);
                oSymItem.Symptom=oItem.Symptom;
                oSymItem.SymptomID=oItem.SymptomID;

                _listSymptomsItem.Add(oSymItem);
            }
            return _listSymptomsItem;
        }

        public List<Sympt> GetApt_Goals(int AptID)
        {
            var objApt_GoalItem = ObjectEntity1.ssp_lstApt_Goals(AptID);
            List<Sympt> _listGoalItem = new List<Sympt>();

            foreach (var oItem in objApt_GoalItem)
            {
                Sympt oSymItem = new Sympt();
                oSymItem.dir = oItem.dir;
                oSymItem.resolved = Convert.ToBoolean(oItem.Resolved);
                oSymItem.RowPosition = Convert.ToInt16(oItem.RowPosition);
                oSymItem.Symptom = oItem.Symptom;
                oSymItem.SymptomID = oItem.GoalItemID;

                _listGoalItem.Add(oSymItem);
            }
            return _listGoalItem;
        }

        public List<OfficeVisitViewModel> GetOfficeVisitDetails(int AptID)
        {
            var objOfficeVisitDetails = ObjectEntity1.ssp_GetOfficeVistitDetails(AptID);
            List<OfficeVisitViewModel> _listItem = new List<OfficeVisitViewModel>();

            foreach (var item in objOfficeVisitDetails)
            {
                OfficeVisitViewModel oOfficevisitItem = new OfficeVisitViewModel();
                oOfficevisitItem.PatientId = item.patient_id;
                oOfficevisitItem.Anastrozole = (bool)item.Anastrozole;
                oOfficevisitItem.AnastrozoleComment = item.AnastrozoleComment;
                oOfficevisitItem.Creams = (bool)item.Creams;
                oOfficevisitItem.CreamComment = item.CreamComment;
                oOfficevisitItem.DHEA = (bool)item.DHEA;
                oOfficevisitItem.DHEAComment = item.DHEAComment;
                oOfficevisitItem.Fasting = (bool)item.Fasting;
                oOfficevisitItem.FastingComment = item.FastingComment;
                oOfficevisitItem.Pregenolone = (bool)item.Pregenolone;
                oOfficevisitItem.PregenoloneComment = item.PregenoloneComment;
                oOfficevisitItem.Testosterone = (bool)item.Testosterone;
                oOfficevisitItem.TestosteroneComment = item.TestosteroneComment;
                oOfficevisitItem.PregenoloneAMPM = item.PregenoloneAMPM;
                oOfficevisitItem.AptID=item.AptID;
                oOfficevisitItem.Diet=item.Diet;
                oOfficevisitItem.DietComment = item.DietComment;
                oOfficevisitItem.EnergeyLevel= item.EnergeyLevel;
                oOfficevisitItem.EnergyComment = item.EnergyComment;
                oOfficevisitItem.ExerciseFreq= item.ExerciseFreq;
                oOfficevisitItem.ExerciseFreqComment = item.ExerciseFreqComment;
                oOfficevisitItem.ExerciseType=item.ExerciseType;
                oOfficevisitItem.ExerciseTypeCommnet = item.ExerciseTypeCommnet;
                oOfficevisitItem.FruitVeggie= item.FruitVeggie;
                oOfficevisitItem.FruitVeggieComment = item.FruitVeggieComment;
                oOfficevisitItem.IntakeType= item.IntakeType;
                oOfficevisitItem.WaterSourceComment = item.WaterSourceComment;
                oOfficevisitItem.Mealtonin = item.Mealtonin;
                oOfficevisitItem.SleepHours = item.SleepHours;
                oOfficevisitItem.SleepQuality= item.SleepQuality;
                oOfficevisitItem.SleepQualityComment = item.SleepQualityComment;
                oOfficevisitItem.WaterIntake = item.WaterIntake.ToString();
                oOfficevisitItem.WaterSource= item.WaterSource;
                oOfficevisitItem.WaterIntakeComment = item.WaterIntakeComment;
                oOfficevisitItem.WorkoutLemgth=item.WorkoutLemgth;
                oOfficevisitItem.WorkoutLemgthComment = item.WorkoutLemgthComment;

                oOfficevisitItem.IllnessNames = item.IllnessNames;
                oOfficevisitItem.LastMammo = item.LastMammo;
                oOfficevisitItem.LastPap = item.LastPap;
                oOfficevisitItem.LastPhysical = item.LastPhysical;
                oOfficevisitItem.LastProstate = item.LastProstate;
                oOfficevisitItem.NewIllness = item.NewIllness;
                oOfficevisitItem.NewMedicationsYN = item.NewMedicationsYN;
                oOfficevisitItem.RealizeGoals = item.RealizeGoals;
                oOfficevisitItem.HappyProgram = item.HappyProgram;
                oOfficevisitItem.Other = item.Other;
                oOfficevisitItem.NewMedsNames = item.NewMedsNames;
                
                _listItem.Add(oOfficevisitItem);
            }
            return _listItem;
        }

        public void DeleteOfficeVisti(int AptID)
        {
            ObjectEntity1.ssp_Delete_Prospect(AptID);
        }

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #region IRepositary Members


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
