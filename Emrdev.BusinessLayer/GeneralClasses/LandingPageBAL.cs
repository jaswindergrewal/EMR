using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
   public class LandingPageBAL
   {       
       LandingPageDAL objDAL = new LandingPageDAL();

       //Method get the data for active tickets 
       public List<MyTicketsViewModel> GetMyTickets(int StaffID,int page, int rows, string sortorder, string ColName)
       {
           return objDAL.GetMyTickets(StaffID, page, rows, sortorder,ColName);
       }

       //Method get the data for Tickets I Closed
       public List<MyTicketsViewModel> GetCreatedClosed(int StaffID, int page, int rows, string sortorder, string ColName)
       {
           return objDAL.GetCreatedClosed(StaffID, page, rows, sortorder, ColName);
       }

       //Methods to get the data for tickets I created
       public List<MyTicketsViewModel> GetMyActive(int StaffID, int page, int rows, string sortorder, string ColName)
       {
           return objDAL.GetMyActive(StaffID, page, rows, sortorder,ColName);
       }

       //Method to get the data for futur tickects       
       public List<MyTicketsViewModel> GetMyGroupTickets(int StaffID, int ID, int page, int rows, string sortorder, string ColName)
       {
           return objDAL.GetMyGroupTickets(StaffID, ID,page, rows, sortorder, ColName);
       }

       public List<MyTicketsViewModel> GetMyTicketsCAL(int StaffID, int page, int rows, string sortorder, string ColName)
       {
           CampaignTypeDAL objCampDal = new CampaignTypeDAL();

           return objCampDal.GetMyTicketsCAL(StaffID, page, rows, sortorder, ColName);
       }

       public List<MyTicketsViewModel> GetCreatedClosedCAL(int StaffID, int page, int rows, string sortorder, string ColName)
       {
           CampaignTypeDAL objCampDal = new CampaignTypeDAL();
           return objCampDal.GetCreatedClosedCAL(StaffID, page, rows, sortorder, ColName);
       }

       public List<MyTicketsViewModel> GetMyActiveCAL(int StaffID, int page, int rows, string sortorder, string ColName)
       {
           CampaignTypeDAL objCampDal = new CampaignTypeDAL();
           return objCampDal.GetMyActiveCAL(StaffID, page, rows, sortorder, ColName);
       }
   
   }
}
