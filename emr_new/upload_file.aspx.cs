using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using Emrdev.AmazonService;

public partial class upload_file : LMCBase
{
    #region"Variables"
    IUploadScanService objService = null;
    #endregion

    #region "events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objService = new UploadScanService();
            bool Disabled = false;

            chkTag.DataSource = objService.GetTagList(Disabled);
            chkTag.DataBind();
        }
    }

    /// <summary>
    /// upload the document 
    /// Jaswinder 9th sept 2013
    /// Architecture changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string thisPath = "";
                thisPath = Server.MapPath("~");
                thisPath += "\\uploads\\";
                if (!Directory.Exists(thisPath + Request.QueryString["PatientID"]))
                {
                    Directory.CreateDirectory(thisPath + Request.QueryString["PatientID"]);
                }
                FileUpload1.SaveAs(thisPath + Request.QueryString["PatientID"] + "\\" + FileUpload1.FileName);
                string fileToUpload = thisPath + Request.QueryString["PatientID"] + "\\" + FileUpload1.FileName;// hdnfilepath.Value;//FileUpload1.PostedFile.FileName; // test file
                string myBucketName = "lmc-emr-uploads";//"punch-scraped-data"; //your s3 bucket name goes here
                string s3DirectoryName = Request.QueryString["PatientID"];
                string s3FileName = FileUpload1.FileName;

                CAmazon myUploader = new CAmazon();
                bool b = myUploader.UploadFile(fileToUpload, myBucketName, s3DirectoryName, s3FileName);
                if (b == true)
                {
                    //Console.WriteLine("Uploaded Successfully");
                    //Console.ReadLine();
                }
                else
                {
                    //Console.WriteLine("Not Uploded");
                    //Console.ReadLine();
                }
                Upload_tblViewModel theUpload = new Upload_tblViewModel();
                string category = string.Empty;
                foreach (ListItem item in chkTag.Items)
                {
                    if (item.Selected == true)
                    {
                        category = item.Value + "," + category.Trim();
                    }

                }
                theUpload.Category = category;
                theUpload.DateEntered = DateTime.Now;
                theUpload.PatientID = int.Parse(Request.QueryString["PatientID"]);
                theUpload.Upload_Path = FileUpload1.FileName;
                theUpload.Upload_Title = txtFilename.Text;
                objService = new UploadScanService();
                objService.InsertDocument(theUpload);
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
            Response.Redirect("ScansUploads.aspx?PatientID=" + Request.QueryString["PatientID"]);
        }
    }

    #endregion
}