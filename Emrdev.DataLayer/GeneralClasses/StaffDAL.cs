using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class StaffDAL : ObjectEntity, IRepositary
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
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).Count();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }

        #endregion

        public List<StaffViewModel> GetStaffDetailsByDepartment(int DepartmentId)
        {
            var objResult = ObjectEntity1.ssp_GetDepartmentStaffDetailsByDeptId(DepartmentId).ToList();
            var objIList = new List<StaffViewModel>();
            Mapper.CreateMap<ssp_GetDepartmentStaffDetailsByDeptId_Result, StaffViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<StaffViewModel> GetStaffDetailsByEmployeeId(int EmployeeID)
        {
            var objResult = GetDetails<Staff>().Where(s => s.EmployeeID == EmployeeID).ToList();
            var objIList = new List<StaffViewModel>();
            Mapper.CreateMap<Staff, StaffViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public StaffViewModel GetFirstOrDefaultStaffByEmployeeId(int EmployeeID)
        {
            var objResult = new StaffViewModel();
            var PatientEntity = new Staff();
            PatientEntity = Get<Staff>(o => o.EmployeeID == EmployeeID);
            Mapper.CreateMap<Staff, StaffViewModel>();
            objResult = Mapper.Map(PatientEntity, objResult);
            return objResult;
        }

       

        public void InsertTicketIntoContactTbl(int PatinetId, string messageBody, int employeeID, int FollowupId)
        {
            ObjectEntity1.contact_tbl_Ticket_Insert(PatinetId, messageBody, employeeID, FollowupId);
        }

        public StaffViewModel GetFirstOrDefaultStaffByUserName(string userName)
        {
            var objResult = new StaffViewModel();
            var PatientEntity = new Staff();
            PatientEntity = Get<Staff>(o => o.username == userName && o.Active_YN == true);
            Mapper.CreateMap<Staff, StaffViewModel>();
            objResult = Mapper.Map(PatientEntity, objResult);
            return objResult;
        }

        /// <summary>
        /// For Get the staff Detail list with customise paging
        /// Jaswinder on 5th aug 2013
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<StaffViewModel> GetStaffList(int PageIndex, int PageSize)
        {
            var objResult = ObjectEntity1.ssp_GetStaffLogins(PageIndex, PageSize).ToList();
            var objIList = new List<StaffViewModel>();
            Mapper.CreateMap<ssp_GetStaffLogins_Result, StaffViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        /// <summary>
        /// Update the staff login details
        /// Jaswinder on 5th aug 2013
        /// </summary>
        /// <param name="EmployeeName"></param>
        /// <param name="Password"></param>
        /// <param name="CanWritePrescript"></param>
        /// <param name="AccessLevel"></param>
        /// <param name="EmployeeId"></param>
        public void UpdateStaffLogin(string EmployeeName, string Password, bool CanWritePrescript, string AccessLevel,int EmployeeId, bool HARep, bool IsActive)
        {
            ObjectEntity1.ssp_UpdateStaffLogins(EmployeeName,AccessLevel, Password,EmployeeId,CanWritePrescript,HARep, IsActive);
        }
        
        /// <summary>
        /// Return 1 as a scalar value if the username not exists other wise 0
        /// jaswinder on 5th aug 2013
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="EmployeeName"></param>
        /// <param name="Access_level"></param>
        /// <param name="CanWritePrescript"></param>
        /// <returns></returns>
        public int InsertStaffLogin(string UserName, string Password, string EmployeeName, string Access_level, bool CanWritePrescript,bool HARep)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(ObjectEntity1.fn_InsertStaffLogins(UserName, Password, EmployeeName, Access_level, CanWritePrescript, HARep).Single());
                return result;
            }
            catch (System.Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// Update AutoshipAccess By EmployeId
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="autoshipAccess"></param>
        public void UpdateStaffAutoShipAccess(int EmployeeID, string autoshipAccess)
        {
            ObjectEntity1.Staff_UpdateAutoship(EmployeeID, autoshipAccess);
        }

        /// <summary>
        /// Select AutoshipAccess By Employee Id
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public string GetAutoshipAccessByEmployeeID(int EmployeeID)
        {
            return ObjectEntity1.Staffs.SingleOrDefault(i => i.EmployeeID == EmployeeID).AutoshipAccess;
        }

        

    }
}
