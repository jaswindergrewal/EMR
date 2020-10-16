using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses 
{
    public class CampaignTypeDAL :ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart1.Set<T>().Add(entityToCreate);
            ObjectEntityPart1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            ObjectEntityPart1.Set<T>();
            ObjectEntityPart1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntityPart1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntityPart1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart1.Set<T>().Where(whereCondition).ToList<T>();
        }


        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<CRM_Campaigns_ViewModel> GetAllCrmCampaign()
        {
            var objResult = ObjectEntityPart1.ssp_GetCrm_Campaign().ToList();
            var objIList = new List<CRM_Campaigns_ViewModel>();
            Mapper.CreateMap<ssp_GetCrm_Campaign_Result, CRM_Campaigns_ViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InserMarketSource(int ProspectID, string MarketSource, bool UpdateFlag,int Isprospect)
        {
            ObjectEntityPart1.ssp_CRMMarketSourseInsert(ProspectID, MarketSource, UpdateFlag, Isprospect);
          
        }

        public string getMarketSource(int prospectID, int TabID)
        {

            ObjectParameter retunSrc = new ObjectParameter("retunSrc", typeof(global::System.String));
            retunSrc.Value = DBNull.Value;
            ObjectEntityPart1.ssp_GetMarketSourceID(prospectID, TabID, retunSrc);
            return retunSrc.Value.ToString();
        }

        public List<ManageGrdProspectViewModel> GetAllProspect()
        {
            var objResult = ObjectEntityPart1.ssp_GetAllProspect().ToList();
            var objIList = new List<ManageGrdProspectViewModel>();
            Mapper.CreateMap<ssp_GetAllProspect_Result, ManageGrdProspectViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public string AddProspectData(int ProspectID, int ChkEventTrue, DateTime EventDate, string EventName, int EventID, int chkMarketSource, string MarketSourceName, int MarketSourceID,string Email)
        {
            ObjectParameter outputParam = new ObjectParameter("outputParam", typeof(string));
            outputParam.Value = string.Empty;
            //ObjectParameter rowsCount = null;//new ObjectParameter("RowsCount", typeof(Int32));
           ObjectEntityPart1.ssp_CrmInterfaceMatchData(ProspectID, ChkEventTrue, EventDate, EventName, EventID, chkMarketSource, MarketSourceName, MarketSourceID,Email,outputParam);
            return outputParam.Value.ToString();
           

        }

        /// <summary>
        /// Method to delete all the selected prospects
        /// Jaswinder 13th may 2013
        /// </summary>
        /// <param name="ProspectID"></param>
        public void DeleteProspectAll(string ProspectID)
        {
            ObjectEntityPart1.ssp_DeleteAll_Prospect(ProspectID);
        }

        /// <summary>
        /// Method to get the post seminar appointmenttype
        /// </summary>
        /// <returns></returns>
        public List<AppointmentTypeModel> GetAppointmentTypes()
        {//emr2017
            //var objResult = ObjectEntityPart1.ssp_Get_CRMPostSeminarAppointmentType().ToList();
            var objIList = new List<AppointmentTypeModel>();
           // Mapper.CreateMap<ssp_Get_CRMPostSeminarAppointmentType_Result, AppointmentTypeModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        //Added by jaswinder for  inserting data in CRM Contact details entries
        public void InsertCRMContactsDetails(int StaffID, int FollowUpID, string Messagebody, int PatientID, int FollowupCategory, int AptID, int ? AptType, int CRMInsertType)
        {
            ObjectEntityPart1.ssp_InsertCRmContactDetails(StaffID, FollowUpID, Messagebody, PatientID, FollowupCategory, AptID, AptType, CRMInsertType);
        
        
        }

        public int insertSurveyQuestions(string Title, string Type, string FieldName)
        {
           
            ObjectParameter questionIDOUT = new ObjectParameter("questionIDOUT", typeof(global::System.Int32));
            questionIDOUT.Value = DBNull.Value;
            //emr2017
            //ObjectEntityPart1.ssp_insertPatientSurveyQuestions(Title, Type, FieldName, questionIDOUT);

            return Convert.ToInt32(questionIDOUT.Value);
        }

        public void insertSurveyQuestionsAnswer(string FieldName, string Answer, int patientID, DateTime createdDate,string EntryID, int ApptID)
        {//Emr2017
            //ObjectEntityPart1.ssp_insertPatientSurveyQuestionsAnswer(FieldName, Answer, patientID, createdDate, EntryID, ApptID);
        }

        public void UpdateQuestionAsActive(string ActiveQuestions)
        {//Emr2017
           // ObjectEntityPart1.ssp_UpdatePatientSurveyQuestions(ActiveQuestions);
        }


        public List<MergedPatientViewModel> GetMergedPatientRecord(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_GetListMergedPatients(page, rows, sidx, sord, IsSearch, SearchColumn, SearchText).ToList();
            var objIList = new List<MergedPatientViewModel>();
            //Mapper.CreateMap<ssp_GetListMergedPatients_Result, MergedPatientViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public bool UndoMergedPatient(int MergedPatientID)
        {
            bool isflag = true;
            try
            {//Emr2017
                //ObjectEntityPart1.Ssp_UndoPatientMerge(MergedPatientID);
            }
            catch(System.Exception ex)
            {
                isflag = false;
            }
            return isflag;
        }

        public List<MergedPatientViewModel> GetPatienstLisTotMerge(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_GetListofPatientsToMerge(page, rows, sidx, sord, IsSearch, SearchColumn, SearchText).ToList();
            var objIList = new List<MergedPatientViewModel>();
            //Mapper.CreateMap<ssp_GetListofPatientsToMerge_Result, MergedPatientViewModel>();
           // objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public bool MergedPatientData(int existingPatientID, int NewPatientID, int StaffID)
        {
            bool isflag = true;
            try
            {//emr2017
                //ObjectEntityPart1.Ssp_UpdatePatientListMerge(existingPatientID, existingPatientID, StaffID);
            }
            catch (System.Exception ex)
            {
                isflag = false;
            }
            return isflag;
        }

        public dynamic GetAllAttend(int EventID)
        {

            var objAttend = ObjectEntityPart1.ssp_Get_Attendent(EventID);
            return objAttend;

        }

        public List<StaffViewModel> GetStaffDetails(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText, int DepartmentID)
        {
            var objResult = ObjectEntityPart1.ssp_GetEmployeebyDepartmentID(page, rows, sidx, sord, IsSearch, SearchColumn, SearchText, DepartmentID).ToList();
            var objIList = new List<StaffViewModel>();
            Mapper.CreateMap<ssp_GetEmployeebyDepartmentID_Result, StaffViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<MyTicketsViewModel> GetMyTicketsCAL(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            var objMyTickets = ObjectEntityPart1.ssp_grdMyTicketsDataCal(StaffID, page, rows, ColName, sortorder);
            List<MyTicketsViewModel> _listMyTickets = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = data.Assigned;
                oMyTickets.Category = data.Category;
                oMyTickets.ChangeColor = data.ChangeColor;
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress > 1) ? "yes" : "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listMyTickets.Add(oMyTickets);
            }
            return _listMyTickets;
        }

        public List<MyTicketsViewModel> GetCreatedClosedCAL(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            var objMyTickets = ObjectEntityPart1.ssp_GetCreatedClosedCal(StaffID, page, rows, ColName, sortorder);
            List<MyTicketsViewModel> _listCreatedClosed = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = data.Assigned;
                oMyTickets.Category = data.Category;
                oMyTickets.ChangeColor = data.ChangeColor;
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress > 1) ? "yes" : "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listCreatedClosed.Add(oMyTickets);
            }
            return _listCreatedClosed;
        }

        public List<MyTicketsViewModel> GetMyActiveCAL(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            var objMyTickets = ObjectEntityPart1.ssp_GetMyActiveCalendar(StaffID, page, rows, ColName, sortorder);


            List<MyTicketsViewModel> _listMyActive = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = data.Assigned;
                oMyTickets.Category = data.Category;
                oMyTickets.ChangeColor = data.ChangeColor;
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress > 1) ? "yes" : "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listMyActive.Add(oMyTickets);
            }
            return _listMyActive;
        }



        public void DeleteExceptions(int orderid)
        {
            ObjectEntityPart1.ssp_DeleteExceptionsByOrderID(orderid);
          
        }

        public List<Patient_Details_ViewModel> GetSearchPatient(string SearchText)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_SearchPatients_By_text(SearchText).ToList();
            var objIList = new List<Patient_Details_ViewModel>();
            //Mapper.CreateMap<ssp_SearchPatients_By_text_Result, Patient_Details_ViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
