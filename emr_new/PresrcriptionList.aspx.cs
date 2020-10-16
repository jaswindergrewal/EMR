using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Obout.Grid;
using OboutInc.Window;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class PresrcriptionList : LMCBase
{
# region "Variables"
    protected int PatientID = -1;
    protected string Clinic = "";
    IPrescriptionService objPrescriptionService = null;
    IStaffService objIStaffService = null;
#endregion

    #region "Events"
    
    /// <summary>
    /// Set the master page on the bases of querystring
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "~/site.master";

    }

    /// <summary>
    /// Page load to set values in session and cookies and bind the grids
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PatientID"] != null) PatientID = int.Parse(Request.QueryString["PatientID"]);
        ScriptManager man = (ScriptManager)Utilities.FindControlRecursive(Master, "ScriptManager");
        if (this.MasterPageFile.Contains("sub.master"))
        {
            PatInfo.Visible = false;
            btnBack.Visible = false;
            btnBackSupp.Visible = false;
        }
        else {
            PatInfo.PatientID = PatientID;

        }

        Calendar.Patient objPat = new Calendar.Patient(PatientID);
        Clinic = objPat.ClinicID.ToString();
       

       
       
        if (!IsPostBack)
        {
           
            grdSupps.Rebind += grdSupps_Rebind;
            grdHistSupp.Rebind += grdHistSupp_Rebind;
            BindData();
        }

        HtmlInputHidden txtPatientID = (HtmlInputHidden)Utilities.FindControlRecursive(Master, "txtPatientID");
        HtmlInputHidden txtFullScreen = (HtmlInputHidden)Utilities.FindControlRecursive(Master, "txtFullScreen");
        HtmlInputHidden txtStaffID = (HtmlInputHidden)Utilities.FindControlRecursive(Master, "txtStaffID");
        HtmlInputHidden txtAptID = (HtmlInputHidden)Utilities.FindControlRecursive(Master, "txtAptID");
        TextBox txtStartDate = (TextBox)Utilities.FindControlRecursive(Master, "txtStartDate");
        TextBox txtEndDate = (TextBox)Utilities.FindControlRecursive(Master, "txtEndDate");

        TextBox txtStartDateRefill = grdScrips.Templates[1].Container.FindControl("txtStartDateRefill") as TextBox;
        txtStartDate.Attributes.Add("readonly", "readonly");
        txtEndDate.Attributes.Add("readonly", "readonly");
        txtStartDateRefill.Attributes.Add("readonly", "readonly");
        txtPatientID.Value = PatientID.ToString();

      
        txtStaffID.Value = ((int)Session["StaffID"]).ToString();
        txtAptID.Value = Request.QueryString["aptid"] != null ? Request.QueryString["aptid"] : "";
        if (Page.MasterPageFile.Contains("sub"))
        {
            txtFullScreen.Value = "no";
        }
        else
        {
            txtFullScreen.Value = "yes";

        }
    }

   /// <summary>
   /// Reload the Drug grid view list
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void grdNew_Rebind(object sender, EventArgs e)
    {
        List<PrescriptionDrugViewModel> lstPrescriptionDrug = null;
        List<DrugViewModel> lstDrugs = null;
        try
        {
            lstPrescriptionDrug = new List<PrescriptionDrugViewModel>();
            objPrescriptionService = new PrescriptionService();
            lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "").ToList();
            int[] currIds = lstPrescriptionDrug.Select(pairs => Convert.ToInt32(pairs.DrugID)).ToArray();

            lstDrugs = new List<DrugViewModel>();
            lstDrugs = objPrescriptionService.GetDrugList().ToList();
            var drugs = lstDrugs.Where(p => p.DrugName != "" && p.Viewable_yn == true && !currIds.Contains(Convert.ToInt32(p.DrugID)) && p.Supplement_yn == false).OrderByDescending(p => p.DrugName).ToList();
            grdNew.DataSource = drugs;
            grdNew.DataBind();

        }
        catch (System.Exception ex)
        {
            
        }
        finally
        {
            lstPrescriptionDrug = null;
            lstDrugs = null;
            objPrescriptionService = null;
        }

    }

    /// <summary>
    /// Reload the Suppliment grid view list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdNewSupp_Rebind(object sender, EventArgs e)
    {
        List<PrescriptionSupplierViewModel> lstSupplierDetails = null;
        List<AutoshipProductsViewModel> lstAutoshipProductsDetails = null;
        try
        {
            objPrescriptionService = new PrescriptionService();
            lstSupplierDetails = new List<PrescriptionSupplierViewModel>();
            lstSupplierDetails = objPrescriptionService.GetSupplementsDetails(PatientID).ToList();

            int[] currIdsSupps = lstSupplierDetails.Select(pairs => Convert.ToInt32(pairs.ProductID)).ToArray();
            string ids = string.Empty;
            for (int i = 0; i < currIdsSupps.Length - 1; i++)
            {
                ids += currIdsSupps[i].ToString() + ',';
            }
            lstAutoshipProductsDetails = new List<AutoshipProductsViewModel>();
            lstAutoshipProductsDetails = objPrescriptionService.GetNewSupplementDetails(ids).ToList();

            grdNewSupp.DataSource = lstAutoshipProductsDetails;//newSspps;
            grdNewSupp.DataBind();

          
        }
        catch (System.Exception ex)
        {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstSupplierDetails = null;
            lstAutoshipProductsDetails = null;
            objPrescriptionService = null;
        }

    }

    /// <summary>
    /// Bind the closed prescription list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdHistSupp_Rebind(object sender, EventArgs e)
    {
        List<PresCripSuppAutoshipProductStaffViewModel> lstPSAPStaffDetails = null;
        try
        {
            objPrescriptionService = new PrescriptionService();
            lstPSAPStaffDetails = new List<PresCripSuppAutoshipProductStaffViewModel>();
            lstPSAPStaffDetails = objPrescriptionService.GetClosedSupplementsDetails(PatientID).ToList();
            grdHistSupp.DataSource = lstPSAPStaffDetails;//histSupp;
            grdHistSupp.DataBind();
          

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPrescriptionService = null;
            lstPSAPStaffDetails = null;
        }
    }

    /// <summary>
    /// Reload the prescription suppliment grid view list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void grdSupps_Rebind(object sedner, EventArgs e)
    {
        List<PrescriptionSupplierViewModel> lstSupplierDetails = null;
        try
        {
            lstSupplierDetails = new List<PrescriptionSupplierViewModel>();
            objPrescriptionService = new PrescriptionService();
            lstSupplierDetails = objPrescriptionService.GetSupplementsDetails(PatientID).ToList();
            grdSupps.DataSource = lstSupplierDetails;
            grdSupps.DataBind();
           
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPrescriptionService = null;
            lstSupplierDetails = null;
        }
    }

    /// <summary>
    /// Bind the closed drug prescription
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdHist_Rebind(object sender, EventArgs e)
    {
        List<PrescripDrugStaffViewModel> lstPrescripDrugStaff = null;
        try
        {
            objPrescriptionService = new PrescriptionService();
            lstPrescripDrugStaff = new List<PrescripDrugStaffViewModel>();

            lstPrescripDrugStaff = objPrescriptionService.GetPrescriptionDrugStaffDetails(PatientID).ToList();
            grdHist.DataSource = lstPrescripDrugStaff; 
            grdHist.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPrescriptionService = null;
            lstPrescripDrugStaff = null;
        }


    }

    /// <summary>
    /// Reload the prescription drug grid view list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdScrips_Rebind(object sender, EventArgs e)
    {
        List<PrescriptionDrugViewModel> lstPrescriptionDrug = null;
        try
        {
            objPrescriptionService = new PrescriptionService();
            lstPrescriptionDrug = new List<PrescriptionDrugViewModel>();
            
            if (chkLongevity.Checked && chkThirdParty.Checked)
            {
               
                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "").ToList();

            }

            if (!chkLongevity.Checked && chkThirdParty.Checked)
            {
               
                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "" && o.ThirdParty == true).ToList();

            }
            if (chkLongevity.Checked && !chkThirdParty.Checked)
            {
               
                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "" && o.ThirdParty == false).ToList();

            }

            grdScrips.DataSource = lstPrescriptionDrug;
            grdScrips.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstPrescriptionDrug = null;
            objPrescriptionService = null;
        }


    }

    /// <summary>
    /// on click ob buttom move to history page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnHistory_Click(object sender, EventArgs e)
    //{
    //    if (Request.QueryString["MasterPage"] == null)
    //        Response.Redirect("prescription_history.aspx?PatientID=" + PatientID.ToString());
    //    else
    //        Response.Redirect("prescription_history_short.aspx?PatientID=" + PatientID.ToString());
    //}

    ///// <summary>
    ///// on click to this page move to history page
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnHistorySupp_Click(object sender, EventArgs e)
    //{
    //    if (Request.QueryString["MasterPage"] == null)
    //        Response.Redirect("supplement_history.aspx?PatientID=" + PatientID.ToString());
    //    else
    //        Response.Redirect("supplement_history_short.asp?PatientID=" + PatientID.ToString());
    //}

    protected void grdAutoship_Rebind(object sender, EventArgs e)
    {
        List<ModifiedPrescribedAutoshipViewModel> lstDetails = null;
        try
        {
            lstDetails = new List<ModifiedPrescribedAutoshipViewModel>();
            objPrescriptionService = new PrescriptionService();
            lstDetails = objPrescriptionService.GetModifiedPrescribedAutoshipList(PatientID).ToList();
            grdAutoship.DataSource = lstDetails;
            grdAutoship.DataBind();

            #region "Old Code"
            //var aShip = from m in ctx.ModifiedSupps
            //            join p in ctx.PresscriptionSupps on m.PrescriptionSuppID equals p.PresscriptionSuppID
            //            join a in ctx.AutoshipProducts on p.ProductID equals a.ProductID
            //            where a.Active == true
            //            && p.PatientID == PatientID
            //            select new
            //            {
            //                Autoship = a.ProductName,
            //                PrescriptionSuppID = p.PresscriptionSuppID,
            //            };

            //grdAutoship.DataSource = aShip;
            //grdAutoship.DataBind();
            #endregion
        }
        catch (System.Exception ex)
        {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstDetails = null;
            objPrescriptionService = null;
        }

    }

    #endregion

    #region "Methods"
    protected void CheckedChanged(object sender, EventArgs e)
    {
        //BindData();
        List<PrescriptionDrugViewModel> lstPrescriptionDrug = null;
        try
        {
            objPrescriptionService = new PrescriptionService();
            lstPrescriptionDrug = new List<PrescriptionDrugViewModel>();

            //List<scrip> scrips = new List<scrip>();
            if (chkLongevity.Checked && chkThirdParty.Checked)
            {
                #region "Old Code"
                //scrips = (from p in ctx.Prescriptions
                //          join d in ctx.Drugs on p.Drug_ID equals d.DrugID
                //          where p.PatientID == PatientID
                //          && p.Closed_yn == false
                //          && d.DrugName != ""
                //          && p.viewable_yn == true
                //          orderby d.DrugName
                //          select new scrip
                //          {
                //              PatientID = p.PatientID,
                //              DrugName = d.DrugName,
                //              Drug_Dose = p.Drug_Dose,
                //              Drug_Dispenses = p.Drug_Dispenses,
                //              Writer = p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
                //              Drug_NumbRefills = p.Drug_NumbRefills,
                //              Drug_DatePrescibed = p.Drug_DatePrescibed,
                //              Drug_EndDate = p.Drug_EndDate,
                //              RePre_yn = p.RePre_yn,
                //              Notes = p.Notes,
                //              DrugID = p.Drug_ID,
                //              PrescriptionID = p.PrescriptionID,
                //              Supplement_yn = d.Supplement_yn,
                //          }).ToList();
                #endregion

                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "").ToList();

            }

            if (!chkLongevity.Checked && chkThirdParty.Checked)
            {
                #region "Old Code"
                //scrips = (from p in ctx.Prescriptions
                //          join d in ctx.Drugs on p.Drug_ID equals d.DrugID
                //          where p.PatientID == PatientID
                //          && p.Closed_yn == false
                //          && p.ThirdParty_YN == true
                //          && d.DrugName != ""
                //          && p.viewable_yn == true
                //          orderby d.DrugName
                //          select new scrip
                //          {
                //              PatientID = p.PatientID,
                //              DrugName = d.DrugName,
                //              Drug_Dose = p.Drug_Dose,
                //              Drug_Dispenses = p.Drug_Dispenses,
                //              Writer = p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
                //              Drug_NumbRefills = p.Drug_NumbRefills,
                //              Drug_DatePrescibed = p.Drug_DatePrescibed,
                //              Drug_EndDate = p.Drug_EndDate,
                //              RePre_yn = p.RePre_yn,
                //              Notes = p.Notes,
                //              DrugID = p.Drug_ID,
                //              PrescriptionID = p.PrescriptionID,
                //              Supplement_yn = d.Supplement_yn,
                //          }).ToList();
                #endregion
                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "" && o.ThirdParty == true).ToList();

            }
            if (chkLongevity.Checked && !chkThirdParty.Checked)
            {
                #region "Old Code"
                //scrips = (from p in ctx.Prescriptions
                //          join d in ctx.Drugs on p.Drug_ID equals d.DrugID
                //          where p.PatientID == PatientID
                //          && p.Closed_yn == false
                //          && p.ThirdParty_YN == false
                //          && d.DrugName != ""
                //          && p.viewable_yn == true
                //          orderby d.DrugName
                //          select new scrip
                //          {
                //              PatientID = p.PatientID,
                //              DrugName = d.DrugName,
                //              Drug_Dose = p.Drug_Dose,
                //              Drug_Dispenses = p.Drug_Dispenses,
                //              Writer = p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
                //              Drug_NumbRefills = p.Drug_NumbRefills,
                //              Drug_DatePrescibed = p.Drug_DatePrescibed,
                //              Drug_EndDate = p.Drug_EndDate,
                //              RePre_yn = p.RePre_yn,
                //              Notes = p.Notes,
                //              PrescriptionID = p.PrescriptionID,
                //              DrugID = p.Drug_ID,
                //              Supplement_yn = d.Supplement_yn,
                //          }).ToList();
                #endregion
                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "" && o.ThirdParty == false).ToList();

            }

            grdScrips.DataSource = lstPrescriptionDrug;//scrips;
            grdScrips.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstPrescriptionDrug = null;
            objPrescriptionService = null;
        }


    }

    private void BindData()
    {
        List<PrescriptionDrugViewModel> lstPrescriptionDrug = null;
        List<PrescripDrugStaffViewModel> lstPrescripDrugStaff = null;
        List<PrescriptionSupplierViewModel> lstSupplierDetails = null;
        List<AutoshipProductsViewModel> lstAutoshipProductsDetails = null;
        List<PresCripSuppAutoshipProductStaffViewModel> lstPSAPStaffDetails = null;
        try
        {
            objPrescriptionService = new PrescriptionService();
            lstPrescriptionDrug = new List<PrescriptionDrugViewModel>();

            
            if (chkLongevity.Checked && chkThirdParty.Checked)
            {
                #region "Old Code"
                //scrips = (from p in ctx.Prescriptions
                //          join d in ctx.Drugs on p.Drug_ID equals d.DrugID
                //          where p.PatientID == PatientID
                //          && p.Closed_yn == false
                //          && d.DrugName != ""
                //          && p.viewable_yn == true
                //          orderby d.DrugName
                //          select new scrip
                //          {
                //              PatientID = p.PatientID,
                //              DrugName = d.DrugName,
                //              Drug_Dose = p.Drug_Dose,
                //              Drug_Dispenses = p.Drug_Dispenses,
                //              Writer = p.ThirdParty_YN == true ? "Third Party" : p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
                //              Drug_NumbRefills = p.Drug_NumbRefills,
                //              Drug_DatePrescibed = p.Drug_DatePrescibed,
                //              Drug_EndDate = p.Drug_EndDate,
                //              RePre_yn = p.RePre_yn,
                //              Notes = p.Notes,
                //              DrugID = p.Drug_ID,
                //              PrescriptionID = p.PrescriptionID,
                //              ThirdParty = p.ThirdParty_YN,
                //              Supplement_yn = d.Supplement_yn,
                //          }).ToList();               
                #endregion

                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "").ToList();
            }

            if (!chkLongevity.Checked && chkThirdParty.Checked)
            {
                #region "Old Code"
                //scrips = (from p in ctx.Prescriptions
                //          join d in ctx.Drugs on p.Drug_ID equals d.DrugID
                //          where p.PatientID == PatientID
                //          && p.Closed_yn == false
                //          && p.ThirdParty_YN == true
                //          && d.DrugName != ""
                //          && p.viewable_yn == true
                //          orderby d.DrugName
                //          select new scrip
                //          {
                //              PatientID = p.PatientID,
                //              DrugName = d.DrugName,
                //              Drug_Dose = p.Drug_Dose,
                //              Drug_Dispenses = p.Drug_Dispenses,
                //              Writer = p.ThirdParty_YN == true ? "Third Party" : p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
                //              Drug_NumbRefills = p.Drug_NumbRefills,
                //              Drug_DatePrescibed = p.Drug_DatePrescibed,
                //              Drug_EndDate = p.Drug_EndDate,
                //              RePre_yn = p.RePre_yn,
                //              Notes = p.Notes,
                //              DrugID = p.Drug_ID,
                //              PrescriptionID = p.PrescriptionID,
                //              ThirdParty = p.ThirdParty_YN,
                //              Supplement_yn = d.Supplement_yn,
                //          }).ToList();
                #endregion

                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "" && o.ThirdParty == true).ToList();

            }
            if (chkLongevity.Checked && !chkThirdParty.Checked)
            {
                #region "Old Code"
                //scrips = (from p in ctx.Prescriptions
                //          join d in ctx.Drugs on p.Drug_ID equals d.DrugID
                //          where p.PatientID == PatientID
                //          && p.Closed_yn == false
                //          && p.ThirdParty_YN == false
                //          && d.DrugName != ""
                //          && p.viewable_yn == true
                //          orderby d.DrugName
                //          select new scrip
                //          {
                //              PatientID = p.PatientID,
                //              DrugName = d.DrugName,
                //              Drug_Dose = p.Drug_Dose,
                //              Drug_Dispenses = p.Drug_Dispenses,
                //              Writer = p.ThirdParty_YN == true ? "Third Party" : p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
                //              Drug_NumbRefills = p.Drug_NumbRefills,
                //              Drug_DatePrescibed = p.Drug_DatePrescibed,
                //              Drug_EndDate = p.Drug_EndDate,
                //              RePre_yn = p.RePre_yn,
                //              Notes = p.Notes,
                //              PrescriptionID = p.PrescriptionID,
                //              DrugID = p.Drug_ID,
                //              Supplement_yn = d.Supplement_yn,
                //          }).ToList();
                #endregion

                lstPrescriptionDrug = objPrescriptionService.GetPrescriptionDrugList(PatientID).Where(o => o.DrugName != "" && o.ThirdParty == false).ToList();

            }

            grdScrips.DataSource = lstPrescriptionDrug;
            grdScrips.DataBind();

            #region "Old Code"
            //int[] currIds = (from p in ctx.Prescriptions
            //                 join d in ctx.Drugs on p.Drug_ID equals d.DrugID
            //                 where p.PatientID == PatientID
            //                 && p.Closed_yn == false
            //                 && d.DrugName != ""
            //                 && p.viewable_yn == true
            //                 orderby d.DrugName
            //                 select d.DrugID).ToArray();
            #endregion

            int[] currIds = lstPrescriptionDrug.Select(pairs => Convert.ToInt32(pairs.DrugID)).ToArray();

            List<DrugViewModel> lstDrugs = new List<DrugViewModel>();
            lstDrugs = objPrescriptionService.GetDrugList().ToList();
            var drugs = lstDrugs.Where(p => p.DrugName != "" && p.Viewable_yn == true && !currIds.Contains(Convert.ToInt32(p.DrugID)) && p.Supplement_yn == false).OrderByDescending(p => p.DrugName).ToList();

            #region "Old Code"
            //var drugs = from d in ctx.Drugs
            //            where d.Viewable_yn == true
            //            && d.DrugName != ""
            //            && !currIds.Contains(d.DrugID)
            //            && d.Supplement_yn == false
            //            orderby d.DrugName
            //            select d;
            #endregion

            grdNew.DataSource = drugs;
            grdNew.DataBind();

            #region "Old Code"
            //var hist = from p in ctx.Prescriptions
            //           join d in ctx.Drugs on p.Drug_ID equals d.DrugID
            //           join s in ctx.Staffs on p.Writer equals s.EmployeeID
            //           where p.PatientID == PatientID
            //           && d.DrugName != ""
            //           && (p.Closed_yn == true || p.viewable_yn == false)
            //           orderby d.DrugName, p.Drug_DatePrescibed descending
            //           select new
            //           {
            //               PatientID = p.PatientID,
            //               DrugName = d.DrugName,
            //               Drug_Dose = p.Drug_Dose,
            //               Drug_Dispenses = p.Drug_Dispenses,
            //               Writer = s.EmployeeName,
            //               Drug_NumbRefills = p.Drug_NumbRefills,
            //               Drug_DatePrescibed = p.Drug_DatePrescibed,
            //               Drug_EndDate = p.Closed_Date,
            //               RePre_yn = p.RePre_yn,
            //               Notes = p.Notes,
            //               DrugID = p.Drug_ID,
            //           };
            #endregion

            lstPrescripDrugStaff = new List<PrescripDrugStaffViewModel>();
            lstPrescripDrugStaff = objPrescriptionService.GetPrescriptionDrugStaffDetails(PatientID).ToList();
            grdHist.DataSource = lstPrescripDrugStaff;
            grdHist.DataBind();

            #region "Old Code"
            //List<Supp> supps = (from p in ctx.PresscriptionSupps
            //                    join d in ctx.AutoshipProducts on p.ProductID equals d.ProductID
            //                    where p.PatientID == PatientID
            //                    && p.Closed_yn == false
            //                    && p.viewable_yn == true
            //                    orderby d.ProductName
            //                    select new Supp
            //                    {
            //                        PatientID = p.PatientID,
            //                        SuppName = d.ProductName,
            //                        SuppDose = p.SuppDose,
            //                        SuppDispenses = p.SuppDispenses,
            //                        Writer = p.Writer == null ? "" : (from s in ctx.Staffs where s.EmployeeID == p.Writer select s.EmployeeName).First(),
            //                        SuppNumbRefills = p.SuppNumbRefills,
            //                        SuppDatePrescibed = p.SuppDatePrescibed,
            //                        SuppEndDate = p.SuppEndDate,
            //                        RePre_yn = p.RePre_yn,
            //                        Notes = p.Notes,
            //                        ProductID = p.ProductID,
            //                        PresscriptionSuppID = p.PresscriptionSuppID,
            //                    }).ToList();
            #endregion

            lstSupplierDetails = new List<PrescriptionSupplierViewModel>();
            lstSupplierDetails = objPrescriptionService.GetSupplementsDetails(PatientID).ToList();
            grdSupps.DataSource = lstSupplierDetails;
            grdSupps.DataBind();

            #region "Old Code"
            //int[] currIdsSupp = (from p in ctx.PresscriptionSupps
            //                     join d in ctx.AutoshipProducts on p.ProductID equals d.ProductID
            //                     where p.PatientID == PatientID
            //                     && p.Closed_yn == false
            //                     && p.viewable_yn == true
            //                     orderby d.ProductName
            //                     select d.ProductID).ToArray();

            //var newSspps = from d in ctx.AutoshipProducts
            //               where (d.Active == true || d.Viewable == true)
            //               && !currIds.Contains(d.ProductID)
            //               orderby d.ProductName
            //               select d;
            #endregion

            int[] currIdsSupps = lstSupplierDetails.Select(pairs => Convert.ToInt32(pairs.ProductID)).ToArray();
            string ids = string.Empty;
            for (int i = 0; i < currIdsSupps.Length - 1; i++)
            {
                ids += currIdsSupps[i].ToString() + ',';
            }
            lstAutoshipProductsDetails = new List<AutoshipProductsViewModel>();
            lstAutoshipProductsDetails = objPrescriptionService.GetNewSupplementDetails(ids).ToList();

            grdNewSupp.DataSource = lstAutoshipProductsDetails;//newSspps;
            grdNewSupp.DataBind();

            #region "Old Code"
            //var histSupp = from p in ctx.PresscriptionSupps
            //               join d in ctx.AutoshipProducts on p.ProductID equals d.ProductID
            //               join s in ctx.Staffs on p.Writer equals s.EmployeeID
            //               where p.PatientID == PatientID
            //               && (p.Closed_yn == true || p.viewable_yn == false)
            //               orderby d.ProductName, p.SuppDatePrescibed
            //               select new
            //               {
            //                   PatientID = p.PatientID,
            //                   SuppName = d.ProductName,
            //                   SuppDose = p.SuppDose,
            //                   SuppDispenses = p.SuppDispenses,
            //                   Writer = s.EmployeeName,
            //                   SuppNumbRefills = p.SuppNumbRefills,
            //                   SuppDatePrescibed = p.SuppDatePrescibed,
            //                   SuppEndDate = p.SuppEndDate,
            //                   RePre_yn = p.RePre_yn,
            //                   Notes = p.Notes,
            //                   DrugID = p.ProductID,
            //               };
            #endregion

            lstPSAPStaffDetails = new List<PresCripSuppAutoshipProductStaffViewModel>();
            lstPSAPStaffDetails = objPrescriptionService.GetClosedSupplementsDetails(PatientID).ToList();

            grdHistSupp.DataSource = lstPSAPStaffDetails;//histSupp;
            grdHistSupp.DataBind();
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPrescriptionService = null;
            lstPrescriptionDrug = null;
            lstPrescripDrugStaff = null;
            lstSupplierDetails = null;
            lstAutoshipProductsDetails = null;
            lstPSAPStaffDetails = null;
        }

    }

    //protected void btnPrintPatient_Click(object sender, EventArgs e)
    //{
    //    //create the follow up for Autoship

    //    Obout.Grid.Grid grdAutoship = (Obout.Grid.Grid)Utilities.FindControlRecursive(Master, "grdAutoship");

    //    Response.Redirect("SciprList.aspx?PatientID=" + PatientID.ToString());
    //}

   /// <summary>
   /// Method to insert prescribed drugs
   /// By Jaswinder
   /// </summary>
   /// <param name="txtDrugNameLocal"></param>
   /// <param name="txtSig"></param>
   /// <param name="txtDisp"></param>
   /// <param name="txtRefill"></param>
   /// <param name="txtStartDate"></param>
   /// <param name="txtEndDate"></param>
   /// <param name="txtNotes"></param>
   /// <param name="txtPatientID"></param>
   /// <param name="txtStaffID"></param>
   /// <param name="txtAptID"></param>
   /// <param name="chkThirdPartyAdd"></param>
   /// <returns></returns>

    [System.Web.Services.WebMethod]
    public static int InsertPrescriptionDrug(string txtDrugNameLocal, string txtSig,string txtDisp,string txtRefill, string txtStartDate,string txtEndDate,string txtNotes,int txtPatientID,int txtStaffID,string txtAptID ,bool chkThirdPartyAdd)
    {
        IPrescriptionService objPrescriptionService = null;
        int intRet = 0;

        try
        {
            objPrescriptionService = new PrescriptionService();
            if (txtAptID == "")
                txtAptID = "-1";
            intRet = objPrescriptionService.InsertPrescriptionDrug(txtDrugNameLocal, txtSig,txtDisp,txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, chkThirdPartyAdd);

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objPrescriptionService = null;
        }
        return intRet;
    }

    /// <summary>
    /// Refill the prescribed drug
    /// By jaswinder
    /// </summary>
    /// <param name="txtSig"></param>
    /// <param name="txtDisp"></param>
    /// <param name="txtRefill"></param>
    /// <param name="txtStartDate"></param>
    /// <param name="txtEndDate"></param>
    /// <param name="txtNotes"></param>
    /// <param name="txtPatientID"></param>
    /// <param name="txtStaffID"></param>
    /// <param name="txtAptID"></param>
    /// <param name="DrugID"></param>
    /// <param name="PrescriptionID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertPrescriptionRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
    {
        IPrescriptionService objPrescriptionService = null;
        int intRet = 0;

        try
        {
            objPrescriptionService = new PrescriptionService();
            if (txtAptID == "")
                txtAptID = "-1";
            intRet = objPrescriptionService.InsertPrescriptionRefill(txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, DrugID, PrescriptionID);

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objPrescriptionService = null;
        }
        return intRet;
    }

    /// <summary>
    /// Closed prescribed drug/Suppliments
    /// By jaswinder
    /// </summary>
    /// <param name="PrescriptionID"></param>
    /// <param name="ElementID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int ClosePrescription(int PrescriptionID, int ElementID)
    {
        IPrescriptionService objPrescriptionService = null;
        int Result = 1;
        try
        {
            objPrescriptionService = new PrescriptionService();

            objPrescriptionService.ClosePrescription(PrescriptionID, ElementID);

        }

        catch (System.Exception ex)
        {
            Result = 0;
        }
        finally
        {
            objPrescriptionService = null;
        }
        return Result;
    }

    /// <summary>
    /// Delete Prescribed drug/Suppliments
    /// By jaswinder
    /// </summary>
    /// <param name="PrescriptionID"></param>
    /// <param name="ElementID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int DeletePrescription(int PrescriptionID, int ElementID)
    {
        IPrescriptionService objPrescriptionService = null;
        int Result = 1;
        try
        {
            objPrescriptionService = new PrescriptionService();

            objPrescriptionService.DeletePrescription(PrescriptionID, ElementID);

        }

        catch (System.Exception ex)
        {
            Result = 0;
        }
        finally
        {
            objPrescriptionService = null;
        }
        return Result;
    }

    /// <summary>
    /// Insert Prescribed Suppliments
    /// By jaswinder
    /// </summary>
    /// <param name="txtDrugNameLocalSupp"></param>
    /// <param name="txtSig"></param>
    /// <param name="txtDisp"></param>
    /// <param name="txtRefill"></param>
    /// <param name="txtStartDate"></param>
    /// <param name="txtEndDate"></param>
    /// <param name="txtNotes"></param>
    /// <param name="txtPatientID"></param>
    /// <param name="txtStaffID"></param>
    /// <param name="txtAptID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertPrescriptionSupp(string txtDrugNameLocalSupp, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID)
    {
        IPrescriptionService objPrescriptionService = null;
        int intRet = 0;

        try
        {
            objPrescriptionService = new PrescriptionService();
            if (txtAptID == "")
                txtAptID = "-1";
            intRet = objPrescriptionService.InsertPrescriptionSupp(txtDrugNameLocalSupp, txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID);

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objPrescriptionService = null;
        }
        return intRet;
    }

    /// <summary>
    /// Refill suppliments
    /// By jaswinder
    /// </summary>
    /// <param name="txtSig"></param>
    /// <param name="txtDisp"></param>
    /// <param name="txtRefill"></param>
    /// <param name="txtStartDate"></param>
    /// <param name="txtEndDate"></param>
    /// <param name="txtNotes"></param>
    /// <param name="txtPatientID"></param>
    /// <param name="txtStaffID"></param>
    /// <param name="txtAptID"></param>
    /// <param name="ProductID"></param>
    /// <param name="PrescriptionID"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertPrescriptionSuppRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int ProductID, int PrescriptionID)
    {
        IPrescriptionService objPrescriptionService = null;
        int intRet = 0;

        try
        {
            objPrescriptionService = new PrescriptionService();
            if (txtAptID == "")
                txtAptID = "-1";
            intRet = objPrescriptionService.InsertPrescriptionSuppRefill(txtSig, txtDisp, txtRefill, txtStartDate, txtEndDate, txtNotes, txtPatientID, txtStaffID, txtAptID, ProductID, PrescriptionID);

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objPrescriptionService = null;
        }
        return intRet;
    }

    [System.Web.Services.WebMethod]
    public static int AutoshipFollowUp(string data)
    {

       
        IPrescriptionService objPrescriptionService = null;
        int intRet = 0;

       
        objPrescriptionService = new PrescriptionService();
        intRet = objPrescriptionService.AutoshipFollowupPrescription(data);
        return intRet;
       
    }

    # endregion

}