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
    
    public partial class ShareFile
    {
        public int UploadID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string Upload_Path { get; set; }
        public string Upload_Title { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public byte[] Pdf_Binary { get; set; }
        public byte[] otherFormats_Binary { get; set; }
    }
}
