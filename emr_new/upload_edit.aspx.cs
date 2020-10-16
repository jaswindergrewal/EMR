using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Emrdev.AmazonService;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 19th aug 2013
/// </summary>

public partial class upload_edit : System.Web.UI.Page
{
    #region "Variables"

    IUploadScanService objService = null;
    uploadtblViewModel objlist = null;

    #endregion

    #region"Events"

    /// <summary>
    /// Get the detail of upload document
    /// Jaswinder aug 19 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objService = new UploadScanService();
                bool Disabled = false;

                chkTag.DataSource = objService.GetTagList(Disabled);
                chkTag.DataBind();
                if (Request.QueryString["UploadID"] != null)
                {
                    objService = new UploadScanService();
                    objlist = new uploadtblViewModel();
                    objlist = objService.GetDocumentUploadedbyID(int.Parse(Request.QueryString["UploadID"]));
                    if (objlist != null)
                    {

                        txtFileName.Text = objlist.Upload_Title;
                        hdnFileName.Value = objlist.Upload_Title;
                        string[] TagId = objlist.Category.Split(',');

                        for (int i = 0; i <= TagId.Length - 1; i++)
                        {
                            if (!string.IsNullOrEmpty(TagId[i]))
                            {

                                foreach (ListItem item in chkTag.Items)
                                {
                                    if (TagId[i] == item.Value)
                                    {
                                        item.Selected = true;
                                        break;
                                    }
                                }

                            }
                        }

                        //ddCategory.SelectedItem.Text = objlist.Category;
                        hdnPatientId.Value = (objlist.PatientID.ToString());

                    }
                }
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

    /// <summary>
    /// Update the data for upload document
    /// Jaswinder 19 aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objService = new UploadScanService();
            string category = string.Empty;
            foreach (ListItem item in chkTag.Items)
            {
                if (item.Selected == true)
                {
                    category = item.Value + "," + category.Trim();
                }

            }
           

            objService.UpdateUploadDocument(int.Parse(Request.QueryString["UploadID"]), txtFileName.Text, category);
            Response.Redirect("ScansUploads.aspx?PatientID=" + hdnPatientId.Value, false);

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

    /// <summary>
    /// On cancel redirect the page to mange page
    /// jaswinder aug 19 3013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?PatientID=" + hdnPatientId.Value, false);
    }
    #endregion
}