using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    [ServiceContract]
    public interface IFreshBookApiService
    {
        [OperationContract]
        List<ssp_FreshBookGetOrdersnotMatchFB_Viewmodel> GetOrdersNotMatchonFB(int UpdateFlag, int page, int rows, string sord, string sidx);

        [OperationContract]
        List<FreshBookOrdersViewModel> GetFreshBookInvoiceByID(string InvoiceIDs);

        [OperationContract]
        List<FreshBookInvoiceItemsViewModel> GetFreshBookInvoiceItemsByID(int InvoiceID);

        [OperationContract]
        List<FreshBookPatientViewModel> GetFreshBookPatientsByID(string PatientIDs);
        
        
        [OperationContract]
        List<ssp_FreshBookGetpatientsnotMatchFB_ViewModel> GetFreshBookPatients(int UpdateFlag, int page, int rows, string sord, string sidx);
        
        [OperationContract]
        DateTime? GetLastContactFatchedDate();

        [OperationContract]
        void EditLastContactFatchedDate();

        [OperationContract]
        List<GetPatientDetailsViewModel> GetXeroPatientDetails();

        [OperationContract]
        void InsertFBPatients_Match(FreshBook_Patients_Match_ViewModel QbMatch);

        [OperationContract]
        void InsertFreshBookClient(FreshBookClient_ViewModel QbMatch);

        [OperationContract]
        List<ssp_FreshBook_MatchedPatients_ViewModel> GetMathedPatients_FreshBook(int page, int rows, string sord, string sidx);

        [OperationContract]
        List<ssp_FreshBook_NotMatchedPatients_ViewModel> GetNotMathedPatients_FreshBook(int page, int rows, string sord, string sidx);

        [OperationContract]
        List<ssp_FreshBook_MatchedPatientsWithFreshbookClientByPatientId_ViewModel> GetMathedFreshbookClientByPatientsId(int page, int rows, string sord, string sidx, int PatientsId);

        [OperationContract]
        string RemoveMatch_AppPatientsWithFreshbookClient(string PatientId, string ContactId);

        [OperationContract]
        string Match_AppPatientsWithFreshbookClient(string PatientId, string ContactId);

        [OperationContract]
        List<ssp_Freshbook_SearchPatientToMatchFreshbookClient_ViewModel> GetFreshbookClientMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName, string Email);

        [OperationContract]
        void InsertInvoiceMatchFB(int InvId, int OrderId);

        [OperationContract]
        void InsertFBItem_Match(FreshBook_Item_Match_ViewModel FbItemMatch);

        //[OperationContract]
        //void UploadTaxDetailsFromExcel(List<TaxFreshbook_ViewModel> TaxList);
       

    }
}
