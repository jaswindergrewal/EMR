using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class CriticalTaskViewModel
    {
        public int TaskID { get; set; }
        public int PatientID { get; set; }
        public bool Requested { get; set; }
        public bool Received { get; set; }
        public bool Reviewed { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<System.DateTime> ReviewedDate { get; set; }
        public Nullable<int> UploadID { get; set; }
        public string TaskName { get; set; }
        public string Upload_Title { get; set; }
        public string Upload_Path { get; set; }
        
    
    }

    public class uploadtblViewModel
    {
        public int UploadID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string Upload_Path { get; set; }
        public string Upload_Title { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Category { get; set; }
        public Nullable<System.DateTime> DateEntered { get; set; }
        public string extentionPath { get; set; }
        public byte[] Pdf_Binary { get; set; }
        public byte[] otherFormats_Binary { get; set; }
    }

    public class ContactPrintViewModel
    {
        public Nullable<int> PatientID { get; set; }
        public int ContactID { get; set; }
        public Nullable<System.DateTime> ContactDateEntered { get; set; }
        public string MessageBody { get; set; }
        public string FollowUpBody { get; set; }
        public string username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AptTypeDesc { get; set; }

    }

    public class uploadtblViewModelCritical
    {
        public int ID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string Upload_Path { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Category { get; set; }
    }

    public class ReportScanUploadViewModel
    {

        public string FileExt { get; set; }
        public byte[] Pdf_Binary { get; set; }
        public byte[] otherFormats_Binary { get; set; }
    }

    public class uploadtaglViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
    }

}
