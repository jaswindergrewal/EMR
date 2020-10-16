using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Text;
public partial class lab_report_short : System.Web.UI.Page
{
    #region Variables
    public string message_id = "";
    public string patientid = "";
    ILabReportService objService = null;
    string LabCodeID = string.Empty;
    string LabCodes = string.Empty;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request["message_id"] != null && Request["message_id"].ToString() != string.Empty)
                {
                    message_id = Request["message_id"].ToString();
                    if (Request["patientid"] != null && Request["patientid"].ToString() != string.Empty)
                    {
                        patientid = Request["patientid"].ToString();
                    }
                    BindLabReportShortDetails();

                }
            }

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    private void BindLabReportShortDetails()
    {
        try
        {
            objService = new LabReportService();
            List<LabReportShortViewModel> objLabReportShortDetails = objService.GetLabReportShortDetails(Convert.ToInt32(message_id));
            if (objLabReportShortDetails != null)
            {

                lblClient_id.Text = objLabReportShortDetails[0].ClientID;
                lblCollectedDt.Text = objLabReportShortDetails[0].CollectedDateTime.ToString();
                lblLabId.Text = objLabReportShortDetails[0].LabID.ToString();
                lblOrderingDr.Text = objLabReportShortDetails[0].ProviderLastName + ", " + objLabReportShortDetails[0].ProviderFirstName + " " + objLabReportShortDetails[0].ProviderMiddleName;
                TimeSpan tt = DateTime.Now - objLabReportShortDetails[0].DOB;

                lblPatientAge.Text = objLabReportShortDetails[0].TotalAge.ToString();
                lblPatientDob.Text = objLabReportShortDetails[0].DOB.ToShortDateString();
                if (objLabReportShortDetails[0].MiddleName != "")
                {
                    lblPatientName.Text = objLabReportShortDetails[0].LastName + ", " + objLabReportShortDetails[0].FirstName + " " + objLabReportShortDetails[0].MiddleName;
                }
                else
                {
                    lblPatientName.Text = objLabReportShortDetails[0].LastName + ", " + objLabReportShortDetails[0].FirstName;
                }
                lblPatientSex.Text = objLabReportShortDetails[0].Sex;
                lblPatientSsn.Text = objLabReportShortDetails[0].SSN.ToString();
                lblReceivedDt.Text = objLabReportShortDetails[0].ReceivedDateTime.ToString();
                string report_status = "";
                switch (objLabReportShortDetails[0].Result)
                {
                    case "F":
                        report_status = "Final";
                        break;
                    case "P":
                        report_status = "Preliminary";
                        break;
                    case "C":
                        report_status = "Correction";
                        break;
                    case "X":
                        report_status = "Cancelled";
                        break;
                }
                lblReport_status.Text = report_status;
                lblReportedDt.Text = objLabReportShortDetails[0].ReportedDateTime.ToString();
                lblRequisitionId.Text = objLabReportShortDetails[0].RequisitionID.Replace("^", " ").ToString();
                //Added on 9th jan 2013 to replace the ^ tag
                lblSpecimenId.Text = objLabReportShortDetails[0].SpecimenID.Replace("^", " ").ToString();
                lblTestName.Text = objLabReportShortDetails[0].ServiceID;
                lblLabId.Text = objLabReportShortDetails[0].LabID.ToString();
                List<LabNotesAndCommentsSegmentsDetailsViewModel> lstLabNotesAndCommentsSegmentsPIDViewModel = objService.labNotesAndCommentsSegmentsNTEPID(Convert.ToInt32(message_id));
                StringBuilder strComment = new StringBuilder();
                foreach (var CommentItem in lstLabNotesAndCommentsSegmentsPIDViewModel)
                {
                    strComment.Append(CommentItem.Comment + "<br>");

                }
                LitPIDComment.Text = strComment.ToString();

                StringBuilder str = new StringBuilder();
                StringBuilder strInner = new StringBuilder();
                int ordersementID = 0;
                foreach (var obrItem in objLabReportShortDetails)
                {
                    objService = new LabReportService();
                    bool abnormal = false;
                    ordersementID = obrItem.OrderSegmentID;

                    List<LabObservationResultDetailSegmentsViewModel> lstLabObservationResultDetailSegmentsViewModel = objService.GetlabObservationResultDetailSegments(obrItem.OrderSegmentID);
                    if (lstLabObservationResultDetailSegmentsViewModel != null)
                    {
                        foreach (var item in lstLabObservationResultDetailSegmentsViewModel)
                        {
                            if (item.AbnormalFlag != "N" && item.AbnormalFlag != "" && item.AbnormalFlag != null)
                            {
                                abnormal = true;
                            }
                            else
                            {
                                abnormal = false;
                            }

                            string lab_result_detail_name = "";
                            lab_result_detail_name = item.ResultID.ToString();

                            string lab_result_drilldown_url = "";
                            lab_result_drilldown_url = "lab_report_drilldown.aspx?patientID=" + patientid.ToString() + "&labName=" + Server.UrlEncode(lab_result_detail_name);
                            lab_result_drilldown_url = "javascript:window.open('" + lab_result_drilldown_url + "', 'drilldown', 'status=0,toolbar=0,menubar=0,scrollbars,width=600,height=400'); void(0)";

                            if (abnormal)
                            {
                                str.Append("<tr style=\"border: 1px solid #C0C0C0; padding: 0; background-color: #C0C0C0\">");
                                str.Append("<td width=\"37%\"><b>&nbsp;&nbsp;<a href=\"" + lab_result_drilldown_url + "\">" + lab_result_detail_name + "</a></b></td>");
                                str.Append("<td width=\"10%\"></td>");
                                str.Append("<td width=\"20%\"><b><span style=\"color:red\">" + item.Value + "</span></b> " + item.AbnormalFlag + "</td>");
                            }
                            else
                            {
                                str.Append("<tr style=\"border: 1px solid #C0C0C0; padding: 0; background-color: #C0C0C0\">");
                                str.Append("<td width=\"37%\">&nbsp;&nbsp;<a href=\"" + lab_result_drilldown_url + "\">" + lab_result_detail_name + "</a></td>");
                                str.Append("<td width=\"10%\">" + item.Value + "</td>");
                                str.Append("<td width=\"20%\"></td>");

                            }
                            str.Append("<td width=\"23%\">" + item.ReferenceRange.ToString() + " " + item.Units.ToString() + "</td>");
                            str.Append("<td width=\"10%\">" + item.SetID + "</td>");
                            str.Append("</tr>");
                            objService = new LabReportService();

                            List<LabNotesAndCommentsSegmentsDetailsViewModel> lstLabNotesAndCommentsSegmentsDetailsViewModel = objService.labNotesAndCommentsSegmentsDetails(item.TableRowID.ToString());


                            if (lstLabNotesAndCommentsSegmentsDetailsViewModel != null && lstLabNotesAndCommentsSegmentsDetailsViewModel.Count > 0)
                            {
                                str.Append("<tr style=\"border: 1px solid #C0C0C0; padding: 0; background-color: #C0C0C0\">");
                                str.Append("<td width=\"37%\">");
                                foreach (var itemNew in lstLabNotesAndCommentsSegmentsDetailsViewModel)
                                {

                                    if (itemNew.Comment != "")
                                    {
                                        str.Append("<pre style='height: 9px;'>" + itemNew.Comment.ToString() + "</pre>\n");

                                    }
                                }
                                str.Append("</td>");
                                str.Append("<td ></td>");
                                str.Append("<td ></td>");
                                str.Append("<td ></td>");
                                str.Append("<td ></td></tr>");
                            }

                            if (item.SetID != "")
                            {
                                if (item.SetID != LabCodeID)
                                {
                                    LabCodeID = item.SetID;

                                    if (item.LabDetails != null)
                                    {
                                        litInner.Text = litInner.Text + "<br><br>" + item.LabDetails;
                                    }
                                }
                            }



                        }
                    }


                    litTestDetails.Text = str.ToString();

                }
                if (litInner.Text == "")
                {

                    List<LabObservationResultDetailSegmentsViewModel> lstGetLabAddress = objService.GetLabAddress(ordersementID, LabCodes);
                    foreach (var labDetails in lstGetLabAddress)
                    {
                        litInner.Text = litInner.Text + "<br><br>" + labDetails.LabDetails;
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}