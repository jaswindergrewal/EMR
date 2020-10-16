using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class LandingPageDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            throw new NotImplementedException();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        #region Methods

        public List<MyTicketsViewModel> GetMyTickets(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            var objMyTickets = ObjectEntity1.ssp_grdMyTicketsData(StaffID, page, rows, ColName, sortorder);
            List<MyTicketsViewModel> _listMyTickets = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = data.Assigned;
                oMyTickets.Category = data.Category;
                oMyTickets.ChangeColor = data.ChangeColor;
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress>1)? "yes": "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listMyTickets.Add(oMyTickets);
            }
            return _listMyTickets;
        }

        public List<MyTicketsViewModel> GetCreatedClosed(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            var objMyTickets = ObjectEntity1.ssp_GetCreatedClosed(StaffID, page, rows, ColName, sortorder);
            List<MyTicketsViewModel> _listCreatedClosed = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = data.Assigned;
                oMyTickets.Category = data.Category;
                oMyTickets.ChangeColor = data.ChangeColor;
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress > 1) ? "yes" : "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listCreatedClosed.Add(oMyTickets);
            }
            return _listCreatedClosed;
        }

        public List<MyTicketsViewModel> GetMyActive(int StaffID, int page, int rows, string sortorder,string ColName)
        {
            var objMyTickets = ObjectEntity1.ssp_GetMyActive(StaffID, page, rows, ColName, sortorder);
            

            List<MyTicketsViewModel> _listMyActive = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = data.Assigned;
                oMyTickets.Category = data.Category;
                oMyTickets.ChangeColor = data.ChangeColor;
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress > 1) ? "yes" : "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listMyActive.Add(oMyTickets);
            }
            return _listMyActive;
        }

        public List<MyTicketsViewModel> GetMyGroupTickets(int StaffID, int ID, int page, int rows, string sortorder, string ColName)
        {
            var objMyTickets = ObjectEntity1.ssp_GetGroupTickets(StaffID, ID, page, rows, ColName, sortorder);
            List<MyTicketsViewModel> _listMyActive = new List<MyTicketsViewModel>();

            foreach (var data in objMyTickets)
            {
                MyTicketsViewModel oMyTickets = new MyTicketsViewModel();
                oMyTickets.Assigned = (data.Assigned!=null)? data.EmployeeName:data.DepartmentName;
                oMyTickets.Category = data.Category;
                if (data.ChangeColor == false)
                {
                    oMyTickets.ChangeColor = false;
                }
                else
                { oMyTickets.ChangeColor = true; }
                //string[] formats = { "yyyy-MM-dd" };
                //var dateTime = DateTime.ParseExact(data.CreateDate,formats, new CultureInfo("en-US"), DateTimeStyles.None);
                oMyTickets.CreateDate = data.CreateDate;
                oMyTickets.DaysOld = data.DaysOld;
                oMyTickets.FollowUp_ID = data.FollowUp_ID;
                oMyTickets.InProgress = (data.InProgress > 1) ? "yes" : "No";
                oMyTickets.Name = data.Name;
                oMyTickets.PatientID = data.PatientID;
                oMyTickets.Priority = data.Priority;
                oMyTickets.Subject = data.Subject;
                oMyTickets.RecordCount = data.RecordCount;
                _listMyActive.Add(oMyTickets);
            }
            return _listMyActive;
        }

        #endregion
    }
}
