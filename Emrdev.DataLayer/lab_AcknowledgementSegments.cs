//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emrdev.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class lab_AcknowledgementSegments
    {
        public int ID { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int MessageID { get; set; }
        public string AcknowledgementCode { get; set; }
        public string MessageControlID { get; set; }
        public string TextMessage { get; set; }
    
        public virtual lab_Messages lab_Messages { get; set; }
    }
}
