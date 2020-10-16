using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AdminDictationBAL
    {
        AdminDictationDAL objDAL = new AdminDictationDAL();

        //Method to get all the diagnosis where viewable=true on the basis of the diagname.
        //used for searching diagnosis name
        public List<Dictation_Diagnosis_ViewModel> GetDictationDiagnosis(string DiagnosisText)                                                      
        {
                     
            var _objDiagnosisList = new List<Dictation_Diagnosis_ViewModel>();
            var DiagnosisEntity = new List<Dictation_Diagnosis>();
            if (DiagnosisText == "")
            {
                DiagnosisEntity = objDAL.GetAll<Dictation_Diagnosis>(o => o.Viewable_YN == true).OrderBy(o => o.DiagnosisName).ToList();
            }
            else
            {
                DiagnosisEntity = objDAL.GetAll<Dictation_Diagnosis>(o => o.Viewable_YN == true && o.DiagnosisName.Contains(DiagnosisText)).OrderBy(o => o.DiagnosisName).ToList();
            }
            Mapper.CreateMap<Dictation_Diagnosis, Dictation_Diagnosis_ViewModel>();
            _objDiagnosisList = Mapper.Map(DiagnosisEntity, _objDiagnosisList);
            return _objDiagnosisList;
        }

        //Method to get the plan list on the basis of diagnosis id.
        public List<Dictation_Plan_ViewModel> GetDictationDiagnosisPlans(int DiagnosisId)
        {
            var Dictation_PlanDiagnosis = objDAL.List<Dictation_PlanDiagnosis>();
            var _objPlanlist = new List<Dictation_Plan_ViewModel>();
            var Dictation_Plan = objDAL.List<Dictation_Plan>();
            var PlanEntity = new List<Dictation_Plan>();
            PlanEntity = (from p in Dictation_Plan
                                                 join pd in Dictation_PlanDiagnosis on p.PlanID equals pd.PlanID
                                                 where pd.DiagnosisID == DiagnosisId
                                                 select p).ToList();

            Mapper.CreateMap<Dictation_Plan, Dictation_Plan_ViewModel>();
            _objPlanlist = Mapper.Map(PlanEntity, _objPlanlist);
            return _objPlanlist;
        }


        //Method to get all plan where viewable=true for tree nodes.
        //used for searching plans
        public List<Dictation_Plan_ViewModel> GetDictationPlans(string PlanText)
        {
            var  _objPlanList=new List<Dictation_Plan_ViewModel>();
            var PlanEntity = new List<Dictation_Plan>();
            if (string.IsNullOrEmpty(PlanText))
            {
                PlanEntity = objDAL.GetAll<Dictation_Plan>(o => o.Viewable_YN == true).OrderBy(o => o.PlanName).ToList();
            }
            else
            {
                PlanEntity = objDAL.GetAll<Dictation_Plan>(o => o.Viewable_YN == true && o.PlanName.Contains(PlanText)).OrderBy(o => o.PlanName).ToList();
            }

            Mapper.CreateMap<Dictation_Plan, Dictation_Plan_ViewModel>();
            _objPlanList = Mapper.Map(PlanEntity, _objPlanList);
            return _objPlanList;
        }

       
        //Method to insert new Dictaion diagnosis
        public void InsertDictationDiagnosis(Dictation_Diagnosis_ViewModel Diag)
        {

            Dictation_Diagnosis DiagnosisEntity = new Dictation_Diagnosis();
            Mapper.CreateMap<Dictation_Diagnosis_ViewModel, Dictation_Diagnosis>();
            DiagnosisEntity = AutoMapper.Mapper.Map(Diag, DiagnosisEntity);
            objDAL.Create(DiagnosisEntity);
        }


        //Method to Update existing Dictaion diagnosis record.
        public void UpdateDictationDiagnosis(int DiagnosisID, string DiagDesrcip, string DiagName, string Code, string KeyWords)
        {
            Dictation_Diagnosis _objdiag = objDAL.Get<Dictation_Diagnosis>(o => o.DiagnosisID == DiagnosisID);

            if (_objdiag != null)
            {
                _objdiag.DiagnosisDescrip = DiagDesrcip;
                _objdiag.DiagnosisName = DiagName;
                _objdiag.ICDCode = Code;
                _objdiag.KeyWords = KeyWords;
                objDAL.Edit(_objdiag);

            }
        }


        //Method to insert new Dictaion Plans
        public void InsertDictationPlans(Dictation_Plan_ViewModel Plans,int DiagnosisID)
        {
            Dictation_Plan PlanEntity = new Dictation_Plan();
            Mapper.CreateMap<Dictation_Plan_ViewModel, Dictation_Plan>();
            PlanEntity = AutoMapper.Mapper.Map(Plans, PlanEntity);
            objDAL.Create(PlanEntity);
            if (DiagnosisID != -1)
            {
                Dictation_PlanDiagnosis DiagPlanEntity = new Dictation_PlanDiagnosis();
                DiagPlanEntity.DiagnosisID = DiagnosisID;
                DiagPlanEntity.PlanID = PlanEntity.PlanID;
                objDAL.Create(DiagPlanEntity);
            }
           
        }


        //Method to Renove Dictaion Diagnosis
        public void DeleteDictationDiagnosis(int DiagnosisID)
        {
            objDAL.DeleteDictationDiagnosis(DiagnosisID);
        }


        //Method to get Dictaion Diagnosis by diagnosis id
        public List<Dictation_Diagnosis_ViewModel> GetDictationDiagnosisByID(int DiagnosisID)
        {
            var _objDictationDiagnosis=new List<Dictation_Diagnosis_ViewModel>();
            List<Dictation_Diagnosis> DictationDiagnosisEntity = objDAL.GetAll<Dictation_Diagnosis>(o => o.DiagnosisID == DiagnosisID).ToList();

            Mapper.CreateMap<Dictation_Diagnosis, Dictation_Diagnosis_ViewModel>();
            _objDictationDiagnosis = Mapper.Map(DictationDiagnosisEntity, _objDictationDiagnosis);
            return _objDictationDiagnosis;
        }


        //Method to Update Dictaion plan record
        public void UpdateDictationPlans(int PlanID, string rdoEditPlanCategory, string PlanDescrip, string PlanName, string KeyWords,int DiagnosisID)
        {
            Dictation_Plan _objEditPlan = objDAL.Get<Dictation_Plan>(o => o.PlanID == PlanID);

            if (_objEditPlan != null)
            {
                _objEditPlan.Category = rdoEditPlanCategory;
                _objEditPlan.PlanDescrip = PlanDescrip;
                _objEditPlan.PlanName = PlanName;
                _objEditPlan.KeyWords = KeyWords;
                objDAL.Edit(_objEditPlan);
                Dictation_PlanDiagnosis _objEditDiagPlan = objDAL.Get<Dictation_PlanDiagnosis>(o => o.PlanID == PlanID);
                if (_objEditDiagPlan != null)
                {
                    if (DiagnosisID != -1)
                    {
                        _objEditDiagPlan.DiagnosisID = DiagnosisID;
                        objDAL.Edit(_objEditDiagPlan);
                    }
                }
                else
                {
                    Dictation_PlanDiagnosis DiagPlanEntity = new Dictation_PlanDiagnosis();
                    DiagPlanEntity.DiagnosisID = DiagnosisID;
                    DiagPlanEntity.PlanID = PlanID;
                    objDAL.Create(DiagPlanEntity);
                }

            }
        }


        //Method to Delete Dictaion plan record 
        public void DeleteDictationPlan(int DiagnosisID, int PlanID, bool IsDiagnosis)
        {
            objDAL.DeleteDictationPlan(DiagnosisID, PlanID, IsDiagnosis);
        }


        //Method to get Dictaion plan by id 
        public List<Dictation_Plan_ViewModel> GetDictationPlanByID(int PlanID)
        {
            return objDAL.GetDictationPlanByID(PlanID);
        }

        //Method to get the count for the records in Dictation_PlanDiagnosis table
        public long GetCountForPlanDiagnosis(int DiagnosisID, int PlanID)
        {
            return objDAL.Count<Dictation_PlanDiagnosis>(o => o.PlanID == PlanID && o.DiagnosisID == DiagnosisID);
        }


        //MEthod to insert new record in 
        public void InsertDictaionDiagnosiPlan(Dictation_PlanDiagnosis_ViewModel PlnDiag)
        {
            Dictation_PlanDiagnosis PlanEntity = new Dictation_PlanDiagnosis();
            Mapper.CreateMap<Dictation_PlanDiagnosis_ViewModel, Dictation_PlanDiagnosis>();
            PlanEntity = AutoMapper.Mapper.Map(PlnDiag, PlanEntity);
            objDAL.Create(PlanEntity);
        }

        public string InsertDictation_Diagnosis(string DiagnosisDescrip, string DiagnosisName, string ICDCode, string KeyWords, bool Viewable_YN)
        {
            return objDAL.InsertDictation_Diagnosis(DiagnosisDescrip, DiagnosisName, ICDCode, KeyWords, Viewable_YN).ToString();
        }

        public string InsertDictation_Plan(string Category, string PlanDesc, string PlanName, string KeyWords, bool Viewable_YN)
        {
            return objDAL.InsertDictation_Plan(Category, PlanDesc, PlanName, KeyWords, Viewable_YN).ToString();
        }

        public string CheckExistDictation_Plan(string Category, string PlanName, int PlanId)
        {
            return objDAL.CheckExistDictation_Plan(Category, PlanName, PlanId).ToString();
        }

        public string ValidateAndUpdateDictation_Diagnosis(int DiagnosisID, string DiagnosisName, string ICDCode)
        {
            return objDAL.ValidateAndUpdateDictation_Diagnosis(DiagnosisID, DiagnosisName, ICDCode).ToString();
        }
    }
}
