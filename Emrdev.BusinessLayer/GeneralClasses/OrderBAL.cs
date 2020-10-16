using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class OrderBAL
    {
        #region Global

        Emrdev.DataLayer.OrderDAL objDAL;

        #endregion


        #region Delete Order

        public void DeleteOrder(int orderId)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.DeleteOrder(orderId);
        }

        #endregion

        #region Order To Close

        public List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToClose(DateTime? startDate, DateTime? endDate, string status, int productId, string Radiobuttonstatus)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.OrderGetToClose(startDate, endDate, status, productId, Radiobuttonstatus);
        }

        public List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToCloseAutoShip(DateTime? startDate, DateTime? endDate)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.OrderGetToCloseAutoShip(startDate, endDate);
        }

        #endregion

        #region Validate Patient

        public string ValidatePatient(string patientNameString)
        {
            //Split Incoming string to extract PatientName,BirDate and Clinic
            string[] searcher = patientNameString.Split('(');
            if (searcher.Count() != 3)//Not a Valid Record
                return null;
            string patientName = searcher[0].Trim();
            string clinic = searcher[1].Split(')')[0].Trim();
            switch (clinic)
            {
                case "T":
                    clinic = "South";
                    break;
                case "S":
                    clinic = "Seattle";
                    break;
                case "K":
                    clinic = "Kirkland";
                    break;
                case "L":
                    clinic = "Lynnwood";
                    break;
            }
            string birthDate = string.Empty;
            try
            {
                birthDate = DateTime.Parse(patientNameString.Split('(')[2].Split(')')[0]).ToShortDateString();
                if (birthDate.Length < 10)
                    birthDate = "0" + birthDate;
            }
            catch { }
            if (birthDate == "1/1/0001") birthDate = null;

            objDAL = new DataLayer.OrderDAL();
            return objDAL.ValidatePatient(patientName, birthDate, clinic);
        }

        #endregion

        #region Get Orders By BatchId

        public List<Emrdev.ViewModelLayer.Orders_GetBatchViewModel> GetAllOrdersByBatchId(int batchId)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.GetAllOrdersByBatchId(batchId);
        }

        #endregion

        #region Get OrderItems by OrderId

        public List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> GetOrderItemsByOrderId(int orderId)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.GetOrderItemsByOrderId(orderId);
        }

        #endregion

        #region Cancel An Order

        public void CancelAnOrder(int orderId)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.CancelAnOrder(orderId);
        }

        public void CancelAnOrderWithReason(int orderId, bool xeroDelete, string deleteReason)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.CancelAnOrderWithReason(orderId, xeroDelete, deleteReason);
        }

        public void DeleteOrderItem(int orderItemId)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.DeleteOrderItem(orderItemId);
        }

        public void AddOrderItems(int OrderId, int Quantity, decimal Price, string Weight, int ProductId)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.AddOrderItems(OrderId, Quantity, Price, Weight, ProductId);

        }
        #endregion

        public List<AutoShipStatusOrder> GetOrderById(int orderId)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.GetOrderById(orderId);
            
        }

        #region Get OrderItem By Id

        public Emrdev.ViewModelLayer.ProfileItem_ShipViewModel GetOrderItemById(int orderItemId)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.GetOrderItemById(orderItemId);
        }

        #endregion

        #region Reset Order Close Date

        public void ResetOrderCloseDate(int orderId)
        {
            objDAL = new OrderDAL();
            objDAL.ResetOrderCloseDate(orderId);
            CampaignTypeDAL objCamp = new CampaignTypeDAL();
            objCamp.DeleteExceptions(orderId);
        }

        #endregion

        #region Close An Order

        public void CloseOrder(int orderId)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.CloseOrder(orderId);
        }

        #endregion

        #region Update Note By OrderId

        public void UpdateNoteByOrderId(int orderId, string note)
        {
            objDAL = new DataLayer.OrderDAL();
            objDAL.UpdateNoteByOrderId(orderId, note);
        }

        #endregion


        #region Reset Order Status

        public void ResetOrderStatus(int orderId, string Status)
        {
            objDAL = new OrderDAL();
            objDAL.ResetOrderStatus(orderId, Status);

        }

        public void ResetToggleOrderStatus(int orderId, string Status)
        {
            objDAL = new OrderDAL();
            objDAL.ResetToggleOrderStatus(orderId, Status);

        }
        public void UpdateAutoShipNote(int orderId, string Note,int UserId)
        {
            objDAL = new OrderDAL();
            objDAL.UpdateAutoShipNote(orderId, Note,UserId);
        }

        public void UpdateOrderNote(int orderId, string Note, string ShippingAddress, string ShippingCity, string ShippingState, string ShippingZip,int UserId)
        {
            objDAL = new OrderDAL();
            objDAL.UpdateOrderNote(orderId, Note, ShippingAddress, ShippingCity, ShippingState, ShippingZip,UserId);
        }

        public void UpdateQty(int OrderItemId, int qty)
        {
            objDAL = new OrderDAL();
            objDAL.UpdateQty(OrderItemId, qty);
        }
        public List<AutoShipProduct> GetAllSkuProduts(int? ProductId)
        {
            objDAL = new OrderDAL();
            return objDAL.GetAllSkuProduts(ProductId);
        }

        #endregion

        #region Order To Select for Ready
        public ReadyToShippedOrder GetOrderForShippedByID(int OrderId)
        {
            objDAL = new OrderDAL();
            return objDAL.GetOrderForShippedByID(OrderId);
        }
        #endregion

        public List<Emrdev.ViewModelLayer.CallFirePatientListModel> GetPatientForCallFire(DateTime? startDate, DateTime? endDate, int apptType, string clinic, int provider)
        {
            objDAL = new DataLayer.OrderDAL();
            return objDAL.GetPatientForCallFire(startDate, endDate, apptType, clinic, provider);
        }

        public int CheckDuplicateForProduct(int ID, string Name, string Sku, string tableName)
        {
            int isExist = 0;
            if (tableName == "AutoshipProducts")
            {// used in Autoship/AutoShip.aspx.cs
                if (ID == 0)
                {
                    objDAL = new DataLayer.OrderDAL();
                    var objfirst = objDAL.GetProduct<Emrdev.DataLayer.AutoshipProduct>(o => o.ProductName == Name);
                    if (objfirst != null)
                        isExist = 1;
                    else
                    {
                        objDAL = new DataLayer.OrderDAL();
                        objfirst = objDAL.GetProduct<Emrdev.DataLayer.AutoshipProduct>(o => o.Sku == Sku);
                        if (objfirst != null)
                            isExist = 2;
                    }
                }
                else
                {
                    objDAL = new DataLayer.OrderDAL();
                    var objfirst = objDAL.GetProduct<Emrdev.DataLayer.AutoshipProduct>(o => o.ProductName == Name && o.ProductID != ID);
                    if (objfirst != null)
                        isExist = 1;
                    else
                    {
                        objDAL = new DataLayer.OrderDAL();
                        objfirst = objDAL.GetProduct<Emrdev.DataLayer.AutoshipProduct>(o => o.Sku == Sku && o.ProductID != ID);
                        if (objfirst != null)
                            isExist = 2;
                    }
                }


            }

            return isExist;

        }
    }
}
