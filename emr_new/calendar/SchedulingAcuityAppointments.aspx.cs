using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;


public partial class calendar_SchedulingAcuityAppointments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownloadAppointments_Click(object sender, EventArgs e)
    {
        
        string url = "https://acuityscheduling.com/api/v1/appointments?max=2000&minDate="+DateTime.Now;
        WebRequest myReq = WebRequest.Create(url);

        string username = "14443559";//System.Configuration.ConfigurationManager.AppSettings["CallFireUserName"];
        string password = "8f7a0977e9f27d1697cc50647a898af7";// System.Configuration.ConfigurationManager.AppSettings["CallFirePassword"];
        string usernamePassword = username + ":" + password;
        CredentialCache mycache = new CredentialCache();
        mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
        myReq.Credentials = mycache;
        myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

        myReq.Method = "Get";
        myReq.ContentType = "application/json";
        WebResponse wr = myReq.GetResponse();

        Stream stream = wr.GetResponseStream();
        StreamReader reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        // receiveStream = wr.GetResponseStream();
        List<AcuityAppointment> acuitystream;
        //using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
        //{
        //    string content = reader.ReadToEnd();
        acuitystream = new JavaScriptSerializer().Deserialize<List<AcuityAppointment>>(result);
        //}
        acuitystream = acuitystream.OrderByDescending(a => a.id).ToList();
        wr.Close();

        IAcuitySchedulingService objService=new AcuitySchedulingService();
        string APIKey = System.Configuration.ConfigurationManager.AppSettings["MailChimpApiKey"];
        Calendar.AppointmentType AppointmentType = Calendar.AppointmentTypes.GetApptType(154);
        
        objService.SaveAcuityAppointment(acuitystream, (int)Session["StaffID"], APIKey, AppointmentType.MailChimpCampaignId);

        


    }




    protected void btnBellamedica_Click(object sender, EventArgs e)
    {
        //for BellaMedica
        //        User ID: 18854014
        //API Key: ead2602529dbeddfa5ba52c092bf3ddb

        string url = "https://acuityscheduling.com/api/v1/appointments?max=2000&minDate=" + DateTime.Now;
        WebRequest myReq = WebRequest.Create(url);
        string username = "18854014";//System.Configuration.ConfigurationManager.AppSettings["CallFireUserName"];
          string password = "ead2602529dbeddfa5ba52c092bf3ddb";// System.Configuration.ConfigurationManager.AppSettings["CallFirePassword"];
         string usernamePassword = username + ":" + password;
        CredentialCache mycache = new CredentialCache();
         mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
         myReq.Credentials = mycache;
         myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

         myReq.Method = "Get";
         myReq.ContentType = "application/json";
         //stream.Dispose();
         WebResponse wrBellaMedica = myReq.GetResponse();

         Stream streameBellaMedica = wrBellaMedica.GetResponseStream();
         StreamReader readerBellaMedica = new StreamReader(streameBellaMedica);
         var result = readerBellaMedica.ReadToEnd();
         // receiveStream = wr.GetResponseStream();
         List<AcuityAppointment> acuitystreamBellamedica;
         //using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
         //{
         //    string content = reader.ReadToEnd();
         acuitystreamBellamedica = new JavaScriptSerializer().Deserialize<List<AcuityAppointment>>(result);
         //}
         acuitystreamBellamedica = acuitystreamBellamedica.OrderByDescending(a => a.id).ToList();
         wrBellaMedica.Close();

        AcuitySchedulingService objService = new AcuitySchedulingService();

        string APIKey = System.Configuration.ConfigurationManager.AppSettings["MailChimpApiKey"];
        Calendar.AppointmentType AppointmentType =Calendar.AppointmentTypes.GetApptType(161);
        objService.SaveAcuityAppointment(acuitystreamBellamedica, (int)Session["StaffID"], APIKey, AppointmentType.MailChimpCampaignId);

     
    }
}
