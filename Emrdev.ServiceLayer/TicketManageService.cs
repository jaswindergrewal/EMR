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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TicketManageService" in both code and config file together.
    public class TicketManageService : ITicketManageService
    {

        TicketManageBAL objBAL = new TicketManageBAL();
        public List<ViewModelLayer.TicketManageViewModel> GetAllTicketManageList()
        {
            List<TicketManageViewModel> _objTicketManageList = objBAL.GetAllTicketManageList();
            return _objTicketManageList;
        }

        public void UpdateTicketManageList(int ProcessID, string ProcessName, string Interval, string Enabled, string Note)
        {
            objBAL.UpdateTicketManageList(ProcessID, ProcessName, Interval, Enabled, Note);
        }


        public List<ContactListViewModel> GetTicketOnlyDetails(int TicketID)
        {
            List<ContactListViewModel> objContactListViewModal = objBAL.GetTicketOnlyDetails(TicketID);
            return objContactListViewModal;
        }

        public List<ContactListViewModel> GetContactsNoteDetails(int ActiveTicket)
        {
            List<ContactListViewModel> objContactListViewModal = objBAL.GetContactsNoteDetails(ActiveTicket);
            return objContactListViewModal;
        }

        public TicketPatientViewModel GetAllTicketManageList(int ActiveTicket)
        {
            return objBAL.GetAllTicketManageList(ActiveTicket);
        }
    }
}
