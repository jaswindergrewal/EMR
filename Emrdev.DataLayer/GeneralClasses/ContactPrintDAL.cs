using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class ContactPrintDAL: ObjectEntity
    {
        public ContactPrintViewModel GetContactPrintDetails(int ContactID)
        {
            var objResult = ObjectEntity1.ssp_GetContactPrints(ContactID);
            ContactPrintViewModel objIList = new ContactPrintViewModel();

            if (objResult != null)
            {
                foreach (var entity in objResult.ToList())
                {
                    objIList.username = entity.username;
                    objIList.AptTypeDesc = entity.username;
                    objIList.ContactDateEntered = entity.ContactDateEntered;
                    objIList.ContactID = entity.ContactID;
                    objIList.FirstName = entity.FirstName;
                    objIList.FollowUpBody = entity.FollowUpBody;
                    objIList.LastName = entity.LastName;
                    objIList.MessageBody = entity.MessageBody;
                    objIList.PatientID = entity.PatientID;

                }
            }
            return objIList;
        }
    }
}
