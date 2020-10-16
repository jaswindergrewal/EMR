using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using System.Net;
using System.IO;
using System.Xml;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class SharepointPrescriptionDetailBAL
    {
        SharepointPrescriptionDetailDAL objSharepointPrescriptionDetailDAL = new SharepointPrescriptionDetailDAL();
        //public void InsertSharepointPrescriptionDetail(SharepointPrescriptionDetailAddEditViewModel viewModelSharepointPrescriptionDetail)
        //{
        //    SharePointPresciptionDetail cls = new SharePointPresciptionDetail();
        //    AutoMapper.Mapper.CreateMap<SharepointPrescriptionDetailAddEditViewModel, SharePointPresciptionDetail>();
        //    cls = AutoMapper.Mapper.Map(viewModelSharepointPrescriptionDetail, cls);
        //    objSharepointPrescriptionDetailDAL.Create(cls);
        //}
        //public void UpdateSharepointPrescriptionDetail(SharepointPrescriptionDetailAddEditViewModel EditviewModelSharepointPrescriptionDetail)
        //{
        //    SharePointPresciptionDetail cls = new SharePointPresciptionDetail();
        //    AutoMapper.Mapper.CreateMap<SharepointPrescriptionDetailAddEditViewModel, SharePointPresciptionDetail>();
        //    cls = AutoMapper.Mapper.Map(EditviewModelSharepointPrescriptionDetail, cls);
        //    objSharepointPrescriptionDetailDAL.Edit(cls);
        //}
        public List<SharepointPrescriptionDetailViewModel> GetAllSharepointPrescriptionDetailList(int ID)
        {
            return objSharepointPrescriptionDetailDAL.GetAllSharepointPrescriptionDetailList(ID);
        }
        public List<SharepointPrescriptionDetailViewModel> GetAllSharepointPrescriptionDetailReport(string PatientName, string Clinic, string Physician, DateTime? LastRefill, DateTime? MedStartDate, bool IsDiet, bool IsMedical)
        {
            return objSharepointPrescriptionDetailDAL.GetAllSharepointPrescriptionDetailReport(PatientName,  Clinic,  Physician,  LastRefill,  MedStartDate,  IsDiet,  IsMedical);
        }
        public void InsertUpdateSharePointPrescriptionDetail(int PresciptionId, string PatientName, string Clinic, string Vials, DateTime? LastRefill, DateTime? MedStartDate, string Physician, string Comments, string Diet, string Medical)
        {
            objSharepointPrescriptionDetailDAL.InsertUpdateSharePointPrescriptionDetail(PresciptionId, PatientName, Clinic, Vials, LastRefill, MedStartDate, Physician, Comments, Diet, Medical);
        }
        public void AddUpdateSharePointPrescriptionDetail(string rssURL)
        {
            string PatientName = string.Empty;
            string link = string.Empty;
            string RestPatientItems = string.Empty;
            string RecoredID = string.Empty;
            string ColumnName = string.Empty;
            string value = string.Empty;
            WebRequest myRequest = WebRequest.Create(rssURL);
            myRequest.UseDefaultCredentials = true;
            myRequest.PreAuthenticate = true;
            myRequest.Credentials = CredentialCache.DefaultCredentials;
            WebResponse myResponse = myRequest.GetResponse();
            Stream rssStream = myResponse.GetResponseStream();
            // Load a XML Document
            XmlDocument rssDoc = new XmlDocument();
            rssDoc.Load(rssStream);
            XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
            for (int i = 0; i < rssItems.Count; i++)
            {
                SharepointPrescriptionDetailViewModel ObjSharepointPrescriptionDetailViewModel = new SharepointPrescriptionDetailViewModel();
                XmlNode rssDetail;
                //Get Patient Name
                rssDetail = rssItems.Item(i).SelectSingleNode("title");

                if (rssDetail != null)
                {
                    ObjSharepointPrescriptionDetailViewModel.PatientName = rssDetail.InnerText;
                }
                else
                {
                    ObjSharepointPrescriptionDetailViewModel.PatientName = "";
                }

                // Get ID
                rssDetail = rssItems.Item(i).SelectSingleNode("link");
                if (rssDetail != null)
                {
                    link = rssDetail.InnerText;
                    if ((link.ToString().IndexOf("?ID") > 0))
                    {
                        var URL_Parts = link.Split('?');
                        var Url_Parts1 = URL_Parts[1].Split('/');
                        RecoredID = Url_Parts1[0].ToString();
                        ObjSharepointPrescriptionDetailViewModel.ID = Convert.ToInt32(RecoredID.Remove(0, 3));
                    }
                    else
                    {
                        ObjSharepointPrescriptionDetailViewModel.ID = 0;
                    }
                }
                // GET REST ITEMS OF PATIENTS LIST:   Vials, Last Refill, MedStartDate, Physician, Comments, Diet, Medical
                rssDetail = rssItems.Item(i).SelectSingleNode("description");
                if (rssDetail != null)
                {
                    string Pattern = "<div><b>";
                    RestPatientItems = rssDetail.InnerText.Replace(Pattern, "");
                    RestPatientItems = RestPatientItems.Replace("</div>", ",");
                    RestPatientItems = RestPatientItems.Replace("</b>", "");
                    RestPatientItems = RestPatientItems.Replace("\n", "");

                    string[] splitDescArray;
                    splitDescArray = RestPatientItems.Split(',');

                    SharepointPrescriptionDetailBAL ObjSharepointPrescriptionDetailBAL = new SharepointPrescriptionDetailBAL();

                    for (int j = 0; j < splitDescArray.Length; j++)
                    {
                        string strExtract = splitDescArray[j];
                        string[] strHeadingWithItem = strExtract.Split(':');
                        ColumnName = strHeadingWithItem[0];
                        if (ColumnName != "")
                        {
                            value = strHeadingWithItem[1].Trim();
                        }
                        switch (ColumnName.ToUpper())
                        {
                            case "CLINIC":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.Clinic = value;
                                    break;
                                }
                            case "# OF VIALS":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.Vials = value;
                                    break;
                                }
                            case "LAST REFILL":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.LastRefillDateTime = Convert.ToDateTime(value);
                                    break;
                                }
                            case "MEDSTART DATE":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.MedStartDateTime = Convert.ToDateTime(value);
                                    break;
                                }
                            case "PHYSICIAN":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.Physician = value;
                                    break;
                                }
                            case "DIET":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.Diet = Convert.ToBoolean(value);
                                    break;
                                }
                            case "MEDICAL":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.Medical = Convert.ToBoolean(value);
                                    break;
                                }
                            case "COMMENTS":
                                {
                                    ObjSharepointPrescriptionDetailViewModel.Comments = value;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    ObjSharepointPrescriptionDetailBAL.InsertUpdateSharePointPrescriptionDetail(ObjSharepointPrescriptionDetailViewModel.ID, ObjSharepointPrescriptionDetailViewModel.PatientName, ObjSharepointPrescriptionDetailViewModel.Clinic, ObjSharepointPrescriptionDetailViewModel.Vials, ObjSharepointPrescriptionDetailViewModel.MedStartDateTime, ObjSharepointPrescriptionDetailViewModel.MedStartDateTime, ObjSharepointPrescriptionDetailViewModel.Physician, ObjSharepointPrescriptionDetailViewModel.Comments,Convert.ToString(ObjSharepointPrescriptionDetailViewModel.Diet),Convert.ToString(ObjSharepointPrescriptionDetailViewModel.Medical));
                }
                else
                {
                    RestPatientItems = "";
                }
            }
        }
    }
}
