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
  public  class TicketManageBAL
    {
      TicketManageDAL objDAL = new TicketManageDAL();

        //Method to get all Tickets
        public List<TicketManageViewModel> GetAllTicketManageList()
        {
            var _objTicketList = new List<TicketManageViewModel>();
            var TicketEntity = new List<Tickets_Manage>();
            TicketEntity = objDAL.GetAll<Tickets_Manage>(o => o.ProcessName != "").OrderBy(o => o.ProcessName).ToList();
            Mapper.CreateMap<Tickets_Manage, TicketManageViewModel>();
            _objTicketList = Mapper.Map(TicketEntity, _objTicketList);
            return _objTicketList;
        }

        //Method to update Manage tickets
        public void UpdateTicketManageList(int ProcessID, string ProcessName, string Interval, string Enabled, string Note)
        {
            Tickets_Manage TicketEntity = new Tickets_Manage();
            TicketEntity = objDAL.Get<Tickets_Manage>(o => o.ProcessID == ProcessID);
            TicketEntity.ProcessName = ProcessName;
            TicketEntity.Interval = int.Parse(Interval);
            TicketEntity.Enabled = bool.Parse(Enabled);
            TicketEntity.Note = Note;
            objDAL.Edit(TicketEntity);

        }
        public List<ContactListViewModel> GetTicketOnlyDetails(int TicketID)
        {
            List<ContactListViewModel> objContactListViewModal = objDAL.GetTicketOnlyDetails(TicketID);
            return objContactListViewModal;
        }

        public List<ContactListViewModel> GetContactsNoteDetails(int ActiveTicket)
        {
            List<ContactListViewModel> objContactListViewModal = objDAL.GetContactsNoteDetails(ActiveTicket);
            return objContactListViewModal;
        }

        public TicketPatientViewModel GetAllTicketManageList(int ActiveTicket)
        {
            return objDAL.GetAllTicketManageList(ActiveTicket);
        }
    }
}
