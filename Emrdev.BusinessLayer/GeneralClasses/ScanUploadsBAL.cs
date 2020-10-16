using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class ScanUploadsBAL
    {
        ScanUploadsDAL objDAL = new ScanUploadsDAL();

        public List<uploadtaglViewModel> GetTagList(bool Disabled)
        {
            var _objTagList = new List<uploadtaglViewModel>();
            var TagEntity = new List<UploadTag>();
            TagEntity = objDAL.GetAll<UploadTag>(o => o.Disabled == Disabled).ToList();

            Mapper.CreateMap<UploadTag, uploadtaglViewModel>();
            _objTagList = Mapper.Map(TagEntity, _objTagList);
            return _objTagList;
        }

        public List<uploadtaglViewModel> GetAllTagList()
        {
            var _objTagList = new List<uploadtaglViewModel>();
            var TagEntity = new List<UploadTag>();
            TagEntity = objDAL.GetAll<UploadTag>(o => o.Id > 0).ToList();

            Mapper.CreateMap<UploadTag, uploadtaglViewModel>();
            _objTagList = Mapper.Map(TagEntity, _objTagList);
            return _objTagList;
        }




        public void InsertUpdateTags(int TagId, bool Disabled, string Name)
        {
            UploadTag TagEntity = new UploadTag();

            TagEntity = objDAL.Get<UploadTag>(o => o.Id == TagId);
            objDAL = new ScanUploadsDAL();
            if (TagEntity != null)
            {
                TagEntity.Disabled = Disabled;
                TagEntity.Name = Name;
                objDAL.Edit(TagEntity);

            }
            else
            {
                UploadTag TagEntity1 = new UploadTag();
                TagEntity1.Disabled = Disabled;
                TagEntity1.Name = Name;
                objDAL.Create(TagEntity1);
            }
        }


        /// <summary>
        /// Get all the uploaded files for the particular patients
        /// </summary>
        /// <param name="PatientId"></param>

        public List<uploadtblViewModel> GetScanList(int PatientId)
        {
            var _objScanList = new List<uploadtblViewModel>();
            var ScanEntity = new List<Upload_tbl>();
            ScanEntity = objDAL.GetAll<Upload_tbl>(o => o.PatientID == PatientId).OrderByDescending(o => o.DateEntered).ThenBy(o => o.Category).ToList();
            foreach (var item in ScanEntity)
            {


                string TagName = string.Empty;

                uploadtblViewModel data = new uploadtblViewModel();
                data.DateEntered = item.DateEntered;
                data.PatientID = item.PatientID;
                data.Upload_Path = item.Upload_Path;
                data.Upload_Title = item.Upload_Title;
                data.UploadID = item.UploadID;
                string[] TagId = item.Category.Split(',');
                int Id = 0;
                for (int i = 0; i <= TagId.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(TagId[i]))
                    {
                        Id = Convert.ToInt32(TagId[i]);
                        var CategoryName = objDAL.Get<UploadTag>(o => o.Id == Id);
                        TagName = CategoryName.Name + "," + TagName;
                    }
                }
                data.Category = TagName;
                _objScanList.Add(data);

            }
            //Mapper.CreateMap<Upload_tbl, uploadtblViewModel>();
            //_objScanList = Mapper.Map(ScanEntity, _objScanList);
            return _objScanList;
        }


        /// <summary>
        /// Get the uploaded files details by id
        /// jaswinder 16th aug 2013
        /// </summary>
        /// <param name="UploadId"></param>
        /// <returns></returns>
        public uploadtblViewModel GetDocumentUploadedbyID(int UploadId)
        {
            var _objScanList = new uploadtblViewModel();
            var ScanEntity = new Upload_tbl();
            ScanEntity = objDAL.Get<Upload_tbl>(o => o.UploadID == UploadId);

            Mapper.CreateMap<Upload_tbl, uploadtblViewModel>();
            _objScanList = Mapper.Map(ScanEntity, _objScanList);
            return _objScanList;
        }

        /// <summary>
        /// Update the upload files details
        /// Jaswinder 16th aug 2013
        /// </summary>
        /// <param name="UploadId"></param>
        /// <param name="FileName"></param>
        /// <param name="Category"></param>
        public void UpdateUploadDocument(int UploadId, string FileName, string Category)
        {
            objDAL.UpdateUploadDocument(UploadId, FileName, Category);
        }

        /// <summary>
        /// Delete the uploaded document
        /// </summary>
        /// <param name="UploadID"></param>
        public void DeleteUpload(int UploadID)
        {
            objDAL.DeleteUpload(UploadID);
        }

        /// <summary>
        /// To insert the Document
        /// Jaswinder 9 sept 2013
        /// </summary>
        /// <param name="UploadDocumentView"></param>
        public void InsertDocument(Upload_tblViewModel UploadDocumentView)
        {
            Upload_tbl uploadEntity = new Upload_tbl();
            AutoMapper.Mapper.CreateMap<Upload_tblViewModel, Upload_tbl>();
            uploadEntity = AutoMapper.Mapper.Map(UploadDocumentView, uploadEntity);

            objDAL.Create(uploadEntity);
        }
        public void InsertShareFileDocument(Upload_tblViewModel UploadDocumentView)
        {
            ShareFile uploadEntity = new ShareFile();
            AutoMapper.Mapper.CreateMap<Upload_tblViewModel, ShareFile>();
            uploadEntity = AutoMapper.Mapper.Map(UploadDocumentView, uploadEntity);

            objDAL.Create(uploadEntity);
        }

        //Emr2017
        //public List<ReportScanUploadViewModel> ReportScanupload(int PatientID, int All, DateTime FromDate, DateTime ToDate)
        //{
        //    return objDAL.ReportScanupload(PatientID, All, FromDate, ToDate);
        //}

        public List<uploadtblViewModel> GetScanList(int PatientId, string Category)
        {
            var _objScanList = new List<uploadtblViewModel>();
            var ScanEntity = new List<Upload_tbl>();
            ScanEntity = objDAL.GetAll<Upload_tbl>(o => o.PatientID == PatientId).OrderByDescending(o => o.DateEntered).ThenBy(o=>o.Category).ToList();
            string[] SearchTagId = Category.Split(',');
            foreach (var item in ScanEntity)
            {


                string TagName = string.Empty;

                uploadtblViewModel data = new uploadtblViewModel();
                data.DateEntered = item.DateEntered;
                data.PatientID = item.PatientID;
                data.Upload_Path = item.Upload_Path;
                data.Upload_Title = item.Upload_Title;
                data.UploadID = item.UploadID;
                string[] TagId = item.Category.Split(',');
                int Id = 0;
                bool SearchItem = false;
                for (int i = 0; i <= TagId.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(TagId[i]))
                    {
                        Id = Convert.ToInt32(TagId[i]);
                        if (SearchItem == false)
                        {
                            for (int j = 0; j <= SearchTagId.Length - 1; j++)
                            {
                                if (!string.IsNullOrEmpty(SearchTagId[j]))
                                {
                                    if (Id == Convert.ToInt32(SearchTagId[j]))
                                    {
                                        SearchItem = true;
                                    }
                                }
                            }
                        }

                        var CategoryName = objDAL.Get<UploadTag>(o => o.Id == Id);
                        TagName = CategoryName.Name + "," + TagName;
                    }
                }
                data.Category = TagName;
                if (SearchItem == true)
                { _objScanList.Add(data); }


            }
            //Mapper.CreateMap<Upload_tbl, uploadtblViewModel>();
            //_objScanList = Mapper.Map(ScanEntity, _objScanList);
            return _objScanList;
        }
    }
}
