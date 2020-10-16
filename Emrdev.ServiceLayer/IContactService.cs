using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IContactService" in both code and config file together.
    [ServiceContract]
    public interface IContactService
    {
        [OperationContract]
        void UpdateContact(ContacttblViewModel objContacttblViewModel);

        [OperationContract]
        ContactRecordCloseViewModel GetContactRecordCloseDetails(int ContactID);

        [OperationContract]
        void InsertContactDetail(Emrdev.ViewModelLayer.Contact_tblViewModel objModel);

        [OperationContract]
        List<Emrdev.ViewModelLayer.Contact_Type_tblViewModel> SelectAllContactType();
    }
}
