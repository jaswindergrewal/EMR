using System;
using System.Collections;
using System.Data;
using System.Xml.Linq;
using System.Linq;
using Emrdev.ServiceLayer;

namespace Quest
{

    public partial class LabTable : System.Web.UI.Page
    {
        // The patient ID has to be in the query string, or we can't do anything
        private int patientID = -1;
        ILabsService objService = null;

        //the location of the xml file.  Needs to have the @ in front because \ is a special character to C#
        private string PathToGroupsXML = string.Empty;//@"C:\inetpub\emr_test\NewEMR\LabChart\LabGroups.xml";

        /// <summary>
        /// to show the lab test data on the basis of the LabGroup XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["patientID"] != null)
                {
                    try
                    {
                        this.patientID = Convert.ToInt32(this.Request.QueryString["patientID"]);

                        PathToGroupsXML = Server.MapPath("~/LabChart/LabGroups.xml"); // xml file path.
                        XElement groups = XElement.Load(PathToGroupsXML);
                        SortedList labResults = new SortedList();
                        SortedList datesUsed = new SortedList();

                        this.patientID = Convert.ToInt32(this.Request.QueryString["patientID"]);
                        objService = new LabsService();
                        DataTable labData = objService.GetLabOldChartDetails(patientID);
                        //Added by jaswinder to show patientName
                        if (labData.Rows.Count > 0)
                        {
                            lblPatnames.Text = labData.Rows[0]["PatientName"].ToString();
                        }

                        labData.Columns.Remove("PatientName");
                        labData.Columns.Add("SortOrder", System.Type.GetType("System.Int32"));

                        //Get  the sororder from the XML and names for the test
                        for (int i = 0; i < labData.Rows.Count; i++)
                        {

                            var thisName = from n in groups.Descendants("TestName")
                                           where n.Value.ToLower().Trim() == labData.Rows[i]["TestName"].ToString().ToLower().Trim()
                                           select new
                                           {
                                               name = n.Parent.Attribute("name").Value,
                                           };
                            if (thisName.Count() > 0)
                            {
                                labData.Rows[i]["TestName"] = thisName.First().name;

                            }
                            XElement theGroup = (from g in groups.Descendants("group")
                                                 where g.Attribute("name").Value.ToLower().Trim() == labData.Rows[i]["TestName"].ToString().ToLower().Trim()
                                                 select g).FirstOrDefault();
                            if (theGroup != null)
                            {
                                labData.Rows[i]["SortOrder"] = theGroup.Attribute("SortOrder").Value;
                            }
                            else
                            {
                                labData.Rows[i].Delete();
                            }

                        }
                        labData.DefaultView.Sort = "SortOrder";
                        labData.Columns.Remove("SortOrder");

                        //code by jaswinder 
                        //for showing the same goup data in one row 25th april 2014

                        labData.AcceptChanges();

                        var distinctRows = labData.DefaultView.ToTable(true, "TestName").Rows.OfType<DataRow>().Select(k => k[0] + "").ToArray();
                        DataTable dtRsult = labData.Clone();

                        foreach (string strPimary in distinctRows)
                        {
                            DataRow clonedRow = dtRsult.NewRow();
                            clonedRow[0] = strPimary;

                            DataRow[] _dataRow = labData.DefaultView.ToTable().Select("TestName='" + strPimary + "'");

                            foreach (DataRow _dr in _dataRow)
                            {
                                for (int i = 1; i < labData.Columns.Count; i++)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(_dr[i]).Trim()))
                                        clonedRow[i] = _dr[i];
                                }
                            }
                            dtRsult.Rows.Add(clonedRow);
                        }

                        //End

                        this.DataGridLabResults.DataSource = dtRsult;
                        this.DataGridLabResults.DataBind();
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
                else
                {
                    Response.Redirect("~/landingpage.aspx");
                }
            }

        }
    }
}
