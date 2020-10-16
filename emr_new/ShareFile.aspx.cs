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


public partial class ShareFile : LMCBase
{
    #region"Variables"
    IUploadScanService objService = null;
    IPatientService objPatient = null;
    #endregion

    #region "events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



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
                thisPath += "\\ShareFiles\\";
                if (!Directory.Exists(thisPath + Request.QueryString["PatientID"]))
                {
                    Directory.CreateDirectory(thisPath + Request.QueryString["PatientID"]);
                }
                FileUpload1.SaveAs(thisPath + Request.QueryString["PatientID"] + "\\" + FileUpload1.FileName);
                int PatientId = Convert.ToInt32(Request.QueryString["PatientID"]);

                //string hostname = "Longevity138.sharefile.com";
                //string username = "sukhbirk4u@gmail.com";
                //string password = "3H#@T^xM3tEGDb";
                //string clientId = "Xv05EIpbAIxROPOhg3A8hIjvuHFYaF43";
                //string clientSecret = "xN6f1rE2cBK6a4D8B6WXcu1CqKiPdLVKvLHWVzTKtnlXj6Nb";

                string hostname = "lmclinic.sharefile.com";
                string username = "mike.cleek@relieffactor.com";
                string password = "nnnd o5tr g53h m2sf";
                string clientId = "j9E9nGQdFwDfGc1M5L36kGDHa9hI9jYV";
                string clientSecret = "szbpJ9IF8aowHShNU8emHyj5xcVju6rpE1X9zA6DqtOesgx9";

                string Emailfilepath = thisPath + Request.QueryString["PatientID"] + "\\" + "email.txt";
                System.IO.File.WriteAllText(Emailfilepath, txtBody.Text);
                

                objPatient = new PatientService();
                PatientViewModel pat = objPatient.GetPatientDataById(Convert.ToInt32(PatientId));
                string folderName;
                if (pat.Birthday != null)
                { folderName = pat.LastName + " " + pat.FirstName + " ( " + ((DateTime)pat.Birthday).ToShortDateString() + " )"; }
                else
                {
                    folderName = pat.LastName + " " + pat.FirstName + " ( None Entered )";
                }

                folderName = folderName.Replace("/", "");
                OAuth2Token token = ShareFiles.Authenticate(hostname, clientId, clientSecret, username, password);
                if (token != null)
                {
                    //ShareFiles.Getuser(token);
                   // ShareFiles.GetRoot(token, true);
                    ShareFiles.GetFolderWithQueryParameters(token, "fof45da3-206b-4a0d-8e57-cb37546f43c3", folderName, thisPath + PatientId + "\\" + FileUpload1.FileName, pat.Email, Emailfilepath,txtSubject.Text);

                    /// string Message =ShareFiles.CreateFolder(token, "fof2bbe7-3b5e-419d-a8b3-b8d14f82d383", pat.LastName+" "+pat.FirstName+" "+pat.Birthday.ToString(),"ShareFile");
                    // string Message = ShareFiles.CreateFolder(token, "fof2bbe7-3b5e-419d-a8b3-b8d14f82d383", PatientId, "hello");
                    //ShareFiles.UploadFile(token, "fo54565c-ede5-42b7-ae6d-303898c958db", thisPath + PatientId + "\\" + FileUpload1.FileName);
                }
                else
                {

                }
                ////string fileToUpload = thisPath + Request.QueryString["PatientID"] + "\\" + FileUpload1.FileName;// hdnfilepath.Value;//FileUpload1.PostedFile.FileName; // test file
                ////string myBucketName = "lmc-sharefiles";//"punch-scraped-data"; //your s3 bucket name goes here
                ////string s3DirectoryName = Request.QueryString["PatientID"];
                ////string s3FileName = FileUpload1.FileName;

                ////CAmazon myUploader = new CAmazon();
                ////bool b = myUploader.UploadFile(fileToUpload, myBucketName, s3DirectoryName, s3FileName);

                ////Upload_tblViewModel theUpload = new Upload_tblViewModel();


                ////theUpload.DateEntered = DateTime.Now;
                ////theUpload.PatientID = int.Parse(Request.QueryString["PatientID"]);
                ////theUpload.Upload_Path = FileUpload1.FileName;
                ////theUpload.Upload_Title = txtFilename.Text;
                ////objService = new UploadScanService();
                ////objService.InsertShareFileDocument(theUpload);
                ////AutoshipUtilities.SendFileShareMessage(Convert.ToInt32( Request.QueryString["PatientID"]), FileUpload1.FileName, txtFilename.Text);

                Response.Redirect("~/sharefile.aspx?PatientID="+Request.QueryString["PatientID"].ToString(), false);
                Context.ApplicationInstance.CompleteRequest();
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
            //Response.Redirect("Share.aspx?PatientID=" + Request.QueryString["PatientID"]);
        }
    }

    #endregion
}