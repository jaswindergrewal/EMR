using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LandingPageService" in both code and config file together.
    public class LandingPageService : ILandingPageService
    {
        LandingPageBAL objBAL = new LandingPageBAL();
        public List<MyTicketsViewModel> GetMyTickets(int StaffID,int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetMyTickets(StaffID,page, rows, sortorder,ColName);
        }

        public List<MyTicketsViewModel> GetCreatedClosed(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetCreatedClosed(StaffID,page, rows, sortorder,ColName);
        }

        public List<MyTicketsViewModel> GetMyActive(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetMyActive(StaffID, page, rows, sortorder,ColName);
        }

        public List<MyTicketsViewModel> GetMyGroupTickets(int StaffID, int ID, int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetMyGroupTickets(StaffID, ID, page, rows, sortorder, ColName);
        }

        public List<MyTicketsViewModel> GetMyTicketsCAL(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetMyTicketsCAL(StaffID, page, rows, sortorder, ColName);
        }

        public List<MyTicketsViewModel> GetCreatedClosedCAL(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetCreatedClosedCAL(StaffID, page, rows, sortorder, ColName);
        }

        public List<MyTicketsViewModel> GetMyActiveCAL(int StaffID, int page, int rows, string sortorder, string ColName)
        {
            return objBAL.GetMyActiveCAL(StaffID, page, rows, sortorder, ColName);
        }
    }
}
