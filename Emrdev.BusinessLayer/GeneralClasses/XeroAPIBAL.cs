using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using AutoMapper;

namespace Emrdev.GeneralClasses
{
    public class XeroAPIBAL
    {

        XeroAPIDAL _objDAL = new XeroAPIDAL();
        /// <summary>
        /// This method is use to fetching xero cerdentials
        /// </summary>
        /// <returns>xero cerdentials</returns>
        public XeroCredentialViewModel GetXeroCerdential()
        {
            XeroCredential xeroCredential = _objDAL.Get<XeroCredential>(o => o.Id > 0);
            var XeroCredentialVM = new XeroCredentialViewModel();
            Mapper.CreateMap<XeroCredential, XeroCredentialViewModel>();
            XeroCredentialVM = Mapper.Map(xeroCredential, XeroCredentialVM);
            return XeroCredentialVM;
        }

        public string EditXeroCredential(XeroCredentialViewModel XeroCredentialVM)
        {
            string Result = "";

            try
            {
                XeroCredential xeroCredential = new XeroCredential();
                //XeroCredential xeroCredential = xeroPatients.GetAll<XeroCredential>(x => x.Id == XeroCredentialVM.Id).FirstOrDefault();

                Mapper.CreateMap<XeroCredentialViewModel, XeroCredential>();
                xeroCredential = Mapper.Map(XeroCredentialVM, xeroCredential);
                _objDAL.Edit<XeroCredential>(xeroCredential);
                Result = "Success";


            }
            catch (System.Exception ex)
            {
                Result = "Unsuccess";
            }
            return Result;
        }
        

        public void InsertXeroMatch(QB_MatchViewModel QbMatch)
        {
            _objDAL.InsertXeroMatch(QbMatch);
        }

        public List<XeroNotMatchedContacts> GetXeroPatientsNotMathed(int page, int rows, string sord, string sidx)
        {
            return _objDAL.GetXeroPatientsNotMathed(page, rows, sord, sidx);
        }

        public List<XEROpatientsMatchedSearchModel> GetXeroPatientsMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName)
        {
            return _objDAL.GetXeroPatientsMathedSearch(page, rows, sord, sidx, FirstName, LastName);
        }

        public DateTime? GetLastContactFatchedDate()
        {
            return _objDAL.GetLastContactFatchedDate();
        }

        public void EditLastContactFatchedDate()
        {
            _objDAL.EditLastContactFatchedDate();
        }

        public void InsertXeroNotMatch(XeroPatientViewModel QbMatch)
        {
            _objDAL.InsertXeroNotMatch(QbMatch);
        }

        public string MatchAppPatientsWithXeroContacts(string PatientId, string ContactId)
        {
            return _objDAL.MatchAppPatientsWithXeroContacts(PatientId, ContactId);
        }

        //public List<PatientViewModel> GetXeroPatientsByID(string PatientIDs)
        //{
        //    //return _objDAL.GetXeroPatientsByID(PatientIDs);
        //    return _objDAL.GetXeroPatientsByIDNew(PatientIDs);
        //}

        public List<XeroOrders> GetXeroInvoiceByID(string OrderIDs)
        {
            //return _objDAL.GetXeroInvoiceByID(OrderIDs);
            return _objDAL.GetXeroInvoiceByIDNew(OrderIDs);
        }

        public List<XeroInvoiceItems> GetXeroInvoiceItemsByID(int OrderID)
        {
            return _objDAL.GetXeroInvoiceItemsByID(OrderID);
        }

        public void UpdateInvoiceXeroId(int InvoiceNumber, Guid InvoiceId)
        {
            _objDAL.UpdateInvoiceXeroId(InvoiceNumber, InvoiceId);
        }

        public void InsertXeroAccounts(string Code, string Name)
        {
            _objDAL.InsertXeroAccounts(Code, Name);
        }

        public List<XeroAccounts> GetXeroAccounts()
        {
            return _objDAL.GetXeroAccounts();
        }

        public void UpdateOrderPaid(int OrderId)
        {

             _objDAL.UpdateOrderPaid(OrderId);
        }

        public void UpdateFreeShippingStatus(int OrderId)
        {

            _objDAL.UpdateFreeShippingStatus(OrderId);
        }

    }
}
