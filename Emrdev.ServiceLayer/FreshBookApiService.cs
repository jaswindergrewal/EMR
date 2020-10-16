using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    public class FreshBookApiService : IFreshBookApiService
    {
        FreshBookAPIBAL _objBAL = new FreshBookAPIBAL();

        public List<ssp_FreshBookGetOrdersnotMatchFB_Viewmodel> GetOrdersNotMatchonFB(int UpdateFlag, int page, int rows, string sord, string sidx)
        {
            return _objBAL.GetOrdersNotMatchonFB(UpdateFlag, page, rows, sord, sidx);
        }

        public List<FreshBookOrdersViewModel> GetFreshBookInvoiceByID(string InvoiceIDs)
        {
            return _objBAL.GetFreshBookInvoiceByID(InvoiceIDs);
        }

        public List<FreshBookInvoiceItemsViewModel> GetFreshBookInvoiceItemsByID(int InvoiceID)
        {
            return _objBAL.GetFreshBookInvoiceItemsByID(InvoiceID);
        }

        public List<FreshBookPatientViewModel> GetFreshBookPatientsByID(string PatientIDs)
        {
            return _objBAL.GetFreshBookPatientsByID(PatientIDs);
        }

        public List<ssp_FreshBookGetpatientsnotMatchFB_ViewModel> GetFreshBookPatients(int UpdateFlag, int page, int rows, string sord, string sidx)
        {
            return _objBAL.GetFreshBookPatients(UpdateFlag, page, rows, sord, sidx);
        }

        public DateTime? GetLastContactFatchedDate()
        {
            return _objBAL.GetLastContactFatchedDate();
        }

        public void EditLastContactFatchedDate()
        {
            _objBAL.EditLastContactFatchedDate();
        }

        public List<GetPatientDetailsViewModel> GetXeroPatientDetails()
        {
            return _objBAL.GetXeroPatientDetails();
        }
       
        public void InsertFBPatients_Match(FreshBook_Patients_Match_ViewModel QbMatch)
        {
            _objBAL.InsertFBPatients_Match(QbMatch);
        }

        public void InsertFreshBookClient(FreshBookClient_ViewModel QbMatch)
        {
            _objBAL.InsertFreshBookClient(QbMatch);
        }

        public List<ssp_FreshBook_MatchedPatients_ViewModel> GetMathedPatients_FreshBook(int page, int rows, string sord, string sidx)
        {
            return _objBAL.GetMathedPatients_FreshBook(page, rows, sord, sidx);
        }

        public List<ssp_FreshBook_NotMatchedPatients_ViewModel> GetNotMathedPatients_FreshBook(int page, int rows, string sord, string sidx)
        {
            return _objBAL.GetNotMathedPatients_FreshBook(page, rows, sord, sidx);
        }

        public List<ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel> GetMathedFreshbookClientByPatientsId(int page, int rows, string sord, string sidx, int PatientsId)
        {
            return _objBAL.GetMathedFreshbookClientByPatientsId(page, rows, sord, sidx, PatientsId);
        }

        public string Match_AppPatientsWithFreshbookClient(string PatientId, string ContactId)
        {
            return _objBAL.Match_AppPatientsWithFreshbookClient(PatientId, ContactId);
        }

        public string RemoveMatch_AppPatientsWithFreshbookClient(string PatientId, string ContactId)
        {
            return _objBAL.RemoveMatch_AppPatientsWithFreshbookClient(PatientId, ContactId);
        }
        public List<ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel> GetFreshbookClientMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName, string Email)
        {
            return _objBAL.GetFreshbookClientMathedSearch(page, rows, sord, sidx, FirstName, LastName, Email);
        }

        public void InsertInvoiceMatchFB(int InvId, int OrderId)
        { 
        _objBAL.InsertInvoiceMatchFB( InvId,  OrderId);
        }

        public void InsertFBItem_Match(FreshBook_Item_Match_ViewModel FbItemMatch)
        {
            _objBAL.InsertFBItem_Match(FbItemMatch);
        }

        //public void UploadTaxDetailsFromExcel(List<TaxFreshbook_ViewModel> TaxList)
        //{
        //    _objBAL.UploadTaxDetailsFromExcel(TaxList);
        //}
    }
}
