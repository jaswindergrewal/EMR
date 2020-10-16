using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using NineRays.WebControls;
using System.Text;
using System.Data.SqlClient;

public partial class Managertest : System.Web.UI.Page
{
    //EMRDataContext ctx = new EMRDataContext(ConfigurationManage.ConnectionStrings["db"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            DataSet reportTable = new DataSet();
            SqlCommand cmd = new SqlCommand("select * from LabReports_Panels", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(reportTable);
            GridView1.DataSource = reportTable.Tables[0];
            GridView1.DataBind();
            
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label panelid = (Label)e.Row.FindControl("lblID");
            string id = panelid.Text;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                DataSet reportTable = new DataSet();
                SqlCommand cmd = new SqlCommand("select * from LabReports_Groups where PanelID=" + id, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(reportTable);
                GridView gv1 = (GridView)e.Row.FindControl("GridView2");
                gv1.DataSource = reportTable.Tables[0];
                gv1.DataBind();

            }

            //GridView gv1 = (GridView)e.Row.FindControl("GridView2");
            //DataSet ds1 = BOL.GetPrintSize();
            //ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton1");
            //gv1.DataSource = ds1;
            //gv1.DataBind();
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label GroupID = (Label)e.Row.FindControl("lblGroupID");
            string id = GroupID.Text;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                DataSet reportTable = new DataSet();
                SqlCommand cmd = new SqlCommand("select distinct TestName from LabReports_Tests where GroupID=" + id, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(reportTable);
                GridView gv1 = (GridView)e.Row.FindControl("GridView3");
                gv1.DataSource = reportTable.Tables[0];
                gv1.DataBind();

            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var viewRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        var btnSelect = (GridView)viewRow.FindControl("GridView3");
        var btnlink = (LinkButton)viewRow.FindControl("ImageButton1");
        var btnlink1 = (LinkButton)viewRow.FindControl("LinkButton3");
        if (e.CommandName == "SelectGrid")
        {
           

            
               
            btnSelect.Visible = true;
            btnlink.Visible = false;
            btnlink1.Visible = true;
    
           


        }
        else if (e.CommandName == "CloseGrid")
        {
            btnSelect.Visible = false;
            btnlink.Visible = true;
            btnlink1.Visible = false;
        }
    }
}