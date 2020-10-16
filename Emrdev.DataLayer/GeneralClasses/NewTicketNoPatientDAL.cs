using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class NewTicketNoPatientDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        public List<DepartmentStaffViewModel> GetDepartmentStaff()
        {
            var objStaff = ObjectEntity1.ssp_GetDepartmentStaff();
            List<DepartmentStaffViewModel> _listStaff = new List<DepartmentStaffViewModel>();

            foreach (var data in objStaff)
            {
                DepartmentStaffViewModel oStaff = new DepartmentStaffViewModel();
                oStaff.EmployeeID = Convert.ToInt16(data.EmployeeID);
                oStaff.EmployeeName = data.EmployeeName;
                oStaff.DepartmentStaffID = data.DepartmentStaffId;
                _listStaff.Add(oStaff);
            }

            return _listStaff;
        }

        public List<AptFollowupsTypeViewModel> GetAptFollowups(int StaffID, int Emp)
        {
            var objApt = ObjectEntity1.ssp_Getapt_Followups_Type(StaffID,Emp);
            List<AptFollowupsTypeViewModel> _listApt = new List<AptFollowupsTypeViewModel>();

            foreach (var data in objApt)
            {
                AptFollowupsTypeViewModel oApt = new AptFollowupsTypeViewModel();
                oApt.FollowUp_Type_ID = Convert.ToInt16(data.FollowUp_Type_ID);
                oApt.FollowUp_Type_Desc = data.FollowUp_Type_Desc;
                _listApt.Add(oApt);
            }

            return _listApt;
        }

        public List<DepartmentViewModel> GetDepartments()
        {
            var objDept = ObjectEntity1.ssp_GetDepartments();
            List<DepartmentViewModel> _listDept = new List<DepartmentViewModel>();

            foreach (var data in objDept)
            {
                DepartmentViewModel oDept = new DepartmentViewModel();
                oDept.DepartmentID = Convert.ToInt16(data.DepartmentID);
                oDept.DepartmentName = data.DepartmentName;
                _listDept.Add(oDept);
            }

            return _listDept;
        }

        public void InsertContactTbl(string MessageBody, int StaffID, int FollowUpID)
        {
            ObjectEntity1.contact_tbl_Ticket_Insert(null, MessageBody, StaffID, FollowUpID);
        }


        public void InsertUpdateApt_Followups(int ActiveId, int AssignId, int rdoSeverityId, string rdoSeverityText, string rdoDeptId, string UserName, int StaffId, bool CboCloseId
                                       , string Content, string AssignText)
        {

            ObjectEntity1.ssp_InsertUpdateTicketClose(ActiveId, AssignId, rdoSeverityId, rdoSeverityText, rdoDeptId, UserName, StaffId, CboCloseId
                                             , Content, AssignText);
        }

        
    }
}
