using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data;
using Emrdev.ViewModelLayer;
using System.Data.Entity.Infrastructure;


namespace Emrdev.DataLayer.GeneralClasses
{
    public class FreshBookAPIDAL : ObjectEntity, IRepositary
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
            ObjectEntityPart1.Set<T>().Remove(entityToDelete);
            ObjectEntityPart1.SaveChanges();
            //throw new NotImplementedException();
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
            return ObjectEntityPart1.Set<T>().Where(whereCondition).FirstOrDefault();
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


        public List<ssp_FreshBookGetOrdersnotMatchFB_Viewmodel> GetOrdersNotMatchonFB(int UpdateFlag, int page, int rows, string sord, string sidx)
        {
            var objResult = ObjectEntityPart1.ssp_FreshBookGetOrdersnotMatchFB(UpdateFlag, page, rows, sidx, sord).ToList();
            var objIList = new List<ssp_FreshBookGetOrdersnotMatchFB_Viewmodel>();
            Mapper.CreateMap<ssp_FreshBookGetOrdersnotMatchFB_Result, ssp_FreshBookGetOrdersnotMatchFB_Viewmodel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<FreshBookOrdersViewModel> GetFreshBookInvoiceByID(string InvoiceIDs)
        {
            var objResult = ObjectEntityPart1.ssp_FBGetOrdersbyOrderID(InvoiceIDs).ToList();
            var objIList = new List<FreshBookOrdersViewModel>();
            Mapper.CreateMap<ssp_FBGetOrdersbyOrderID_Result, FreshBookOrdersViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        public List<FreshBookInvoiceItemsViewModel> GetFreshBookInvoiceItemsByID(int InvoiceID)
        {
            var objResult = ObjectEntityPart1.ssp_FreshBookGetOrderItemsbyOrderID(InvoiceID).ToList();
            var objIList = new List<FreshBookInvoiceItemsViewModel>();
            Mapper.CreateMap<ssp_FreshBookGetOrderItemsbyOrderID_Result, FreshBookInvoiceItemsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<FreshBookPatientViewModel> GetFreshBookPatientsByID(string PatientID)
        {
            var objResult = ObjectEntityPart1.ssp_FreshBookGetpatientsbyPatientID(PatientID).ToList();
            var objIList = new List<FreshBookPatientViewModel>();
            Mapper.CreateMap<ssp_FreshBookGetpatientsbyPatientID_Result, FreshBookPatientViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList; ;
        }


        public List<ssp_FreshBookGetpatientsnotMatchFB_ViewModel> GetFreshBookPatients(int UpdateFlag, int page, int rows, string sord, string sidx)
        {
            var objResult = ObjectEntityPart1.ssp_FreshBookGetpatientsnotMatchFB(UpdateFlag, page, rows, sidx, sord).ToList();
            var objIList = new List<ssp_FreshBookGetpatientsnotMatchFB_ViewModel>();
            Mapper.CreateMap<ssp_FreshBookGetpatientsnotMatchFB_Result, ssp_FreshBookGetpatientsnotMatchFB_ViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public DateTime? GetLastContactFatchedDate()
        {
            DateTime? LAstFatchContactDateVal = new DateTime();
            try
            {
                var xeroLogList = ObjectEntityPart1.XeroLogs.ToList();
                if (xeroLogList.Count > 0)
                {
                    LAstFatchContactDateVal = xeroLogList.Skip(1).FirstOrDefault().UpdatedDateUTC;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {

            }
            return LAstFatchContactDateVal;
        }

        public void EditLastContactFatchedDate()
        {
            try
            {
                var xeroLogList = ObjectEntityPart1.XeroLogs.Where(x => x.LogFor == "FreshBook").ToList();
                if (xeroLogList.Count > 0)
                {
                    XeroLog xeroLog = xeroLogList.FirstOrDefault();
                    xeroLog.UpdatedDateUTC = DateTime.UtcNow.Date;
                    Edit<XeroLog>(xeroLog);
                }
                else
                {
                    XeroLog xeroLog = new XeroLog();
                    xeroLog.UpdatedDateUTC = DateTime.UtcNow.Date;
                    xeroLog.LogFor = "FreshBook";
                    Create<XeroLog>(xeroLog);
                }

            }
            catch (System.Exception ex)
            {
            }
        }

        public List<GetPatientDetailsViewModel> GetXeroPatientDetails()
        {
            var objResult = ObjectEntityPart1.ssp_GetPatientDetails().ToList();
            var objIList = new List<GetPatientDetailsViewModel>();
            Mapper.CreateMap<ssp_GetPatientDetails_Result, GetPatientDetailsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertFBPatients_Match(FreshBook_Patients_Match_ViewModel FBMatch)
        {
            try
            {
                FreshBook_Patients_Match FBClient = Get<FreshBook_Patients_Match>(x => x.PatientID == FBMatch.PatientID && x.ClientId == FBMatch.ClientId);
                if (FBClient != null)
                {


                    FBClient.IsRecUpdate = 0;
                    Edit<FreshBook_Patients_Match>(FBClient);
                }
                else
                {
                    FreshBook_Patients_Match FBP_Match = new FreshBook_Patients_Match();
                    Mapper.CreateMap<FreshBook_Patients_Match_ViewModel, FreshBook_Patients_Match>();
                    FBP_Match = Mapper.Map(FBMatch, FBP_Match);
                    Create<FreshBook_Patients_Match>(FBP_Match);
                }

            }
            catch (System.Exception ex)
            {
            }
        }

        public void InsertFreshBookClient(FreshBookClient_ViewModel FBMatch)
        {
            try
            {
                var FBClient = Get<FreshBookClient>(x => x.ClientId == FBMatch.ClientId);
                FreshBookClient FBP_Match = new FreshBookClient();
                Mapper.CreateMap<FreshBookClient_ViewModel, FreshBookClient>();
                FBP_Match = Mapper.Map(FBMatch, FBP_Match);
                if (FBClient != null)
                {
                    //FBClient=FBP_Match;
                    //ObjectEntityPart1.SaveChanges();
                    if (FBP_Match.IsDeleted != true)
                        FBP_Match.IsDeleted = FBClient.IsDeleted == true ? true : false;
                    ((IObjectContextAdapter)ObjectEntityPart1).ObjectContext.Detach(FBClient);
                    Edit<FreshBookClient>(FBP_Match);
                }
                else
                {
                    Create<FreshBookClient>(FBP_Match);
                }

            }
            catch (System.Exception ex)
            {
            }
        }

        /// <summary>
        /// This method get all local Patients who have matching relationship with FreshBook client 
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="rows">Number of rows</param>
        /// <param name="sord">Sort by "column name" </param>
        /// <param name="sidx">Sorting Order</param>
        /// <returns></returns>
        public List<ssp_FreshBook_MatchedPatients_ViewModel> GetMathedPatients_FreshBook(int page, int rows, string sord, string sidx)
        {
            var MatchedPatientsList = ObjectEntityPart1.ssp_FreshBook_MatchedPatients(page, rows, sidx, sord).ToList();
            var MatchedPatientsListVM = new List<ssp_FreshBook_MatchedPatients_ViewModel>();
            Mapper.CreateMap<ssp_FreshBook_MatchedPatients_Result, ssp_FreshBook_MatchedPatients_ViewModel>();
            MatchedPatientsListVM = Mapper.Map(MatchedPatientsList, MatchedPatientsListVM);
            return MatchedPatientsListVM;
        }

        /// <summary>
        /// This method get all local Patients who have matching relationship with FreshBook client 
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="rows">Number of rows</param>
        /// <param name="sord">Sort by "column name" </param>
        /// <param name="sidx">Sorting Order</param>
        /// <returns></returns>
        public List<ssp_FreshBook_NotMatchedPatients_ViewModel> GetNotMathedPatients_FreshBook(int page, int rows, string sord, string sidx)
        {
            var NotMatchedPatientsList = ObjectEntityPart1.ssp_FreshBook_NotMatchedPatients(page, rows, sidx, sord).ToList();
            var NotMatchedPatientsListVM = new List<ssp_FreshBook_NotMatchedPatients_ViewModel>();
            Mapper.CreateMap<ssp_FreshBook_NotMatchedPatients_Result, ssp_FreshBook_NotMatchedPatients_ViewModel>();
            NotMatchedPatientsListVM = Mapper.Map(NotMatchedPatientsList, NotMatchedPatientsListVM);
            return NotMatchedPatientsListVM;
        }


        public List<ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel> GetMathedFreshbookClientByPatientsId(int page, int rows, string sord, string sidx, int PatientsId)
        {
            var FreshbookClientList = ObjectEntityPart1.ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId(page, rows, sidx, sord, PatientsId).ToList();
            var FreshbookClientListVM = new List<ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel>();
            Mapper.CreateMap<ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_Result, ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel>();
            FreshbookClientListVM = Mapper.Map(FreshbookClientList, FreshbookClientListVM);
            return FreshbookClientListVM;
        }


        public List<ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel> GetFreshbookClientMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName, string Email)
        {
            var objResult = ObjectEntityPart1.ssp_Freshbook_SearchPatientToMatchFreshbookClient(page, rows, sord, sidx, FirstName, LastName, Email).ToList();
            var objIList = new List<ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel>();
            Mapper.CreateMap<ssp_Freshbook_SearchPatientToMatchFreshbookClient_Result, ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        //For Finding order in invoice table
        public bool GetInvoiceByOredrId(int OrderId)
        {
           List<ssp_FBGetExistInvoiceOrdersbyOrderID_Result> isExistInvoice = ObjectEntityPart1.ssp_FBGetExistInvoiceOrdersbyOrderID(OrderId.ToString()).ToList(); ;
           if (isExistInvoice.Count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
        }

        ///This region contains handler which are used to match and unmatch freshbook clients with local contacts.
        #region Matching and remove matching of freshBook clients with local contacts Handlers
        /// <summary>
        /// First setting IsDeleted value of FreshBookClients table to true and then add patientId and ContactId to FreshBook_Patients_Match tbl.
        /// </summary>
        /// <param name="PatientId">Unique id of Patients table.</param>
        /// <param name="ClientId">Unique id of FreshBookClients table and it is ClientId fatched form FreshBook Client</param>
        /// <returns>If both operation id done then success either Unsuccess</returns>
        public string Match_AppPatientsWithFreshbookClient(string PatientId, string ClientId)
        {
            FreshBook_Patients_Match_ViewModel FBMatchVM = new FreshBook_Patients_Match_ViewModel();
            FBMatchVM.PatientID = Convert.ToInt32(PatientId);
            FBMatchVM.ClientId = Convert.ToInt64(ClientId);
            if (InsertFBPatientMatch(FBMatchVM))
            {
                if (RemoveFreshbookClient(ClientId))
                    return "Success";
                else return "Unsuccess";
            }
            else
            {
                return "Unsuccess";
            }

        }

        /// <summary>
        /// First setting IsDeleted value of FreshBookClients table to false and then remove patientId and ContactId to FreshBook_Patients_Match tbl.
        /// </summary>
        /// <param name="PatientId">Unique id of Patients table.</param>
        /// <param name="ClientId">Unique id of FreshBookClients table and it is ClientId fatched form FreshBook Client</param>
        /// <returns>If both operation id done then success either Unsuccess</returns>

        public string RemoveMatch_AppPatientsWithFreshbookClient(string PatientId, string ClientId)
        {
            FreshBook_Patients_Match_ViewModel FBMatchVM = new FreshBook_Patients_Match_ViewModel();
            FBMatchVM.PatientID = Convert.ToInt32(PatientId);
            FBMatchVM.ClientId = Convert.ToInt64(ClientId);
            if (RemoveFBPatientMatch(FBMatchVM))
            {
                if (InsertFreshbookClient(ClientId))
                    return "Success";
                else return "Unsuccess";
            }
            else
            {
                return "Unsuccess";
            }
        }

        /// <summary>
        /// This method creating an entry in FreshBook_Patients_Match table.
        /// </summary>
        /// <param name="FBPatientMatch">view model which contain patients id and freshbook client id.</param>
        /// <returns>success of opration return true and unsuccess return false.</returns>
        public bool InsertFBPatientMatch(FreshBook_Patients_Match_ViewModel FBPatientMatch)
        {
            bool result = false;
            try
            {
                var FBPatients = Get<FreshBook_Patients_Match>(x => x.PatientID == FBPatientMatch.PatientID && x.ClientId == FBPatientMatch.ClientId);
                if (FBPatients != null)
                {

                }
                else
                {
                    FreshBook_Patients_Match FBPMatch = new FreshBook_Patients_Match();
                    FBPMatch.ClientId = FBPatientMatch.ClientId;
                    FBPMatch.PatientID = FBPatientMatch.PatientID;
                    Create<FreshBook_Patients_Match>(FBPMatch);
                }
                result = true;
            }
            catch (System.Exception ex)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// This method remove an entry in FreshBook_Patients_Match table.
        /// </summary>
        /// <param name="FBPatientMatch">view model which contain patients id and freshbook client id.</param>
        /// <returns>success of opration return true and unsuccess return false.</returns>
        public bool RemoveFBPatientMatch(FreshBook_Patients_Match_ViewModel FBPatientMatch)
        {
            bool result = false;
            try
            {
                var FBPatients = Get<FreshBook_Patients_Match>(x => x.PatientID == FBPatientMatch.PatientID && x.ClientId == FBPatientMatch.ClientId);
                if (FBPatients != null)
                {
                    Delete<FreshBook_Patients_Match>(FBPatients);
                }
                result = true;
            }
            catch (System.Exception ex)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// This method is setting IsDeleted field to false in FreshBookClient table.
        /// </summary>
        /// <param name="ClientId">Client id </param>
        /// <returns>If all fine then return true which showing success and unsuccess return false.</returns>
        public bool InsertFreshbookClient(string ClientId)
        {
            bool Result = false;
            try
            {
                long ClientIdlong = Convert.ToInt64(ClientId);
                var FBClient = Get<FreshBookClient>(x => x.ClientId == ClientIdlong);
                FBClient.IsDeleted = false;
                Edit<FreshBookClient>(FBClient);
                Result = true;
            }
            catch (System.Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// This method is setting IsDeleted field to true in FreshBookClient table.
        /// </summary>
        /// <param name="ClientId">Client id </param>
        /// <returns>success of opration return true and unsuccess return false.</returns>
        public bool RemoveFreshbookClient(string ClientId)
        {
            bool Result = false;
            try
            {
                long ClientIdlong = Convert.ToInt64(ClientId);
                var FBClient = Get<FreshBookClient>(x => x.ClientId == ClientIdlong);
                FBClient.IsDeleted = true;
                Edit<FreshBookClient>(FBClient);
                Result = true;
            }
            catch (System.Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        #endregion

        public List<GetAllCreatedInvoice_ViewModel> GetAllCreatedInvoice()
        {
            List<GetAllCreatedInvoice_Result> AllExistInvoice = ObjectEntityPart1.GetAllCreatedInvoice().ToList();
            var objIList = new List<GetAllCreatedInvoice_ViewModel>();
            Mapper.CreateMap<GetAllCreatedInvoice_Result, GetAllCreatedInvoice_ViewModel>();
            objIList = Mapper.Map(AllExistInvoice, objIList);
            return objIList;
        }

       
        public void FBCreatedInvoice(float ProductId, bool IsCreated, float InvoiceId)
        {
            ObjectEntityPart1.ssp_FBCreatedInvoice(ProductId, InvoiceId, IsCreated);
        }

        public List<ssp_GetFBCreatedInvoice_ViewModel> GetFBCreatedInvoice()
        {
           List<ssp_GetFBCreatedInvoice_Result> AllCreatedInvoice= ObjectEntityPart1.ssp_GetFBCreatedInvoice().ToList();
           var objIList = new List<ssp_GetFBCreatedInvoice_ViewModel>();
           Mapper.CreateMap<ssp_GetFBCreatedInvoice_Result, ssp_GetFBCreatedInvoice_ViewModel>();
           objIList = Mapper.Map(AllCreatedInvoice, objIList);
           return objIList;
        }
    }
}