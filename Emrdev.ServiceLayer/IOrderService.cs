using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public interface IOrderService
    {
        [OperationContract]
        void DeleteOrder(int orderId);

        [OperationContract]
        List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToClose(DateTime? startDate, DateTime? endDate, string status, int productId, string Radiobuttonstatus);

        [OperationContract]
        List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToCloseAutoShip(DateTime? startDate, DateTime? endDate);
        

        [OperationContract]
        string ValidatePatient(string patientName);

        [OperationContract]
        List<Emrdev.ViewModelLayer.Orders_GetBatchViewModel> GetAllOrdersByBatchId(int batchId);

        [OperationContract]
        List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> GetOrderItemsByOrderId(int orderId);

        [OperationContract]
        void CancelAnOrder(int orderId);

        [OperationContract]
        void CancelAnOrderWithReason(int orderId,bool xeroDelete,string deleteReason);

        [OperationContract]
        void DeleteOrderItem(int orderItemId);

        [OperationContract]
        void AddOrderItems(int OrderId, int Quantity, decimal Price, string Weight, int ProductId);

        [OperationContract]
        List<AutoShipStatusOrder> GetOrderById(int orderId);

        [OperationContract]
        Emrdev.ViewModelLayer.ProfileItem_ShipViewModel GetOrderItemById(int orderItemId);

        [OperationContract]
        void ResetOrderCloseDate(int orderId);

        [OperationContract]
        void CloseOrder(int orderId);

        [OperationContract]
        void UpdateNoteByOrderId(int orderId, string note);

        [OperationContract]
        void ResetOrderStatus(int orderId, string Status);

        [OperationContract]
        void ResetToggleOrderStatus(int orderId, string Status);

        [OperationContract]
        void UpdateAutoShipNote(int orderId, string Note,int UserId);

        [OperationContract]
        void UpdateOrderNote(int orderId, string Note, string ShippingAddress, string ShippingCity, string ShippingState, string ShippingZip,int UserId);

        [OperationContract]
        void UpdateQty(int OrderItemId, int qty);

        [OperationContract]
        List<AutoShipProduct> GetAllSkuProduts(int? ProductId);

        [OperationContract]
        ReadyToShippedOrder GetOrderForShippedByID(int OrderId);
        [OperationContract]
        List<Emrdev.ViewModelLayer.CallFirePatientListModel> GetPatientForCallFire(DateTime? startDate, DateTime? endDate, int apptType, string clinic, int provider);

        [OperationContract]
        int CheckDuplicateForProduct(int ID, string Name, string Sku, string tableName);
    }
}
