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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NewTicketNoPatientService" in both code and config file together.
    public class NewTicketNoPatientService : INewTicketNoPatientService
    {
        NewTicketNoPatientBAL objBAL = new NewTicketNoPatientBAL();
        public List<DepartmentStaffViewModel> GetDepartmentStaff()
        {
            return objBAL.GetDepartmentStaff();
        }

        public List<AptFollowupsTypeViewModel> GetAptFollowups(int StaffID, int Emp)
        {
            return objBAL.GetAptFollowups(StaffID, Emp);
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            return objBAL.GetDepartments();
        }

        public void InsertAptFollowUp(apt_FollowUpsViewModel theFollow, string theContent, int StaffID)
        {
            objBAL.InsertAptFollowUp(theFollow, theContent, StaffID);
        }

        public void InsertUpdateApt_Followups(FollowupViewModel VeiwModel,int ActiveId, int AssignId, int rdoSeverityId, string rdoSeverityText, string rdoDeptId, string UserName, int StaffId, bool CboCloseId
                                       , string Content, string AssignText)
        {
            objBAL.InsertUpdateApt_Followups(VeiwModel, ActiveId, AssignId, rdoSeverityId, rdoSeverityText, rdoDeptId, UserName, StaffId, CboCloseId
                                             , Content, AssignText);
        }

        public void InsertContactDetails(string MessageBody, int StaffID, int ActiveTicketId)
        {
            objBAL.InsertContactDetails(MessageBody, StaffID, ActiveTicketId);
        }

    }
}
