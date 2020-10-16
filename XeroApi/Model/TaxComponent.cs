using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroApi.Model
{
    public class TaxComponent:ModelBase
    {
         public string Name{get;set;}
         public decimal ? Rate{get;set;}
         public bool IsCompound { get; set; }
    
    }
    public class TaxComponents : ModelList<TaxComponent>
    {
    }
}
