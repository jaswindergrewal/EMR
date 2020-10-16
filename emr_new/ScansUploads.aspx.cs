using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;
using System.IO;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using Emrdev.AmazonService;
public partial class ScansUploads : LMCBase
{
    #region Variables
    protected int PatientID = 0;
    IUploadScanService objService = null;
    #endregion

    #region Event
    /// <summary>
    /// Get all the uploaded files for the particular patients
    /// </summary>

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                PatientID = int.Parse(Request.QueryString["PatientID"]);
                objService = new UploadScanService();
                List<uploadtblViewModel> lst = objService.GetScanList(PatientID);
                List<uploadtblViewModel> _listupload = new List<uploadtblViewModel>();
                foreach (var i in lst)
                {
                    uploadtblViewModel newlst = new uploadtblViewModel();
                    newlst.PatientID = i.PatientID;
                    newlst.Upload_Path = i.Upload_Path;
                    newlst.Upload_Title = i.Upload_Title;
                    newlst.Category = i.Category;
                    newlst.DateEntered = i.DateEntered;
                    newlst.UploadID = i.UploadID;
                    string path = Server.MapPath("~");
                    string imagePath = path + "\\uploads\\" + i.PatientID.ToString();

                    string fileExtension = System.IO.Path.GetExtension(imagePath + "\\" + i.Upload_Path).ToLower();
                    switch (fileExtension)
                    {
                        case ".jpg":
                            newlst.extentionPath = "images/images.png";
                            break;

                        case ".jpeg":
                            newlst.extentionPath = "images/images.png";
                            break;

                        case ".pdf":
                            newlst.extentionPath = "images/pdficon_small.png";
                            break;

                        case ".txt":
                            newlst.extentionPath = "images/txt.jpg";
                            break;
                        case ".doc":
                            newlst.extentionPath = "images/doc.gif";
                            break;
                        case ".xls":
                            newlst.extentionPath = "images/excel.gif";
                            break;

                        default:

                            newlst.extentionPath = "images/images.png";
                            break;

                    }

                    _listupload.Add(newlst);
                }


                rptDocuments.DataSource = _listupload;
                rptDocuments.DataBind();

                objService = new UploadScanService();
                lsttag.DataSource = objService.GetAllTagList();
                lsttag.DataTextField = "Name";
                lsttag.DataValueField = "Id";
                lsttag.DataBind();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }

    [WebMethod]
    public static string DownLoadFile(string patientId,string UploadId)
    {
        UploadScanService objService = new UploadScanService();
        uploadtblViewModel objlist = new uploadtblViewModel();
        objlist = objService.GetDocumentUploadedbyID(int.Parse(UploadId));
        string myBucketName = "lmc-emr-uploads";//"punch-scraped-data"; //your s3 bucket name goes here
        string s3DirectoryName = patientId;
        string s3FileName = objlist.Upload_Path;

        CAmazon myDownloader = new CAmazon();
         myDownloader.DownloadFile(s3DirectoryName, s3FileName, myBucketName);
        return s3FileName;

        
    }

    /// <summary>
    /// Delete Upload
    /// </summary>
    /// <param name="UploadID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string DeleteUpload(string UploadID)
    {
        UploadScanService objService = null;
        string strDeleteConfirmation = string.Empty;
        try
        {
            objService = new UploadScanService();
            uploadtblViewModel objlist = new uploadtblViewModel();
            objlist = objService.GetDocumentUploadedbyID(int.Parse(UploadID));
            string myBucketName = "lmc-emr-uploads";//"punch-scraped-data"; //your s3 bucket name goes here
            string s3DirectoryName = objlist.PatientID.ToString(); ;
            string s3FileName = objlist.Upload_Path;

            CAmazon myDownloader = new CAmazon();
            bool b = myDownloader.DeleteFile(s3DirectoryName, s3FileName, myBucketName);
            objService = new UploadScanService();
            objService.DeleteUpload(Convert.ToInt32(UploadID));
            strDeleteConfirmation = "Upload has been deleted successfully";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            strDeleteConfirmation = ex.Message;
        }
        finally
        {
            objService = null;
        }
        return strDeleteConfirmation;
    }
    #endregion
    protected void txtSearch_Click(object sender, EventArgs e)
    {
        string SelectedTagId = string.Empty;
        foreach (ListItem item in lsttag.Items)
        {
            if (item.Selected == true)
            {
                SelectedTagId = item.Value + "," + SelectedTagId;
            }
        }
        PatientID = int.Parse(Request.QueryString["PatientID"]);
        objService = new UploadScanService();
        List<uploadtblViewModel> lst = objService.GetScanList(PatientID, SelectedTagId);
        List<uploadtblViewModel> _listupload = new List<uploadtblViewModel>();
        foreach (var i in lst)
        {
            uploadtblViewModel newlst = new uploadtblViewModel();
            newlst.PatientID = i.PatientID;
            newlst.Upload_Path = i.Upload_Path;
            newlst.Upload_Title = i.Upload_Title;
            newlst.Category = i.Category;
            newlst.DateEntered = i.DateEntered;
            newlst.UploadID = i.UploadID;
            string path = Server.MapPath("~");
            string imagePath = path + "\\uploads\\" + i.PatientID.ToString();

            string fileExtension = System.IO.Path.GetExtension(imagePath + "\\" + i.Upload_Path).ToLower();
            switch (fileExtension)
            {
                case ".jpg":
                    newlst.extentionPath = "images/images.png";
                    break;

                case ".jpeg":
                    newlst.extentionPath = "images/images.png";
                    break;

                case ".pdf":
                    newlst.extentionPath = "images/pdficon_small.png";
                    break;

                case ".txt":
                    newlst.extentionPath = "images/txt.jpg";
                    break;
                case ".doc":
                    newlst.extentionPath = "images/doc.gif";
                    break;
                case ".xls":
                    newlst.extentionPath = "images/excel.gif";
                    break;

                default:

                    newlst.extentionPath = "images/images.png";
                    break;

            }

            _listupload.Add(newlst);
        }


        rptDocuments.DataSource = _listupload;
        rptDocuments.DataBind();


    }
}