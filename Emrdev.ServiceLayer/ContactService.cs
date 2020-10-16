using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContactService" in both code and config file together.
    public class ContactService : IContactService
    {
        ContactBAL objBAL = new ContactBAL();
        public void UpdateContact(ContacttblViewModel objContacttblViewModel)
        {
            objBAL.UpdateContact(objContacttblViewModel);
        }
        public ContactRecordCloseViewModel GetContactRecordCloseDetails(int ContactID)
        {
            return objBAL.GetContactRecordCloseDetails(ContactID);
        }


        #region Insert Contact Detail

        public void InsertContactDetail(Contact_tblViewModel objModel)
        {
            objBAL.InsertContactDetail(objModel);
        }

        #endregion


        public List<Contact_Type_tblViewModel> SelectAllContactType()
        {
            return objBAL.SelectAllContactType();
        }
    }
}
