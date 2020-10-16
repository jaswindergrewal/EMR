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
    public class NewTicketNoPatientBAL
    {
        NewTicketNoPatientDAL objDAL = new NewTicketNoPatientDAL();

        /// <summary>
        /// Methods to get all departemts staff
        /// </summary>
        /// <returns></returns>
        public List<DepartmentStaffViewModel> GetDepartmentStaff()
        {
            return objDAL.GetDepartmentStaff();
        }

        /// <summary>
        /// Method to fill apttype dropdowm 
        /// </summary>
        /// <param name="StaffID"></param>
        /// <param name="Emp"></param>
        /// <returns></returns>
        public List<AptFollowupsTypeViewModel> GetAptFollowups(int StaffID, int Emp)
        {
            return objDAL.GetAptFollowups(StaffID, Emp);
        }

        /// <summary>
        /// Method get all departments
        /// </summary>
        /// <returns></returns>
        public List<DepartmentViewModel> GetDepartments()
        {

            return objDAL.GetDepartments();
        }

        /// <summary>
        /// insert the details of newTicketNoPatient
        /// </summary>
        /// <param name="theFollow"></param>
        /// <param name="theContent"></param>
        /// <param name="StaffID"></param>
        public void InsertAptFollowUp(apt_FollowUpsViewModel theFollow, string theContent, int StaffID)
        {
            apt_FollowUps AptFollowupEntity = new apt_FollowUps();
            Mapper.CreateMap<apt_FollowUpsViewModel, apt_FollowUps>();
            AptFollowupEntity = AutoMapper.Mapper.Map(theFollow, AptFollowupEntity);
            //insert data in apt_FollowUps table
            objDAL.Create(AptFollowupEntity);
            //insert data in ContactTbl table with the followupid
            InsertContactTbl("New Ticket Entered.\r\n" + theContent + "\r\nTicket " + AptFollowupEntity.FollowUp_ID.ToString(), StaffID, AptFollowupEntity.FollowUp_ID);
            if (AptFollowupEntity.Severity == 1)
            {
                if (AptFollowupEntity.Assigned != null)
                {
                    Staff emp = objDAL.Get<Staff>(o => o.EmployeeID == theFollow.Assigned);
                    //Send mail to the employee if servity is 2 and Assigned is not null
                    //SendMail.SendGmailMessage(emp.username + "@LongevityMedicalClinic.com", emp.EmployeeName, "", "", "You have received a new High Priority ticket.");
                }
            }
        }

        /// <summary>
        /// insert method for contacts
        /// </summary>
        /// <param name="MessageBody"></param>
        /// <param name="StaffID"></param>
        /// <param name="FollowUpID"></param>
        public void InsertContactTbl(string MessageBody, int StaffID, int FollowUpID)
        {

            objDAL.InsertContactTbl(MessageBody, StaffID, FollowUpID);
        }

        public void InsertUpdateApt_Followups(FollowupViewModel VeiwModel, int ActiveId, int AssignId, int rdoSeverityId, string rdoSeverityText, string rdoDeptId, string UserName, int StaffId, bool CboCloseId
                                        , string Content, string AssignText)
        {
            apt_FollowUps AptFollowupEntity = new apt_FollowUps();
            Mapper.CreateMap<FollowupViewModel, apt_FollowUps>();
            AptFollowupEntity = AutoMapper.Mapper.Map(VeiwModel, AptFollowupEntity);
            objDAL.Edit(AptFollowupEntity);

            objDAL.InsertUpdateApt_Followups(ActiveId, AssignId, rdoSeverityId, rdoSeverityText, rdoDeptId, UserName, StaffId, CboCloseId
                                             , Content, AssignText);

            if (AptFollowupEntity.Severity == 1 && CboCloseId == false)
            {
                if (rdoDeptId != "Dept")
                {
                    Staff emp = objDAL.Get<Staff>(o => o.EmployeeID == AssignId);
                    //Send mail to the employee if servity is 1 and CboCloseId is false
                   
                   // SendMail.SendGmailMessage(emp.username + "@LongevityMedicalClinic.com", emp.EmployeeName, AptFollowupEntity.FollowUp_Body, "", "You have received a new High Priority ticket.");
                }

            }
        }

        public void InsertContactDetails(string MessageBody, int StaffID, int ActiveTicketId)
        {
            objDAL.InsertContactTbl(MessageBody, StaffID, ActiveTicketId);
        }
    }
}
