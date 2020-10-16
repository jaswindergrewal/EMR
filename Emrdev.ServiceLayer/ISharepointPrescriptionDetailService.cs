using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using System.Runtime.Serialization;

namespace Emrdev.ServiceLayer
{
    [ServiceContract]
    public interface ISharepointPrescriptionDetailService
    {
        [OperationContract]
        void InsertSharepointPrescriptionDetail(SharepointPrescriptionDetailAddEditViewModel viewModelSharepointPrescriptionDetail);
        
        [OperationContract]
        void DoWork();

        [OperationContract]
        void UpdateSharepointPrescriptionDetail(SharepointPrescriptionDetailAddEditViewModel EditviewModelSharepointPrescriptionDetail);

        [OperationContract]
        List<SharepointPrescriptionDetailViewModel> GetAllSharepointPrescriptionDetailList(int ID);

        [OperationContract]
        List<SharepointPrescriptionDetailViewModel> GetAllSharepointPrescriptionDetailReport(string PatientName, string Clinic, string Physician, DateTime? LastRefill, DateTime? MedStartDate, bool IsDiet, bool IsMedical);

        [OperationContract]
        void InsertUpdateSharePointPrescriptionDetail(int PresciptionId, string PatientName, string Clinic, string Vials, DateTime? LastRefill, DateTime? MedStartDate, string Physician, string Comments, string Diet, string Medical);
             
        [OperationContract]
        void AddUpdateSharePointPrescriptionDetail(string rssURL);
    }
}
