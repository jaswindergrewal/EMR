using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INewTicketNoPatientService" in both code and config file together.
    [ServiceContract]
    public interface INewTicketNoPatientService
    {
        [OperationContract]
        List<DepartmentStaffViewModel> GetDepartmentStaff();

        [OperationContract]
        List<AptFollowupsTypeViewModel> GetAptFollowups(int StaffID, int Emp);

        [OperationContract]
        List<DepartmentViewModel> GetDepartments();

        [OperationContract]
        void InsertAptFollowUp(apt_FollowUpsViewModel theFollow ,string theContent ,int StaffID);

        [OperationContract]
        void InsertUpdateApt_Followups(FollowupViewModel VeiwModel,int ActiveId, int AssignId, int rdoSeverityId, string rdoSeverityText, string rdoDeptId, string UserName, int StaffId, bool CboCloseId
                                       , string Content, string AssignText);
        [OperationContract]
        void InsertContactDetails(string MessageBody, int StaffID, int ActiveTicketId);
    }
}
