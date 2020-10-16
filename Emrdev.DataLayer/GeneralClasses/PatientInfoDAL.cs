using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using System.Data.Objects;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class PatientInfoDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
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

        #region Get Patient Info By Id

        public Emrdev.ViewModelLayer.PatientViewModel GetPatientInfoById(int patientId)
        {
            //return ObjectEntity1.Patients.Select(i => new Emrdev.ViewModelLayer.PatientInfoViewModel { PatientId = i.PatientID, FirstName = i.FirstName, LastName = i.LastName, MiddleInitial = i.MiddleInitial, Gender = i.Sex, Clinic = i.Clinic, NameAlert = i.NameAlert, BirthDay = i.Birthday, InActive = i.Inactive, Allergies = i.Allergies, NickName = i.Nickname, InvoiceDue = i.InvoiceDue, ExpirationDate = i.ExpirationDate, InvoiceDueDate = i.InvoiceDueDate, InvoicePaid = i.InvoicePaid, RenewalException = i.RenewalException, TermsInMonths = i.TermsInMonths }).SingleOrDefault(i => i.PatientId == patientId);
            RenewalPackagesDAL objDAL = new RenewalPackagesDAL();
            return objDAL.GetPatientDetailById(patientId);
        }

        #endregion


        #region QBFullName By PatientId

        public string GetQBFullNameByPatientId(int patientId)
        {
            var record= ObjectEntity1.QB_Match.Join(ObjectEntity1.QB_Customers, m => m.QBid, c => c.ListID, (m, c) => new { Match = m, Customer = c }).SingleOrDefault(i => i.Match.PatientID == patientId);
            if (record == null)
                return "No QB match";
            else
                return record.Customer.FullName;
        }

        #endregion

        #region Get Invoice By Patient Id

        public List<Tuple<string, decimal?, DateTime?, string, string>> GetInvoiceByPatientId(int patientId, string[] typeIDs)
        {
            #region Old Working Query

            //var Invoices = from i in ObjectEntity1.QB_Invoices
            //               join it in ObjectEntity1.QB_Item on i.InvoiceLineItemRefListID equals it.ListID
            //               join m in ObjectEntity1.QB_Match on i.CustomerRefListID equals m.QBid
            //               join p in ObjectEntity1.Patients on m.PatientID equals p.PatientID
            //               where typeIDs.Contains(i.InvoiceLineItemRefListID)
            //               && p.PatientID == patientId
            //               orderby i.DueDate descending
            //               select new 
            //               {
            //                   InvoiceLineItemRefListID = i.InvoiceLineItemRefListID,
            //                   SalesPrice = it.SalesPrice,
            //                   DueDate = i.DueDate,
            //                   OpenBalance = i.OpenBalance,
            //                   IsPaid = i.IsPaid,
            //               };
            //return Invoices;
            #endregion
            var listInvoice = ObjectEntity1.QB_Invoices.Join
                (ObjectEntity1.QB_Item, invoice => invoice.InvoiceLineItemRefListID, item => item.ListID, (invoice, item)
               => new { Invoice = invoice, Item = item }).Join(ObjectEntity1.QB_Match, invoice => invoice.Invoice.CustomerRefListID, match => match.QBid, (invoice, match)
               => new { Match = match, Invoice = invoice }).Join(ObjectEntity1.Patients, match => match.Match.PatientID, patient => patient.PatientID, (match, patient)
               => new { Match = match, Patient = patient }).Where(i => i.Patient.PatientID == patientId && typeIDs.Contains(i.Match.Invoice.Invoice.InvoiceLineItemRefListID)).AsEnumerable().Select(j
               => new Tuple<string, decimal?, DateTime?, string, string>(j.Match.Invoice.Invoice.InvoiceLineItemRefListID, j.Match.Invoice.Item.SalesPrice, j.Match.Invoice.Invoice.DueDate, j.Match.Invoice.Invoice.OpenBalance, j.Match.Invoice.Invoice.IsPaid)).ToList();
            return listInvoice;
        }


        #endregion

        #region Get Invoice By Date Order

        public DateTime? GetInvoiceDateByDateOrder(int patientId)
        {
            #region Old Working Code
            //var inv = (from i in ObjectEntity1.QB_Invoices
            //           join m in ObjectEntity1.QB_Match on i.CustomerRefListID equals m.QBid
            //                  where m.PatientID == patientId
            //                  && i.InvoiceLineItemRefListID == "80001240-1316387771"
            //                  orderby i.Date descending
            //                  select i).FirstOrDefault();
            #endregion
            var joined= ObjectEntity1.QB_Invoices.Join(ObjectEntity1.QB_Match, invoice => invoice.CustomerRefListID, match
                => match.QBid, (invoice, match) => new { invoice = invoice, match = match }).OrderByDescending(i => i.invoice.Date).FirstOrDefault(i
                    => i.invoice.InvoiceLineItemRefListID == "80001240-1316387771" && i.match.PatientID == patientId).invoice.Date;
            return joined;
        }

        #endregion


        #region Get InvoiceDate By Date Order and InvoiceLineItemRefListID Collection
        public DateTime? GetInvoiceDateByDateOrder(int patientId, string[] typeIDs)
        {
            #region Old Working Code
            //QB_Invoice inv = (from i in ctx.QB_Invoices
            //                  join m in ctx.QB_Matches on i.CustomerRefListID equals m.QBid
            //                  where m.PatientID == patSupp.PatientID
            //                  && typeIDs.Contains(i.InvoiceLineItemRefListID)
            //                  orderby i.Date descending
            //                  select i).FirstOrDefault();
            #endregion
            var joined= ObjectEntity1.QB_Invoices.Join(ObjectEntity1.QB_Match, invoice => invoice.CustomerRefListID, match
                => match.QBid, (invoice, match) => new { invoice = invoice, match = match }).OrderByDescending(i => i.invoice.Date).FirstOrDefault(i
                    => typeIDs.Contains(i.invoice.InvoiceLineItemRefListID) && i.match.PatientID == patientId).invoice.Date;
            return joined;
        }

        #endregion


        #region Count Patient ProfileItems

        public int ProfileItemCount(int patientId)
        {
            return ObjectEntity1.ProfileItems.Where(i => i.PatientID == patientId).Count();
        }

        #endregion
    }
}
