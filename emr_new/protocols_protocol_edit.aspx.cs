using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder aug 20th 2013
/// </summary>

public partial class protocols_protocol_edit : System.Web.UI.Page
{
    #region "Variables"
    IProtocolService objService = null;
    public ProtocolViewModel lstProtocol = null;
    public string ProtocolId;

    #endregion

    #region "Events"

    /// <summary>
    /// Get protocol detail on page load
    /// jaswinder 20th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["protocol_id"] != null)
        {
            objService = new ProtocolService();
            ProtocolId = Request.QueryString["protocol_id"];
            lstProtocol = objService.GetProtocolByID(int.Parse(ProtocolId));
            if (!IsPostBack)
            {

                if (lstProtocol != null)
                {
                    ed.Content = lstProtocol.Protocol_Desc;
                    txtTitle.Text = lstProtocol.Protocol_Title;
                }
            }
        }
    }

    /// <summary>
    /// update the Protocol details by ID
    /// Jaswinder 20th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objService = new ProtocolService();
            objService.UpdateProtocolDetails(int.Parse(Request.QueryString["protocol_id"]), ed.Content, txtTitle.Text);

            Response.Redirect("Protocols_protocol_list.aspx", false);

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

    /// <summary>
    /// Redirect page to protocol list page
    /// Jaswinder 20th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("protocols_protocol_list.aspx", false);
    }

    #endregion
}