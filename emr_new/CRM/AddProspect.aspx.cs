using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Emrdev.ServiceLayer;
using System.IO;
using System.Web.Services;


public partial class CRM_AddProspect : LMCBase
{
    #region Variables
    IAddProspectService objService = null;
    #endregion

    #region Events
    /// <summary>
    /// bind dropdownlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                objService = new AddProspectService();
                //TO Fill DropDown With Events
                ddlSeminar.DataSource = objService.GetAllEvents();
                ddlSeminar.DataBind();
                ddlSeminar.Items.Insert(0, new ListItem("--Select Event--", "0"));

                //TO Fill DropDown With MarketingSources
                ddlHowHear.DataSource = objService.GetHearFromProspect();
                ddlHowHear.DataBind();
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

    /// <summary>
    /// Insert the data for crm_prospect table and also add a new file for addprospecttable with the first name
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddContact_Click(object sender, EventArgs e)
    {

        try
        {
            objService = new AddProspectService();
            //insert Data in Crm_Prospect table
            objService.InsertProspect(
                txtAddress.Text,
                txtCity.Text,
                txtEmail.Text,
                txtFirstName.Text,
                true,
                txtLastName.Text,
                txtPhone.Text,
                ddlHowHear.SelectedValue,
                string.Empty,
                ddlState.SelectedValue,
                4,
                txtZip.Text,
                (int)Session["StaffID"],
                int.Parse(ddlSeminar.SelectedValue)
                );


            //Write a new file in AddProspectFiles folder
            string thisPath = Server.MapPath("~");
            thisPath += "\\AddProspectFiles\\";

            StreamWriter wr = new StreamWriter(thisPath + txtFirstName.Text + txtLastName.Text + ".lmc");
            wr.WriteLine(txtEmail.Text + "|" + txtFirstName.Text + "|" + txtLastName.Text + "|" + int.Parse(ddlSeminar.SelectedValue) + "|" + txtPhone.Text);
            wr.Close();
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtZip.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('You have successfully added the prospect data.') </script>");

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
    #endregion

    /// <summary>
    /// this method is using for check the duplicate Email address during Add/Edit the record.
    /// Tab: Manage Prospect
    /// created by: Deepak Thakur
    /// created date: 02.Aug.2013
    /// </summary>
    /// <param name="diagnosisID"></param>
    /// <param name="diagName"></param>
    /// <param name="iCDCode"></param>
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateProspect(string prospectID, string emailAddress)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(prospectID))
                prospectID = "0";
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateProspect(Convert.ToInt32(prospectID), emailAddress);
            if (isExist == true)
                strDuplicate = "duplicate";
            else
                strDuplicate = string.Empty;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            _objService = null;
        }
        return strDuplicate;
    }
}
