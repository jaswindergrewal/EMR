using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminResellerService" in both code and config file together.
    [ServiceContract]
    public interface IAdminResellerService
    {
        [OperationContract]
        List<AdminResellerViewModel> GetAllRellers();

        [OperationContract]
        List<StatusViewModel> GetResellerStatus();

        [OperationContract]
        List<EventViewModel> GetResellerEvent();

        [OperationContract]
        List<SaleRepViewModel> GetSaleRep();

        [OperationContract]
        List<ResellerMarketingSourceViewModel> GetMarketingSource();

        [OperationContract]
        void InsertContact(AdminResellerContactViewModel Cont);

        [OperationContract]
        void InsertResellerInfo(AdminResellerViewModel res, int StaffID);

        [OperationContract]
        void InsertUpdateStatus(int SatusId, bool Active, string StatusName);

        [OperationContract]
        void InsertUpdateEvent(int EventId, bool Active, string EventName);

        [OperationContract]
        void InsertUpdateSource(int ResellerMarketingSourceID, bool Active, string SourceName);

        [OperationContract]
        bool CheckDuplicateResellerStatus(int ResllerStatusId, string StatusName);

        [OperationContract]
        bool CheckDuplicateEvent(int EventID, string EventName);

        [OperationContract]
        bool CheckDuplicateSource(int ResellerMarketingSourceID, string SourceName);

        [OperationContract]
        void DeleteStatusManagement(int Id);

        [OperationContract]
        void DeleteEventManagement(int Id);

        [OperationContract]
        void DeleteMarketingSourceManagement(int Id);

    }
}
