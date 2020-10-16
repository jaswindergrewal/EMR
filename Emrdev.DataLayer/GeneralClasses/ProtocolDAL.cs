using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class ProtocolDAL : ObjectEntity, IRepositary
    {

        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Methods"
        /// <summary>
        /// Get the staff protocol list
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<ProtocolViewModel> GetProtocolList(int PageIndex, int PageSize)
        {

            var objResult = ObjectEntity1.ssp_GetStaffProtocolList(PageIndex, PageSize).ToList();
            var objIList = new List<ProtocolViewModel>();
            Mapper.CreateMap<ssp_GetStaffProtocolList_Result, ProtocolViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Get all the protocol symptoms by protocolid
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <returns></returns>
        public List<SymptomViewModel> GetProtocolSymptoms(int ProtocolId)
        {
            var objResult = ObjectEntity1.ssp_GetSymptomsByProtocolId(ProtocolId).ToList();
            var objIList = new List<SymptomViewModel>();
            Mapper.CreateMap<ssp_GetSymptomsByProtocolId_Result, SymptomViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        /// <summary>
        /// Get all the dignosis by protocolis
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <returns></returns>
        public List<DiagnosistblViewModel> GetProtocolDiagnosis(int ProtocolId)
        {
            var objResult = ObjectEntity1.ssp_GetDiagnosisByProtocolId(ProtocolId).ToList();
            var objIList = new List<DiagnosistblViewModel>();
            Mapper.CreateMap<ssp_GetDiagnosisByProtocolId_Result, DiagnosistblViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        /// <summary>
        /// Delete protocol Diagnosis
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="DiagnosisId"></param>
        public void DeleteProtocolDiagnosis(int ProtocolId, int DiagnosisId)
        {
            ObjectEntity1.ssp_DeleteProtocolDiagnosis(ProtocolId, DiagnosisId);
        }

        /// <summary>
        /// Delete protocol Symptoms
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="SymptomId"></param>
        public void DeleteProtocolSymptoms(int ProtocolId, int SymptomId)
        {
            ObjectEntity1.ssp_DeleteProtocolSymptoms(ProtocolId, SymptomId);
        }

        /// <summary>
        /// Insert Protocol symptom and retun a single value from database
        /// jaswinder aug 6 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="SymptomId"></param>
        /// <returns></returns>
        public int InsertProtocolSymptoms(int ProtocolId, int SymptomId)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(ObjectEntity1.ssp_InsertProtocolSymptom(ProtocolId, SymptomId).Single());
                return result;
            }
            catch (System.Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// insert protocol diagnosis and retun a single value from database
        /// jaswinder 6th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        /// <param name="SymptomId"></param>
        /// <returns></returns>
        public int InsertProtocolDiagnosis(int ProtocolId, int SymptomId)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(ObjectEntity1.ssp_InsertProtocolDiagnosis(ProtocolId, SymptomId).Single());
                return result;
            }
            catch (System.Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// Insert Symptom in symptom table
        /// jaswinder 07th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public int InsertSymptoms(string SymptomText)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(ObjectEntity1.ssp_InsertSymptom(SymptomText).Single());
                return result;
            }
            catch (System.Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// Get Symptom list 
        /// jaswinder 07th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public List<SymptomViewModel> BindSymptomList(int PageIndex, int PAGE_SIZE)
        {
            var objResult = ObjectEntity1.ssp_GetSymptomsDetails(PageIndex, PAGE_SIZE).ToList();
            var objIList = new List<SymptomViewModel>();
            Mapper.CreateMap<ssp_GetSymptomsDetails_Result, SymptomViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        /// <summary>
        /// Insert Diagnosis details
        /// jaswinder 08th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public int InsertDiagnosis(string ICDCode, string Diagnosis, bool Viewable)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(ObjectEntity1.ssp_InsertDiagnosis(Diagnosis, Viewable,ICDCode ).Single());
                return result;
            }
            catch (System.Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// Get Diagnosis list 
        /// jaswinder 07th aug 2013
        /// </summary>
        /// <param name="SymptomText"></param>
        /// <returns></returns>
        public List<DiagnosistblViewModel> BindDiagnosisList(int PageIndex, int PageSize)
        {
            var objResult = ObjectEntity1.ssp_GetDiagnosisDetailsList(PageIndex, PageSize).ToList();
            var objIList = new List<DiagnosistblViewModel>();
            Mapper.CreateMap<ssp_GetDiagnosisDetailsList_Result, DiagnosistblViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
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
            int result = 0;
            try
            {
                result = Convert.ToInt32(ObjectEntity1.ssp_UpdateDiagnosis(Diagnosis, Viewable, ICDCode, DiagnosisId).Single());
                return result;
            }
            catch (System.Exception )
            {
                return result;
            }
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
            ObjectEntity1.ssp_UpdateProtocolDetails(ProtocolId, Title, Content);
        }


        /// <summary>
        /// Delete the protocol Details
        /// Jaswinder 16 th aug 2013
        /// </summary>
        /// <param name="ProtocolId"></param>
        public void DeleteProtocol(int ProtocolId)
        {
            ObjectEntity1.ssp_DeleteProtocol(ProtocolId);
        }


        public List<ICD10CodesViewmodel> GetIcd10Codes()
        {
            var objResult = ObjectEntity1.ssp_GetICDCodes().ToList();
            var objIList = new List<ICD10CodesViewmodel>();
            Mapper.CreateMap<ssp_GetICDCodes_Result, ICD10CodesViewmodel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InserUpdateIcd10Codes(ICD10CodesViewmodel IcdCode)
        {
            ObjectEntity1.ssp_InsertIcdCodes(IcdCode.Id,IcdCode.ICD10Code,IcdCode.Description,IcdCode.Gender,IcdCode.IsActive);
        }

        public List<ICD10CodesViewmodel> GetPatientIcd10Codes(int AptID, int PatientID)
        {
            var objResult = ObjectEntity1.ssp_GetPatientIcdCodes(PatientID,AptID).ToList();
            var objIList = new List<ICD10CodesViewmodel>();
            Mapper.CreateMap<ssp_GetPatientIcdCodes_Result, ICD10CodesViewmodel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<ICD10CodesViewmodel> GetPatientIcd10CodesSuppliments(int AptID, int PatientID)
        {
            var objResult = ObjectEntity1.ssp_GetPatientIcdCodesSuppliments(PatientID, AptID).ToList();
            var objIList = new List<ICD10CodesViewmodel>();
            Mapper.CreateMap<ssp_GetPatientIcdCodesSuppliments_Result, ICD10CodesViewmodel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertPatientIcdCodes(int PatientID, int AptID, string IcdCode)
        {
            ObjectEntity1.ssp_InsertPatientIcdCode(PatientID,  IcdCode, AptID);
        }

        public List<ICD10CodesViewmodel> GetPatientAssessIcd10Codes(int AptID, int PatientID)
        {
            var objResult = ObjectEntity1.ssp_GetPatientAssIcdCodes(PatientID, AptID).ToList();
            var objIList = new List<ICD10CodesViewmodel>();
            Mapper.CreateMap<ssp_GetPatientAssIcdCodes_Result, ICD10CodesViewmodel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        #endregion

    }

    
}
