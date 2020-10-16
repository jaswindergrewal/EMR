using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;



namespace Emrdev.GeneralClasses
{
    public class FreshBookAPIBAL
    {
        FreshBookAPIDAL _objDAL = new FreshBookAPIDAL();

        public List<ssp_FreshBookGetOrdersnotMatchFB_Viewmodel> GetOrdersNotMatchonFB(int UpdateFlag, int page, int rows, string sord, string sidx)
        {
            return _objDAL.GetOrdersNotMatchonFB(UpdateFlag, page, rows, sord, sidx);
        }

        public List<FreshBookOrdersViewModel> GetFreshBookInvoiceByID(string InvoiceIDs)
        {
            return _objDAL.GetFreshBookInvoiceByID(InvoiceIDs);
        }

        public List<FreshBookInvoiceItemsViewModel> GetFreshBookInvoiceItemsByID(int InvoiceID)
        {
            return _objDAL.GetFreshBookInvoiceItemsByID(InvoiceID);
        }

        public List<FreshBookPatientViewModel> GetFreshBookPatientsByID(string PatientIDs)
        {
            return _objDAL.GetFreshBookPatientsByID(PatientIDs);
        }

        public List<ssp_FreshBookGetpatientsnotMatchFB_ViewModel> GetFreshBookPatients(int UpdateFlag, int page, int rows, string sord, string sidx)
        {
            return _objDAL.GetFreshBookPatients(UpdateFlag, page, rows, sord, sidx);
        }
        public DateTime? GetLastContactFatchedDate()
        {
            return _objDAL.GetLastContactFatchedDate();
        }

        public void EditLastContactFatchedDate()
        {
            _objDAL.EditLastContactFatchedDate();
        }

        public List<GetPatientDetailsViewModel> GetXeroPatientDetails()
        {
            return _objDAL.GetXeroPatientDetails();
        }
      
        public void InsertFBPatients_Match(FreshBook_Patients_Match_ViewModel QbMatch)
        {
            _objDAL.InsertFBPatients_Match(QbMatch);
        }

        public void InsertFreshBookClient(FreshBookClient_ViewModel QbMatch)
        {
            _objDAL.InsertFreshBookClient(QbMatch);
        }

        public List<ssp_FreshBook_MatchedPatients_ViewModel> GetMathedPatients_FreshBook(int page, int rows, string sord, string sidx)
        {
            return _objDAL.GetMathedPatients_FreshBook(page, rows, sord, sidx);
        }

        public List<ssp_FreshBook_NotMatchedPatients_ViewModel> GetNotMathedPatients_FreshBook(int page, int rows, string sord, string sidx)
        {
            return _objDAL.GetNotMathedPatients_FreshBook(page, rows, sord, sidx);
        }

        public List<ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel> GetMathedFreshbookClientByPatientsId(int page, int rows, string sord, string sidx, int PatientsId)
        {
            return _objDAL.GetMathedFreshbookClientByPatientsId(page, rows, sord, sidx, PatientsId);
        }

        public string Match_AppPatientsWithFreshbookClient(string PatientId, string ContactId)
        {
            return _objDAL.Match_AppPatientsWithFreshbookClient(PatientId, ContactId);
        }

        public string RemoveMatch_AppPatientsWithFreshbookClient(string PatientId, string ContactId)
        {
            return _objDAL.RemoveMatch_AppPatientsWithFreshbookClient(PatientId, ContactId);
        }

        public List<ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel> GetFreshbookClientMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName, string Email)
        {
            return _objDAL.GetFreshbookClientMathedSearch(page, rows, sord, sidx, FirstName, LastName, Email);
        }

        public void InsertInvoiceMatchFB(int InvId, int OrderId)
        {
            FreshBook_Invoice_Match InvMatch=new FreshBook_Invoice_Match();
            InvMatch.FbInvId = InvId;
            InvMatch.OrderID = OrderId;
            InvMatch.IsRecUpdate = 0;
            InvMatch.IsPaid = "0";
            _objDAL.Create(InvMatch);
        }

        public void InsertFBItem_Match(FreshBook_Item_Match_ViewModel FbItemMatch)
        {
            FreshBook_Item_Match ItemMatch = new FreshBook_Item_Match();
            ItemMatch.FbItemID = FbItemMatch.FbItemID;
            ItemMatch.ProductId = FbItemMatch.ProductId;
            ItemMatch.StockInHand = 0;
            _objDAL.Create(ItemMatch);
        }

       
    }
}
