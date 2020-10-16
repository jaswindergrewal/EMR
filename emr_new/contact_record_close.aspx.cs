using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact_record_close : System.Web.UI.Page
{
    IContactService objService = null;
    protected ContacttblViewModel objContacttblViewModel;
    protected ContactRecordCloseViewModel objContactRecordCloseViewModel;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request["ContactID"] != null && Request["ContactID"].ToString() != string.Empty)
                {
                    BindFollowUpDetails(Convert.ToInt32(Request["ContactID"].ToString()));
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

    #region BindFollowUpDetails
    /// <summary>
    /// Bind FollowUp Details.
    /// </summary>
    /// <param name="ContactID"></param>
    public void BindFollowUpDetails(int ContactID)
    {
        objService = new ContactService();
        objContactRecordCloseViewModel = objService.GetContactRecordCloseDetails(ContactID);
        lblFirestName.Text = objContactRecordCloseViewModel.FirstName.ToString();
        lblLastName.Text = objContactRecordCloseViewModel.LastName.ToString();
        lblContactDateEntered.Text = objContactRecordCloseViewModel.ContactDateEntered.ToString();
        lblAptTypeDesc.Text = objContactRecordCloseViewModel.AptTypeDesc.ToString();
        lblEnteredBy.Text = objContactRecordCloseViewModel.EnteredBy.ToString();
        bool ReqsFollow_YN = objContactRecordCloseViewModel.ReqsFollow_YN != null ? Convert.ToBoolean(objContactRecordCloseViewModel.ReqsFollow_YN) : false;
        if (ReqsFollow_YN)
        {
            pnlReqsFollow_YN.Visible = true;
            lblReqsFollow_YN.Text = objContactRecordCloseViewModel.ReqsFollow_YN.ToString();
            lblFollowUp_Date.Text = objContactRecordCloseViewModel.FollowUp_Date.ToString();
            lblCat_Desc.Text = objContactRecordCloseViewModel.Cat_Desc.ToString();
        }
        lblMessageBody.Text = objContactRecordCloseViewModel.MessageBody.ToString();
        ViewState["ContactID"] = objContactRecordCloseViewModel.ContactID.ToString();
        txtAreaFollowUpBody.Value = objContactRecordCloseViewModel.FollowUpBody != null ?objContactRecordCloseViewModel.FollowUpBody.ToString():"";
        chkFollowUp_Completed.Checked = objContactRecordCloseViewModel.FollowUP_Completed != null ? Convert.ToBoolean(objContactRecordCloseViewModel.FollowUP_Completed) : false;
        ViewState["PatientID"] = objContactRecordCloseViewModel.PatientID;
        btnBack.PostBackUrl = "Patientinfo.aspx?PatientID=" + objContactRecordCloseViewModel.PatientID;

    }
    #endregion

    #region btnSubmit_Click
    /// <summary>
    /// this event used for update contact information.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request["ContactID"] != null && Request["ContactID"].ToString() != string.Empty)
            {
                objService = new ContactService();
                objContacttblViewModel = new ContacttblViewModel();
                objContacttblViewModel.ContactID = Convert.ToInt32(Request["ContactID"].ToString());
                objContacttblViewModel.FollowUpBody = txtAreaFollowUpBody.Value.ToString();
                objContacttblViewModel.FollowUP_Completed = chkFollowUp_Completed.Checked;
                objContacttblViewModel.FollowUp_ActualDate = DateTime.Now;
                objService.UpdateContact(objContacttblViewModel);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "Success", "alert('You have successfully updated the record.')", true);
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
    #endregion
}