using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Emrdev.ServiceLayer;

public partial class AddPhoto : LMCBase
{
    #region "variables"

    IPatientService objService = null;
    string thisPath = "";
    #endregion

    #region "Events"
    /// <summary>
    /// on pageload set the value of clinic dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PatientID"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    objService = new PatientService();

                    ddlClinic.DataSource = objService.GetClinics();
                    ddlClinic.DataTextField = "ClinicName";
                    ddlClinic.DataValueField = "ClinicID";
                    ddlClinic.DataBind();

                    Calendar.Patient pat = new Calendar.Patient(int.Parse(Request.QueryString["PatientID"]));
                    ddlClinic.SelectedValue = pat.ClinicID;
                }
                catch (System.Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
                }
                finally
                {
                    objService = null;
                }
            }
        }
        else
        {
            Response.Redirect("~/landingpage.aspx");
        }
    }


    /// <summary>
    /// Add the jpg/gif/png photo fro patient in tempphoto folder with patientid folder & delete the existing photo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ImageUpload.HasFile && ImageUpload.FileName != string.Empty && ImageUpload.FileContent.Length > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(ImageUpload.FileName).ToLower();
                if (fileExtension == ".gif" || fileExtension == ".jpg" || fileExtension == ".png")
                {

                    thisPath = Server.MapPath("~");
                    thisPath += "\\TempPhoto\\";

                    if (!Directory.Exists(thisPath + Request.QueryString["PatientID"]))
                    {
                        Directory.CreateDirectory(thisPath + "\\" + Request.QueryString["PatientID"]);
                    }
                    else
                    {
                        string[] files = Directory.GetFiles(thisPath + Request.QueryString["PatientID"]);

                        foreach (string filename in files)
                        {
                            System.IO.File.Delete(filename);

                        }
                    }
                    ImageUpload.SaveAs(thisPath + "\\" + Request.QueryString["PatientID"] + "\\" + ImageUpload.FileName);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> parent.change_parent_url('Manage.aspx?PatientID=" + Request.QueryString["PatientID"] + "'); </script>");
                }
            }
           
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
       
    }

    #endregion
}