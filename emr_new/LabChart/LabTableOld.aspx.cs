using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

namespace Quest
{
    public partial class LabTable : LMCBase
    {
        // The patient ID has to be in the query string, or we can't do anything
        private int patientID = -1;
        ILabsService objService = null;

        /// <summary>
        /// Get the details of lab test details
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
                        objService = new LabsService();
                        DataTable labData = objService.GetLabOldChartDetails(patientID);
                        //Added by jaswinder to show patientName
                        if (labData.Rows.Count > 0)
                        {
                            lblPatnames.Text = labData.Rows[0]["PatientName"].ToString();
                        }

                        labData.Columns.Remove("PatientName");
                        this.DataGridLabResults.DataSource = labData;
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

		
					
				

