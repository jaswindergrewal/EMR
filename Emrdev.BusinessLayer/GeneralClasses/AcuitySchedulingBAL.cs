using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AcuitySchedulingBAL
    {
        AcuitySchedulingDAL obj = new AcuitySchedulingDAL();


        public void SaveAcuityAppointment(List<AcuityAppointment> acuityStream, int staffId, string APIKey, string mailChimpCampaignId)
        {
            foreach (var item in acuityStream)
            {
                
                string clinicName = string.Empty;
                int providerId = 0;
                if (item.calendarId == 1575425)
                {
                    clinicName = "Kirkland";
                    providerId = 17;
                }
                else if (item.calendarId == 1580585 || item.calendarId == 2627694)
                {
                    clinicName = "South";
                    providerId = 19;
                }
                else if (item.calendarId == 1935306)
                {
                    clinicName = "Lynnwood";
                    providerId = 18;
                }
                else if(item.calendarId== 3489576)
                {
                    clinicName = "Kirkland";
                    providerId = 38;
                    
                    
                }
                AcuityScheduling Appointment = obj.Get<AcuityScheduling>(o => o.AppointmentId == item.id && o.Clinic==clinicName);
                DateTime endtimeValue = Convert.ToDateTime(item.endTime);
                DateTime starttimeValue = Convert.ToDateTime(item.time);
                DateTime startDateTime = item.date.Add(TimeSpan.Parse(starttimeValue.ToString("HH:mm")));
                DateTime endDateTime = item.date.Add(TimeSpan.Parse(endtimeValue.ToString("HH:mm")));
                
                apt_rec CheckAppointment = null;
                if (Appointment != null)
                {
                    CheckAppointment = obj.Get<apt_rec>(o => o.patient_id == Appointment.PatientID && o.ProviderID == providerId && o.ApptStart == startDateTime && o.ApptEnd == endDateTime);
                }
                if (CheckAppointment == null)
                {

                    if (Appointment == null)
                    {
                        AcuityScheduling insertAcuityData = new AcuityScheduling();
                        insertAcuityData.firstName = item.firstName;
                        insertAcuityData.lastName = item.lastName;
                        insertAcuityData.CreatedDate = item.dateCreated;
                        insertAcuityData.AppointmentId = item.id;
                        insertAcuityData.AptDate = item.date;

                        insertAcuityData.StartTime = item.time;
                        insertAcuityData.EndTime = item.endTime;
                        insertAcuityData.Location = item.Location;
                        insertAcuityData.phone = item.phone;
                        insertAcuityData.email = item.email;
                        insertAcuityData.Clinic = clinicName;

                        obj.Create(insertAcuityData);

                    }
                    AcuityScheduling patDetails = obj.Get<AcuityScheduling>(o => o.firstName == item.firstName && o.lastName == item.lastName && o.phone == item.phone && o.email == item.email);
                    Nullable<int> PatientId;
                    if (patDetails != null && patDetails.PatientID != null)
                    {
                        PatientId = patDetails.PatientID;
                    }
                    else
                    {
                        Patient patientDetails = obj.Get<Patient>(o => o.FirstName == item.firstName && o.LastName == item.lastName && o.Email == item.email);
                        if (patientDetails != null)
                        {
                            PatientId = patientDetails.PatientID;
                            patDetails.PatientID = PatientId;

                            obj.Edit(patDetails);
                        }
                        else
                        {
                            Patient pat = new Patient();
                            string[] streetlocation = item.Location.Split(',');
                            string City = string.Empty;
                            string State = string.Empty;
                            string Zip = string.Empty;
                            foreach (var i in streetlocation)
                            {
                                if (i.Contains("Kirkland") || i.Contains("Lynnwood") || i.Contains("Tacoma"))
                                {
                                    string[] location = i.Trim().Split(' ');
                                    int strLength = location.Length;
                                    if (strLength == 3)
                                    {
                                        City = location[0];
                                        State = location[1];
                                        Zip = location[2];
                                    }
                                }
                            }

                            pat.BillingCity = City;
                            pat.BillingState = State;
                            int streetlocationLength = streetlocation.Length;
                            if (streetlocationLength > 1)
                            {
                                pat.BillingStreet = streetlocation[0].Trim() + " , " + streetlocation[1].Trim();
                            }
                            else
                            {
                                pat.BillingStreet = item.Location;
                            }
                            pat.BillingZip = Zip;
                            pat.FirstName = item.firstName;
                            pat.LastName = item.lastName;
                            pat.HomePhone = item.phone;
                            pat.Email = item.email;
                            pat.EmergencyContact = "";
                            pat.AllowApptReassign = false;
                            pat.Medical = false;
                            pat.Aesthetics = false;
                            pat.Autoship = false;
                            pat.Retail = false;
                            pat.Affiliate = false;
                            pat.DiabetesSOC = false;
                            pat.HeartSOC = false;
                            pat.AutoShipAlerts = "";
                            pat.AutoshipNote = "";
                            pat.MedicareOptOut_YN = false;
                            pat.EatingPlanReceived_YN = false;
                            pat.AutoshipEmail = false;
                            pat.AutoshipCancelReasonID = -1;
                            pat.AutoshipCancelOther = "";
                            pat.Birthday = DateTime.Now;

                            pat.Clinic = clinicName;
                            pat.Inactive = false;
                            pat.ConciergeID = "0";// staffId.ToString();
                            pat.Cancel_NoShow_frm_signed = false;
                            pat.HIPPA_signed = false;
                            pat.NameAlert = false;
                            pat.Home_detailed_info = false;
                            pat.Home_CB_only = false;
                            pat.Work_Detailed_info = false;
                            pat.Work_CB_only = false;
                            pat.Cell_CB_Only = false;
                            pat.Cell_Detailed_info = false;
                            pat.Email_auth_detailed_info = false;
                            pat.Fax_auth_detailed_info = false;


                            obj.Create(pat);

                            patDetails.PatientID = pat.PatientID;
                            PatientId = pat.PatientID;
                            obj.Edit(patDetails);
                            // HttpContext.Current.Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + PatientId.ToString(), false);
                        }
                    }
                    List<apt_rec> oldApts = obj.GetAll<apt_rec>(o => (o.patient_id == 7445 || o.patient_id == 22267 || o.patient_id == 22268 || o.patient_id == 22269 || o.patient_id== 23965 || o.patient_id == 24970) && o.ProviderID == providerId && o.ApptStart == startDateTime && o.ApptEnd == endDateTime).ToList();
                    if (oldApts.Count > 0)
                    {
                        foreach (apt_rec a in oldApts)
                        {
                            if(a.AppointmentTypeID==161 && a.patient_id== 23965)
                            {
                                a.patient_id = PatientId;
                                obj.Edit(a);
                            }
                            else if(a.AppointmentTypeID==174)
                            {
                                a.patient_id = PatientId;
                                obj.Edit(a);
                            }
                            else if (a.AppointmentTypeID == 154)
                            {
                                a.patient_id = PatientId;
                                obj.Edit(a);
                            }

                        }
                    }
                    else
                    {
                        CheckAppointment = obj.Get<apt_rec>(o => o.patient_id == PatientId && o.ProviderID == providerId && o.ApptStart == startDateTime && o.ApptEnd == endDateTime);
                        if (CheckAppointment == null)
                        {
                            apt_rec appt = new apt_rec();
                            appt.ApptEnd = endDateTime;
                            appt.ApptStart = startDateTime;
                            appt.patient_id = PatientId;
                            appt.LabsCheckedIn = false;
                            if(providerId==38)
                            {
                                if (item.type == "Beautiful New You Consultation")
                                {
                                    appt.AppointmentTypeID = 174;
                                }
                                  else  appt.AppointmentTypeID = 161;
                            }
                            else { appt.AppointmentTypeID = 154; }
                           // appt.AppointmentTypeID = 154;
                            appt.ProviderID = providerId;
                            appt.date_entered = DateTime.Now;
                            appt.ActionNeeded = "No";
                            appt.Clinic = clinicName;
                            appt.closed_yn = false;
                            appt.SaleMade_yn = -1;
                            appt.StatusID = 8;
                            appt.AllDay = false;
                            appt.EmailOnChange = false;
                            appt.Results = 0;
                            appt.Notes = "";
                            appt.Email = "";
                            obj.Create<apt_rec>(appt);
                        }
                    }

                    //FormailChimp campaign
                    if (Appointment == null)
                    {
                        if (item.calendarId != 3489576)
                        {
                            if (!string.IsNullOrEmpty(item.email))
                            {



                                string[] ItemData = Regex.Split(APIKey, "-");

                                if (ItemData != null)
                                {
                                    if (ItemData.Length == 2)
                                    {

                                        if (!string.IsNullOrEmpty(mailChimpCampaignId))
                                        {
                                            string[] CampaignData = Regex.Split(mailChimpCampaignId, "~");

                                            if (CampaignData != null)
                                            {
                                                if (CampaignData.Length == 2)
                                                {
                                                    var SaveData = AddOrUpdateListMember(ItemData[1], ItemData[0], CampaignData[1], item.email, item.firstName, item.lastName);

                                                }
                                            }
                                        }


                                    }
                                }
                            }
                        }
                    }
                     //End for mailchimpcampaign

                }


            }
        }

        //public void SaveAcuityAppointment(List<AcuityAppointment> acuityStream, int staffId,string APIKey, string mailChimpCampaignId)
        //{
        //    foreach( var item in acuityStream)
        //    {
        //        AcuityScheduling Appointment=obj.Get<AcuityScheduling>(o => o.AppointmentId == item.id );
        //        if(Appointment==null)
        //        {
        //            string clinicName = string.Empty;
        //            int providerId = 0;
        //            if (item.calendarId == 1575425)
        //            {
        //                clinicName = "Kirkland";
        //                providerId = 17;
        //            }
        //            else if (item.calendarId == 1580585 || item.calendarId == 2627694)
        //            {
        //                clinicName = "South";
        //                providerId = 19;
        //            }
        //            else if (item.calendarId == 1935306)
        //            {
        //                clinicName = "Lynnwood";
        //                providerId = 18;
        //            }

        //            AcuityScheduling insertAcuityData = new AcuityScheduling();
        //            insertAcuityData.firstName = item.firstName;
        //            insertAcuityData.lastName = item.lastName;
        //            insertAcuityData.CreatedDate = item.dateCreated;
        //            insertAcuityData.AppointmentId = item.id;
        //            insertAcuityData.AptDate = item.date;
        //            DateTime endtimeValue = Convert.ToDateTime(item.endTime);
        //            DateTime starttimeValue = Convert.ToDateTime(item.time);
        //            DateTime startDateTime = item.date.Add(TimeSpan.Parse(starttimeValue.ToString("HH:mm")));
        //            DateTime endDateTime = item.date.Add(TimeSpan.Parse(endtimeValue.ToString("HH:mm")));
        //            insertAcuityData.StartTime = item.time;
        //            insertAcuityData.EndTime = item.endTime;
        //            insertAcuityData.Location = item.Location;
        //            insertAcuityData.phone = item.phone;
        //            insertAcuityData.email   = item.email;
        //            insertAcuityData.Clinic = clinicName;

        //                obj.Create(insertAcuityData);


        //            AcuityScheduling patDetails = obj.Get<AcuityScheduling>(o => o.firstName == item.firstName && o.lastName == item.lastName && o.phone == item.phone && o.email == item.email);
        //            Nullable<int> PatientId;
        //            if(patDetails!=null && patDetails.PatientID!=null)
        //            {
        //                PatientId = patDetails.PatientID;
        //            }
        //            else
        //            {
        //                Patient patientDetails = obj.Get<Patient>(o => o.FirstName == item.firstName && o.LastName == item.lastName &&  o.Email == item.email);
        //                if (patientDetails != null)
        //                {
        //                    PatientId = patientDetails.PatientID;
        //                    patDetails.PatientID = PatientId;

        //                    obj.Edit(patDetails);
        //                }
        //                else
        //                {
        //                    Patient pat = new Patient();
        //                    string[] streetlocation = item.Location.Split(',');
        //                    string City = string.Empty;
        //                    string State = string.Empty;
        //                    string Zip = string.Empty;
        //                    foreach (var i in streetlocation)
        //                    {
        //                        if (i.Contains("Kirkland") || i.Contains("Lynnwood") || i.Contains("Tacoma"))
        //                        {
        //                            string[] location = i.Trim().Split(' ');
        //                            int strLength = location.Length;
        //                            if (strLength == 3)
        //                            {
        //                                City = location[0];
        //                                State = location[1];
        //                                Zip = location[2];
        //                            }
        //                        }
        //                    }

        //                    pat.BillingCity = City;
        //                    pat.BillingState = State;
        //                    int streetlocationLength = streetlocation.Length;
        //                    if (streetlocationLength > 1)
        //                    {
        //                        pat.BillingStreet = streetlocation[0].Trim() + " , " + streetlocation[1].Trim();
        //                    }
        //                    else
        //                    {
        //                        pat.BillingStreet = item.Location;
        //                    }
        //                    pat.BillingZip = Zip;
        //                    pat.FirstName = item.firstName;
        //                    pat.LastName = item.lastName;
        //                    pat.HomePhone = item.phone;
        //                    pat.Email = item.email;
        //                    pat.EmergencyContact = "";
        //                    pat.AllowApptReassign = false;
        //                    pat.Medical = false;
        //                    pat.Aesthetics = false;
        //                    pat.Autoship = false;
        //                    pat.Retail = false;
        //                    pat.Affiliate = false;
        //                    pat.DiabetesSOC = false;
        //                    pat.HeartSOC = false;
        //                    pat.AutoShipAlerts = "";
        //                    pat.AutoshipNote = "";
        //                    pat.MedicareOptOut_YN = false;
        //                    pat.EatingPlanReceived_YN = false;
        //                    pat.AutoshipEmail = false;
        //                    pat.AutoshipCancelReasonID = -1;
        //                    pat.AutoshipCancelOther = "";
        //                    pat.Birthday = DateTime.Now;

        //                    pat.Clinic = clinicName;
        //                    pat.Inactive = false;
        //                    pat.ConciergeID = staffId.ToString();
        //                    pat.Cancel_NoShow_frm_signed = false;
        //                    pat.HIPPA_signed = false;
        //                    pat.NameAlert = false;
        //                    pat.Home_detailed_info = false;
        //                    pat.Home_CB_only = false;
        //                    pat.Work_Detailed_info = false;
        //                    pat.Work_CB_only = false;
        //                    pat.Cell_CB_Only = false;
        //                    pat.Cell_Detailed_info = false;
        //                    pat.Email_auth_detailed_info = false;
        //                    pat.Fax_auth_detailed_info = false;


        //                    obj.Create(pat);

        //                    patDetails.PatientID = pat.PatientID;
        //                    PatientId = pat.PatientID;
        //                    obj.Edit(patDetails);
        //                    // HttpContext.Current.Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + PatientId.ToString(), false);
        //                }
        //            }
        //            List<apt_rec> oldApts = obj.GetAll<apt_rec>(o =>( o.patient_id == 7445 || o.patient_id == 22267 || o.patient_id == 22268 || o.patient_id == 22269) && o.ProviderID==providerId && o.ApptStart==startDateTime && o.ApptEnd==endDateTime ).ToList();
        //            if (oldApts.Count>0)
        //            {
        //                foreach (apt_rec a in oldApts)
        //                {
        //                    if (a.AppointmentTypeID == 154)
        //                    {
        //                        a.patient_id = PatientId;
        //                        obj.Edit(a);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                apt_rec appt = new apt_rec();
        //                appt.ApptEnd = endDateTime;
        //                appt.ApptStart = startDateTime;
        //                appt.patient_id = PatientId;
        //                appt.LabsCheckedIn = false;
        //                appt.AppointmentTypeID = 154;
        //                appt.ProviderID = providerId;
        //                appt.date_entered = DateTime.Now;
        //                appt.ActionNeeded="No";
        //                appt.Clinic = clinicName;
        //                appt.closed_yn = false;
        //                appt.SaleMade_yn = -1;
        //                appt.StatusID = 8;
        //                appt.AllDay = false;
        //                appt.EmailOnChange = false;
        //                appt.Results = 0;
        //                appt.Notes = "";
        //                appt.Email = "";
        //                obj.Create<apt_rec>(appt);
        //            }

        //            //FormailChimp campaign
        //            if (!string.IsNullOrEmpty(item.email))
        //            {



        //                string[] ItemData = Regex.Split(APIKey, "-");

        //                if (ItemData != null)
        //                {
        //                    if (ItemData.Length == 2)
        //                    {

        //                        if (!string.IsNullOrEmpty(mailChimpCampaignId))
        //                        {
        //                            string[] CampaignData = Regex.Split(mailChimpCampaignId, "~");

        //                            if (CampaignData != null)
        //                            {
        //                                if (CampaignData.Length == 2)
        //                                {
        //                                    var SaveData = AddOrUpdateListMember(ItemData[1], ItemData[0], CampaignData[1], item.email, item.firstName, item.lastName);

        //                                }
        //                            }
        //                        }


        //                    }
        //                }
        //            }

        //            //End for mailchimpcampaign

        //        }


        //    }
        //}


        private static string AddOrUpdateListMember(string dataCenter, string apiKey, string listId, string subscriberEmail, string FirstName, string LastName)
        {
            var sampleListMember = new JavaScriptSerializer().Serialize(
                new
                {
                    email_address = subscriberEmail,
                    merge_fields =
                    new
                    {
                        FNAME = FirstName,
                        LNAME = LastName
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

        public List<SharePointPatientViewModel> ListSharePointPatients()
        {
            return obj.ListSharePointPatients();
        }

        public SharePointPatientViewModel GetSharePointPatientsById(int Id)
        {
            return obj.GetSharePointPatientsById(Id);
        }

        public void SaveUpdateSharePointPatients(SharePointPatientViewModel PatientDetails)
        {
            obj.SaveUpdateSharePointPatients(PatientDetails);
        }

        public void DeleteSharePointPatient(int PatientId)
        {
            SharePointPatients SharePointPatient = new SharePointPatients();
            SharePointPatient = null;
            SharePointPatient = obj.Get<Emrdev.DataLayer.SharePointPatients>(o => o.Id == PatientId);
            if (SharePointPatient != null)
                obj.Delete(SharePointPatient);
        }


    }
}
