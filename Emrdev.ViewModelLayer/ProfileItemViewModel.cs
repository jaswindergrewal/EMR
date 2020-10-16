using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public partial class ProfileItemViewModel
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

    public partial class ProfileItemsGetTree
    {
        public int ProfileItemID { get; set; }
        public string Frequency { get; set; }
        public string ProductName { get; set; }
        public System.DateTime NextShipDate { get; set; }
        public int Quantity { get; set; }
        public string DiscountName { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public string Pending { get; set; }
        public int GroupNumber { get; set; }
        public System.DateTime LastDate { get; set; }
        public string Exception { get; set; }
        public bool Affiliate { get; set; }
    }

    public partial class ProfileItem_GetByID_ViewModel
    {
        public int ProfileItemID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Frequency { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> DayToShip { get; set; }
        public System.DateTime LastShipped { get; set; }
        public int ProductID { get; set; }
        public int DiscountID { get; set; }
        public System.DateTime NextShipDate { get; set; }
        public string FrequencyValue { get; set; }
    }

    public  class ProfileItemsOrdersViewModel
    {
        public int ProfileItemID { get; set; }
        public int PatientID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Frequency { get; set; }
        public string ProductName { get; set; }
        public decimal AutoshipPrice { get; set; }
        public System.DateTime NextShipDate { get; set; }
        public System.DateTime LastShipped { get; set; }
        public int DiscountID { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    }
}
