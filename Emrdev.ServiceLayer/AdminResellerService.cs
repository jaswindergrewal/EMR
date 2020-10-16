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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminResellerService" in both code and config file together.
    public class AdminResellerService : IAdminResellerService
    {
        AdminResellerBAL objBAL = new AdminResellerBAL();
        public List<AdminResellerViewModel> GetAllRellers()
        {
            return objBAL.GetAllRellers();
        }

        public List<StatusViewModel> GetResellerStatus()
        {
            return objBAL.GetResellerStatus();
        }

        public List<EventViewModel> GetResellerEvent()
        {
            return objBAL.GetResellerEvent();
        }

        public List<SaleRepViewModel> GetSaleRep()
        {
            return objBAL.GetSaleRep();
        }

        public List<ResellerMarketingSourceViewModel> GetMarketingSource()
        {
            return objBAL.GetMarketingSource();
        }

        public void InsertContact(AdminResellerContactViewModel Cont)
        {
            objBAL.InsertContact(Cont);
        }

        public void InsertResellerInfo(AdminResellerViewModel res, int StaffID)
        {
            objBAL.InsertResellerInfo(res, StaffID);
        }

        public void InsertUpdateStatus(int SatusId, bool Active, string StatusName)
        {
            objBAL.InsertUpdateStatus(SatusId, Active, StatusName);
        }

        public  void InsertUpdateEvent(int EventId, bool Active, string EventName)
        {
            objBAL.InsertUpdateEvent(EventId, Active, EventName);
        }

        public void InsertUpdateSource(int ResellerMarketingSourceID, bool Active, string SourceName)
        {
            objBAL.InsertUpdateSource(ResellerMarketingSourceID, Active, SourceName);
        }


        public bool CheckDuplicateResellerStatus(int ResllerStatusId, string StatusName)
        {
            return objBAL.CheckDuplicateResellerStatus(ResllerStatusId, StatusName);
        }


        public bool CheckDuplicateEvent(int EventID, string EventName)
        {
            return objBAL.CheckDuplicateEvent(EventID, EventName);
        }


        public bool CheckDuplicateSource(int ResellerMarketingSourceID, string SourceName)
        {
            return objBAL.CheckDuplicateSource(ResellerMarketingSourceID, SourceName);
        }


        public void DeleteStatusManagement(int Id)
        {
            objBAL.DeleteStatusManagement(Id);
        }

        public void DeleteEventManagement(int Id)
        {
            objBAL.DeleteEventManagement(Id);
        }

        public void DeleteMarketingSourceManagement(int Id)
        {
            objBAL.DeleteMarketingSourceManagement(Id);
        }
    }
}
