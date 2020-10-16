using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;


namespace Emrdev.DataLayer.GeneralClasses
{
    public class AdminResellerDAL : ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>();
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

        #region Methods
        public List<SaleRepViewModel> GetSaleRep()
        {
            var objSaleRep = ObjectEntity1.ssp_GetSaleRep();
            List<SaleRepViewModel> _listSaleRep = new List<SaleRepViewModel>();

            foreach (var data in objSaleRep)
            {
                SaleRepViewModel oSaleRep = new SaleRepViewModel();
                oSaleRep.SalesRep = Convert.ToInt16(data.EmployeeID);
                oSaleRep.EmployeeName = data.EmployeeName;
                _listSaleRep.Add(oSaleRep);
            }

            return _listSaleRep;
        }

     
        public List<AdminResellerViewModel> GetAllRellers()
        {
            var objAdminReseller = ObjectEntity1.ssp_GetAdminResellersData();
            List<AdminResellerViewModel> _listAdminReseller = new List<AdminResellerViewModel>();

            foreach (var data in objAdminReseller)
            {
                AdminResellerViewModel oAdminReseller = new AdminResellerViewModel();
                oAdminReseller.ResellerID = data.ResellerID;
                oAdminReseller.ResellerNumber = data.ResellerNumber;
                oAdminReseller.BusinessName = data.BusinessName;
                oAdminReseller.ContactFirstName = data.ContactFirstName;
                oAdminReseller.ContactLastName = data.ContactLastName;
                oAdminReseller.FirstName = data.FirstName;
                oAdminReseller.LastName = data.LastName;
                oAdminReseller.Phone = data.Phone;
                oAdminReseller.Fax = data.Fax;
                oAdminReseller.Email = data.Email;
                oAdminReseller.Status = data.StatusName;
                oAdminReseller.Active_YN = data.Active_YN;
                oAdminReseller.AttendedDinner = data.AttendedDinner;
                oAdminReseller.StreetAddress = data.StreetAddress;
                oAdminReseller.City = data.City;
                oAdminReseller.State = data.State;
                oAdminReseller.Zip = data.Zip;
                oAdminReseller.Description = data.Description;
                oAdminReseller.Notes = data.Notes;
                oAdminReseller.SalesRep = data.SalesRep;
                oAdminReseller.SalesRepName = data.SalesRepName;
                oAdminReseller.EventID = data.EventID;
                oAdminReseller.ContactString = GetContacts(data.ResellerID);
                oAdminReseller.DateEnrolled = data.DateEnrolled;
                oAdminReseller.CoManageAgreement = data.CoManageAgreement;
                oAdminReseller.CoManageDate = data.CoManageDate;
                oAdminReseller.ContractSigned = data.ContractSigned;
                oAdminReseller.ContractDate = data.ContractDate;
                oAdminReseller.ResellerMarketingSourceID = data.ResellerMarketingSourceID;
                oAdminReseller.LeadStatus = data.LeadStatus;
                oAdminReseller.StatusID = data.statusID;
                _listAdminReseller.Add(oAdminReseller);
            }

            return _listAdminReseller;
        }

        private string GetContacts(int ResellerID)
        {
            string retString = "<table border='0' cellpadding='6' cellspacing='6' class='border' style=\"table-layout: fixed; width: 100%\"><tr bgcolor='#D6B781' class='PageTitle'><td align='left'>Contacts</td><td colspan='2' align='right'><input type='button' id='btnAddContact' class='button' value='Add Contact' onclick='winAddContact.Open();' /></td></tr><tr><td width=\"25%\">Date</td><td width=\"50%\">Message</td><td width=\"25%\">Entered By</td></tr>";
            var contacts = ObjectEntity1.ssp_GetSellerContacts(ResellerID);

            if (contacts != null)
            {
                foreach (var c in contacts)
                {

                    retString += "<tr><td valign='top' width='25%'>" + GetDateEntered(c.DateEntered).Trim() + "</td><td valign='top' width='50%'><div style=\"word-wrap: break-word;\">" + WordWrap.Wrap(c.MessageBody, 50) + "</div></td><td valign='top'>" + c.EmployeeName + "</td></tr>";
                }
            }
            else
            {
                retString += "<tr><td colspan=\"3\">No Contacts</td></tr>";
            }

            retString += "</table>";
            return retString;

        }

        private string GetDateEntered(DateTime dte)
        {
            return dte.ToShortDateString() + " " + dte.ToShortTimeString();
        }

        public void DeleteStatusManagement(int Id)
        {
            ObjectEntity1.ssp_DeleteStatusManagement(Id);
        }

        public void DeleteEventManagement(int Id)
        {
            ObjectEntity1.ssp_DeleteEventManagement(Id);
        }

        public void DeleteMarketingSourceManagement(int Id)
        {
            ObjectEntity1.ssp_DeleteMarketingSourceManagement(Id);
        }

        #endregion
    }
}
