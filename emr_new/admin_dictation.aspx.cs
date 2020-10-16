using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using NineRays.WebControls;
using Emrdev.ServiceLayer;
using System.Data;
using Emrdev.ViewModelLayer;
using System.Web.Services;

public partial class admin_dictation : LMCBase
{
    #region Variable
    IAdminDictationService objService = null;
    #endregion
    #region Events
    /// <summary>
    /// fills tree nodes and diagnosis dropdown on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fill tree nodes with Dictation Diagnosis
            PopulateDiag();

            //fill tree nodes with Dictation Plans
            PopulatePlans();

            //fill diagnosis dropdown

            objService = new AdminDictationService();
            string Parameter = string.Empty;
            var Diagnosislist = objService.GetDictationDiagnosis(Parameter);
            drpDiagnosis.DataSource = Diagnosislist;
            drpDiagnosis.DataTextField = "DiagnosisName";
            drpDiagnosis.DataValueField = "DiagnosisID";
            drpDiagnosis.DataBind();
            drpDiagnosis.Items.Insert(0, new ListItem("Select Diagnosis", "-1"));

            drpEditDiagPlan.DataSource = Diagnosislist;
            drpEditDiagPlan.DataTextField = "DiagnosisName";
            drpEditDiagPlan.DataValueField = "DiagnosisID";
            drpEditDiagPlan.DataBind();
            drpEditDiagPlan.Items.Insert(0, new ListItem("Select Diagnosis", "-1"));
        }
    }

    protected void btnSearchDiag_Click(object sender, EventArgs e)
    {
        PopulateDiag();
    }

    protected void btnSearchPlans_Click(object sender, EventArgs e)
    {
        PopulatePlans();
    }

    /// <summary>
    /// Add the diagnosis in the Dictation_Diagnosis table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddDiag_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new AdminDictationService();
            if (txtCode.Text == "" || txtDiagDescrip.Text == "" || txtDiagName.Text == "")
                modMissing.Show();
            else
            {
                string KeyWords = "";
                if (txtKey1Diag.Text != "") if (KeyWords == "") KeyWords += txtKey1Diag.Text; else KeyWords += "|" + txtKey1Diag.Text;
                if (txtKey2Diag.Text != "") if (KeyWords == "") KeyWords += txtKey2Diag.Text; else KeyWords += "|" + txtKey2Diag.Text;
                if (txtKey3Diag.Text != "") if (KeyWords == "") KeyWords += txtKey3Diag.Text; else KeyWords += "|" + txtKey3Diag.Text;
                if (txtKey4Diag.Text != "") if (KeyWords == "") KeyWords += txtKey4Diag.Text; else KeyWords += "|" + txtKey4Diag.Text;
                if (txtKey5Diag.Text != "") if (KeyWords == "") KeyWords += txtKey5Diag.Text; else KeyWords += "|" + txtKey5Diag.Text;

                string strConfirm = objService.InsertDictation_Diagnosis(txtDiagDescrip.Text, txtDiagName.Text, txtCode.Text, KeyWords, true).ToString();
                LabelMessage.Visible = true;
                if (strConfirm == "not exist")
                {
                    LabelMessage.Text = "You have successfully added the records.";
                    txtDiagDescrip.Text = "";
                    txtDiagName.Text = "";
                    txtCode.Text = "";
                    txtKey1Diag.Text = "";
                    txtKey2Diag.Text = "";
                    txtKey3Diag.Text = "";
                    txtKey4Diag.Text = "";
                    txtKey5Diag.Text = "";
                }
                else
                    LabelMessage.Text = "You can't add duplicate " + strConfirm + " records.";


                //Dictation_Diagnosis_ViewModel diag = new Dictation_Diagnosis_ViewModel();
                //diag.DiagnosisDescrip = txtDiagDescrip.Text;
                //diag.DiagnosisName = txtDiagName.Text;
                //diag.ICDCode = txtCode.Text;
                //diag.KeyWords = KeyWords;
                //diag.Viewable_YN = true;
                //objService.InsertDictationDiagnosis(diag);

                PopulateDiag();
            }
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
    /// Edit the diagnosis by rightclick on the diagnoses tree node
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditDiagOK_Click(object sender, EventArgs e)
    {
        try
        {
            int DiagnosisID = int.Parse(lblDiagID.Text);
            objService = new AdminDictationService();
            string KeyWords = "";
            if (txtKey1DiagEdit.Text != "") if (KeyWords == "") KeyWords += txtKey1DiagEdit.Text; else KeyWords += "|" + txtKey1DiagEdit.Text;
            if (txtKey2DiagEdit.Text != "") if (KeyWords == "") KeyWords += txtKey2DiagEdit.Text; else KeyWords += "|" + txtKey2DiagEdit.Text;
            if (txtKey3DiagEdit.Text != "") if (KeyWords == "") KeyWords += txtKey3DiagEdit.Text; else KeyWords += "|" + txtKey3DiagEdit.Text;
            if (txtKey4DiagEdit.Text != "") if (KeyWords == "") KeyWords += txtKey4DiagEdit.Text; else KeyWords += "|" + txtKey4DiagEdit.Text;
            if (txtKey5DiagEdit.Text != "") if (KeyWords == "") KeyWords += txtKey5DiagEdit.Text; else KeyWords += "|" + txtKey5DiagEdit.Text;

            objService.UpdateDictationDiagnosis(DiagnosisID, txtEditDiagDesrcip.Text, txtEditDiagName.Text, txtEditCode.Text, KeyWords);

            PopulateDiag();
            txtEditDiagDesrcip.Text = "";
            txtEditDiagName.Text = "";
            txtEditCode.Text = "";
            txtKey1DiagEdit.Text = "";
            txtKey2DiagEdit.Text = "";
            txtKey3DiagEdit.Text = "";
            txtKey4DiagEdit.Text = "";
            txtKey5DiagEdit.Text = "";
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
    /// Edit the plan by rightclick on the plan tree node
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditPlanOK_Click(object sender, EventArgs e)
    {
        try
        {
            int PlanID = int.Parse(lblPlanID.Text);
            string KeyWords = "";
            if (txtKey1PlanEdit.Text != "") if (KeyWords == "") KeyWords += txtKey1PlanEdit.Text; else KeyWords += "|" + txtKey1PlanEdit.Text;
            if (txtKey2PlanEdit.Text != "") if (KeyWords == "") KeyWords += txtKey2PlanEdit.Text; else KeyWords += "|" + txtKey2PlanEdit.Text;
            if (txtKey3PlanEdit.Text != "") if (KeyWords == "") KeyWords += txtKey3PlanEdit.Text; else KeyWords += "|" + txtKey3PlanEdit.Text;
            if (txtKey4PlanEdit.Text != "") if (KeyWords == "") KeyWords += txtKey4PlanEdit.Text; else KeyWords += "|" + txtKey4PlanEdit.Text;
            if (txtKey5PlanEdit.Text != "") if (KeyWords == "") KeyWords += txtKey5PlanEdit.Text; else KeyWords += "|" + txtKey5PlanEdit.Text;


            objService = new AdminDictationService();
            objService.UpdateDictationPlans(PlanID, rdoEditPlanCategory.SelectedItem.Text, txtEditPlanDescrip.Text, txtEditPlanName.Text, KeyWords, int.Parse(drpEditDiagPlan.SelectedItem.Value));



            PopulatePlans();
            PopulateDiag();

            foreach (ListItem i in rdoEditPlanCategory.Items)
            {
                i.Selected = false;
            }
            txtEditPlanDescrip.Text = "";
            txtEditPlanName.Text = "";
            txtKey1PlanEdit.Text = "";
            txtKey2PlanEdit.Text = "";
            txtKey3PlanEdit.Text = "";
            txtKey4PlanEdit.Text = "";
            txtKey5PlanEdit.Text = "";
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
    /// this method will be used to check the duplicate records.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="planName"></param>
    /// <param name="PlanId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string checkExistingRecord(string category, string planName, string PlanId)
    {
        IAdminDictationService objService = null;
        string strDuplicate = string.Empty;
        try
        {
            objService = new AdminDictationService();
            strDuplicate = objService.CheckExistDictation_Plan(category, HttpContext.Current.Server.HtmlDecode(planName), Convert.ToInt32(PlanId));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return strDuplicate;
    }

    /// <summary>
    /// this method will be used to check the duplicate records for Diagnosis.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="planName"></param>
    /// <param name="PlanId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string validateDiagnosisPlan(string diagnosisID, string diagName, string iCDCode)
    {
        IAdminDictationService objService = null;
        string strDuplicate = string.Empty;
        try
        {
            objService = new AdminDictationService();
            strDuplicate = objService.ValidateAndUpdateDictation_Diagnosis(Convert.ToInt32(diagnosisID), HttpContext.Current.Server.HtmlDecode(diagName), iCDCode);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return strDuplicate;
    }

    /// <summary>
    /// Add new the planin the database and also show it on plan tree
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddPlan_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new AdminDictationService();
            if (txtNewPlanDesc.Text == "" || txtNewPlanName.Text == "" || rdoPlanCat.SelectedIndex == -1)
                modMissing.Show();
            else
            {
                string KeyWords = "";
                if (txtKey1Plan.Text != "") if (KeyWords == "") KeyWords += txtKey1Plan.Text; else KeyWords += "|" + txtKey1Plan.Text;
                if (txtKey2Plan.Text != "") if (KeyWords == "") KeyWords += txtKey2Plan.Text; else KeyWords += "|" + txtKey2Plan.Text;
                if (txtKey3Plan.Text != "") if (KeyWords == "") KeyWords += txtKey3Plan.Text; else KeyWords += "|" + txtKey3Plan.Text;
                if (txtKey4Plan.Text != "") if (KeyWords == "") KeyWords += txtKey4Plan.Text; else KeyWords += "|" + txtKey4Plan.Text;
                if (txtKey5Plan.Text != "") if (KeyWords == "") KeyWords += txtKey5Plan.Text; else KeyWords += "|" + txtKey5Plan.Text;

                string status = objService.InsertDictation_Plan(rdoPlanCat.SelectedItem.Text, txtNewPlanDesc.Text, txtNewPlanName.Text, KeyWords, true);
                LabelMessagePlan.Visible = true;
                if (status == "not exist")
                {
                    LabelMessagePlan.Text = "You have successfully added the records.";
                    txtKey1Plan.Text = "";
                    txtKey2Plan.Text = "";
                    txtKey3Plan.Text = "";
                    txtKey4Plan.Text = "";
                    txtKey5Plan.Text = "";
                    foreach (ListItem i in rdoEditPlanCategory.Items)
                    {
                        i.Selected = false;
                    }

                    txtNewPlanDesc.Text = "";
                    txtNewPlanName.Text = "";
                    drpDiagnosis.SelectedItem.Value = "-1";
                }
                else
                    LabelMessagePlan.Text = "You can't add duplicate " + status + " records.";

                //Dictation_Plan_ViewModel plan = new Dictation_Plan_ViewModel();
                //plan.Category = rdoPlanCat.SelectedItem.Text;
                //plan.PlanDescrip = txtNewPlanDesc.Text;
                //plan.PlanName = txtNewPlanName.Text;
                //plan.KeyWords = KeyWords;
                //plan.Viewable_YN = true;
                //objService.InsertDictationPlans(plan, int.Parse(drpDiagnosis.SelectedItem.Value));
                PopulatePlans();
                PopulateDiag();


            }
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
    ///If Dictation_planDiag table has any with for the particular diagnosis then it create the child node for that diagnosis
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void treDiags_NodeInserted(object sender, FlyTreeNodeEventArgs e)
    {
        try
        {

            long CheckExist = objService.GetCountForPlanDiagnosis(int.Parse(e.Node.Parent.Value), int.Parse(e.Node.Value));

            if (CheckExist == 0)
            {
                objService = new AdminDictationService();
                Dictation_PlanDiagnosis_ViewModel PlnDiag = new Dictation_PlanDiagnosis_ViewModel();

                PlnDiag.DiagnosisID = int.Parse(e.Node.Parent.Value);
                PlnDiag.PlanID = int.Parse(e.Node.Value);
                objService.InsertDictaionDiagnosiPlan(PlnDiag);
            }
            PopulateDiag();

            Response.Redirect("admin_dictation.aspx");
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
    /// By right click we can edit and remove the Diagnosis from the tree.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DiagMenu_Command(Object sender, FlyContextMenuCommandEventArgs e)
    {
        try
        {
            objService = new AdminDictationService();
            FlyTreeNode theNode = treDiags.FindByID(e.CommandArgument);
            int DiagnosisID = int.Parse(theNode.Value);
            hdnDiagnosis.Value = DiagnosisID.ToString(); ;
            switch (e.CommandName)
            {
                case "Remove":


                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "ConfirmDelete();", true);
                    //    objService.DeleteDictationDiagnosis(DiagnosisID);
                    //    PopulateDiag();

                    break;
                case "Edit":

                    List<Dictation_Diagnosis_ViewModel> theDiag = objService.GetDictationDiagnosisByID(DiagnosisID);
                    foreach (var diag in theDiag)
                    {
                        txtKey1DiagEdit.Text = "";
                        txtKey2DiagEdit.Text = "";
                        txtKey3DiagEdit.Text = "";
                        txtKey4DiagEdit.Text = "";
                        txtKey5DiagEdit.Text = "";

                        string[] KeyWords = diag.KeyWords.Split('|');
                        int counter = 0;
                        foreach (string kWord in KeyWords)
                        {
                            TextBox theBox = (TextBox)Utilities.FindControlRecursive(Master, "txtKey" + (counter + 1).ToString() + "DiagEdit");
                            theBox.Text = kWord;
                            counter++;

                        }

                        txtEditCode.Text = diag.ICDCode;
                        txtEditDiagDesrcip.Text = diag.DiagnosisDescrip;
                        txtEditDiagName.Text = diag.DiagnosisName;
                        lblDiagID.Text = diag.DiagnosisID.ToString();
                        modEditDiag.Show();

                    }
                    break;
            }
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
    /// By right click we can edit and remove the plan from the tree
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PlanMenu_Command(Object sender, FlyContextMenuCommandEventArgs e)
    {
        try
        {
            objService = new AdminDictationService();
            FlyTreeNode theNode = treDiags.FindByID(e.CommandArgument);
            if (theNode == null)
            {
                theNode = trePlans.FindByID(e.CommandArgument);
            }

            int PlanID = int.Parse(theNode.Value);
            HiddenFieldEditPlan.Value = PlanID.ToString();
            switch (e.CommandName)
            {
                case "Remove":
                    bool isDiag = false;
                    //check to see if it is in diag or plan tree
                    if (theNode.Parent.Parent.Value == "Plan")
                    {
                        isDiag = false;
                    }
                    else
                    {
                        isDiag = true;
                    }
                    HiddenFieldEditCat.Value = isDiag.ToString();
                    int DiagnosisID = 0;
                    if (isDiag)
                    {
                        //if in diag, remove from lookup
                        DiagnosisID = int.Parse(theNode.Parent.Value);
                        hdnDiagnosis.Value = DiagnosisID.ToString();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "ConfirmDeletePlan();", true);
                        //objService.DeleteDictationPlan(DiagnosisID, PlanID, isDiag);
                        //PopulateDiag();
                    }
                    else
                    {
                        hdnDiagnosis.Value = DiagnosisID.ToString();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "ConfirmDeletePlan();", true);
                        //objService.DeleteDictationPlan(DiagnosisID, PlanID, isDiag);
                        //objService = null;
                        //PopulatePlans();
                    }
                    break;
                case "Edit":

                    List<Dictation_Plan_ViewModel> Plan = objService.GetDictationPlanByID(PlanID);
                    foreach (var thePlan in Plan)
                    {

                        txtKey1PlanEdit.Text = "";
                        txtKey2PlanEdit.Text = "";
                        txtKey3PlanEdit.Text = "";
                        txtKey4PlanEdit.Text = "";
                        txtKey5PlanEdit.Text = "";

                        string[] KeyWords = thePlan.KeyWords.Split('|');
                        int counter = 0;
                        foreach (string kWord in KeyWords)
                        {
                            TextBox theBox = (TextBox)Utilities.FindControlRecursive(Master, "txtKey" + (counter + 1).ToString() + "PlanEdit");
                            theBox.Text = kWord;
                            counter++;

                        }
                        lblPlanID.Text = thePlan.PlanID.ToString();
                        txtEditPlanDescrip.Text = thePlan.PlanDescrip;
                        txtEditPlanName.Text = thePlan.PlanName;
                        ViewState["Editplan"] = txtEditPlanName.Text;
                        HiddenFieldEditPlan.Value = txtEditPlanName.Text;
                        if (thePlan.DiagnosisID != null)
                        {
                            drpEditDiagPlan.SelectedValue = thePlan.DiagnosisID.ToString();
                        }
                        else
                        {
                            drpEditDiagPlan.SelectedValue = "-1";
                        }
                        foreach (ListItem i in rdoEditPlanCategory.Items)
                        {
                            if (i.Text == thePlan.Category)
                            {
                                i.Selected = true;
                                ViewState["category"] = i.Text;
                                HiddenFieldEditCat.Value = i.Text;
                            }
                            else
                            {
                                i.Selected = false;
                            }
                        }
                        modEditPlan.Show();
                    }
                    break;
            }
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
    #region Methods
    /// <summary>
    /// method to fill the tree nodes with diagnoses and also provide search
    /// </summary>
    private void PopulateDiag()
    {
        try
        {
            objService = new AdminDictationService();
            treDiags.Nodes.Clear();
            FlyTreeNode root = new FlyTreeNode("Available Diagnoses");
            root.Value = "Diag";
            //first get all defined diagnoses
            List<Dictation_Diagnosis_ViewModel> diags = new List<Dictation_Diagnosis_ViewModel>();
            diags = objService.GetDictationDiagnosis(txtSearchDiag.Text);

            //foreach each diag
            foreach (Dictation_Diagnosis_ViewModel d in diags)
            {
                //Create node
                NineRays.WebControls.FlyTreeNode dNode = new FlyTreeNode(d.DiagnosisName);
                dNode.DragDropAcceptNames = "plan";
                dNode.ContextMenuID = "DiagMenu";
                dNode.Value = d.DiagnosisID.ToString();

                List<Dictation_Plan_ViewModel> plans = new List<Dictation_Plan_ViewModel>();
                plans = objService.GetDictationDiagnosisPlans(d.DiagnosisID);
                //for each diag, get plans and create nodes
                foreach (Dictation_Plan_ViewModel p in plans)
                {
                    FlyTreeNode pNode = new FlyTreeNode(p.PlanName);
                    pNode.DragDropName = "plan";
                    pNode.ContextMenuID = "PlanMenu";
                    pNode.Value = p.PlanID.ToString();
                    dNode.ChildNodes.Add(pNode);
                }
                //Add node
                root.ChildNodes.Add(dNode);
            }
            treDiags.Nodes.Add(root);
        }
        catch (System.Exception ex)
        {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
    }


    /// <summary>
    /// Method to fill tree nodes with dictation plans
    /// </summary>
    private void PopulatePlans()
    {

        try
        {
            objService = new AdminDictationService();
            trePlans.Nodes.Clear();
            FlyTreeNode root = new FlyTreeNode("Available Plan Items");
            root.Value = "Plan";
            List<Dictation_Plan_ViewModel> plans = new List<Dictation_Plan_ViewModel>();

            plans = objService.GetDictationPlans(txtSearchPlans.Text);
            FlyTreeNode Diet = new FlyTreeNode("Diet");
            FlyTreeNode Exercise = new FlyTreeNode("Exercise");
            FlyTreeNode Other = new FlyTreeNode("Other");
            foreach (Dictation_Plan_ViewModel p in plans)
            {
                FlyTreeNode pNode = new FlyTreeNode(p.PlanName);
                pNode.DragDropName = "plan";
                pNode.Value = p.PlanID.ToString();
                pNode.ContextMenuID = "PlanMenu";
                switch (p.Category)
                {
                    case "Diet":
                        Diet.ChildNodes.Add(pNode);
                        break;
                    case "Exercise":
                        Exercise.ChildNodes.Add(pNode);
                        break;
                    case "Other":
                        Other.ChildNodes.Add(pNode);
                        break;
                }

            }

            root.ChildNodes.Add(Diet);
            root.ChildNodes.Add(Exercise);
            root.ChildNodes.Add(Other);
            trePlans.Nodes.Add(root);
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
    /// Method to call a Delete record for diagnosis
    /// </summary>
    /// <param name="DiagnosisID"></param>
    [WebMethod]
    public static void DeleteRecordDiag(string DiagnosisID)
    {
        IAdminDictationService objService = new AdminDictationService();
        objService.DeleteDictationDiagnosis(Convert.ToInt16(DiagnosisID));
        objService = null;

    }

    /// <summary>
    /// Method to call a Delete record for Plan
    /// </summary>
    /// <param name="DiagnosisID"></param>
    [WebMethod]
    public static void DeleteRecordPlan(string DiagnosisID, string PlanID, string IsDiag)
    {
        IAdminDictationService objService = new AdminDictationService();
        objService.DeleteDictationPlan(Convert.ToInt16(DiagnosisID), Convert.ToInt16(PlanID), Convert.ToBoolean(IsDiag));
        objService = null;
    }


    #endregion
}