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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SymptomService" in both code and config file together.
    public class SymptomService : ISymptomService
    {
        SymptomBAL objSymptomBAL = new SymptomBAL();
        public void DoWork()
        {
        }

        public List<SymptomSupplementViewModel> GetSymptomSupplement(int SymptomID, int SupplementID)
        {
            List<SymptomSupplementViewModel> objViewModel = objSymptomBAL.GetSymptomSupplement(SymptomID, SupplementID);
            return objViewModel;
        }


        public int GetSymptomId()
        {
            int i = objSymptomBAL.GetSymptomId();
            return i;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateSymptomList(int SymptomId)
        {
            List<AutoshipProductsForSyymptomViewModel> objLst = objSymptomBAL.PopulateSymptomList(SymptomId);
            return objLst;
        }

        public int GetDiagnosisID()
        {
            int i = objSymptomBAL.GetDiagnosisID();
            return i;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateDiagList(int DiagnosisId)
        {
            List<AutoshipProductsForSyymptomViewModel> objLst = objSymptomBAL.PopulateDiagList(DiagnosisId);
            return objLst;
        }

        public int GetRangeID()
        {
            int i = objSymptomBAL.GetRangeID();
            return i;
        }

        public List<AutoshipProductsForSyymptomViewModel> PopulateLabList(int GroupRangeId)
        {
            List<AutoshipProductsForSyymptomViewModel> objLst = objSymptomBAL.PopulateLabList(GroupRangeId);
            return objLst;
        }

        public void AddGroupRange(GroupRangeViewModel viewModelQB)
        {
            objSymptomBAL.AddGroupRange(viewModelQB);
        }


        public dynamic BindRangeListBox()
        {
           return objSymptomBAL.BindRangeListBox();
        }


        public void DeleteSymptomSupplement(int SymptomId)
        {
            objSymptomBAL.DeleteSymptomSupplement(SymptomId);
        }

        public void InsertSymptomSupplement(SymptomSupplementViewModel viewModelQB)
        {
            objSymptomBAL.InsertSymptomSupplement(viewModelQB);
        }


        public void DeleteDiagnosisSupplements(int Diagnosis_ID)
        {
            objSymptomBAL.DeleteDiagnosisSupplements(Diagnosis_ID);
        }

        public void InsertDiagnosisSupplement(DiagnosisSupplementViewModel viewModelDS)
        {
            objSymptomBAL.InsertDiagnosisSupplement(viewModelDS);
        }


        public void DeleteGroupRangeSupplement(int GroupRangeID)
        {
            objSymptomBAL.DeleteGroupRangeSupplement(GroupRangeID);
        }

        public void InsertGroupRangeSupplement(GroupRangeSupplementViewModel viewModelGR)
        {
            objSymptomBAL.InsertGroupRangeSupplement(viewModelGR);
        }
    }
}
