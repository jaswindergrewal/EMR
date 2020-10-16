using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProfileItemService" in both code and config file together.
    [ServiceContract]
    public interface IProfileItemService
    {
        [OperationContract]
        ProfileItemViewModel GetProfitItemList(int ProfileItemID);

        [OperationContract]
        void UpdateAffliliateInProfileItems(int ProfileItemId, bool Affliliate);

        [OperationContract]
        void AddRecordsInProfileItem(int PatientId, int ProductID, int Quantity, DateTime StartDate, DateTime EndDate, DateTime NextShipDate, DateTime LastShipped, int DayToShip, int DiscountID);

        [OperationContract]
        List<ProfileItemsGetTree> ProfileItemGetTree(int PatientId);

        [OperationContract]
        System.Data.DataTable ProfileItemGetTree1(int patientId, string connection);

        [OperationContract]
        void DoWork();

        [OperationContract]
        Emrdev.ViewModelLayer.ProfileItem_GetByID_ViewModel GetProfileItemById(int profielItemId);

        [OperationContract]
        void UpdateNextShipDateByProfileItemId(int profileItemId, DateTime nextShipDate);
    }
}
