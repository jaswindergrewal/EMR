using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Text;


public partial class DictationConsole_Subjective : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack && !IsCallback & Request.QueryString["ref"] == null)
		{
			Response.Redirect("Subjective.aspx?ref=yes");
		}


		List<Persist> ps = new List<Persist>();

		ps.Add(new Persist { SymptomID = 1, Symptom = "Hypertension", RowPosition = 1, dir = "images/ArrowUp.gif" });
		ps.Add(new Persist { SymptomID = 2, Symptom = "Dry Hair", RowPosition = 2, dir = "images/ArrowDown.gif" });
		ps.Add(new Persist { SymptomID = 3, Symptom = "Acne", RowPosition = 3, dir = "images/ArrowSide.gif" });
		ps.Add(new Persist { SymptomID = 4, Symptom = "Coarse Skin", RowPosition = 4, dir = "images/ArrowUp.gif" });
		grdPersist.DataSource = ps;
		grdPersist.DataBind();

		List<Persist> ns = new List<Persist>();

		ns.Add(new Persist { SymptomID = 1, Symptom = "Losing Hair", RowPosition = 1, dir = "images/ArrowUp.gif" });
		ns.Add(new Persist { SymptomID = 2, Symptom = "Fluid Retention", RowPosition = 2, dir = "images/ArrowDown.gif" });
		ns.Add(new Persist { SymptomID = 3, Symptom = "Low Body Temp", RowPosition = 3, dir = "images/ArrowSide.gif" });
		ns.Add(new Persist { SymptomID = 1, Symptom = "Losing Hair", RowPosition = 4, dir = "images/ArrowUp.gif" });
		ns.Add(new Persist { SymptomID = 2, Symptom = "Fluid Retention", RowPosition = 5, dir = "images/ArrowDown.gif" });
		ns.Add(new Persist { SymptomID = 3, Symptom = "Low Body Temp", RowPosition = 6, dir = "images/ArrowSide.gif" });
		ns.Add(new Persist { SymptomID = 1, Symptom = "Losing Hair", RowPosition = 7, dir = "images/ArrowUp.gif" });
		ns.Add(new Persist { SymptomID = 2, Symptom = "Fluid Retention", RowPosition = 8, dir = "images/ArrowDown.gif" });
		ns.Add(new Persist { SymptomID = 3, Symptom = "Low Body Temp", RowPosition = 9, dir = "images/ArrowSide.gif" });
		ns.Add(new Persist { SymptomID = 1, Symptom = "Losing Hair", RowPosition = 10, dir = "images/ArrowUp.gif" });
		ns.Add(new Persist { SymptomID = 2, Symptom = "Fluid Retention", RowPosition = 11, dir = "images/ArrowDown.gif" });
		ns.Add(new Persist { SymptomID = 3, Symptom = "Low Body Temp", RowPosition = 12, dir = "images/ArrowSide.gif" });
		grdNew.DataSource = ns;
		grdNew.DataBind();

		List<Persist> nw = new List<Persist>();

		nw.Add(new Persist { SymptomID = 1, Symptom = "Migraines", RowPosition = 1, dir = "images/ArrowUp.gif" });
		nw.Add(new Persist { SymptomID = 2, Symptom = "Loose Stools", RowPosition = 2, dir = "images/ArrowDown.gif" });
		nw.Add(new Persist { SymptomID = 3, Symptom = "Edgy", RowPosition = 3, dir = "images/ArrowSide.gif" });
		nw.Add(new Persist { SymptomID = 4, Symptom = "Trouble Sleeping", RowPosition = 4, dir = "images/ArrowDown.gif" });
		nw.Add(new Persist { SymptomID = 5, Symptom = "Loss of Meantal Clarity", RowPosition = 5, dir = "images/ArrowUp.gif" });
		grdResolved.DataSource = nw;
		grdResolved.DataBind();

		grdGoals.DataSource = nw;
		grdGoals.DataBind();

	}
	[WebMethod]
	public static string SaveRowsPosition(string data)	
	{
		string[] gridsData = data.Split('|');
		UpdateRowPositions(gridsData[0], "Products");
		UpdateRowPositions(gridsData[1], "PurchasedProducts");
		return "true";
	}



	protected static void UpdateRowPositions(string data, string tableName)
	{
		//Dictionary<int, int> rowPositions = new Dictionary<int, int>();
		//string[] items = data.Split(',');

		//foreach (string item in items)
		//{
		//    if (!string.IsNullOrEmpty(item))
		//    {
		//        string[] itemData = item.Split('*');
		//        rowPositions.Add(int.Parse(itemData[0]), int.Parse(itemData[1]));
		//    }
		//}

		//DataTable dt = HttpContext.Current.Session["AllProducts"] as DataTable;

		//DataTable newDataTable = new DataTable();
		//newDataTable.Columns.Add("ProductID");
		//newDataTable.Columns.Add("RowPosition", typeof(int));
		//newDataTable.Columns.Add("ProductName");
		//newDataTable.Columns.Add("SKU");
		//newDataTable.Columns.Add("Price");

		//newDataTable.DefaultView.Sort = "RowPosition ASC";

		//foreach (DataRow dr in dt.Rows)
		//{
		//    int productId = int.Parse(dr["ProductID"].ToString());
		//    if (rowPositions.ContainsKey(productId))
		//    {
		//        DataRow row = newDataTable.NewRow();
		//        row["ProductID"] = dr["ProductID"];

		//        int rowPosition = rowPositions[productId];
		//        row["RowPosition"] = rowPosition;

		//        row["ProductName"] = dr["ProductName"];
		//        row["SKU"] = dr["SKU"];
		//        row["Price"] = dr["Price"];

		//        newDataTable.Rows.Add(row);
		//    }
		//}

		//HttpContext.Current.Session[tableName] = newDataTable;
	}


}
