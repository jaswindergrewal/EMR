using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{

    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            PopulateTreeview();

        }

    }

    private void PopulateTreeview()
    {

        DataSet ds = new DataSet();
        DataTable dtparent = new DataTable();
        DataTable dtchild = new DataTable();
       
        DataTable s = new DataTable();
       
        dtparent = FillParentTable();
        dtparent.TableName = "A";

        dtchild = FillChildTable();
        dtchild.TableName = "B";
       
        ds.Tables.Add(dtparent);
        ds.Tables.Add(dtchild);

        DataTable dtGroupchild = new DataTable();
        dtGroupchild = FillChildTestTable();
       
        ds.Tables.Add(dtGroupchild);
       
        ds.Relations.Add("children", dtparent.Columns["PanelID"],

                                      dtchild.Columns["PanelID"]);

        ds.Relations.Add("childrenGroup", dtchild.Columns["GroupID"],

                                      dtGroupchild.Columns["GroupID"]);
       

        tvTables.Nodes.Clear();
        TreeNode masterNode = new TreeNode((string)("Panel Name"), "0");
        tvTables.Nodes.Add(masterNode);
        tvTables.CollapseAll();
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow masterRow in ds.Tables[0].Rows)
            {

                TreeNode masterPanelNode = new TreeNode((string)masterRow["PanelName"], Convert.ToString(masterRow["PanelID"]));

                masterNode.ChildNodes.Add(masterPanelNode);
                masterNode.Value = Convert.ToString(masterRow["PanelID"]);
                masterNode.CollapseAll();

                TreeNode masterNode1 = new TreeNode((string)("Group Name"), "0");
                masterPanelNode.ChildNodes.Add(masterNode1);
                masterPanelNode.CollapseAll();

                foreach (DataRow childRow in masterRow.GetChildRows("Children"))
                {
                    TreeNode childNode = new TreeNode((string)childRow["GroupName"], Convert.ToString(childRow["PanelID"]));
                    masterNode1.ChildNodes.Add(childNode);
                    childNode.Value = Convert.ToString(childRow["GroupID"]);
                  
                   
                        foreach (DataRow childRow1 in childRow.GetChildRows("childrenGroup"))
                        {
                            TreeNode childGroupNode = new TreeNode((string)childRow1["TestName"], Convert.ToString(childRow1["GroupID"]));
                            childNode.ChildNodes.Add(childGroupNode);
                            childGroupNode.Value = Convert.ToString(childRow1["TestID"]);
                        }

                    }

                }
            }
        }
    


    private DataTable FillParentTable()
    {

        DataTable dt = new DataTable();

        conn.Open();

        string cmdstr = "select * from LabReports_Panels";

        SqlCommand cmd = new SqlCommand(cmdstr, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(dt);

        conn.Close();

        return dt;

    }

    private DataTable FillChildTable()
    {

        DataTable dt = new DataTable();

        conn.Open();

        string cmdstr = "select * from LabReports_Groups";

        SqlCommand cmd = new SqlCommand(cmdstr, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(dt);

        conn.Close();

        return dt;

    }

    private DataTable FillChildTestTable()
    {

        DataTable dt = new DataTable();

        conn.Open();

        string cmdstr = "select * from LabReports_tests where GroupID<>0 ";

        SqlCommand cmd = new SqlCommand(cmdstr, conn);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(dt);

        conn.Close();

        return dt;

    }

}
