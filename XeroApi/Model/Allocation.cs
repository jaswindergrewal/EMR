using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroApi.Model
{
    public class Allocation : EndpointModelBase
    {
       
       public decimal? AppliedAmount { get; set; }

        public Invoice Invoice { get; set; }
       
        public DateTime? Date { get; set; }
    }


    public class Allocations : ModelList<Allocation>
    {
    }
}
