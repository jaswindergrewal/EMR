using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminAutoTicketService" in both code and config file together.
    public class AdminAutoTicketService : IAdminAutoTicketService
    {
        AdminAutoTicketBAL objBAL = new AdminAutoTicketBAL();
        public List<AdminAutoTicketViewModel> GetAutoticketList()
        {
            return objBAL.GetAutoticketList();
        }


        #region Save New Ticket

        public void SaveNewTicket(AdminAutoTicketViewModel objModel)
        {
            objBAL.SaveNewTicket(objModel);
        }

        #endregion

        #region Update Ticket

        public void UpdateTicket(AdminAutoTicketViewModel objModel)
        {
            objBAL.UpdateTicket(objModel);
        }

        #endregion


        #region Delete Ticket By Id

        public void DeleteTicketById(int ticketId)
        {
            objBAL.DeleteTicketById(ticketId);
        }

        #endregion

        public List<DepartmentViewModel> GetAutoshipDepartments()
        {
            return objBAL.GetAutoshipDepartments();
        }

        public List<apt_FollowUp_typesViewModel> GetFollowupTypes()
        {
            return objBAL.GetFollowupTypes();
        }
    }
}
