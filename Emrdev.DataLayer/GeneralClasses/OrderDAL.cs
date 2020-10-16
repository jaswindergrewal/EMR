using AutoMapper;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer
{
    public class OrderDAL : ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetProduct<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        #endregion

        #region Delete Order

        public void DeleteOrder(int orderId)
        {
            ObjectEntity1.sp_DeleteOrderById(orderId);
        }

        #endregion

        #region Orders Get To Close

        public List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToClose(DateTime? startDate, DateTime? endDate, string status, int productId, string Radiobuttonstatus)
        {
            var objResult = ObjectEntityPart2.Orders_GetOrderbystatus(startDate, endDate, status,productId,Radiobuttonstatus).ToList();
            var objIList = new List<Emrdev.ViewModelLayer.OrderViewModel>();
            Mapper.CreateMap<Orders_GetOrderbystatus_Result, Emrdev.ViewModelLayer.OrderViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
           // return ObjectEntityPart2.Orders_GetOrderbystatus(startDate, endDate, status,productId,Radiobuttonstatus).Select(i => new Emrdev.ViewModelLayer.OrderViewModel { OrderID = i.OrderID, DatePrep = i.Date_Prepared, ShipName = i.Name, ShipAddress1 = i.Address, ShipCity = i.City, ShipState = i.State, ShipZip = i.Zip, PatientID = i.PatientID, Note = i.Note }).ToList();
        }

        public List<Emrdev.ViewModelLayer.OrderViewModel> OrderGetToCloseAutoShip(DateTime? startDate, DateTime? endDate)
        {
            var objResult = ObjectEntityPart2.Orders_GetOrderbystatusAutoship(startDate, endDate).ToList();
            var objIList = new List<Emrdev.ViewModelLayer.OrderViewModel>();
            Mapper.CreateMap<Orders_GetOrderbystatusAutoship_Result, Emrdev.ViewModelLayer.OrderViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
            // return ObjectEntityPart2.Orders_GetOrderbystatus(startDate, endDate, status,productId,Radiobuttonstatus).Select(i => new Emrdev.ViewModelLayer.OrderViewModel { OrderID = i.OrderID, DatePrep = i.Date_Prepared, ShipName = i.Name, ShipAddress1 = i.Address, ShipCity = i.City, ShipState = i.State, ShipZip = i.Zip, PatientID = i.PatientID, Note = i.Note }).ToList();
        }

        #endregion

        #region Validate Patient

        public string ValidatePatient(string patientName, string birthDate, string clinic)
        {
            return ObjectEntity1.Patients.FirstOrDefault(i => (i.LastName + " " + i.FirstName + " " + i.MiddleInitial) == patientName && i.Birthday == Convert.ToDateTime(birthDate) && i.Clinic == clinic && !i.Inactive.Value).LastName;
        }

        #endregion

        #region Get Orders By BatchId

        public List<Emrdev.ViewModelLayer.Orders_GetBatchViewModel> GetAllOrdersByBatchId(int batchId)
        {
            return ObjectEntity1.Orders_GetBatch(batchId).Select(i => new Emrdev.ViewModelLayer.Orders_GetBatchViewModel { AutoshipDiscounts = i.AutoshipDiscounts, Batch = i.Batch, CloseDate = i.CloseDate, DatePrep = i.DatePrep, EmployeeID = i.EmployeeID, FirstName = i.FirstName, Invoiced = i.Invoiced, LastName = i.LastName, Note = i.Note, OrderID = i.OrderID, AutoshipNote = i.AutoshipNote, PatientID = i.PatientID, ShipAddress1 = i.ShipAddress1, ShipAddress2 = i.ShipAddress2, ShipCity = i.ShipCity, ShipDate = i.ShipDate, ShipName = i.ShipName, ShipState = i.ShipState, ShipZip = i.ShipZip }).ToList();
        }

        #endregion

        #region Get OrderItems by OrderId

        public List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> GetOrderItemsByOrderId(int orderId)
        {
            return ObjectEntity1.OrderItems_GetOrder(orderId).Select(i => new Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel { OrderItemID = i.OrderItemID, ProductName = i.ProductName, Quantity = i.Quantity, Price = i.Price, CloseDate = i.CloseDate, ProfileItemID = i.ProfileItemID, ProductID = i.ProductID }).ToList();
        }

        #endregion

        #region Cancel An Order

        public void CancelAnOrder(int orderId)
        {
            //ObjectEntity1.Order_Cancel(orderId);
            ObjectEntityPart2.ssp_DeleteOrder(orderId);
        }

        public void CancelAnOrderWithReason(int orderId, bool xeroDelete, string deleteReason)
        {
           ObjectEntityPart2.ssp_DeleteOrderWithReason(orderId, xeroDelete, deleteReason);
        }

        public void DeleteOrderItem(int orderItemId)
        {
            ObjectEntityPart2.ssp_DeleteOrderItem(orderItemId);
        }


        public void AddOrderItems(int OrderId, int Quantity, decimal Price, string Weight, int ProductId)
        {
            ObjectEntityPart2.ssp_AddNewOrderItem(OrderId, Quantity, Price, Weight, ProductId);

        }

        #endregion

        public List<AutoShipStatusOrder>GetOrderById(int orderId)
        {
            var objResult = ObjectEntityPart2.ssp_GetOrderById(orderId).ToList();
            var objIList = new List<Emrdev.ViewModelLayer.AutoShipStatusOrder>();
            Mapper.CreateMap<ssp_GetOrderById_Result, Emrdev.ViewModelLayer.AutoShipStatusOrder>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
        #region Get OrderItem By Id

        public Emrdev.ViewModelLayer.ProfileItem_ShipViewModel GetOrderItemById(int orderItemId)
        {
            return ObjectEntity1.ProfileItem_Ship(orderItemId).Select(i => new Emrdev.ViewModelLayer.ProfileItem_ShipViewModel { ProfileItemID = i.ProfileItemID, PatientID = i.PatientID, ProductID = i.ProductID, Quantity = i.Quantity, Frequency = i.Frequency, StartDate = i.StartDate, EndDate = i.EndDate, LastShipped = i.LastShipped, DayToShip = i.DayToShip, DateEntered = i.DateEntered, NextShipDate = i.NextShipDate, DiscountID = i.DiscountID, Affliliate = i.Affliliate }).FirstOrDefault();
        }

        #endregion

        #region Reset Order Close Date

        public void ResetOrderCloseDate(int orderId)
        {
            Order objOrder=ObjectEntity1.Orders.SingleOrDefault(i => i.OrderID == orderId);
            if (objOrder != null)
            {
                objOrder.CloseDate = DateTime.Now;
                Edit<Order>(objOrder);
            }
            

        }

        #endregion

        #region Close An Order

        public void CloseOrder(int orderId)
        {
            ObjectEntity1.Order_Close(orderId);
        }

        #endregion

        #region Update Note By OrderId

        public void UpdateNoteByOrderId(int orderId, string note)
        {
            Order objOrder=ObjectEntity1.Orders.SingleOrDefault(i => i.OrderID == orderId);
            if (objOrder != null)
            {
                objOrder.Note = note;
                Edit<Order>(objOrder);
            }
        }

        #endregion

        public void ResetOrderStatus(int orderId, string Status)
        {
            
            ObjectEntityPart2.ssp_Orders_UpdateStatus(orderId, Status);
        }

        public void ResetToggleOrderStatus(int orderId, string Status)
        {

            ObjectEntityPart2.ssp_Orders_UpdateStatusToggle(orderId, Status);
        }

        public void UpdateAutoShipNote(int orderId, string Note,int UserId)
        {
            ObjectEntityPart2.ssp_UpdateAutoShipNote(orderId, Note,UserId);
        }

        public void UpdateOrderNote(int orderId, string Note, string ShippingAddress, string ShippingCity, string ShippingState, string ShippingZip,int UserId)
        {
            ObjectEntityPart2.ssp_UpdateOrderNote(orderId, Note, ShippingAddress, ShippingCity, ShippingState, ShippingZip,UserId);
        }

         public void UpdateQty(int OrderItemId, int qty)
        {
            ObjectEntityPart2.OrderItem_Update(OrderItemId, qty);
        }

         public List<AutoShipProduct> GetAllSkuProduts(int? ProductId)
         {
             var objResult = ObjectEntityPart2.ssp_GetProductDetailsForOrderitems(ProductId).ToList();
             var objIList = new List<Emrdev.ViewModelLayer.AutoShipProduct>();
             Mapper.CreateMap<ssp_GetProductDetailsForOrderitems_Result, Emrdev.ViewModelLayer.AutoShipProduct>();
             objIList = Mapper.Map(objResult, objIList);
             return objIList;
         }

        public ReadyToShippedOrder GetOrderForShippedByID(int OrderId)
        {
            return ObjectEntityPart2.Orders_GetOrderForShipped(OrderId).Select(
                 i => new Emrdev.ViewModelLayer.ReadyToShippedOrder
                 {
                     orderNumber = Convert.ToString(i.OrderID),
                     orderDate = i.orderDate,
                     orderStatus = i.orderStatus,
                     orderKey = Convert.ToString(i.orderKey),
                     Name = i.Name,
                     company = i.company,
                     street1 = i.street1,
                     street2 = i.street2,
                     city = i.city,
                     state = i.state,
                     PostalCode = i.PostalCode,
                     Weight = i.Weight,
                     Length = i.Length,
                     Width = i.Width,
                     Height = i.Height
                 }).FirstOrDefault();
        }

        #region GEt patient for call fire

        public List<Emrdev.ViewModelLayer.CallFirePatientListModel> GetPatientForCallFire(DateTime? startDate, DateTime? endDate, int apptType, string clinic, int provider)
        {
            var objResult = ObjectEntityPart2.ssp_GetPatientListForAppointmentDate(startDate, endDate, apptType,clinic,provider).ToList();
            var objIList = new List<Emrdev.ViewModelLayer.CallFirePatientListModel>();
            Mapper.CreateMap<ssp_GetPatientListForAppointmentDate_Result, Emrdev.ViewModelLayer.CallFirePatientListModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
            // return ObjectEntityPart2.Orders_GetOrderbystatus(startDate, endDate, status,productId,Radiobuttonstatus).Select(i => new Emrdev.ViewModelLayer.OrderViewModel { OrderID = i.OrderID, DatePrep = i.Date_Prepared, ShipName = i.Name, ShipAddress1 = i.Address, ShipCity = i.City, ShipState = i.State, ShipZip = i.Zip, PatientID = i.PatientID, Note = i.Note }).ToList();
        }

        #endregion+

    }
}
