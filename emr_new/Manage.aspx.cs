using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class Manage : LMCBase
{
    #region "Variables"
    protected int PatientID = 0;
    IFollowUpTypeService objIFollowUpTypeService = null;
    IPatientService objIPatientService = null;
    ISpecialAttentionService objSpecialattenService = null;
    string thisPath = "";
    #endregion

    #region "Events"
    /// <summary>
    /// loading patient details and menus on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["TicketCount"] == null) Session["TicketCount"] = 0;
            if (Request.QueryString["PatientID"] != null) PatientID = int.Parse(Request.QueryString["PatientID"]);
            HDStaffID.Value = ((int)Session["StaffID"]).ToString();
            HDPatientID.Value = Request.QueryString["PatientID"];
            if (Request.QueryString["TicketID"] != null)
            {
                objIFollowUpTypeService = new FollowUpTypeService();
                PatientID = objIFollowUpTypeService.GetPatientIdByAptFollowUps(int.Parse(Request.QueryString["TicketID"]));
                // patientid for usercontrol 
                PatInfo.PatientID = PatientID;
                HDPatientID.Value = PatientID.ToString();
                HDPatientIdByAptfollowUps.Value = (PatientID).ToString();
            }

            // patientid for usercontrol 
            PatInfo.PatientID = PatientID;
            objIPatientService = new PatientService();
            bool isAffiliate = objIPatientService.GetPatientAffiliate(PatientID);

            if (isAffiliate)
            {
                hidAffiliate.Value = "yes";
            }
            else
            {
                hidAffiliate.Value = "no";
            }
            if (!IsPostBack)
            {
                //Code added by jaswinder to show patient photo
                thisPath = Server.MapPath("~");
                thisPath += "\\TempPhoto\\";

                if (Directory.Exists(thisPath + Request.QueryString["PatientID"]))
                {
                    string[] files = Directory.GetFiles(thisPath + Request.QueryString["PatientID"]);
                    string lastFileName = string.Empty;
                    foreach (string filename in files)
                    {
                        if (string.Compare(filename, lastFileName) >= 1)
                        {

                            lastFileName = filename.Split('\\').Last();
                        }
                    }
                    if (File.Exists(thisPath + Request.QueryString["PatientID"] + "\\" + lastFileName))
                    {
                        imgPhoto.ImageUrl = "TempPhoto/" + Request.QueryString["PatientID"] + "/" + lastFileName;
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/images/default.jpg";
                    }
                }
                else
                {
                    imgPhoto.ImageUrl = "~/images/default.jpg";
                }
                System.Web.UI.HtmlControls.HtmlControl frame2 = (System.Web.UI.HtmlControls.HtmlControl)FindControlRecursive(Page.Master, "PageContents");
                //Commented by jaswinder as Client want to open in  new window
                //if(Request.QueryString["AutoShip"]!=null && Request.QueryString["AutoShip"].ToString()=="True")
                //{
                //    frame2.Attributes["src"] = "autoship/shortmanage.aspx?PatientID=" + PatientID.ToString() + "&StaffID=" + Request.QueryString["StaffID"].ToString() + "&MasterPage=~/LabSSRS/sub.master"; 
                //}
                if (Request.QueryString["Contact"] != null && Request.QueryString["Contact"].ToString() == "True")
                {
                    frame2.Attributes["src"] = "Contacts.aspx?PatientID=" + PatientID.ToString();
                }
                else if (Request.QueryString["PendingFollowup"] != null && Request.QueryString["PendingFollowup"].ToString() == "True")
                {
                    frame2.Attributes["src"] = "PendingFollowUps.aspx?PatientID=" + PatientID.ToString();
                }
                else if (Request.QueryString["TicketID"] != null)
                {
                    Session["ActiveTicket"] = Request.QueryString["TicketID"];
                    frame2.Attributes["src"] = "Ticket.aspx?PatientID=" + PatientID.ToString();
                }
                else
                {
                    frame2.Attributes["src"] = "PatientInfo.aspx?PatientID=" + PatientID.ToString();
                }
                HDQBCount.Value = objIPatientService.QBMatchCount(PatientID).ToString();
                objSpecialattenService = new SpecialAttentionService();
                HDSpecialAttention.Value = objSpecialattenService.GetSpecialAttentionCount(PatientID).ToString();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIPatientService = null;
        }

    }

    //On cancel redirect to landing page
    protected void btnCancelDoc_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandingPage.aspx");
    }

    #endregion

    #region "Method"

    /// <summary>
    /// Find the controls
    /// </summary>
    /// <param name="Root"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static Control FindControlRecursive(Control Root, string Id)
    {
        if (Root.ID == Id)
            return Root;

        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive(Ctl, Id);
            if (FoundCtl != null)
                return FoundCtl;
        }

        return null;
    }

    /// <summary>
    /// Check the QB Matches
    /// </summary>
    /// <param name="PatientId"></param>
    /// <param name="StaffId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static string CheckQBMatch(int PatientId, int StaffId)
    {
        string strMatchQB = string.Empty;
        IPatientService objService = null;
        try
        {
            objService = new PatientService();
            strMatchQB = objService.CheckMatch(PatientId, StaffId);

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;

        }
        return strMatchQB;

    }


    [System.Web.Services.WebMethod]
    public static PatientViewModel CheckPatientMed_renewalDate(int PatientId)
    {
        PatientViewModel strMatchQB = null;
        QBCustMatchPatientService objService = null;
        try
        {
            objService = new QBCustMatchPatientService();
            strMatchQB = objService.GetPatientDetailById(PatientId);

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;

        }
        return strMatchQB;

    }

    /// <summary>
    /// Get tickets by patient ID
    /// </summary>
    /// <param name="PatientId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<FollowupViewModel> GetTickets(int PatientID)
    {
        IFollowUpTypeService objServiceFollowup = null;
        List<FollowupViewModel> TicketList = null;
        try
        {
            objServiceFollowup = new FollowUpTypeService();
            TicketList = objServiceFollowup.GetFollowupListDetails(PatientID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceFollowup = null;
        }
        return TicketList;
    }

    /// <summary>
    /// Method to set session value
    /// </summary>
    /// <param name="SessionValue"></param>
    [System.Web.Services.WebMethod]
    public static void SetSession(int SessionValue)
    {
        if (SessionValue != 0)
            HttpContext.Current.Session["ActiveTicket"] = SessionValue;
    }
    #endregion
}