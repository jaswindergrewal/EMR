using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class ContactPrintBAL
    {
        ContactPrintDAL objDAL = new ContactPrintDAL();

        /// <summary>
        /// To show the contact records for patient on the basis of contactid
        /// </summary>
        /// <param name="sender">ContactID</param>
        /// <param name="e"></param>
        public ContactPrintViewModel GetContactPrintDetails(int ContactID)
        {
            return objDAL.GetContactPrintDetails(ContactID);
        }
    }
}
