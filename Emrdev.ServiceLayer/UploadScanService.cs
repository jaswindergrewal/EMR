using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UploadScanService" in both code and config file together.
    public class UploadScanService : IUploadScanService
    {
        ScanUploadsBAL objBAL = new ScanUploadsBAL();
        
        public List<uploadtaglViewModel> GetTagList(bool Disabled)
        {
            return objBAL.GetTagList(Disabled);
        }


        public List<uploadtaglViewModel> GetAllTagList()
        {
            return objBAL.GetAllTagList();
        }


        public void InsertUpdateTags(int TagId, bool Disabled, string Name)
        {
            objBAL.InsertUpdateTags(TagId, Disabled, Name);
        }
        
        public List<uploadtblViewModel> GetScanList(int PatientId)
        {
            return objBAL.GetScanList(PatientId);
        }

        public List<uploadtblViewModel> GetScanList(int PatientId,string Category)
        {
            return objBAL.GetScanList(PatientId, Category);
        }


        public uploadtblViewModel GetDocumentUploadedbyID(int UploadId)
        {
            return objBAL.GetDocumentUploadedbyID(UploadId);
        }

        public void UpdateUploadDocument(int UploadId, string FileName, string Category)
        {
             objBAL.UpdateUploadDocument(UploadId, FileName, Category);
        }

        public void DeleteUpload(int UploadID)
        {
            objBAL.DeleteUpload(UploadID);
        }

        public void InsertDocument(Upload_tblViewModel UploadDocumentView)
        {
            objBAL.InsertDocument(UploadDocumentView);
        }
        public void InsertShareFileDocument(Upload_tblViewModel UploadDocumentView)
        {
            objBAL.InsertShareFileDocument(UploadDocumentView);
        }

        ////Emr2017
        //public List<ReportScanUploadViewModel> ReportScanupload(int PatientID, int All, DateTime FromDate, DateTime ToDate)
        //{
        //    return objBAL.ReportScanupload(PatientID, All, FromDate, ToDate);
        //}
    }
}
