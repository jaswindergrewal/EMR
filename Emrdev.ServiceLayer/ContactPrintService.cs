using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ContactPrintService" in both code and config file together.
    public class ContactPrintService : IContactPrintService
    {
       ContactPrintBAL objBAL=new ContactPrintBAL();
       public ContactPrintViewModel GetContactPrintDetails(int ContactID)
       {
           return objBAL.GetContactPrintDetails(ContactID);
       }
    }
}
