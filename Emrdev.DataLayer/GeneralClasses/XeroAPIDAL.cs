using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class XeroAPIDAL : ObjectEntity, IRepositary
    {

        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart2.Set<T>().Add(entityToCreate);
            ObjectEntityPart2.SaveChanges();
        }


        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart2.SaveChanges();
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
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
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

        public List<PatientViewModel> GetXeroPatientsByID(string PatientIDS)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_XEROGetpatientsbyPatientID(PatientIDS).ToList();
            var objIList = new List<PatientViewModel>();
            //Mapper.CreateMap<ssp_XEROGetpatientsbyPatientID_Result, PatientViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PatientViewModel> GetXeroPatientsQB(int UpdateFlag, int page, int rows, string sord, string sidx)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_XEROGetpatientsnotMatchQB(UpdateFlag, page, rows, sidx, sord).ToList();
            var objIList = new List<PatientViewModel>();
           // Mapper.CreateMap<ssp_XEROGetpatientsnotMatchQB_Result, PatientViewModel>();
           // objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        public void UpdateInvoiceXeroId(int InvoiceNumber, Guid InvoiceId)
        {
            ObjectEntityPart2.ssp_XEROUpdateInvoice(InvoiceNumber, InvoiceId);
        }

        public void InsertXeroAccounts(string Code, string Name)
        {
            ObjectEntityPart2.ssp_InsertXeroAccounts(Code, Name);
        }

        public void InsertXeroMatch(QB_MatchViewModel QbMatch)
        {


            try
            {
                Guid Xeroid = new Guid(QbMatch.QBid);
                //ObjectEntityPart1.ssp_XEROInsertMatchRecords(QbMatch.PatientID, QbMatch.QBid);
                Patient XeroContact = Get<Patient>(x => x.PatientID == QbMatch.PatientID);
                if (XeroContact != null)
                {

                    XeroContact.XeropatientId = Xeroid;
                    Edit<Patient>(XeroContact);
                }

            }
            catch (System.Exception ex)
            {
            }

            //ObjectEntityPart1.ssp_XEROInsertMatchRecords(QbMatch.PatientID, QbMatch.QBid);
        }
        //public void InsertQbMatch(QB_MatchViewModel QbMatch)
        //{

        //    ObjectEntityPart1.ssp_XEROInsertMatchRecords(QbMatch.PatientID, QbMatch.QBid);
        //}

        public void EditQbMatch(int PatientID, int UpdateFlag)
        {//Emr2017
            //ObjectEntityPart1.ssp_XEROUpdateMatchRecords(PatientID, UpdateFlag);
        }

        public List<OrderViewModel> GetXeroOrdersQB(int UpdateFlag, int page, int rows, string sord, string sidx)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_XEROGetOrdersnotMatchQB(UpdateFlag, page, rows, sidx, sord).ToList();
            var objIList = new List<OrderViewModel>();
            //Mapper.CreateMap<ssp_XEROGetOrdersnotMatchQB_Result, OrderViewModel>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<XeroOrders> GetXeroInvoiceByID(string OrderIDs)
        {//Emr2017
            //var objResult = ObjectEntityPart1.ssp_XEROGetOrdersbyOrderID(OrderIDs).ToList();
            var objIList = new List<XeroOrders>();
            //Mapper.CreateMap<ssp_XEROGetOrdersbyOrderID_Result, XeroOrders>();
            //objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<XeroInvoiceItems> GetXeroInvoiceItemsByID(int OrderID)
        {
            var objResult = ObjectEntityPart2.ssp_XEROGetOrderItemsbyOrderID(OrderID).ToList();
            var objIList = new List<XeroInvoiceItems>();
            Mapper.CreateMap<ssp_XEROGetOrderItemsbyOrderID_Result, XeroInvoiceItems>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<GetPatientDetailsViewModel> GetXeroPatientDetails()
        {
            var objResult = ObjectEntityPart1.ssp_GetPatientDetails().ToList();
            var objIList = new List<GetPatientDetailsViewModel>();
            Mapper.CreateMap<ssp_GetPatientDetails_Result, GetPatientDetailsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<XeroNotMatchedContacts> GetXeroPatientsNotMathed(int page, int rows, string sord, string sidx)
        {
            var objResult = ObjectEntityPart2.ssp_XEROGetpatientsnotMatch(page, rows, sidx, sord).ToList();
            var objIList = new List<XeroNotMatchedContacts>();
            Mapper.CreateMap<ssp_XEROGetpatientsnotMatch_Result, XeroNotMatchedContacts>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<XEROpatientsMatchedSearchModel> GetXeroPatientsMathedSearch(int page, int rows, string sord, string sidx, string FirstName, string LastName)
        {
            var objResult = ObjectEntityPart2.ssp_XEROpatientsMatchedSearch(page, rows, sord, sidx, FirstName, LastName).ToList();
            var objIList = new List<XEROpatientsMatchedSearchModel>();
            Mapper.CreateMap<ssp_XEROpatientsMatchedSearch_Result, XEROpatientsMatchedSearchModel>();
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
                    LAstFatchContactDateVal = xeroLogList.FirstOrDefault().UpdatedDateUTC;
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

                var xeroLogList = ObjectEntityPart1.XeroLogs.ToList();
                if (xeroLogList.Count > 0)
                {
                    XeroLog xeroLog = xeroLogList.FirstOrDefault();
                    xeroLog.UpdatedDateUTC = DateTime.UtcNow;
                    //ObjectEntityPart1.SaveChanges();
                    Edit<XeroLog>(xeroLog);
                }
                else
                {
                    XeroLog xeroLog = new XeroLog();
                    xeroLog.UpdatedDateUTC = DateTime.UtcNow;
                    //ObjectEntityPart1.XeroLogs.Add(xeroLog);
                    // ObjectEntityPart1.SaveChanges();
                    Create<XeroLog>(xeroLog);
                }

            }
            catch (System.Exception ex)
            {
            }
        }

        public void InsertXeroNotMatch(XeroPatientViewModel QbMatch)
        {

            XeroPatient XeroPatients = null;
            XeroPatients = Get<XeroPatient>(x => x.ContactId == QbMatch.ContactId);
            if (XeroPatients != null)
            {
                //XeroPatients.ContactId = QbMatch.ContactId;
                XeroPatients.CellPhone = QbMatch.CellPhone;
                XeroPatients.Email = QbMatch.Email;
                XeroPatients.FirstName = QbMatch.FirstName;
                XeroPatients.HomePhone = QbMatch.HomePhone;
                XeroPatients.LastName = QbMatch.LastName;
                XeroPatients.BillingCity = QbMatch.PostalCity;
                XeroPatients.BillingState = QbMatch.PostalState;
                XeroPatients.BillingStreet = QbMatch.PostalStreet;
                XeroPatients.BillingZip = QbMatch.PostalZip;
               
                //XeroPatients.IsDeleted = false;
                Edit<XeroPatient>(XeroPatients);
            }
            else
            {
                XeroPatients = new XeroPatient();
                XeroPatients.ContactId = QbMatch.ContactId;
                XeroPatients.CellPhone = QbMatch.CellPhone;
                XeroPatients.Email = QbMatch.Email;
                XeroPatients.FirstName = QbMatch.FirstName;
                XeroPatients.HomePhone = QbMatch.HomePhone;
                XeroPatients.LastName = QbMatch.LastName;
                XeroPatients.BillingCity = QbMatch.PostalCity;
                XeroPatients.BillingState = QbMatch.PostalState;
                XeroPatients.BillingStreet = QbMatch.PostalStreet;
                XeroPatients.BillingZip = QbMatch.PostalZip;
                
                 XeroPatients.IsDeleted = false; 
                Create<XeroPatient>(XeroPatients);
            }
            //ObjectEntityPart1.ssp_XEROInsertMatchRecords(QbMatch.PatientID, QbMatch.QBid);
        }

        public string MatchAppPatientsWithXeroContacts(string PatientId, string ContactId)
        {
           
            Guid Xeroid = new Guid(ContactId);
            int patientId= Convert.ToInt32(PatientId);
            //ObjectEntityPart1.ssp_XEROInsertMatchRecords(QbMatch.PatientID, QbMatch.QBid);
            Patient XeroContact = Get<Patient>(x => x.PatientID == patientId);
            if (XeroContact != null)
            {

                XeroContact.XeropatientId = Xeroid;
                Edit<Patient>(XeroContact);
                return "Success";
            }
            else
            {
                return "Unsuccess";
            }

          

        }

        public List<XeroOrders> GetXeroInvoiceByIDNew(string OrderIDs)
        {
            var _objResult = ObjectEntityPart2.ssp_XEROGetOrdersbyOrderIDnew(OrderIDs).ToList();
            var _objIList = new List<XeroOrders>();
            Mapper.CreateMap<ssp_XEROGetOrdersbyOrderIDnew_Result, XeroOrders>();
            _objIList = Mapper.Map(_objResult, _objIList);
            return _objIList;
        }

        public List<XeroAccounts> GetXeroAccounts()
        {
            var _objResult = ObjectEntityPart2.ssp_GetXeroaccounts().ToList();
            var _objIList = new List<XeroAccounts>();
            Mapper.CreateMap<ssp_GetXeroaccounts_Result, XeroAccounts>();
            _objIList = Mapper.Map(_objResult, _objIList);
            return _objIList;
        }

        public void UpdateOrderPaid(int OrderId)
        {

            ObjectEntityPart2.ssp_XEROUpdateInvoicePaid(OrderId);
        }

        public void UpdateFreeShippingStatus(int OrderId)
        {

            ObjectEntityPart2.ssp_UpdateFreeShipping(OrderId);
        }

       
       
    }
}
