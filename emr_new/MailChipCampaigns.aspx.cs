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
using System.Web.Script.Serialization;
using MailChimp;
using System.Text.RegularExpressions;

public partial class MailChipCampaigns : System.Web.UI.Page
{
    IMailChimpCampaignService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MailChimpManager mc = new MailChimpManager(System.Configuration.ConfigurationManager.AppSettings["MailChimpApiKey"]);
            var campaigns = mc.GetCampaigns();
            List<MailChimpCampaignViewModel> CampaignList = new List<MailChimpCampaignViewModel>();
           
            for (int i = 0; i < campaigns.Data.Count; i++)
            {
                MailChimpCampaignViewModel CampaignData = new MailChimpCampaignViewModel();
                CampaignData.MailChimpCampaignId = campaigns.Data[i].Id + "~" + campaigns.Data[i].ListId;
                CampaignData.MailChimpCampaignName = campaigns.Data[i].Title + " " + campaigns.Data[i].Id;
                CampaignList.Add(CampaignData);
            }

            drpCampaignList.DataSource = CampaignList;
            drpCampaignList.DataTextField = "MailChimpCampaignName";
            drpCampaignList.DataValueField = "MailChimpCampaignId";
            drpCampaignList.DataBind();
            drpCampaignList.Items.Insert(0, new ListItem("Select Campaign", "-1"));

            // var list6 = AddOrUpdateListMember("us14", "7054da9c377458d8a595d0e442f6182b", "2133738dad", "abc12333@gmail.com");

        }
    }

    private static string AddOrUpdateListMember(string dataCenter, string apiKey, string listId, string subscriberEmail)
    {
        var sampleListMember = new JavaScriptSerializer().Serialize(
            new
            {
                email_address = "abc12333@gmail.com",
                merge_fields =
                new
                {
                    FNAME = "Foo",
                    LNAME = "Bar"
                },
                status_if_new = "subscribed"
            });

        var hashedEmailAddress = string.IsNullOrEmpty(subscriberEmail) ? "" : CalculateMD5Hash(subscriberEmail.ToLower());
        var uri = string.Format("https://{0}.api.mailchimp.com/3.0/lists/{1}/members/{2}", dataCenter, listId, hashedEmailAddress);
        try
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Accept", "application/json");
                webClient.Headers.Add("Authorization", "apikey " + apiKey);

                return webClient.UploadString(uri, "PUT", sampleListMember);
            }
        }
        catch (WebException we)
        {
            using (var sr = new StreamReader(we.Response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }

    private static string CalculateMD5Hash(string input)
    {
        // Step 1, calculate MD5 hash from input.
        var md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // Step 2, convert byte array to hex string.
        var sb = new StringBuilder();
        foreach (var @byte in hash)
        {
            sb.Append(@byte.ToString("X2"));
        }
        return sb.ToString();
    }

    private string GetLists(string dataCenter, string apiKey, string listId = "")
    {
        var uri = string.Format("https://{0}.api.mailchimp.com/3.0/lists/{1}", dataCenter, listId);
        try
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Accept", "application/json");
                webClient.Headers.Add("Authorization", "apikey " + apiKey);

                return webClient.DownloadString(uri);
            }
        }
        catch (WebException we)
        {
            using (var sr = new StreamReader(we.Response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
    protected void btnSave_Campaigns(object sender, EventArgs e)
    {
        try
        {
            if (drpCampaignList.SelectedItem.Value != "-1")
            {
                objService = new MailChimpCampaignService();
                MailChimpCampaignViewModel mailChimpCampaign = new MailChimpCampaignViewModel();
                string[] ItemData = Regex.Split(drpCampaignList.SelectedItem.Value, "~");

                if (ItemData != null)
                {
                    if (ItemData.Length == 2)
                    {

                        mailChimpCampaign.MailChimpCampaignId = ItemData[0];
                        mailChimpCampaign.MailChimpCampaignListId = ItemData[1];
                    }
                }

                mailChimpCampaign.MailChimpCampaignName = drpCampaignList.SelectedItem.Text;

                objService.SaveMailChimpCampaign(mailChimpCampaign);
            }
        }
        catch (WebException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

}
public class Campaigns
{
    string id { get; set; }
    Settings settings { get; set; }
    
}

public class Settings
{
    string subject_line { get; set; }
    string title { get; set; }
    
}