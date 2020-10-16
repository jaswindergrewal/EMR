using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISymptomService" in both code and config file together.
    [ServiceContract]
    public interface ISymptomService
    {
        [OperationContract]
        List<SymptomSupplementViewModel> GetSymptomSupplement(int SymptomID, int SupplementID);

        [OperationContract]
        int GetSymptomId();

        [OperationContract]
        List<AutoshipProductsForSyymptomViewModel> PopulateSymptomList(int SymptomId);

        [OperationContract]
        int GetDiagnosisID();

        [OperationContract]
        List<AutoshipProductsForSyymptomViewModel> PopulateDiagList(int DiagnosisId);

        [OperationContract]
        int GetRangeID();

        [OperationContract]
        List<AutoshipProductsForSyymptomViewModel> PopulateLabList(int GroupRangeId);

        [OperationContract]
        void AddGroupRange(GroupRangeViewModel viewModelQB);

        [OperationContract]
        dynamic BindRangeListBox();

        [OperationContract]
        void DeleteSymptomSupplement(int SymptomId);

        [OperationContract]
        void InsertSymptomSupplement(SymptomSupplementViewModel viewModelQB);

        [OperationContract]
        void DeleteDiagnosisSupplements(int Diagnosis_ID);

        [OperationContract]
        void InsertDiagnosisSupplement(DiagnosisSupplementViewModel viewModelDS);

        [OperationContract]
        void DeleteGroupRangeSupplement(int GroupRangeID);

        [OperationContract]
        void InsertGroupRangeSupplement(GroupRangeSupplementViewModel viewModelGR);

        [OperationContract]
        void DoWork();
    }
}
