using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class SymptomBAL
    {
        SymptomDAL objSymptomDAL = new SymptomDAL();

        public List<SymptomSupplementViewModel> GetSymptomSupplement(int SymptomID, int SupplementID)
        {
            var _objViewModel = new List<SymptomSupplementViewModel>();
            var _objEntity = new SymptomSupplement();
            _objEntity = objSymptomDAL.Get<SymptomSupplement>(o => o.SymptomID == SymptomID && o.SupplementID == SupplementID);

            Mapper.CreateMap<SymptomSupplement, SymptomSupplementViewModel>();
            _objViewModel = Mapper.Map(_objEntity, _objViewModel);
            return _objViewModel;
        }

        public int GetSymptomId()
        {
            int i = 0;
            //i = objSymptomDAL.Get<Symptom>(s => s.viewable_yn == true && s.SymptomName != string.Empty).SymptomID;
            i = objSymptomDAL.GetSymptomId();
            return i;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateSymptomList(int SymptomId)
        {
            List<AutoshipProductsForSyymptomViewModel> objLst = objSymptomDAL.PopulateSymptomList(SymptomId);
            return objLst;
        }

        public int GetDiagnosisID()
        {
            int i = 0;
            //i = objSymptomDAL.Get<Diagnosis_tbl>(s => s.Viewable_YN == true && s.Diag_Title != string.Empty).Diagnosis_ID;
            i = objSymptomDAL.GetDiagnosisID();
            return i;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateDiagList(int DiagnosisId)
        {
            List<AutoshipProductsForSyymptomViewModel> objLst = objSymptomDAL.PopulateDiagList(DiagnosisId);
            return objLst;
        }

        public int GetRangeID()
        {
            int i = 0;
            i = objSymptomDAL.GetRangeID();
            return i;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateLabList(int GroupRangeId)
        {
            List<AutoshipProductsForSyymptomViewModel> objLst = objSymptomDAL.PopulateLabList(GroupRangeId);
            return objLst;
        }

        public void AddGroupRange(GroupRangeViewModel viewModelQB)
        {
            GroupRange cls = new GroupRange();
            AutoMapper.Mapper.CreateMap<GroupRangeViewModel, GroupRange>();
            cls = AutoMapper.Mapper.Map(viewModelQB, cls);
            objSymptomDAL.Create(cls);
        }

        public dynamic BindRangeListBox()
        {
            var obj = objSymptomDAL.BindRangeListBox();
            return obj;
        }

        public void DeleteSymptomSupplement(int SymptomId)
        {
            objSymptomDAL.DeleteSymptomSupplement(SymptomId);
        }

        public void InsertSymptomSupplement(SymptomSupplementViewModel viewModelQB)
        {
            SymptomSupplement cls = new SymptomSupplement();
            AutoMapper.Mapper.CreateMap<SymptomSupplementViewModel, SymptomSupplement>();
            cls = AutoMapper.Mapper.Map(viewModelQB, cls);
            objSymptomDAL.Create(cls);
        }

        public void DeleteDiagnosisSupplements(int Diagnosis_ID)
        {
            objSymptomDAL.DeleteDiagnosisSupplements(Diagnosis_ID);
        }

        public void InsertDiagnosisSupplement(DiagnosisSupplementViewModel viewModelDS)
        {
            DiagnosisSupplement cls = new DiagnosisSupplement();
            AutoMapper.Mapper.CreateMap<DiagnosisSupplementViewModel, DiagnosisSupplement>();
            cls = AutoMapper.Mapper.Map(viewModelDS, cls);
            objSymptomDAL.Create(cls);
        }

        public void DeleteGroupRangeSupplement(int GroupRangeID)
        {
            objSymptomDAL.DeleteGroupRangeSupplement(GroupRangeID);
        }

        public void InsertGroupRangeSupplement(GroupRangeSupplementViewModel viewModelGR)
        {
            GroupRangeSupplement cls = new GroupRangeSupplement();
            AutoMapper.Mapper.CreateMap<GroupRangeSupplementViewModel, GroupRangeSupplement>();
            cls = AutoMapper.Mapper.Map(viewModelGR, cls);
            objSymptomDAL.Create(cls);
        }

    }
}
