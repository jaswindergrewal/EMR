using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AdminResellerBAL
    {
        AdminResellerDAL objDAL = new AdminResellerDAL();

        /// <summary>
        /// Get the list of all affiliate admin
        /// </summary>
        /// <returns></returns>
        public List<AdminResellerViewModel> GetAllRellers()
        {
            return objDAL.GetAllRellers();
        }

        /// <summary>
        /// Methods to get the list of all marketing sources
        /// </summary>
        /// <returns></returns>
        public List<StatusViewModel> GetResellerStatus()
        {
            var ResellerStatus = objDAL.List<Emrdev.DataLayer.ResellerStatu>();
            List<StatusViewModel> _objResellerStatus = (from r in ResellerStatus
                                                        //where r.Active_YN == true   

                                                        // modify the code as per the client's comments(WB-08/08/2013) that
                                                        // all list should be displayed instead of only active records
                                                        // modified by: Deepak Thakur[12.April.2013]
                                                        where r.StatusName != ""
                                                        select new StatusViewModel
                                                                 {
                                                                     Status = r.StatusName,
                                                                     StatusID = r.ResellerStatusID,
                                                                     Active_YN = r.Active_YN

                                                                 }).OrderBy(o => o.Status).Distinct().ToList();
            return _objResellerStatus;
        }

        /// <summary>
        ///  method to get all events
        /// </summary>
        /// <returns></returns>
        public List<EventViewModel> GetResellerEvent()
        {
            var ResellerEvent = objDAL.List<Emrdev.DataLayer.ResellerEvent>();
            List<EventViewModel> _ResellerEvent = (from r in ResellerEvent
                                                   //where r.Active_YN == true

                                                   // modify the code as per the client's comments(WB-08/08/2013) that
                                                   // all list should be displayed instead of only active records
                                                   // modified by: Deepak Thakur[12.April.2013]
                                                   where r.EventName != ""
                                                   select new EventViewModel
                                                    {
                                                        EventID = r.ResellerEventID,
                                                        EventName = r.EventName,
                                                        Active_YN = r.Active_YN
                                                    }).OrderBy(o => o.EventName).Distinct().ToList();
            return _ResellerEvent;
        }

        /// <summary>
        /// Method to get all sales rep
        /// </summary>
        /// <returns></returns>
        public List<SaleRepViewModel> GetSaleRep()
        {

            return objDAL.GetSaleRep();
        }

        /// <summary>
        /// Method to get all Reseller marketing source
        /// </summary>
        /// <returns></returns>
        public List<ResellerMarketingSourceViewModel> GetMarketingSource()
        {
            var ResellerMarketingSource = objDAL.List<Emrdev.DataLayer.ResellerMarketingSource>();
            List<ResellerMarketingSourceViewModel> _objResellerSource = (from r in ResellerMarketingSource
                                                                         //where r.Active_YN == true

                                                                         // modify the code as per the client's comments(WB-08/08/2013) that
                                                                         // all list should be displayed instead of only active records
                                                                         // modified by: Deepak Thakur[12.April.2013]
                                                                         where r.SourceName != ""
                                                                         select new ResellerMarketingSourceViewModel
                                                                        {
                                                                            SourceName = r.SourceName,
                                                                            ResellerMarketingSourceID = r.ResellerMarketingSourceID,
                                                                            Active_YN = r.Active_YN
                                                                        }).OrderBy(o => o.SourceName).Distinct().ToList();
            return _objResellerSource;
        }

        /// <summary>
        /// insert contacts details
        /// </summary>
        /// <param name="Cont"></param>
        public void InsertContact(AdminResellerContactViewModel Cont)
        {
            Emrdev.DataLayer.ResellerContact obj = new DataLayer.ResellerContact();
            obj.DateEntered = Cont.DateEntered;
            obj.EnteredBy = Cont.EnteredBy;
            obj.MessageBody = Cont.MessageBody;
            obj.ResellerID = Cont.ResellerID;
            objDAL.Create(obj);
        }

        /// <summary>
        /// Insert Reseller information
        /// </summary>
        /// <param name="res"></param>
        /// <param name="StaffID"></param>
        public void InsertResellerInfo(AdminResellerViewModel res, int StaffID)
        {

            Reseller NewRes = new Reseller();
            Reseller oldRes = new Reseller();

            if (res.ResellerID > 0)
            {

                Reseller _objListOld = objDAL.Get<Reseller>(o => o.ResellerID == res.ResellerID);
                if (_objListOld != null)
                {


                    _objListOld.AttendedDinner = res.AttendedDinner;
                    _objListOld.BusinessName = res.BusinessName;
                    _objListOld.City = res.City;
                    _objListOld.ContactFirstName = res.ContactFirstName;
                    _objListOld.ContactLastName = res.ContactLastName;
                    _objListOld.Description = res.Description;
                    _objListOld.Email = res.Email;
                    _objListOld.Fax = res.Fax;
                    _objListOld.FirstName = res.FirstName;
                    _objListOld.LastName = res.LastName;
                    _objListOld.Notes = res.Notes;
                    _objListOld.Phone = res.Phone;
                    _objListOld.SalesRep = res.SalesRep;
                    _objListOld.State = res.State;
                    _objListOld.StatusID = res.StatusID;
                    _objListOld.StreetAddress = res.StreetAddress;
                    _objListOld.Zip = res.Zip;
                    _objListOld.EventID = res.EventID;
                    _objListOld.DateEnrolled = res.DateEnrolled;
                    _objListOld.CoManageAgreement = res.CoManageAgreement;
                    _objListOld.ContractDate = res.ContractDate;
                    _objListOld.ContractSigned = res.ContractSigned;
                    _objListOld.CoManageDate = res.CoManageDate;
                    _objListOld.ResellerMarketingSourceID = res.ResellerMarketingSourceID;
                    _objListOld.LeadStatus = res.LeadStatus;
                    objDAL.Edit(_objListOld);

                }
                oldRes.Active_YN = _objListOld.Active_YN;
                oldRes.AttendedDinner = _objListOld.AttendedDinner;
                oldRes.BusinessName = _objListOld.BusinessName;
                oldRes.City = _objListOld.City;
                oldRes.ContactFirstName = _objListOld.ContactFirstName;
                oldRes.ContactLastName = _objListOld.ContactLastName;
                oldRes.Description = _objListOld.Description;
                oldRes.Email = _objListOld.Email;
                oldRes.Fax = _objListOld.Fax;
                oldRes.FirstName = _objListOld.FirstName;
                oldRes.LastName = _objListOld.LastName;
                oldRes.Notes = _objListOld.Notes;
                oldRes.Phone = _objListOld.Phone;
                oldRes.SalesRep = _objListOld.SalesRep;
                oldRes.State = _objListOld.State;
                oldRes.StatusID = _objListOld.StatusID;
                oldRes.StreetAddress = _objListOld.StreetAddress;
                oldRes.Zip = _objListOld.Zip;
                oldRes.EventID = _objListOld.EventID;
                oldRes.DateEnrolled = _objListOld.DateEnrolled;
                oldRes.CoManageAgreement = _objListOld.CoManageAgreement;
            }


            NewRes.AttendedDinner = res.AttendedDinner;
            NewRes.BusinessName = res.BusinessName;
            NewRes.City = res.City;
            NewRes.ContactFirstName = res.ContactFirstName;
            NewRes.ContactLastName = res.ContactLastName;
            NewRes.Description = res.Description;
            NewRes.Email = res.Email;
            NewRes.Fax = res.Fax;
            NewRes.FirstName = res.FirstName;
            NewRes.LastName = res.LastName;
            NewRes.Notes = res.Notes;
            NewRes.Phone = res.Phone;
            NewRes.SalesRep = res.SalesRep;
            NewRes.State = res.State;
            NewRes.StatusID = res.StatusID;
            NewRes.StreetAddress = res.StreetAddress;
            NewRes.Zip = res.Zip;
            NewRes.EventID = res.EventID;
            NewRes.DateEnrolled = res.DateEnrolled;
            NewRes.CoManageAgreement = res.CoManageAgreement;
            NewRes.ContractDate = res.ContractDate;
            NewRes.ContractSigned = res.ContractSigned;
            NewRes.CoManageDate = res.CoManageDate;
            NewRes.ResellerMarketingSourceID = res.ResellerMarketingSourceID;
            NewRes.LeadStatus = res.LeadStatus;


            if (res.ResellerID == 0)
            {
                NewRes.DateEntered = DateTime.Now;
                objDAL.Create(NewRes);
            }


            ResellerContact cont = new ResellerContact();
            cont.DateEntered = DateTime.Now;
            cont.EnteredBy = StaffID;
            if (res.ResellerID == 0)
                cont.MessageBody = "Contact added by ";//; + (from s in Staff where s.EmployeeID == (int)Session["StaffID"] select s.EmployeeName).First();
            else

                cont.MessageBody = BuildUpdateContact(oldRes, NewRes);

            cont.ResellerID = res.ResellerID;
            objDAL.Create(cont);


        }

        private string BuildUpdateContact(Reseller oldRes, Reseller res)
        {
            string retString = "";
            retString = "Items changed: <br/>";
            if (oldRes.Active_YN != res.Active_YN)
                retString += "Changed from active " + oldRes.Active_YN.ToString() + " to " + res.Active_YN.ToString() + "<br/>";
            if (oldRes.AttendedDinner != res.AttendedDinner)
                retString += "Changed from AttendedDinner " + oldRes.AttendedDinner.ToString() + " to " + res.AttendedDinner.ToString() + "<br/>";
            if (oldRes.BusinessName != res.BusinessName)
                retString += "Changed from BusinessName " + oldRes.BusinessName + " to " + res.BusinessName + "<br/>";
            if (oldRes.City != res.City)
                retString += "Changed from City " + oldRes.City + " to " + res.City + "<br/>";
            if (oldRes.ContactFirstName != res.ContactFirstName)
                retString += "Changed from ContactFirstName " + oldRes.ContactFirstName + " to " + res.ContactFirstName + "<br/>";
            if (oldRes.ContactLastName != res.ContactLastName)
                retString += "Changed from ContactLastName " + oldRes.ContactLastName + " to " + res.ContactLastName + "<br/>";
            if (oldRes.DateEnrolled != res.DateEnrolled)
            {
                string oldEnroll = "";
                string Enroll = "";
                if (oldRes.DateEnrolled == null) oldEnroll = ""; else oldEnroll = ((DateTime)oldRes.DateEnrolled).ToShortDateString();
                if (res.DateEnrolled == null) Enroll = ""; else Enroll = ((DateTime)res.DateEnrolled).ToShortDateString();
                retString += "Changed from DateEnrolled " + oldEnroll + " to " + Enroll + "<br/>";
            }
            if (oldRes.Description != res.Description)
                retString += "Changed from Description " + oldRes.Description + " to " + res.Description + "<br/>";
            if (oldRes.Email != res.Email)
                retString += "Changed from Email " + oldRes.Email + " to " + res.Email + "<br/>";
            if (oldRes.EventID != res.EventID)
                retString += "Changed from EventID" + oldRes.EventID.ToString() + " to " + res.EventID.ToString() + "<br/>";
            if (oldRes.Fax != res.Fax)
                retString += "Changed from Fax " + oldRes.Fax + " to " + res.Fax + "<br/>";
            if (oldRes.FirstName != res.FirstName)
                retString += "Changed from FirstName " + oldRes.FirstName + " to " + res.FirstName + "<br/>";
            if (oldRes.LastName != res.LastName)
                retString += "Changed from LastName " + oldRes.LastName + " to " + res.LastName + "<br/>";
            if (oldRes.Notes != res.Notes)
                retString += "Changed from Notes " + oldRes.Notes + " to " + res.Notes + "<br/>";
            if (oldRes.Phone != res.Phone)
                retString += "Changed from Phone " + oldRes.Phone + " to " + res.Phone + "<br/>";
            if (oldRes.SalesRep != res.SalesRep)
                retString += "Changed from SalesRep " + oldRes.SalesRep.ToString() + " to " + res.SalesRep.ToString() + "<br/>";
            if (oldRes.State != res.State)
                retString += "Changed from State " + oldRes.State + " to " + res.State + "<br/>";
            if (oldRes.StatusID != res.StatusID)
                retString += "Changed from StatusID " + oldRes.StatusID.ToString() + " to " + res.StatusID.ToString() + "<br/>";
            if (oldRes.StreetAddress != res.StreetAddress)
                retString += "Changed from StreetAddress " + oldRes.StreetAddress + " to " + res.StreetAddress + "<br/>";
            if (oldRes.Zip != res.Zip)
                retString += "Changed from Zip " + oldRes.Zip + " to " + res.Zip + "<br/>";
            if (oldRes.CoManageAgreement != res.CoManageAgreement)
                retString += "Changed from CoManageAgreement " + oldRes.CoManageAgreement + " to " + res.CoManageAgreement + "<br/>";


            return retString;
        }

        /// <summary>
        /// Insert/update status details
        /// </summary>
        /// <param name="SatusId"></param>
        /// <param name="Active"></param>
        /// <param name="StatusName"></param>
        public void InsertUpdateStatus(int SatusId, bool Active, string StatusName)
        {
            ResellerStatu ResellerStatus = new ResellerStatu();


            if (SatusId > 0)
            {

                ResellerStatus = objDAL.Get<ResellerStatu>(o => o.ResellerStatusID == SatusId);
                if (ResellerStatus != null)
                {

                    ResellerStatus.ResellerStatusID = SatusId;
                    ResellerStatus.Active_YN = Active;
                    ResellerStatus.StatusName = StatusName;
                    objDAL.Edit(ResellerStatus);

                }
            }
            else
            {

                ResellerStatus.Active_YN = Active;
                ResellerStatus.StatusName = StatusName;
                objDAL.Create(ResellerStatus);
            }

        }

        /// <summary>
        /// Insert/update Event details
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="Active"></param>
        /// <param name="EventName"></param>
        public void InsertUpdateEvent(int EventId, bool Active, string EventName)
        {
            ResellerEvent ResellerEvent = new ResellerEvent();


            if (EventId > 0)
            {

                ResellerEvent = objDAL.Get<ResellerEvent>(o => o.ResellerEventID == EventId);
                if (ResellerEvent != null)
                {

                    ResellerEvent.ResellerEventID = EventId;
                    ResellerEvent.Active_YN = Active;
                    ResellerEvent.EventName = EventName;
                    objDAL.Edit(ResellerEvent);

                }
            }
            else
            {

                ResellerEvent.Active_YN = Active;
                ResellerEvent.EventName = EventName;
                objDAL.Create(ResellerEvent);
            }

        }

        /// <summary>
        /// Insert/update Marketing Source details
        /// </summary>
        /// <param name="ResellerMarketingSourceID"></param>
        /// <param name="Active"></param>
        /// <param name="SourceName"></param>
        public void InsertUpdateSource(int ResellerMarketingSourceID, bool Active, string SourceName)
        {
            ResellerMarketingSource ResellerMarketingSource = new ResellerMarketingSource();


            if (ResellerMarketingSourceID > 0)
            {

                ResellerMarketingSource = objDAL.Get<ResellerMarketingSource>(o => o.ResellerMarketingSourceID == ResellerMarketingSourceID);
                if (ResellerMarketingSource != null)
                {

                    ResellerMarketingSource.ResellerMarketingSourceID = ResellerMarketingSourceID;
                    ResellerMarketingSource.Active_YN = Active;
                    ResellerMarketingSource.SourceName = SourceName;
                    objDAL.Edit(ResellerMarketingSource);

                }
            }
            else
            {

                ResellerMarketingSource.Active_YN = Active;
                ResellerMarketingSource.SourceName = SourceName;
                objDAL.Create(ResellerMarketingSource);
            }

        }

        /// <summary>
        /// method for check the duplicate records in ResellerStatus table
        /// during add/update the data
        /// used admin_reseller_data.aspx.cs
        /// </summary>
        /// <param name="ProspectID"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool CheckDuplicateResellerStatus(int ResllerStatusId, string StatusName)
        {
            bool isExist = false;
            if (ResllerStatusId == 0)
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.ResellerStatu>(o => o.StatusName == StatusName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.ResellerStatu>(o => o.StatusName == StatusName && o.ResellerStatusID != ResllerStatusId);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        /// <summary>
        /// method for check the duplicate records in ResellerEvent table
        /// during add/update the data
        /// used admin_reseller_data.aspx.cs
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public bool CheckDuplicateEvent(int EventID, string EventName)
        {
            bool isExist = false;
            if (EventID == 0)
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.ResellerEvent>(o => o.EventName == EventName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.ResellerEvent>(o => o.EventName == EventName && o.ResellerEventID != EventID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        /// <summary>
        /// method for check the duplicate records in ResellerEvent table
        /// during add/update the data
        /// used admin_reseller_data.aspx.cs
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public bool CheckDuplicateSource(int ResellerMarketingSourceID, string SourceName)
        {
            bool isExist = false;
            if (ResellerMarketingSourceID == 0)
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.ResellerMarketingSource>(o => o.SourceName == SourceName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.ResellerMarketingSource>(o => o.SourceName == SourceName && o.ResellerMarketingSourceID != ResellerMarketingSourceID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        public void DeleteStatusManagement(int Id)
        {
            objDAL.DeleteStatusManagement(Id);
        }

        public void DeleteEventManagement(int Id)
        {
            objDAL.DeleteEventManagement(Id);
        }

        public void DeleteMarketingSourceManagement(int Id)
        {
            objDAL.DeleteMarketingSourceManagement(Id);
        }
    }
}
