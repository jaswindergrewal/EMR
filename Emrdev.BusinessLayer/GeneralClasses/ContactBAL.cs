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
    public class ContactBAL
    {
        ContactDAL objDAL = new ContactDAL();

        //Method to Update existing contact record.
        public void UpdateContact(ContacttblViewModel objContacttblViewModel)
        {
            Contact_tbl ContactEntity = new Contact_tbl();
            Mapper.CreateMap<ContacttblViewModel, Contact_tbl>();
            ContactEntity = AutoMapper.Mapper.Map(objContacttblViewModel, ContactEntity);
            ContactEntity = objDAL.Get<Contact_tbl>(c => c.ContactID == objContacttblViewModel.ContactID);
            if (ContactEntity != null)
            {
                ContactEntity.ContactID = objContacttblViewModel.ContactID;
                ContactEntity.FollowUpBody = objContacttblViewModel.FollowUpBody;
                ContactEntity.FollowUP_Completed = objContacttblViewModel.FollowUP_Completed;
                ContactEntity.FollowUp_ActualDate = objContacttblViewModel.FollowUp_ActualDate;
                objDAL.Edit(ContactEntity);
            }
        }

        #region
        /// <summary>
        /// To show the contact records for patient on the basis of contactid
        /// </summary>
        /// <param name="ContactID"></param>
        /// <returns>ContactRecordCloseViewModel</returns>
        public ContactRecordCloseViewModel GetContactRecordCloseDetails(int ContactID)
        {
            return objDAL.GetContactRecordCloseDetails(ContactID);
        }
        #endregion


        #region Insert Contact Detail

        public void InsertContactDetail(Emrdev.ViewModelLayer.Contact_tblViewModel objModel)
        {

            objDAL = new Emrdev.DataLayer.GeneralClasses.ContactDAL();
            Contact_tbl objEntity=new Contact_tbl();
            objEntity.AptType = objModel.AptType;
            objEntity.PatientID = objModel.PatientID;
            objEntity.MessageBody = objModel.MessageBody;
            objEntity.EnteredBy = objModel.EnteredBy;
            objEntity.Apt_ID = objModel.Apt_ID;
            objEntity.ContactDateEntered = objModel.ContactDateEntered;
            objEntity.FollowUP_Completed = objModel.FollowUP_Completed;
            objDAL.InsertContactDetail(objEntity);
        }

        #endregion

        #region Select Contact Type from "Contact_Type_tbl"  table

        public List<Emrdev.ViewModelLayer.Contact_Type_tblViewModel> SelectAllContactType()
        {
            objDAL = new Emrdev.DataLayer.GeneralClasses.ContactDAL();
            return objDAL.SelectAllContactType();
        }

        #endregion
    }
}
