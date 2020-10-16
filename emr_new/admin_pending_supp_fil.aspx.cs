using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
public partial class admin_pending_pre_fil : LMCBase
{

    #region "Variables"
    protected PendingPrescriptionAproveViewModel PendingPrescriptions = new PendingPrescriptionAproveViewModel();
    PendingPrescriptionAproveService objService = null;
    #endregion
   
	//Load page with the selected prescription id
    protected void Page_Load(object sender, EventArgs e)
	{

        if (!IsPostBack)
        {
            try
            {
                objService = new PendingPrescriptionAproveService();
                if (Request.QueryString["pre_id"] != null)
                {
                    PendingPrescriptions = objService.GetPescriptionSupDetail((int)Session["StaffID"], int.Parse(Request.QueryString["pre_id"]));
                    if (PendingPrescriptions != null)
                    {
                        Sig.Text = PendingPrescriptions.Drug_Dose;
                        Dispenses.Text = PendingPrescriptions.Drug_Dispenses;
                        NumbRefills.Text = PendingPrescriptions.Drug_NumbRefills;
                        DateEntered.Text = DateTime.Now.ToShortDateString();
                        PreNotes.Text = PendingPrescriptions.Notes;
                    }
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
        //sqlClinic.ConnectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        //if (!IsPostBack)
        //{
        //    PendingPrescriptions = (from p in ctx.PresscriptionSupps
        //                            join d in ctx.AutoshipProducts on p.ProductID equals d.ProductID
        //                            join s in ctx.Staffs on p.EnteredBy equals s.EmployeeID
        //                            join pa in ctx.Patients on p.PatientID equals pa.PatientID
        //                            where p.PresscriptionSuppID == int.Parse(Request.QueryString["pre_id"])
        //                            select new PrescipApprove
        //                            {
        //                                PrescriptionID = p.PresscriptionSuppID,
        //                                approved_date = p.Approved_Date,
        //                                Approved_yn = p.Approved_YN,
        //                                DateEntered = p.DateEntered,
        //                                Drug_DatePrescibed = p.SuppDatePrescibed,
        //                                Drug_Dispenses = p.SuppDispenses,
        //                                Drug_Dose = p.SuppDose,
        //                                Drug_EndDate = p.SuppEndDate,
        //                                Drug_NumbRefills = p.SuppNumbRefills,
        //                                DrugName = d.ProductName,
        //                                EmployeeName = s.EmployeeName,
        //                                FirstName = pa.FirstName,
        //                                Lastname = pa.LastName,
        //                                Notes = p.Notes,
        //                                Patientid = p.PatientID,
        //                                viewable_yn = p.viewable_yn,
        //                                AccessLevel = (from s1 in ctx.Staffs where s1.EmployeeID == (int)Session["StaffID"] select s1.access_level).First(),
        //                            }).ToList().First();
        //    Sig.Text = PendingPrescriptions.Drug_Dose;
        //    Dispenses.Text = PendingPrescriptions.Drug_Dispenses;
        //    NumbRefills.Text = PendingPrescriptions.Drug_NumbRefills;
        //    DateEntered.Text = DateTime.Now.ToShortDateString(); ;
        //    PreNotes.Text = PendingPrescriptions.Notes;
        //}
	}

	
    //change the approved colunm values in prescription sup table and update the details
    protected void btnApprove_Click(object sender, EventArgs e)
	{
        try
        {
            objService = new PendingPrescriptionAproveService();
            int PatientID = objService.UpdateSuppliments((int)Session["StaffID"], int.Parse(Request.QueryString["pre_id"]), Sig.Text, Dispenses.Text, NumbRefills.Text, DateTime.Parse(DateEntered.Text), PreNotes.Text);
            Response.Redirect("admin_pending_supp_fil.aspx?pre_id=" + Request.QueryString["pre_id"], false);
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

	//Print the approve suppliment details
    protected void btnPrint_Click(object sender, EventArgs e)
	{

        try
        {
            objService = new PendingPrescriptionAproveService();
            int PatientID = objService.UpdateSuppliments((int)Session["StaffID"], int.Parse(Request.QueryString["pre_id"]), Sig.Text, Dispenses.Text, NumbRefills.Text, DateTime.Parse(DateEntered.Text), PreNotes.Text);

            Calendar.Patient pat = new Calendar.Patient((int)PatientID);
            string Clinic = "";
            switch (pat.Clinic)
            {
                case "Kirkland":
                    Clinic = pat.ClinicID;
                    break;
                case "Seattle":
                    Clinic = pat.ClinicID;
                    break;
                case "South":
                    Clinic = pat.ClinicID;
                    break;
                case "Lynnwood":
                    Clinic = pat.ClinicID;
                    break;
            }

            Response.Redirect("PrescriptionSupp.aspx?PatientID=" + PatientID.ToString() + "&scripIds=" + Request.QueryString["pre_id"] + "&Clinic=" + Clinic, false);
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
