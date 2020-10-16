using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Emrdev.ServiceLayer;


public partial class PatientSurveyQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IEmailTemplateService objService = null;
        XElement root = null;
        
        //Main credentials for wufoo
        string wufooAPIKey = System.Configuration.ConfigurationManager.AppSettings["wufooAPIKey"];
        string authInfo = wufooAPIKey + ":" + System.Configuration.ConfigurationManager.AppSettings["authInfoPassword"];
        authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

        //For fields******************************************************************************
        StringBuilder urlFields = new StringBuilder();
        urlFields.Append(System.Configuration.ConfigurationManager.AppSettings["FieldXMLPath"]);
        HttpWebRequest requestFields = (HttpWebRequest)HttpWebRequest.Create(urlFields.ToString());
        requestFields.Headers["Authorization"] = "Basic " + authInfo;

        string ActiveQuestions=string.Empty;
        using (var webResponseFields = (HttpWebResponse)requestFields.GetResponse())
        {
            using (var readerFields = new StreamReader(webResponseFields.GetResponseStream()))
            {
                var objFields = readerFields.ReadToEnd();
                root = XElement.Parse(objFields);
                if (root != null)
                {
                    XElement theTitle;
                    XElement theType;
                    XElement theFieldName;
                    IEnumerable<XElement> XEleFields = root.Descendants("Field");
                    foreach (XElement XEleField in XEleFields)
                    {
                        theTitle = (from g in XEleField.Descendants("Title")

                                    select g).FirstOrDefault();

                        theType = (from g in XEleField.Descendants("Type")

                                   select g).FirstOrDefault();

                        theFieldName = (from g in XEleField.Descendants("ID")
                                        select g).FirstOrDefault();


                        
                        //Add questions and type to database

                        objService = new EmailTemplateService();
                        int OutQID=objService.insertSurveyQuestions(theTitle.Value, theType.Value, theFieldName.Value);
                        ActiveQuestions += OutQID + ",";

                        //If checkbox field     

                        if (theType.Value == "checkbox")
                        {
                            XElement theSubFields = (from g in XEleField.Descendants("SubFields") select g).FirstOrDefault();
                            var subele = (from g in theSubFields.Descendants("Subfield") select g).ToList();
                            foreach (var sub in subele)
                            {
                                XElement theSubTitle = (from g in sub.Descendants("Label")

                                                        select g).FirstOrDefault();

                                XElement theSubFieldName = (from g in sub.Descendants("ID")

                                                            select g).FirstOrDefault();

                                //Add questions and type to database

                                objService = new EmailTemplateService();
                                objService.insertSurveyQuestions(theSubTitle.Value, "Subfeild/" + theFieldName.Value, theSubFieldName.Value);

                            }
                        }
                    }
                }
            }
        }

        //Mark Questions as active
        objService = new EmailTemplateService();
        objService.UpdateQuestionAsActive(ActiveQuestions);


        //End For fields******************************************************************************************************

        //for entries*********************************************************************************************************
        StringBuilder urlEntries = new StringBuilder();
        urlEntries.Append(System.Configuration.ConfigurationManager.AppSettings["EntriesXMLPath"]);
        HttpWebRequest requestEntries = (HttpWebRequest)HttpWebRequest.Create(urlEntries.ToString());
        requestEntries.Headers["Authorization"] = "Basic " + authInfo;

        using (var webResponseEntries = (HttpWebResponse)requestEntries.GetResponse())
        {
            using (var readerEntries = new StreamReader(webResponseEntries.GetResponseStream()))
            {
                var objEntries = readerEntries.ReadToEnd();

                root = XElement.Parse(objEntries);
                if (root != null)
                {

                    IEnumerable<XElement> XEleFields = root.Descendants("Entry");
                    

                    string EntryId;
                    DateTime CreatedDate;
                    int patientID=0;
                    int ApptID=0;
                    foreach (XElement XEleField in XEleFields)
                    {

                        XElement EntryIdEle = (from g in XEleField.Descendants("EntryId")

                                         select g).FirstOrDefault();

                        EntryId = EntryIdEle.Value;
                        object retVal = 0;
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                        {
                            conn.Open();
                            using (SqlCommand command = new SqlCommand("ssp_GetEntryIDforsurvey", conn))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                retVal = command.ExecuteScalar();
                                conn.Close();
                            }
                        }
                        if (Convert.ToInt32(EntryId) > Convert.ToInt32(retVal))
                        {

                            XElement CreatedDateELE = (from g in XEleField.Descendants("DateCreated")

                                         select g).FirstOrDefault();

                            CreatedDate = Convert.ToDateTime(CreatedDateELE.Value.ToString());

                            XElement patientIDEle = (from g in XEleField.Descendants(System.Configuration.ConfigurationManager.AppSettings["wufooPatientIDField"])

                                         select g).FirstOrDefault();


                            
                            if (patientIDEle == null)
                            {
                                patientID = 0;
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(patientIDEle.Value))
                                    patientID = Convert.ToInt32(patientIDEle.Value);
                            }

                            var elemList1 = (from g in XEleField.Descendants(System.Configuration.ConfigurationManager.AppSettings["wufooApptIDField"])

                                         select g).FirstOrDefault();

                            if (elemList1==null)
                            {
                                ApptID = 0;
                            }
                            else
                            {

                                if(!string.IsNullOrEmpty(elemList1.Value))
                                    ApptID = Convert.ToInt32(elemList1.Value);
                            }
                            var elemList = XEleField.Elements().ToList();
                            foreach (var el in elemList)
                            {
                                string nodeName = el.Name.LocalName;
                                string nodeValue = el.Value;
                                if (nodeName != "EntryId" || nodeName != "DateCreated" || nodeName != System.Configuration.ConfigurationManager.AppSettings["wufooPatientIDField"])
                                {

                                    //Add questions and typr to database
                                    objService = new EmailTemplateService();
                                    objService.insertSurveyQuestionsAnswer(nodeName, nodeValue, patientID, CreatedDate, EntryId,ApptID);

                                   
                                }
                            }
                        }
                    }
                }
            }
        }
        //End For Entries******************************************************************************************************

        iframreport.Visible = true;
        iframreport.Attributes["src"] = System.Configuration.ConfigurationManager.AppSettings["ReportXMLPath"];
    }
}