using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class qb_match : LMCBase
{

    #region Variables
    IQBCustMatchPatientService objService = null;
    int PatientID = 0;
    #endregion

    #region Events

    /// <summary>
    /// If count is greater than 0 then show pnlMatched panel else pnlMatch panel and fill the dropdown
	/// </summary>
	protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            if (Request.QueryString["PatientID"] != null)
            {
                PatientID = int.Parse(Request.QueryString["PatientID"]);
                if (!IsPostBack)
                {
                    objService = new QBCustMatchPatientService();
                    int Count = 0;
                    List<QBCustMatchPatientViewModel> _listQBMatch = new List<QBCustMatchPatientViewModel>();
                   
                    _listQBMatch = objService.GetQBMatchListByPatientId(PatientID);
                    if (_listQBMatch.Count > 0)
                    {
                        foreach (var data in _listQBMatch)
                        {
                            Count = data.CountQB;
                            break;
                        }

                        if (Count == 0)
                        {
                            QBMatchEmrAddressViewModel _objAddress = objService.GetQBMatchAddressByPatientId(PatientID);
                            lblAddress.Text = _objAddress.EmrAddress.ToString();
                            ddlQBCustomers.DataSource = _listQBMatch;
                            ddlQBCustomers.DataTextField = "FullName";
                            ddlQBCustomers.DataValueField = "ListID";
                            ddlQBCustomers.DataBind();
                            pnlMatch.Visible = true;
                            pnlMatched.Visible = false;
                        }
                        else
                        {
                            pnlMatched.Visible = true;
                            pnlMatch.Visible = false;
                        }
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
	/// Insert the data in QB MAtch table for the patient
	/// </summary>

    protected void btnMatch_Click(object sender, EventArgs e)
	{
        try
        {
            System.Web.UI.HtmlControls.HtmlControl frame1 = (System.Web.UI.HtmlControls.HtmlControl)Utilities.FindControlRecursive(Page.Master, "PageContents");
            objService = new QBCustMatchPatientService();
            objService.InsertQBMatch(PatientID, ddlQBCustomers.SelectedValue);
            pnlMatched.Visible = true;
            pnlMatch.Visible = false;
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