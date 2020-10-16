using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int PatientID { get; set; }
        public System.DateTime DatePrep { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public string ShipName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public int Batch { get; set; }
        public System.DateTime ShipDate { get; set; }
        public bool Invoiced { get; set; }
        public string Note { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string QBID { get; set; }
        public int RecordCount { get; set; }
        public string Name { get; set; }
        public System.DateTime Date_Prepared { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string AutoshipNote { get; set; }
        public string Status { get; set; }
        public Nullable<System.Guid> XeropatientId { get; set; }
        public bool Paid { get; set; }
        public Nullable<bool> CallBeforeShip { get; set; }
        public string HotNotes { get; set; }
        public virtual ICollection<OrderItemViewModel> OrderItems { get { return OrderItems; } }

    }

    public class Orders_GetBatchViewModel
    {
        public int OrderID { get; set; }
        public int PatientID { get; set; }
        public System.DateTime DatePrep { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public string ShipName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public int Batch { get; set; }
        public System.DateTime ShipDate { get; set; }
        public bool Invoiced { get; set; }
        public string Note { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string AutoshipNote { get; set; }
        public string AutoshipDiscounts { get; set; }
    }

    public class OrderItems_GetOrderViewModel
    {
        public int OrderItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public int ProfileItemID { get; set; }
        public int ProductID { get; set; }
    }

    public class ProfileItem_ShipViewModel
    {
        public int ProfileItemID { get; set; }
        public int PatientID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Frequency { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public System.DateTime LastShipped { get; set; }
        public Nullable<int> DayToShip { get; set; }
        public System.DateTime DateEntered { get; set; }
        public System.DateTime NextShipDate { get; set; }
        public int DiscountID { get; set; }
        public Nullable<int> Affliliate { get; set; }
    }

    public class AutoShipStatusOrder

    {
        public int OrderID { get; set; }
        public string ShipName { get; set; }
        public System.DateTime DatePrep { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public int PatientID { get; set; }
        public string Note { get; set; }
        public string ShipDate { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string AutoshipNote { get; set; }
        public int OrderItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public string Weight { get; set; }
        public Nullable<System.Guid> XeropatientId { get; set; }
        public bool Paid { get; set; }
        public bool Invoiced { get; set; }
        public string Status { get; set; }
        public bool FreeShipping { get; set; }
        public string HotNotes { get; set; }
    }

    public class AutoShipProduct
    {
        public string Name { get; set; }
        public string Weight { get; set; }
        public decimal AutoshipPrice { get; set; }
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public int ProductID { get; set; }
    }

    public class ReadyToShippedOrder
    {
        public string Name { get; set; }
        public string company { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string PostalCode { get; set; }
        public string country { get; set; }
        public string orderNumber { get; set; }
        public System.DateTime orderDate { get; set; }
        public string orderKey { get; set; }
        public string orderStatus { get; set; }
        public string Weight { get; set; }
        public Nullable<decimal> Length { get; set; }
        public Nullable<decimal> Width { get; set; }
        public Nullable<decimal> Height { get; set; }
    }
}
