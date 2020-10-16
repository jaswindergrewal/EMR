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
    public class ProtocolBAL
    {
        ProtocolDAL objDAL = new ProtocolDAL();

        /// <summary>
        /// Get the list of all the protocols 
        /// jaswinder on 6th Aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<ProtocolViewModel> GetProtocolList(int PageIndex, int PageSize)
        {
            return objDAL.GetProtocolList(PageIndex, PageSize);
        }

        /// <summary>
        /// Get the protocols details by protocol id
        /// jaswinder on 6th Aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <returns></returns>
        public ProtocolViewModel GetProtocolByID(int ProtocolId)
        {
            var _objProtocol = new ProtocolViewModel();
            var ProtocolEntity = new Protocol();
            ProtocolEntity = objDAL.Get<Protocol>(o => o.Protocol_ID == ProtocolId);
            Mapper.CreateMap<Protocol, ProtocolViewModel>();
            _objProtocol = Mapper.Map(ProtocolEntity, _objProtocol);
            return _objProtocol;
        }

        /// <summary>
        /// Get all the protocol symptoms by protocolid
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <returns></returns>
        public List<SymptomViewModel> GetProtocolSymptoms(int ProtocolId)
        {
            return objDAL.GetProtocolSymptoms(ProtocolId);
        }

        /// <summary>
        /// Get all the dignosis by protocolis
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <returns></returns>
        public List<DiagnosistblViewModel> GetProtocolDiagnosis(int ProtocolId)
        {
            return objDAL.GetProtocolDiagnosis(ProtocolId);
        }


        /// <summary>
        /// Delete protocol diagnosis
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="DiagnosisId"></param>
        public void DeleteProtocolDiagnosis(int ProtocolId, int DiagnosisId)
        {
            objDAL.DeleteProtocolDiagnosis(ProtocolId, DiagnosisId);
        }

        /// <summary>
        /// Delete protocol Symptoms
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="SymptomId"></param>
        public void DeleteProtocolSymptoms(int ProtocolId, int SymptomId)
        {
            objDAL.DeleteProtocolSymptoms(ProtocolId, SymptomId);
        }

        /// <summary>
        /// insert new protocol symptom
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="SymptomId"></param>
        /// <returns></returns>
        public int InsertProtocolSymptoms(int ProtocolId, int SymptomId)
        {
            return objDAL.InsertProtocolSymptoms(ProtocolId, SymptomId);
        }

        /// <summary>
        /// insert protocol diagnosis
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="SymptomId"></param>
        /// <returns></returns>
        public int InsertProtocolDiagnosis(int ProtocolId, int SymptomId)
        {
            return objDAL.InsertProtocolDiagnosis(ProtocolId, SymptomId);
        }
        
        /// <summary>
        /// Insert Symptom in symptom table
        /// jaswinder 07th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public int InsertSymptoms(string SymptomText)
        {
            return objDAL.InsertSymptoms(SymptomText);
        }

        /// <summary>
        /// Get Symptom list 
        /// jaswinder 07th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public List<SymptomViewModel> BindSymptomList(int PageIndex, int PAGE_SIZE)
        {
            return objDAL.BindSymptomList(PageIndex, PAGE_SIZE);
        }


        /// <summary>
        /// Insert Diagnosis details
        /// jaswinder 08th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public int InsertDiagnosis(string ICDCode, string Diagnosis, bool Viewable)
        {
            return objDAL.InsertDiagnosis(ICDCode, Diagnosis, Viewable);
        }

        /// <summary>
        /// Get Diagnosis list 
        /// jaswinder 07th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public List<DiagnosistblViewModel> BindDiagnosisList(int PageIndex, int PageSize)
        {
            return objDAL.BindDiagnosisList(PageIndex, PageSize);
        }

        /// <summary>
        /// Get diagnosis details by ID
        /// jaswinder 7th aug 2013
        /// </summary>
        /// <param name="DiagnosisId"></param>
        /// <returns></returns>
        public DiagnosistblViewModel GetDiagnosisByID(int DiagnosisId)
        {
            var _objDiag = new DiagnosistblViewModel();
            var DiagEntity = new Diagnosis_tbl();
            DiagEntity = objDAL.Get<Diagnosis_tbl>(o => o.Diagnosis_ID == DiagnosisId);
            Mapper.CreateMap<Diagnosis_tbl, DiagnosistblViewModel>();
            _objDiag = Mapper.Map(DiagEntity, _objDiag);
            return _objDiag;
            
        }

        /// <summary>
        /// Update Diagnosis detail
        /// jaswinder 7th aug 2013
        /// </summary>
        /// <param name="ICDCode"></param>
        /// <param name="Diagnosis"></param>
        /// <param name="Viewable"></param>
        /// <param name="DiagnosisId"></param>
        /// <returns></returns>
        public int UpdateDiagnosis(string ICDCode, string Diagnosis, bool Viewable, int DiagnosisId)
        {
            return objDAL.UpdateDiagnosis(ICDCode, Diagnosis, Viewable, DiagnosisId);
        }

        /// <summary>
        /// Add protocol details
        /// Jaswinder 16th aug 2013
        /// </summary>
        /// <param name="ProtocolView"></param>
        public void AddProtocolDetails(ProtocolViewModel ProtocolView)
        {
            Protocol protocolEntity = new Protocol();
            Mapper.CreateMap<ProtocolViewModel, Protocol>();
            protocolEntity = Mapper.Map(ProtocolView, protocolEntity);
            objDAL.Create(protocolEntity);
        }

        /// <summary>
        /// Update Protocol Details
        /// Jaswinder 16 th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="Content"></param>
        /// <param name="Title"></param>
        public void UpdateProtocolDetails(int ProtocolId, string Content, string Title)
        {
            objDAL.UpdateProtocolDetails(ProtocolId, Content, Title);
        }

        
        /// <summary>
        /// Delete the protocol Details
        /// Jaswinder 16 th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        public void DeleteProtocol(int ProtocolId)
        {
            objDAL.DeleteProtocol(ProtocolId);
        }

        public List<ICD10CodesViewmodel> GetIcd10Codes()
        {
            return objDAL.GetIcd10Codes();
        }

        public void InserUpdateIcd10Codes(ICD10CodesViewmodel IcdCode)
        {
            objDAL.InserUpdateIcd10Codes(IcdCode);
        }
        public List<ICD10CodesViewmodel> GetPatientIcd10Codes(int AptID, int PatientID)
        {
            return objDAL.GetPatientIcd10Codes(AptID, PatientID);
        }
        public List<ICD10CodesViewmodel> GetPatientIcd10CodesSuppliments(int AptID, int PatientID)
        {
            return objDAL.GetPatientIcd10CodesSuppliments(AptID, PatientID);
        }
        public void InsertPatientIcdCodes(int PatientID, int AptID, string IcdCode)
        {
            objDAL.InsertPatientIcdCodes(PatientID, AptID, IcdCode);
        }

        public List<ICD10CodesViewmodel> GetPatientAssessIcd10Codes(int AptID, int PatientID)
        {
            return objDAL.GetPatientAssessIcd10Codes(AptID, PatientID);
        }
    }
}
