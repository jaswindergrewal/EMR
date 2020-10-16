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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITicketManageService" in both code and config file together.
    [ServiceContract]
    public interface ITicketManageService
    {
        [OperationContract]
        List<TicketManageViewModel> GetAllTicketManageList();

        [OperationContract]
        void UpdateTicketManageList(int ProcessID, string ProcessName, string Interval, string Enabled, string Note);

        [OperationContract]
        List<ContactListViewModel> GetTicketOnlyDetails(int TicketID);

        [OperationContract]
        List<ContactListViewModel> GetContactsNoteDetails(int ActiveTicket);

        TicketPatientViewModel GetAllTicketManageList(int ActiveTicket);

    }
}
