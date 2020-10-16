using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Net;
using Emrdev.ServiceLayer;
using System.Data;
using System.Text;
using System.IO;

using System.Drawing;
using System.Runtime.Serialization.Json;
using Emrdev.ViewModelLayer;

public partial class CallFirePage : CallFireServicesTestClient
{
    DataTable dt = new DataTable();
    IAppointmentConsole objAppointmentService = null;
    IPatientService objService = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            objAppointmentService = new AppointmentConsole(); ;
            ddlAppointmentType.DataSource = objAppointmentService.GetAppointmentTypeList();
            ddlAppointmentType.DataTextField = "TypeName";
            ddlAppointmentType.DataValueField = "ID";
            ddlAppointmentType.DataBind();
            ddlAppointmentType.Items.Insert(0, new ListItem(""));
            objService = new PatientService();

            ddlClinic.DataSource = objService.GetClinics();
            ddlClinic.DataTextField = "ClinicName";
            ddlClinic.DataValueField = "ClinicName";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem(""));

            ddlProviders.DataSource = Calendar.Providers.getProviderList();
            ddlProviders.DataTextField = "ProviderName";
            ddlProviders.DataValueField = "id";
            ddlProviders.DataBind();
            ddlProviders.Items.Insert(0, new ListItem(""));


            txtBegin.Text = DateTime.Today.AddDays(1).ToString("d");
            txtEnd.Text = DateTime.Today.AddDays(1).ToString("d");
            txtBegin.Attributes.Add("readonly", "readonly");
            txtEnd.Attributes.Add("readonly", "readonly");



        }


        /*   string url = "https://api.callfire.com/v2/campaigns/ivrs";
           WebRequest myReq = WebRequest.Create(url);

           string username =System.Configuration.ConfigurationManager.AppSettings["CallFireUserName"]; 
           string password = System.Configuration.ConfigurationManager.AppSettings["CallFirePassword"];
           string usernamePassword = username + ":" + password;
           CredentialCache mycache = new CredentialCache();
           mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
           myReq.Credentials = mycache;
           myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

           myReq.Method = "Post";
           myReq.ContentType = "application/json";

           string name = "jas";
           IVRCallFire ivrcall = new IVRCallFire();
           ivrcall.fromNumber = "14252961441";
           ivrcall.name = "test12345";
           ivrcall.dialplanXml = "<dialplan><play type=\"tts\">Congratulations! You have successfully configured a CallFire I V R "+name+".</play></dialplan>";
       
           recipients recipient = new recipients();
           recipient.phoneNumber = "14252961441";

           List<recipients> recipientList = new List<recipients>();

           recipientList.Add(recipient);
           ivrcall.recipients = recipientList;
           ivrcall.start = true;

           string postData = JsonSerializer<IVRCallFire>(ivrcall); 

           byte[] bytes = Encoding.UTF8.GetBytes(postData);
           Stream requestStream = myReq.GetRequestStream();
           requestStream.Write(bytes, 0, bytes.Length);

           try
           {
               WebResponse wr = myReq.GetResponse();
               Stream receiveStream = wr.GetResponseStream();
               StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
               string content = reader.ReadToEnd();

               IVRCallFire result = JsonDeserialize<IVRCallFire>(content);
               string IvrId = result.id;

               url = "https://api.callfire.com/v2/campaigns/ivrs/"+IvrId+"/start";
               WebRequest myReqCallStart = WebRequest.Create(url);
               myReqCallStart.Method = "Post";
               myReqCallStart.Credentials = mycache;
               myReqCallStart.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

               wr = myReqCallStart.GetResponse();
               receiveStream = wr.GetResponseStream();
               reader = new StreamReader(receiveStream, Encoding.UTF8);
               content = reader.ReadToEnd();
          
           }
           catch (WebException ex)
           {
               if (ex.Status == WebExceptionStatus.ProtocolError)
               {
                   HttpWebResponse err = ex.Response as HttpWebResponse;
                   if (err != null)
                   {
                       string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                       // txtResults = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
                   }
               }
               else
               {

               }
           }*/





    }

    [System.Web.Services.WebMethod]
    public static List<CallFirePatientListModel> GetPatientList(string startDate, string endDate, string appointmentType, string clinicData, string providerData)
    {

        IOrderService objService = null;
        List<CallFirePatientListModel> lstStaff = null;
        try
        {
            objService = new OrderService();
            DateTime StartDate = Convert.ToDateTime(startDate);
            DateTime EndDate = Convert.ToDateTime(endDate);
            int apptType = 0;
            if (appointmentType != "")
            {
                apptType = Convert.ToInt16(appointmentType);
            }
            
            int provider = 0;
            if (providerData != "")
            {
                provider = Convert.ToInt16(providerData);
            }
            lstStaff = objService.GetPatientForCallFire(StartDate, EndDate, apptType, clinicData, provider);
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstStaff;
    }
    protected void btnCallFire_Click(object sender, EventArgs e)
    {

        string url = "https://api.callfire.com/v2/campaigns/ivrs";
        string CallFireData = Selecteddataforcall.Value;
        string AppointmentId = SelectedCampaignId.Value;
        IEmailTemplateService templatService = new EmailTemplateService();

        EmailTemplateViewModel TemplateViewModel = null;
        string body = string.Empty;
        TemplateViewModel = templatService.GetIVRTemplate(Convert.ToInt16(AppointmentId));
        if (TemplateViewModel != null)
        {
            body = TemplateViewModel.TemplateDesc;

            // Split string on spaces.
            // ... This will separate all the words.
            string[] data = CallFireData.Split('/');
            int firstLine = 0;
            foreach (string item in data)
            {
                firstLine = firstLine + 1;
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");

                string CallFireItems = item;
                // Split string on spaces.
                // ... This will separate all the words.
                string[] ItemData = CallFireItems.Split('~');
                if (firstLine > 1)
                {
                    if (ItemData != null)
                    {
                        if (ItemData.Length == 3)
                        {
                            string IVRContent = string.Empty;
                            url = "https://api.callfire.com/v2/campaigns/ivrs";
                            WebRequest myReq = WebRequest.Create(url);

                            string username = System.Configuration.ConfigurationManager.AppSettings["CallFireUserName"];
                            string password = System.Configuration.ConfigurationManager.AppSettings["CallFirePassword"];
                            string usernamePassword = username + ":" + password;
                            CredentialCache mycache = new CredentialCache();
                            mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
                            myReq.Credentials = mycache;
                            myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

                            myReq.Method = "Post";
                            myReq.ContentType = "application/json";

                            DateTime d;
                            d = DateTime.Parse(ItemData[1]);
                            var dtPart = String.Format("{0:MMMM dd yyyy}", d);
                            var tmPart = d.ToShortTimeString();


                            IVRCallFire ivrcall = new IVRCallFire();
                            ivrcall.fromNumber = System.Configuration.ConfigurationManager.AppSettings["CallFireFromNumber"]; //"14252961441";
                            ivrcall.name = "IVRCall-" + g;
                            IVRContent = body;
                            IVRContent = IVRContent.Replace("{UserName}", ItemData[0]);
                            IVRContent = IVRContent.Replace("{AppointmentDate}", dtPart + " at " + tmPart);

                            string dialPlan = "<dialplan name=\"Root\">" +
             "<amd>" +
                 "<live>	<goto name=\"goto_Live\">Live</goto></live>" +
                "<machine><goto name=\"goto_Machine\">Machine</goto></machine>" +
             "</amd>" +
             "<menu maxDigits=\"1\" name=\"Live\">" +
                 "<play type=\"tts\" voice=\"female1\" name=\"play_Live\"><![CDATA[<vtml_speed value=\"100\">" + IVRContent + "</vtml_speed>]]></play>" +
                 "<keypress pressed=\"1\" name=\"Transfer\"><transfer name=\"Reschedule\">" + System.Configuration.ConfigurationManager.AppSettings["CallFireFromNumber"] + "</transfer></keypress>" +
                                "<keypress pressed=\"default\" name=\"incorrect_Selection\">" +
                                    "<play type=\"tts\" voice=\"female1\" name=\"play_Inorrect_Selection\"></play>" +
                                    "<goto name=\"replay_Live\">Live</goto>" +
                                "</keypress>" +
             "</menu>" +
                                 "<play type=\"tts\" voice=\"female1\" name=\"Machine\"><![CDATA[<vtml_speed value=\"100\">" + IVRContent + "</vtml_speed>]]></play>" +
             "<hangup name=\"Hangup\"/>" +
         "</dialplan>";


                            ivrcall.dialplanXml = dialPlan;
                            recipients recipient = new recipients();
                            recipient.phoneNumber = ItemData[2];

                            List<recipients> recipientList = new List<recipients>();

                            recipientList.Add(recipient);
                            ivrcall.recipients = recipientList;

                            string postData = JsonSerializer<IVRCallFire>(ivrcall);
                            byte[] bytes = Encoding.UTF8.GetBytes(postData);
                            using (Stream requestStream = myReq.GetRequestStream())
                            {
                                requestStream.Write(bytes, 0, bytes.Length);
                            }




                            try
                            {
                                WebResponse wr = myReq.GetResponse();

                                // Stream receiveStream = wr.GetResponseStream();
                                string content = string.Empty;
                                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                                {
                                    content = reader.ReadToEnd();
                                }

                                IVRCallFire result = JsonDeserialize<IVRCallFire>(content);
                                string IvrId = result.id;

                                url = "https://api.callfire.com/v2/campaigns/ivrs/" + IvrId + "/start";
                                WebRequest myReqCallStart = WebRequest.Create(url);
                                myReqCallStart.Method = "Post";
                                myReqCallStart.Credentials = mycache;
                                myReqCallStart.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

                                wr = myReqCallStart.GetResponse();
                                // receiveStream = wr.GetResponseStream();
                                using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
                                {
                                    content = reader.ReadToEnd();
                                }

                                wr.Close();
                            }
                            catch (WebException ex)
                            {
                                if (ex.Status == WebExceptionStatus.ProtocolError)
                                {
                                    HttpWebResponse err = ex.Response as HttpWebResponse;
                                    if (err != null)
                                    {
                                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                                        // txtResults = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }

            }


            //DataTable dt = ViewState["dt"] as DataTable;
            //string NumberForAPI = string.Empty;
            //foreach (GridViewRow row in gridPatient.Rows)
            //{
            //    if (row.Cells[1].Text != "")
            //    {
            //        if (NumberForAPI == "")
            //            NumberForAPI = row.Cells[1].Text + "," + row.Cells[1].Text;
            //        else
            //            NumberForAPI = NumberForAPI + "\n" + row.Cells[1].Text + "," + row.Cells[1].Text;
            //    }
            //}

            //if (dt.Rows.Count > 0)
            //{
            //    DataColumn col = dt.Columns["HomePhone"];

            //    foreach (DataRow row in dt.Rows)
            //    {

            //        if (row["CellPhone"] != "")
            //        {
            //            if (NumberForAPI == "")
            //                NumberForAPI = row["CellPhone"] + "," + row["FirstName"];
            //            else
            //                NumberForAPI = NumberForAPI + "\n" + row["CellPhone"] + "," + row["FirstName"];
            //        }

            //        else if (row["HomePhone"] != "")
            //        {
            //            if (NumberForAPI == "")
            //                NumberForAPI = row["HomePhone"] + "," + row["FirstName"];
            //            else
            //                NumberForAPI = NumberForAPI + "\n" + row["HomePhone"] + "," + row["FirstName"];
            //        }
            //        else if (row["WorkPhone"] != "")
            //        {
            //            if (NumberForAPI == "")
            //                NumberForAPI = row["WorkPhone"] + "," + row["FirstName"];
            //            else
            //                NumberForAPI = NumberForAPI + "\n" + row["WorkPhone"] + "," + row["FirstName"];
            //        }
            //        // strJsonData = row[col].ToString();
            //    }
            //post.PostItems.Add("numbers", NumberForAPI);
            //post.Type = PostSubmitter.PostTypeEnum.Post;
            //string result = post.Post();
            //Console.WriteLine("Result is");
            //Console.WriteLine(result);
            ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#<%=loadingdiv.ClientID%>').hide();", true);
        }
    }

    // List<string> phone=new List<string>();
    ////string Phone1="2134000543,ankit";
    ////phone.Add("14253227199,Jeremy");
    //phone.Add("+919876675013,sankit");
    //foreach (var item in phone)
    //{
    //    post.PostItems.Add("numbers", item);
    //    post.Type = PostSubmitter.PostTypeEnum.Post;
    //    string result = post.Post();
    //    Console.WriteLine("Result is");
    //    Console.WriteLine(result);
    //    post.PostItems = null;
    //}



    public static string JsonSerializer<T>(T t)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream();
        ser.WriteObject(ms, t);
        string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();
        return jsonString;
    }

    /// <summary>
    /// JSON Deserialization
    /// </summary>
    public static T JsonDeserialize<T>(string jsonString)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        T obj = (T)ser.ReadObject(ms);
        return obj;
    }


}

public class recipients
{
    public string phoneNumber { get; set; }
}

public class IVRCallFire
{
    public string id { get; set; }
    public string name { get; set; }
    public string fromNumber { get; set; }
    public string dialplanXml { get; set; }
    public List<recipients> recipients { get; set; }
    public Boolean start { get; set; }
}

