using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace XeroApi.Model
{
    [DataContract(Namespace = "")]
    public abstract class ItemDetails
    {
        [DataMember(EmitDefaultValue = false)]
        public decimal UnitPrice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AccountCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TaxType { get; set; }
    }
}
