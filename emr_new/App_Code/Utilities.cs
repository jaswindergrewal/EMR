using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Emrdev.ServiceLayer;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
    public Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void SendGmailMessage(string toAddress, string toName, string MessageBody, string Attachment, string Subject)
    {
        var IfromAddress = new MailAddress("contactus@LongevityMedicalClinic.com", "Tickets");
        var ItoAddress = new MailAddress(toAddress, toName);
        const string fromPassword = "StarFish44!";
        string subject = Subject;
        string body = MessageBody;

        var smtp = new SmtpClient
        {
            Host = "pod51019.outlook.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(IfromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(IfromAddress.Address, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
    }

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

    public static int CalculateAge(DateTime birthDate)
    {
        DateTime now = DateTime.Today;
        int years = now.Year - birthDate.Year;

        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            --years;

        return years;
    }

    public static bool Expiring(string renMonth)
    {
        int theMonth = 0;
        if (renMonth == null) renMonth = "";
        switch (renMonth.ToLower())
        {
            case "jan":
                theMonth = 1;
                break;
            case "january":
                theMonth = 1;
                break;
            case "feb":
                theMonth = 2;
                break;
            case "february":
                theMonth = 2;
                break;
            case "mar":
                theMonth = 3;
                break;
            case "march":
                theMonth = 3;
                break;
            case "apr":
                theMonth = 4;
                break;
            case "april":
                theMonth = 4;
                break;
            case "may":
                theMonth = 5;
                break;
            case "jun":
                theMonth = 6;
                break;
            case "june":
                theMonth = 6;
                break;
            case "jul":
                theMonth = 7;
                break;
            case "july":
                theMonth = 7;
                break;
            case "aug":
                theMonth = 8;
                break;
            case "august":
                theMonth = 8;
                break;
            case "sept":
                theMonth = 9;
                break;
            case "september":
                theMonth = 9;
                break;
            case "oct":
                theMonth = 10;
                break;
            case "october":
                theMonth = 10;
                break;
            case "nov":
                theMonth = 11;
                break;
            case "november":
                theMonth = 11;
                break;
            case "dec":
                theMonth = 12;
                break;
            case "december":
                theMonth = 12;
                break;
            default:
                return false;

        }
        int CheckMonth = theMonth - 3;
        if (CheckMonth <= 0)
        {
            CheckMonth += 12;
        }
        if (CheckMonth <= DateTime.Now.Month)
        {
            return true;
        }
        return false;
    }

    public static int? AssignAppts(int PatientID, int FolloupID, bool SaveChanges)
    {
        IAppointmentConsole objIAppointmentConsole = null;
        int i = 0;
        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            if (SaveChanges)
            {
                i = objIAppointmentConsole.AssignApptsUtilities(PatientID, FolloupID);
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return i;

        #region "Old Code"
        //EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        ////get follow up
        //apt_FollowUp fup = (from f in ctx.apt_FollowUps
        //                    where f.FollowUp_ID == FolloupID
        //                    select f).First();

        //bool fsting = fup.FollowUp_Body.Contains("Fasting required: Yes");

        //// get all apts for patient of blood test

        //apt_rec apt = (from a in ctx.apt_recs
        //               where a.patient_id == PatientID
        //               && (a.AppointmentTypeID == 7 || a.AppointmentTypeID == 27)
        //               && a.ApptStart > DateTime.Now
        //               orderby a.ApptStart
        //               select a).FirstOrDefault();

        //// if found,match to most recent
        //if (apt != null)
        //{
        //    if (SaveChanges)
        //    {
        //        fup.AptAssigned = apt.apt_id;
        //        fup.FollowUp_Completed_YN = true;
        //        fup.FollowUp_Assigned_YN = true;
        //        ctx.SubmitChanges();
        //    }
        //    return apt.apt_id;
        //}
        //else
        //{
        //    return null;
        //}
        #endregion
    }


    //Send Survey  Mail to Patients
    public static void SendSurveyMessage(string toAddress, string toName, string Subject, int PatientID, int ApptID, string FormPath, int ApptTypeId)
    {
        var IfromAddress = new MailAddress("contactus@LongevityMedicalClinic.com", "Tickets");

        var ItoAddress = new MailAddress(toAddress, toName);
        const string fromPassword = "StarFish44!";
        string subject = "Longevity Patient Survey";
        string body = string.Empty;

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from EmailTemplate where appointmentId=" + ApptTypeId, conn);
            //string FormURL = System.Configuration.ConfigurationManager.AppSettings["PathforForm"] + System.Configuration.ConfigurationManager.AppSettings["wufooPatientIDField"];
            //FormURL += "=" + PatientID.ToString() + "&" + System.Configuration.ConfigurationManager.AppSettings["wufooApptIDField"] + "=" + ApptID.ToString();
            //

            string FormURL = FormPath;
            //FormURL += "=" + PatientID.ToString() + "&" + System.Configuration.ConfigurationManager.AppSettings["wufooApptIDField"] + "=" + ApptID.ToString();
            //
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                body = reader["TemplateDesc"].ToString();
                body = body.Replace("{UserName}", toName);
                body = body.Replace("{Url}", FormURL);
            }
        }


        var smtp = new SmtpClient
        {
            Host = "pod51019.outlook.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(IfromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(IfromAddress.Address, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        })
        {
            smtp.Send(message);
        }
    }

   


}