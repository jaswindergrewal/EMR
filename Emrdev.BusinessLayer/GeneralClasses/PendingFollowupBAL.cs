using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class PendingFollowupBAL
    {
        PendingFollowupDAL objDAL = new PendingFollowupDAL();
        //Method to get the PendingFollowupBAL.
        public PendingFollowupViewModel GetPendingFollowUpDetail(int FollowUp_ID, int PatientID)
        {
            var apt_FollowUps = objDAL.List<apt_FollowUps>();
            var apt_FollowUp_types = objDAL.List<apt_FollowUp_types>();
            var patients = objDAL.List<Patient>();
            
            PendingFollowupViewModel _objPendingFollowup = (from f in apt_FollowUps
                                                           join t in apt_FollowUp_types on f.FollowUp_Cat equals t.FollowUp_Type_ID 
                                                           join p in patients on f.PatientID equals p.PatientID
                                                            //Code review point add :condition && f.PatientID == PatientID
                                                           where f.FollowUp_ID == FollowUp_ID && f.PatientID == PatientID
                                                           select new PendingFollowupViewModel
                                                           {
                                                               FollowUp_Type_Desc = t.FollowUp_Type_Desc,
                                                               DateEntered = f.DateEntered,
                                                               Range_Start = f.Range_Start,
                                                               Range_End = f.Range_End,
                                                               FollowUp_Completed_YN = f.FollowUp_Completed_YN,
                                                               FollowUp_Body = f.FollowUp_Body,
                                                               PatientName=p.LastName + " "+ p.FirstName,
                                                               Apt_ID=f.Apt_ID,
                                                               //CloseLink = "&nbsp;- [<a href='admin_pending_consult_close.asp?followup_id=" + f.FollowUp_ID.ToString() + "&patientid=" + PatientID.ToString() + ">close</a>]",
                                                           }).FirstOrDefault();


            return _objPendingFollowup;

        }

        public List<ContactTypeViewModel> GetContactTypeList()
        {
            var _objContactList = new List<ContactTypeViewModel>();
            var ContactEntity = new List<Contact_Type_tbl>();
            ContactEntity = objDAL.GetAll<Contact_Type_tbl>(o=>o.AptTypeID!=-1).OrderBy(o => o.AptTypeDesc).ToList();
            Mapper.CreateMap<Contact_Type_tbl, ContactTypeViewModel>();
            _objContactList = Mapper.Map(ContactEntity, _objContactList);
            return _objContactList;
            
        }

        public List<ContactListViewModel> GetContactList(int FollowUpID)
        {
            return objDAL.GetContactList(FollowUpID);

        }

        public List<PendingConsultRequestViewModel> GetPendingFollowups()
        {
            return objDAL.GetPendingFollowups();
        }

        public void CloseFollowup(int FollowUpID)
        {
            objDAL.CloseFollowup(FollowUpID);
        }
    }
}
