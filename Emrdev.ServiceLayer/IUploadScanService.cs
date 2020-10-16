using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUploadScanService" in both code and config file together.
    [ServiceContract]
    public interface IUploadScanService
    {
        [OperationContract]
        List<uploadtaglViewModel> GetTagList(bool Disabled);

        [OperationContract]
        List<uploadtaglViewModel> GetAllTagList();


        [OperationContract]
        void InsertUpdateTags(int TagId, bool Disabled , string Name);

        [OperationContract]
        List<uploadtblViewModel>GetScanList(int PatientId) ;

        [OperationContract]
        List<uploadtblViewModel> GetScanList(int PatientId,string Category);

        [OperationContract]
        uploadtblViewModel GetDocumentUploadedbyID(int UploadId);

        [OperationContract]
        void UpdateUploadDocument(int UploadId, string FileName, string Category);

        [OperationContract]
        void DeleteUpload(int UploadID);

        [OperationContract]
        void InsertDocument(Upload_tblViewModel UploadDocumentView);
        [OperationContract]
        void InsertShareFileDocument(Upload_tblViewModel UploadDocumentView);

        ////Emr2017
        //[OperationContract]
        //List<ReportScanUploadViewModel> ReportScanupload(int PatientID, int All, DateTime FromDate, DateTime ToDate);
    }
}
