using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILandingPageService" in both code and config file together.
    [ServiceContract]
    public interface ILandingPageService
    {
        [OperationContract]
        List<MyTicketsViewModel> GetMyTickets(int StaffID, int page, int rows, string sortorder, string ColName);

        [OperationContract]
        List<MyTicketsViewModel> GetCreatedClosed(int StaffID, int page, int rows, string sortorder, string ColName);

        [OperationContract]
        List<MyTicketsViewModel> GetMyActive(int StaffID, int page, int rows, string sortorder,string ColName);

        [OperationContract]
        List<MyTicketsViewModel> GetMyGroupTickets(int StaffID, int ID, int page, int rows, string sortorder, string ColName);

        [OperationContract]
        List<MyTicketsViewModel> GetMyTicketsCAL(int StaffID, int page, int rows, string sortorder, string ColName);

        [OperationContract]
        List<MyTicketsViewModel> GetCreatedClosedCAL(int StaffID, int page, int rows, string sortorder, string ColName);

        [OperationContract]
        List<MyTicketsViewModel> GetMyActiveCAL(int StaffID, int page, int rows, string sortorder, string ColName);
        
    }
}
