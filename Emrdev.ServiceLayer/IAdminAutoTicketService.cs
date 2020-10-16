using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminAutoTicketService" in both code and config file together.
    [ServiceContract]
    public interface IAdminAutoTicketService
    {
        [OperationContract]
        List<AdminAutoTicketViewModel> GetAutoticketList();

        [OperationContract]
        void SaveNewTicket(Emrdev.ViewModelLayer.AdminAutoTicketViewModel objModel);

        [OperationContract]
        void UpdateTicket(Emrdev.ViewModelLayer.AdminAutoTicketViewModel objModel);

        [OperationContract]
        void DeleteTicketById(int ticketId);

        [OperationContract]
        List<DepartmentViewModel> GetAutoshipDepartments();

        [OperationContract]
        List<apt_FollowUp_typesViewModel> GetFollowupTypes();
    }
}
