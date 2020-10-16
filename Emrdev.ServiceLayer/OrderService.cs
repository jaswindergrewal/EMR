using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class OrderService:IOrderService
    {
        #region Global

        Emrdev.GeneralClasses.OrderBAL objBAL=new GeneralClasses.OrderBAL();

        #endregion

        #region Delete Order

        public void DeleteOrder(int orderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.DeleteOrder(orderId);
        }

        #endregion

        #region Order To Close

        public List<ViewModelLayer.OrderViewModel> OrderGetToClose(DateTime? startDate, DateTime? endDate,string status,int productId, string Radiobuttonstatus)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.OrderGetToClose(startDate, endDate, status, productId, Radiobuttonstatus);
        }

        public List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToCloseAutoShip(DateTime? startDate, DateTime? endDate)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.OrderGetToCloseAutoShip(startDate, endDate);
        }

        public List<Emrdev.ViewModelLayer.CallFirePatientListModel> GetPatientForCallFire(DateTime? startDate, DateTime? endDate, int apptType, string clinic, int provider)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetPatientForCallFire(startDate, endDate,apptType,clinic,provider);
        }



        #endregion

        #region Validate Patient

        public string ValidatePatient(string patientName)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.ValidatePatient(patientName);
        }

        #endregion

        #region Get Orders By BatchId

        public List<Emrdev.ViewModelLayer.Orders_GetBatchViewModel> GetAllOrdersByBatchId(int batchId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetAllOrdersByBatchId(batchId);
        }

        #endregion

        #region Get OrderItems by OrderId

        public List<ViewModelLayer.OrderItems_GetOrderViewModel> GetOrderItemsByOrderId(int orderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetOrderItemsByOrderId(orderId);
        }

        #endregion

        #region Cancel An Order By Order Id

        public void CancelAnOrder(int orderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.CancelAnOrder(orderId);
        }
        public void CancelAnOrderWithReason(int orderId, bool xeroDelete, string deleteReason)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.CancelAnOrderWithReason( orderId,  xeroDelete,  deleteReason);
        }
        
        public void DeleteOrderItem(int orderItemId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.DeleteOrderItem(orderItemId);
        }

        public void AddOrderItems(int OrderId, int Quantity, decimal Price, string Weight, int ProductId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.AddOrderItems(OrderId, Quantity, Price, Weight, ProductId);

        }
        #endregion
        public List<AutoShipStatusOrder> GetOrderById(int orderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetOrderById(orderId);

        }

        #region Get OrderItem By Id

        public ViewModelLayer.ProfileItem_ShipViewModel GetOrderItemById(int orderItemId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetOrderItemById(orderItemId);
        }

        #endregion

        #region Reset Order Close Date

        public void ResetOrderCloseDate(int orderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.ResetOrderCloseDate(orderId);
        }

        #endregion

        #region Close An Order

        public void CloseOrder(int orderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.CloseOrder(orderId);
        }

        #endregion

        #region Update Note By OrderId

        public void UpdateNoteByOrderId(int orderId, string note)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.UpdateNoteByOrderId(orderId, note);
        }

        #endregion

        public void ResetOrderStatus(int orderId, string Status)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.ResetOrderStatus(orderId, Status);
        }

        public void ResetToggleOrderStatus(int orderId, string Status)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.ResetToggleOrderStatus(orderId, Status);
        }
        public void UpdateAutoShipNote(int orderId, string Note,int userId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.UpdateAutoShipNote(orderId, Note,userId);
        }

        public void UpdateOrderNote(int orderId, string Note, string ShippingAddress, string ShippingCity, string ShippingState, string ShippingZip,int UserId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.UpdateOrderNote(orderId, Note, ShippingAddress, ShippingCity, ShippingState, ShippingZip,UserId);
        }

        public void UpdateQty(int OrderItemId, int qty)
        {
            objBAL = new GeneralClasses.OrderBAL();
            objBAL.UpdateQty(OrderItemId, qty);
        }

       public List<AutoShipProduct> GetAllSkuProduts(int? ProductId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetAllSkuProduts(ProductId);
        }
        public int CheckDuplicateForProduct(int ID, string Name, string Sku, string tableName)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.CheckDuplicateForProduct(ID, Name, Sku, tableName);
        }

        #region Order To Select for Ready
        public ReadyToShippedOrder GetOrderForShippedByID(int OrderId)
        {
            objBAL = new GeneralClasses.OrderBAL();
            return objBAL.GetOrderForShippedByID(OrderId);
        }
        #endregion

    }
}
