using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class IntakeDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

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
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
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
            return ObjectEntity1.Set<T>().Where(whereCondition).Count<T>();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }

      


        #endregion

        public void InsertIntakeSymptoms(int PatientId, int MasterId, string YrStr, string OtherSymptoms)
        {

            ObjectEntity1.ssp_InsertIntakeSymptoms(PatientId, MasterId, YrStr, OtherSymptoms);
        }

        public void InsertIntakeGoals(int PatientId, int MasterId, string ChkGoalStr, string LessPain, string OtherSymptoms)
        {
            ObjectEntity1.ssp_InsertIntakeGoals(PatientId, MasterId, ChkGoalStr, LessPain, OtherSymptoms);
        }


        public void InsertIntakeSuppliments(int PatientId, int MasterId, string ChkStr, string Other)
        {
            ObjectEntity1.ssp_InsertIntakeSuppliements(PatientId, MasterId, ChkStr, Other);
        }

        public List<IntakePrescriptionViewModel> GetPatientprescription(int PatientID)
        {
            var objResult = ObjectEntity1.ssp_Getintakeprescription(PatientID).ToList();
            var objIList = new List<IntakePrescriptionViewModel>();
            Mapper.CreateMap<ssp_Getintakeprescription_Result, IntakePrescriptionViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void UpdateIntakeForm(Emrdev.ViewModelLayer.IntakeServiceViewModel objModel)
        {
            //Update Intake Form 
            intake_form_goals objEntity = new intake_form_goals();
            objEntity.goal_id = objModel.GoalId;
            objEntity.master_form_id = objModel.MasterFormId;
            objEntity.patient_id = objModel.PatientId;
            objEntity.Date_Entered = objModel.DateEntered;
            objEntity.balance_hormones_YN = objModel.BalanceHormones;
            objEntity.improve_energy_YN = objModel.ImproveEnergy;
            objEntity.feel_better_YN = objModel.FeelBetter;
            objEntity.feel_stronger_YN = objModel.FeelStronger;
            objEntity.improve_sleep_YN = objModel.ImproveSleep;
            objEntity.eliminate_hot_flashes_YN = objModel.EliminateHotFlashes;
            objEntity.eliminate_prescriptions_YN = objModel.EliminatePrescriptions;
            objEntity.weight_loss_YN = objModel.WeightLoss;
            objEntity.stabalize_PMS_YN = objModel.StabalizePMS;
            objEntity.stop_hair_loss_YN = objModel.StopHairLoss;
            objEntity.sense_of_well_being_YN = objModel.SenseOfWellBeing;
            objEntity.enhance_immune_sys_YN = objModel.EnhanceImmuneSys;
            objEntity.less_pain_YN = objModel.LessPain;
            objEntity.less_pain_where = objModel.LessPainWhere;
            objEntity.improve_libido_YN = objModel.ImproveLibido;
            objEntity.improve_sex_life_YN = objModel.ImproveSexLife_YN;
            objEntity.improve_muscle_YN = objModel.ImproveMuscle;
            objEntity.improve_memory_YN = objModel.ImproveMemory;
            objEntity.bladder_control_YN = objModel.BladderControl;
            objEntity.improve_skin_YN = objModel.ImproveSkin;
            objEntity.better_stamina_YN = objModel.BetterStamina;
            objEntity.general_wellness_YN = objModel.GeneralWellness;
            objEntity.reduce_stress_YN = objModel.ReduceStress;
            objEntity.improve_metabolism_YN = objModel.ImproveMetabolism;
            objEntity.start_hormones_YN = objModel.StartHormones;
            objEntity.other = objModel.other;
            Edit<intake_form_goals>(objEntity);
        }

        public Emrdev.ViewModelLayer.IntakeServiceViewModel GetByGoalId(int goalId)
        {
            return ObjectEntity1.intake_form_goals.Select(i => new Emrdev.ViewModelLayer.IntakeServiceViewModel
            {
                GoalId = i.goal_id,
                MasterFormId = i.master_form_id,
                PatientId = i.patient_id,
                DateEntered = i.Date_Entered,
                BalanceHormones = i.balance_hormones_YN,
                ImproveEnergy = i.improve_energy_YN,
                FeelBetter = i.feel_better_YN,
                FeelStronger = i.feel_stronger_YN,
                ImproveSleep = i.improve_sleep_YN,
                EliminateHotFlashes = i.eliminate_hot_flashes_YN,
                EliminatePrescriptions = i.eliminate_prescriptions_YN,
                WeightLoss = i.weight_loss_YN,
                StabalizePMS = i.stabalize_PMS_YN,
                StopHairLoss = i.stop_hair_loss_YN,
                SenseOfWellBeing = i.sense_of_well_being_YN,
                EnhanceImmuneSys = i.enhance_immune_sys_YN,
                LessPain = i.less_pain_YN,
                LessPainWhere = i.less_pain_where,
                ImproveLibido = i.improve_libido_YN,
                ImproveSexLife_YN = i.improve_sex_life_YN,
                ImproveMuscle = i.improve_muscle_YN,
                ImproveMemory = i.improve_memory_YN,
                BladderControl = i.bladder_control_YN,
                ImproveSkin = i.improve_skin_YN,
                BetterStamina = i.better_stamina_YN,
                GeneralWellness = i.general_wellness_YN,
                ReduceStress = i.reduce_stress_YN,
                ImproveMetabolism = i.improve_metabolism_YN,
                StartHormones = i.start_hormones_YN,
                other = i.other
            }).SingleOrDefault(j => j.GoalId == goalId);

        }
        
    }


}
