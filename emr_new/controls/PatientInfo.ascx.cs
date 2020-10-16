using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class controls_PatientInfo : System.Web.UI.UserControl
{
    #region Global

    public int PatientID { get; set; }
    public string UcStrAllergy { get; set; }
    protected int Age = 0;
    protected string qbName = string.Empty;

    Emrdev.ServiceLayer.PatientInfoService objService;
    protected Emrdev.ViewModelLayer.PatientViewModel objModelPatient;

    #endregion


    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        FillDetail();
    }

    #endregion


    #region Fill Patient Detail

    /// <summary>
    /// Method to Set the patient details wheter there is QB match and its dateof birth,Allergy info etc.
    /// </summary>
    public void FillDetail()
    {
        if (PatientID > 0)
        {
            objService = new Emrdev.ServiceLayer.PatientInfoService();
            //Get PatientInfo By PatientID
            objModelPatient = objService.GetPatientInfoById(PatientID);
            qbName = objService.GetQBCustomerFullNameByPatientId(PatientID);
            if (string.IsNullOrEmpty(qbName)) qbName = "No match in QB"; //If qbName is null
            if (objModelPatient.Birthday.HasValue)
            {
                Age = Utilities.CalculateAge(objModelPatient.Birthday.Value);//  Math.Floor(((DateTime.Now - pat.Birthday).TotalDays) / 365);
            }
            else
            {
                Age = 0;
            }
            if (objModelPatient.NameAlert.HasValue)
            {
                if (objModelPatient.NameAlert.Value)
                    tdName.BgColor = "#CC6666";
            }
            System.Web.UI.HtmlControls.HtmlControl frame1 = (System.Web.UI.HtmlControls.HtmlControl)Utilities.FindControlRecursive(Page.Master, "PageContents");
            if (frame1 == null)
            {
                btnAllergies.Visible = false;
            }
            else
            {
                btnAllergies.Visible = true;
            }


            if (objModelPatient.Inactive.Value)
                lblInactive.Visible = true;
            else
                lblInactive.Visible = false;

            if (objModelPatient.Allergies == null)
            {
                UcStrAllergy = "Pending";
                lblAllergies.Text = UcStrAllergy;
            }
            else
            {
                UcStrAllergy = objModelPatient.Allergies;
                lblAllergies.Text = UcStrAllergy;
                if (objModelPatient.NameAlert.HasValue)
                {
                    if (objModelPatient.NameAlert.Value)
                    {
                        lblAllergies.ForeColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        lblAllergies.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblAllergies.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (objModelPatient.Nickname == null)
            {
                lblNickName.Text = "None.";
            }
            else
                lblNickName.Text = objModelPatient.Nickname;
            //if (objModelPatient.ProgramName == null)
            //{
            //    lblManagementProgram.Text = "None.";
            //}
            //else
            //lblManagementProgram.Text = objModelPatient.ProgramName;
            /*Commented by jaswinder to  change the logic for renewal patient
             Date : 19 june 2014*/
            /* string[] typeIDs = { "80001240-1316387771", "3240000-1110208284", "68A0000-1145673910", "6890000-1145673687", "6870000-1145673532", "6880000-1145673590", "80000EBC-1284404338", "3230000-1110208284", "80000EBB-1284399169", "3240000-1110208284" };
             var Invoices=objService.GetInvoiceDetailByPatientId(PatientID, typeIDs);
             if (Invoices.Count() > 0) //if Invoices contains record
             {
                 //Item1=InvoiceLineItemRefListID,
                 //Item2=SalesPrice,
                 //Item3=DueDate,
                 //Itemn4=OpenBalance,
                 //Item5=IsPaid,
                 if (Invoices.First().Item1 == "80001240-1316387771")
                 {
                     if (Invoices.First().Item2 > objModelPatient.InvoiceDue)
                         objModelPatient.ExpirationDate = ((DateTime)Invoices.First().Item3).AddMonths(6);
                     else
                         objModelPatient.ExpirationDate = ((DateTime)Invoices.First().Item3);
                 }
                 else
                 {
                     if (Invoices.First().Item5.ToLower() == "true")
                         objModelPatient.ExpirationDate = ((DateTime)Invoices.First().Item3).AddYears(1);
                     else
                         objModelPatient.ExpirationDate = ((DateTime)Invoices.First().Item3);
                 }
                 objModelPatient.InvoiceDue = decimal.Parse(Invoices.First().Item4);
                 objModelPatient.InvoiceDueDate = Invoices.First().Item3;
                 objModelPatient.InvoicePaid = Invoices.First().Item5.ToLower() == "true" ? true : false;
                 if (Invoices.First().Item4 != "" && Invoices.First().Item4 != null)
                 {
                     objModelPatient.InvoiceDue = decimal.Parse(Invoices.First().Item4);
                 }
                 objService.UpdatePatientInformation(objModelPatient);//Save Patient Information                   
             }
             else
             {
                 objModelPatient.ExpirationDate = null;
             }

             var renDate = objModelPatient.ExpirationDate;
             string renMonth = "";
             if (objModelPatient.RenewalException == "" || objModelPatient.RenewalException == null)
             {
                 if (renDate != null)
                 {
                     if (objModelPatient.TermsInMonths == 6)
                     {
                         DateTime? invoiceDate=objService.GetInvoiceDateByDateOrder(PatientID);
                         if (invoiceDate.HasValue)
                         {
                             if (invoiceDate.Value.Day > 10)
                             {
                                 renDate = ((DateTime)renDate).AddMonths(1);
                             }
                         }
                         renMonth = ((DateTime)renDate).ToString("MMMM") + " - 6 Months";
                     }
                     else
                     {
                         DateTime? invoiceDate=objService.GetInvoiceDateByDateOrder(PatientID, typeIDs);
                         if (invoiceDate.HasValue)
                         {
                             if (invoiceDate.Value.Day > 10)
                             {
                                 renDate = ((DateTime)renDate).AddMonths(1);
                             }
                         }
                         renMonth = ((DateTime)renDate).ToString("MMMM") + " - 12 Months";
                     }
                     //renMonth = ((DateTime)renDate).ToString("MMMM");
                     lblRenewalMonth.ForeColor = System.Drawing.Color.Black;
                     if (objModelPatient.InvoiceDue >= 2000)
                     {
                         lblRenewalMonth.BackColor = System.Drawing.Color.Red;
                     }
                 }
                 else
                 {
                     renMonth = "None Found";
                     lblRenewalMonth.ForeColor = System.Drawing.Color.Black;
                 }
             }
             else
             {
                 renMonth = objModelPatient.RenewalException;
             }*/
            if (objModelPatient.PatientPackage > 0)
            {

                lblRenewalMonth.Text = ((DateTime)objModelPatient.PackageDateentered.AddMonths(objModelPatient.PackageDuration)).ToString("dd MMMM  yyyy");
                lblRenewalMonth.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblRenewalMonth.Text = "None Found";
                lblRenewalMonth.ForeColor = System.Drawing.Color.Black;
            }

            //Count Patient ProfileItems
            if (objService.ProfileItemCount(PatientID) > 0)
            {
                lblAutoship.Text = "Yes";
            }
            else
            {
                lblAutoship.BackColor = System.Drawing.Color.Red;
                lblAutoship.ForeColor = System.Drawing.Color.White;
                lblAutoship.Text = "No";
            }
        }
    }

    #endregion


    #region Button On Click For Redirection

    protected void btnAllergies_Click(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlControl frame1 = (System.Web.UI.HtmlControls.HtmlControl)Utilities.FindControlRecursive(Page.Master, "PageContents");
        frame1.Attributes["src"] = "allergies_edit.aspx?patientid=" + PatientID.ToString();
    }

    protected void btnNewTicket_Click(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlControl frame1 = (System.Web.UI.HtmlControls.HtmlControl)Utilities.FindControlRecursive(Page.Master, "PageContents");
        frame1.Attributes["src"] = "NewTicket.aspx?PatientID=" + PatientID.ToString() + "&IsAutoShipTicket=True";
    }

    #endregion


}