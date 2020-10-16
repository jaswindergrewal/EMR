using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class AdminAutoTicketBAL
    {
        AdminAutoTicketDAL objDAL = new AdminAutoTicketDAL();
        public List<AdminAutoTicketViewModel> GetAutoticketList()
        {
            return objDAL.GetAutoticketList();
            //var _objAutoList = new List<AdminAutoTicketViewModel>();
            //var AutoEntity = new List<AutoTicket>();
            //AutoEntity = objDAL.GetAll<AutoTicket>(o => o.AutoticketName != null).OrderBy(o => o.AutoticketName).ToList();

            //Mapper.CreateMap<AutoTicket, AdminAutoTicketViewModel>();
            //_objAutoList = Mapper.Map(AutoEntity, _objAutoList);
            //return _objAutoList;
        }


        #region Save New Ticket

        public void SaveNewTicket(Emrdev.ViewModelLayer.AdminAutoTicketViewModel objModel)
        {
            /* Create New Ticket */
            AutoTicket objEntity=new AutoTicket();
            objEntity.AutoticketName = objModel.AutoticketName;
            objEntity.Subject = objModel.Subject;
            objEntity.Body = objModel.Body;
            objEntity.FollowUp_Type_ID = objModel.FollowUp_Type_ID;
            objEntity.Assigned = objModel.Assigned;
            objEntity.DeptAssign = objModel.DeptAssign;
            objEntity.StartDate = objModel.StartDate;
            objEntity.LastSent = objModel.LastSent;
            objEntity.CreatedID = objModel.CreatedID;
            objEntity.Frequency = objModel.Frequency;
            objEntity.FrequencyType = objModel.FrequencyType;
            objDAL.Create(objEntity);            
        }

        #endregion

        #region Update Ticket

        public void UpdateTicket(Emrdev.ViewModelLayer.AdminAutoTicketViewModel objModel)
        {
            AutoTicket objEntity=objDAL.Get<AutoTicket>(i => i.AutoTicketID == objModel.AutoTicketID);
            objEntity.Assigned = objModel.Assigned;
            objEntity.AutoticketName = objModel.AutoticketName;
            objEntity.Body = objModel.Body;
            objEntity.CreatedID = objModel.CreatedID;
            objEntity.DeptAssign = objModel.DeptAssign;
            objEntity.FollowUp_Type_ID = objModel.FollowUp_Type_ID;
            objEntity.Frequency = objModel.Frequency;
            objEntity.FrequencyType = objModel.FrequencyType;
            objEntity.LastSent = objModel.LastSent;
            objEntity.StartDate = objModel.StartDate;
            objEntity.Subject = objModel.Subject;
            objDAL.Edit(objEntity);
        }

        #endregion

        #region Delete Ticket By Id

        public void DeleteTicketById(int ticketId)
        {
            AutoTicket objEntity=objDAL.Get<AutoTicket>(i => i.AutoTicketID == ticketId);
            objDAL.Delete(objEntity);
        }

        #endregion

        public List<DepartmentViewModel> GetAutoshipDepartments()
        {
            return objDAL.GetAutoshipDepartments();
        }

        public List<apt_FollowUp_typesViewModel> GetFollowupTypes()
        {
            var _objAutoList = new List<apt_FollowUp_typesViewModel>();
            var AutoEntity = new List<apt_FollowUp_types>();
            AutoEntity = objDAL.GetAll<apt_FollowUp_types>(o => o.TicketType_YN == true ).OrderBy(o=>o.FollowUp_Type_Desc).ToList();

            Mapper.CreateMap<apt_FollowUp_types, apt_FollowUp_typesViewModel>();
            _objAutoList = Mapper.Map(AutoEntity, _objAutoList);
            return _objAutoList;
        }
    }
}
