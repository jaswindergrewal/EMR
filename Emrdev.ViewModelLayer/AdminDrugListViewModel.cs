using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class AdminDrugListViewModel
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Viewable_yn { get; set; }
        public string Gender { get; set; }
        public string DrugType { get; set; }
        public string DrugCategory { get; set; }
        public Nullable<bool> Supplement_yn { get; set; }
        public Nullable<bool> Reviewed { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> ProductID { get; set; }
    }

    public class sp_GetDrugList_Result
    {
        public int DrugID { get; set; }
        public string DrugName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Viewable_yn { get; set; }
        public string Gender { get; set; }
        public string DrugType { get; set; }
        public string DrugCategory { get; set; }
        public Nullable<bool> Supplement_yn { get; set; }
        public Nullable<bool> Reviewed { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}
