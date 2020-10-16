using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Configuration;

public partial class _ShortManager : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Server.MachineName == "RICKN-PC")
		{
			Session["MM_Username"] = "rick.nolte";
			Session["UserID"] = "132";
		}
		if (Application["QODBC"] == null)
		{
			//OdbcConnection conn = new OdbcConnection(@"Driver={QODBC Driver for QuickBooks};DFQ=\\quickbooks\QB_LMC\LMC_v9.QBW;OpenMode=M;OLE DB Services=-2;OptimizerOn=Yes;");

			//conn.Open();
			//Application["QODBC"] = conn;
		}

		if (!IsPostBack)
		{
			((TextBox)GenerateOrders.FindControl("txtDate")).Text = DateTime.Today.ToShortDateString(); ;

			User usr = new User((string)Request.QueryString["StaffID"]);
			if (usr.AutoshipAccess == "none") Response.Redirect("../LandingPage.aspx");
			txtBegin.Text = DateTime.Today.ToString("d");
			txtEnd.Text = DateTime.Today.AddMonths(1).ToString("d");

		}
	}



	protected void CheckPatientVal(object sender, ServerValidateEventArgs e)
	{
		Calendar.Patient pat = Calendar.Patients.CheckPatient(e.Value);
		if (pat.LastName != null)
		{
			e.IsValid = true;
			Session["Valid"] = "true";
		}
		else
		{
			e.IsValid = false;
			Session["Valid"] = "false";
		}
	}

	public string GetContactMessage(decimal ProfileItemID, string msgType)
	{
		string message = "";
		switch (msgType)
		{
			case "addItem":
				message = "AS Product Name added. ";
				break;
			case "editItem":
				message = "AS Product Name edited. ";
				break;
			case "editExc":
				message = "AS Exception edited. ";
				break;
			case "addExc":
				message = "AS Exception added. ";
				break;
			case "deleteItem":
				message = "AS Item deleted. ";
				break;
			case "deleteExc":
				message = "AS Exception deleted. ";
				break;
		}
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("ProfileItem_GetByID", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ProfileItemID", ProfileItemID);
			SqlDataReader reader = AutoshipUtilities.OpenReader(cmd);//cmd.ExecuteReader();
			reader.Read();
			message += "Product: " + (string)reader["ProductName"] + "\r\n";
			message += "Quantity: " + ((int)reader["Quantity"]).ToString() + "\r\nFrequency: " + ((string)reader["Frequency"]).ToString().Replace("-", "") + "\r\nStart Date: " + ((DateTime)reader["StartDate"]).ToString("d");
			if (reader["EndDate"] != DBNull.Value)
				message += "\r\nEnd Date: " + ((DateTime)reader["EndDate"]).ToString("d");
			else
				message += "\r\nEnd Date: Indefinite";
			message += "\r\nEntered by: " + (string)Session["MM_Username"];

		}

		return message;

	}


	protected void btnAddProduct_Click(object sender, EventArgs e)
	{
		string sql = "INSERT INTO AutoshipProducts (PRoductName,AutoshipPrice) VALUES ('" + txtProductName.Text + "'," + txtAutoShipPrice.Text + ")";
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd1 = new SqlCommand(sql, conn);
			cmd1.CommandType = CommandType.Text;
			cmd1.ExecuteNonQuery();
		}
		ProductsGrid.DataBind();
		txtProductName.Text = "";
		txtAutoShipPrice.Text = "";
	}

	protected void ProductsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("AutoshipProducts_Update", conn);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@ProductName", e.NewValues["ProductName"]);
			cmd.Parameters.AddWithValue("@AutoshipPrice", double.Parse(e.NewValues["AutoshipPrice"].ToString().Trim()));
			cmd.Parameters.AddWithValue("@Active", e.NewValues["Active"]);
			cmd.Parameters.AddWithValue("@ProductID", ProductsGrid.Rows[ProductsGrid.EditIndex].Cells[1].Text);
            cmd.Parameters.AddWithValue("@Viewable", true);
            cmd.Parameters.AddWithValue("@Reviewed", true);
			cmd.ExecuteNonQuery();
		}
		ProductsGrid.EditIndex = -1;

		e.Cancel = true;
	}

	protected void btnPreviewOrders_Click(object sender, EventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			DateTime StartDate = DateTime.Parse(txtDate.Text);
			//get all items with a start date before date entered
			DataTable Candidates = AutoshipReportsUtil.GetCandidates(StartDate, conn);
			//remove candidates that have a freqeuency that rules them out
			bool[] RowsToRemove = new bool[Candidates.Rows.Count];
			for (int x = 0; x < Candidates.Rows.Count; x++)
			{
				DataRow dr = Candidates.Rows[x];


				if ((DateTime)dr["NextShipDate"] > StartDate)
				{
					RowsToRemove[x] = true;
				}
				else
				{
					RowsToRemove[x] = false;
				}

			}
			//remove the ones that were marked
			for (int x = 0; x < RowsToRemove.Count(); x++)
			{
				if (RowsToRemove[x])
					Candidates.Rows[x].Delete();
			}

			Session["Batch"] = AutoshipReportsUtil.GenOrders(Candidates, true, conn, StartDate, int.Parse(Session["UserID"].ToString()));
			//Display the batch for approval
			if (OrderPreviewSource.SelectParameters.Count == 0)
				OrderPreviewSource.SelectParameters.Add(new Parameter("Batch", DbType.Int32, Session["Batch"].ToString()));
			else
				OrderPreviewSource.SelectParameters[0].DefaultValue = Session["Batch"].ToString();
			OrderViewGrid.DataBind();
			OrderPreviewTable.Visible = true;

		}
	}
	protected void btnGenOrders_OnClick(object sender, EventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			DateTime StartDate = DateTime.Parse(txtDate.Text);
			//get all items with a start date before date entered
			DataTable Candidates = AutoshipReportsUtil.GetCandidates(StartDate, conn);
			//remove candidates that have a freqeuency that rules them out
			bool[] RowsToRemove = new bool[Candidates.Rows.Count];
			for (int x = 0; x < Candidates.Rows.Count; x++)
			{
				DataRow dr = Candidates.Rows[x];


				if ((DateTime)dr["NextShipDate"] > StartDate)
				{
					RowsToRemove[x] = true;
				}
				else
				{
					RowsToRemove[x] = false;
				}

			}
			//remove the ones that were marked
			for (int x = 0; x < RowsToRemove.Count(); x++)
			{
				if (RowsToRemove[x])
					Candidates.Rows[x].Delete();
			}

			Session["Batch"] = AutoshipReportsUtil.GenOrders(Candidates, false, conn, StartDate, int.Parse(Session["UserID"].ToString()));
			//Display the batch for approval
			if (OrderPreviewSource.SelectParameters.Count == 0)
				OrderPreviewSource.SelectParameters.Add(new Parameter("Batch", DbType.Int32, Session["Batch"].ToString()));
			else
				OrderPreviewSource.SelectParameters[0].DefaultValue = Session["Batch"].ToString();
			OrderViewGrid.DataBind();
			OrderPreviewTable.Visible = true;

		}
	}
	protected void OrderViewGrid_SelectedIndexChanged(object sender, EventArgs e)
	{
		OrderItemPreviewSource.SelectParameters["OrderID"].DefaultValue = ((int)OrderViewGrid.SelectedDataKey.Values["OrderID"]).ToString();
		OrderItemsPreviewGrid.DataBind();
	}

	protected void btnPrint_Click(object sender, EventArgs e)
	{
		//Response.Redirect("http://lmcsql/Reports/Pages/Report.aspx?ItemPath=%2fAutoship%2fDaily+Shipments");
        Response.Redirect("http://50.23.221.50/Reports/Pages/Report.aspx?ItemPath=%2fAutoship%2fDaily+Shipments"); // this is for staging server path
		//iTextSharp.text.Document doc = new iTextSharp.text.Document();
		//string DateString = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("mm");
		//iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new System.IO.FileStream(Request.PhysicalApplicationPath + "\\Output_files\\Invoices" + DateString + ".pdf", System.IO.FileMode.Create));
		//doc.Open();
		//iTextSharp.text.Table tble = new iTextSharp.text.Table(10);


		//using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		//{
		//    //Get order batch
		//    conn.Open();
		//    SqlCommand cmd = new SqlCommand("OrderItems_GetBatch", conn);
		//    cmd.CommandType = CommandType.StoredProcedure;
		//    cmd.Parameters.AddWithValue("@BatchID", Session["Batch"].ToString());

		//    SqlDataReader reader = Utilities.OpenReader(cmd);//cmd.ExecuteReader();
		//    //loop through - 1 line per item, Patient info once per order
		//    int OrdID = 0;
		//    iTextSharp.text.Table tbl = new iTextSharp.text.Table(6);
		//    while (reader.Read())
		//    {
		//        iTextSharp.text.Cell cell;
		//        iTextSharp.text.Phrase phrase = new iTextSharp.text.Phrase();
		//        iTextSharp.text.Font headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA + iTextSharp.text.Font.BOLD, 8);

		//        iTextSharp.text.Font bodyFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 6);
		//        if (OrdID != (int)reader["OrderID"])
		//        {
		//            //print header
		//            cell = new iTextSharp.text.Cell();
		//            cell.Colspan = 6;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase(reader["ShipName"].ToString(), headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 6;
		//            cell.HorizontalAlignment = 1;
		//            cell.Header = true;
		//            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
		//            cell.UseBorderPadding = true;
		//            cell.UseDescender = true;
		//            cell.GrayFill = 50;
		//            tbl.AddCell(cell);
		//            if ((string)reader["AutoshipDiscounts"] != "")
		//            {
		//                phrase = new iTextSharp.text.Phrase("Alert: " + (string)reader["AutoshipDiscounts"] + " Note: " + (string)reader["AutoshipNote"], headerFont);
		//                cell = new iTextSharp.text.Cell(phrase);
		//                //cell.BackgroundColor = iTextSharp.text.Color.GRAY;
		//            }
		//            else
		//            {
		//                phrase = new iTextSharp.text.Phrase("Alert: None." + " Note: " + (string)reader["AutoshipNote"], headerFont);
		//                cell = new iTextSharp.text.Cell(phrase);
		//                //cell.BackgroundColor = iTextSharp.text.Color.GRAY;
		//            }
		//            cell.Colspan = 6;
		//            cell.HorizontalAlignment = 1;
		//            cell.Header = true;
		//            cell.VerticalAlignment = 1;
		//            cell.UseDescender = true;
		//            cell.UseBorderPadding = true;

		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Name", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Address", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("City", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("State", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Zip", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase((string)reader["ShipName"], bodyFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase((string)reader["ShipAddress1"], bodyFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase((string)reader["ShipCity"], bodyFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase((string)reader["ShipState"], bodyFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase((string)reader["ShipZip"], bodyFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("", bodyFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Items", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 6;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("ShipDate", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Product", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Quantity", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Price", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = false;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//            phrase = new iTextSharp.text.Phrase("Extended Price", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);

		//            phrase = new iTextSharp.text.Phrase("Discount", headerFont);
		//            cell = new iTextSharp.text.Cell(phrase);
		//            cell.Colspan = 1;
		//            cell.Header = true;
		//            cell.HorizontalAlignment = 1;
		//            cell.UseDescender = true;
		//            tbl.AddCell(cell);
		//        }
		//        //Print Item
		//        int quan = (int)reader["Quantity"];
		//        decimal price = (decimal)reader["Price"];
		//        decimal ExtPric = quan * price;
		//        phrase = new iTextSharp.text.Phrase(((DateTime)reader["ShipDate"]).ToShortDateString(), bodyFont);
		//        cell = new iTextSharp.text.Cell(phrase);
		//        cell.Colspan = 1;
		//        cell.Header = false;
		//        cell.HorizontalAlignment = 1;
		//        cell.UseDescender = true;
		//        tbl.AddCell(cell);
		//        phrase = new iTextSharp.text.Phrase((string)reader["ProductName"], bodyFont);
		//        cell = new iTextSharp.text.Cell(phrase);
		//        cell.Colspan = 1;
		//        cell.Header = false;
		//        cell.HorizontalAlignment = 1;
		//        cell.UseDescender = true;
		//        tbl.AddCell(cell);
		//        phrase = new iTextSharp.text.Phrase(((int)reader["Quantity"]).ToString(), bodyFont);
		//        cell = new iTextSharp.text.Cell(phrase);
		//        cell.Colspan = 1;
		//        cell.Header = false;
		//        cell.HorizontalAlignment = 1;
		//        cell.UseDescender = true;
		//        tbl.AddCell(cell);
		//        phrase = new iTextSharp.text.Phrase(price.ToString("C"), bodyFont);
		//        cell = new iTextSharp.text.Cell(phrase);
		//        cell.Colspan = 1;
		//        cell.Header = false;
		//        cell.HorizontalAlignment = 1;
		//        cell.UseDescender = true;
		//        tbl.AddCell(cell);
		//        phrase = new iTextSharp.text.Phrase(ExtPric.ToString("C"), bodyFont);
		//        cell = new iTextSharp.text.Cell(phrase);
		//        cell.Colspan = 1;
		//        cell.Header = false;
		//        cell.HorizontalAlignment = 1;
		//        cell.UseDescender = true;
		//        tbl.AddCell(cell);
		//        phrase = new iTextSharp.text.Phrase(((string)reader["DiscountName"]).ToString(), bodyFont);
		//        cell = new iTextSharp.text.Cell(phrase);
		//        cell.Colspan = 1;
		//        cell.Header = false;
		//        cell.HorizontalAlignment = 1;
		//        cell.UseDescender = true;
		//        tbl.AddCell(cell);
		//        OrdID = (int)reader["OrderID"];
		//    }
		//    doc.Add(tbl);
		//    doc.Close();
		//    Invoices.HRef = "output_files/Invoices" + DateString + ".pdf";
		//    Invoices.Visible = true;
		//}
	}

	protected void CloseOrdersGrid_SelectedIndexChanged(object sender, EventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			//Get order batch
			conn.Open();
			SqlTransaction tranClose =  conn.BeginTransaction("Close");
			
			//cmd.Transaction = tranClose;
			try
			{
				if ((string)Session["CommandName"] == "Delete")
				{
					SqlCommand cmd = new SqlCommand("", conn);
					cmd.Transaction = tranClose; 
					cmd.CommandText = "Order_Cancel";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@OrderID", CloseOrdersGrid.SelectedRow.Cells[1].Text);
					cmd.ExecuteNonQuery();
				}
				if ((string)Session["CommandName"] != "Delete")
				{
					SqlCommand cmd = new SqlCommand("", conn);
					cmd.Transaction = tranClose;
					
					cmd.CommandText = "OrderItems_GetOrder";
					cmd.Parameters.AddWithValue("@OrderID", CloseOrdersGrid.SelectedRow.Cells[1].Text);
					cmd.CommandType = CommandType.StoredProcedure;
					SqlDataAdapter da = new SqlDataAdapter(cmd); 
					//da.SelectCommand = cmd;
					DataTable dt = new DataTable();
					da.Fill(dt);

					SqlCommand UpdateShip = new SqlCommand("ProfileItem_Ship", conn);
					UpdateShip.Transaction = tranClose;
					foreach (DataRow reader in dt.Rows)
					{
						UpdateShip.Parameters.Clear();
						UpdateShip.Parameters.AddWithValue("@OrderItemID", (int)reader["OrderItemID"]);
						UpdateShip.CommandType = CommandType.StoredProcedure;
						//UpdateShip.ExecuteNonQuery();
						SqlDataReader theItem = UpdateShip.ExecuteReader();
						if (theItem.Read())
						{
							if ((int)theItem["DayToShip"] > 28)
							{
								if (((DateTime)theItem["NextShipDate"]).Day != (int)theItem["DayToShip"])
								{
									try
									{
										DateTime NewNext = DateTime.Parse((((DateTime)theItem["NextShipDate"]).Month).ToString() + "/" + ((int)theItem["DayToShip"]).ToString() + "/" + (((DateTime)theItem["NextShipDate"]).Year).ToString());
										SqlCommand ChangeDay = new SqlCommand("update ProfileItems set NextShipDate='" + NewNext.ToShortDateString() + "' where ProfileItemID=" + ((int)theItem["ProfileItemID"]).ToString(), conn, tranClose);
										ChangeDay.CommandType = CommandType.Text;
										theItem.Close();
										ChangeDay.ExecuteNonQuery();
									}
									catch (FormatException ex)
									{
										theItem.Close();
									}

								}
								else
								{
									theItem.Close();
								}
							}
							else
							{
								try
								{
									theItem.Close();
								}
								catch { }
							}
						}
						else
							theItem.Close();


					}
					cmd.CommandText = "Orders_CloseOrder";
					cmd.ExecuteNonQuery();
				}
				tranClose.Commit();
			}
			catch
			{
				tranClose.Rollback();
			}

			SqlCommand cmd1 = new SqlCommand("Orders_GetToClose", conn);
			cmd1.CommandType = CommandType.StoredProcedure;
			cmd1.Parameters.AddWithValue("@StartDate", txtBegin.Text);
			cmd1.Parameters.AddWithValue("@EndDate", txtEnd.Text);
			SqlDataReader reader1 = AutoshipUtilities.OpenReader(cmd1);//cmd1.ExecuteReader();
			CloseOrdersGrid.DataSource = reader1;
			CloseOrdersGrid.Visible = true;
			CloseOrdersGrid.DataBind();
		}

	}

	protected void CloseOrdersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == "Invoices")
		{
			string QBId = "";
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("select QB_Match.QBId from QB_Match join Orders on Orders.PatientID = QB_MAtch.PatientID where Orders.OrderID="
					+ (CloseOrdersGrid.Rows[int.Parse((string)e.CommandArgument)]).Cells[2].Text, conn);
				cmd.Connection = conn;
				SqlDataReader reader = AutoshipUtilities.OpenReader(cmd);//cmd.ExecuteReader();
				if(reader.Read())
				{
					QBId = (string)reader[0];
				}
				reader.Close();
			}
			OdbcConnection Qconn = (OdbcConnection)Application["QODBC"];
			OdbcCommand Qcmd = new OdbcCommand("sp_report OpenInvoices show TxnType_Title, Date_Title, RefNumber_Title, PONumber_Title, Terms_Title, DueDate_Title, Aging_Title, OpenBalance_Title, Text, Blank, TxnType, Date, RefNumber, PONumber, Terms, DueDate, Aging, OpenBalance parameters DateMacro = 'LastYearToDate'", Qconn);
			Qcmd.CommandType = CommandType.Text;
			OdbcDataAdapter da = new OdbcDataAdapter(Qcmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
		}
		else
			Session["CommandName"] = e.CommandName;
	}
	protected void CloseOrdersGrid_RowDeleting(Object sender, GridViewDeleteEventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			//Get order batch
			conn.Open();
			SqlCommand cmd = new SqlCommand("Order_Close", conn);
			if ((string)Session["CommandName"] == "Delete")
				cmd.CommandText = "Order_Cancel";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@OrderID", e.Keys[0]);
			cmd.ExecuteNonQuery();

			if ((string)Session["CommandName"] != "Delete")
			{
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = cmd;
				cmd.CommandText = "OrderItems_GetOrder";
				DataTable dt = new DataTable();
				da.Fill(dt);

				SqlCommand UpdateShip = new SqlCommand("ProfileItem_Ship", conn);
				foreach (DataRow reader in dt.Rows)
				{
					UpdateShip.Parameters.Clear();
					UpdateShip.Parameters.AddWithValue("@OrderItemID", (int)reader["OrderItemID"]);
					UpdateShip.CommandType = CommandType.StoredProcedure;
					UpdateShip.ExecuteNonQuery();
				}
				cmd.CommandText = "Orders_CloseOrder";
				cmd.ExecuteNonQuery();

			}
			SqlCommand cmd1 = new SqlCommand("Orders_GetToClose", conn);
			cmd1.CommandType = CommandType.StoredProcedure;
			cmd1.Parameters.AddWithValue("@StartDate", txtBegin.Text);
			cmd1.Parameters.AddWithValue("@EndDate", txtEnd.Text);
			SqlDataReader reader1 = AutoshipUtilities.OpenReader(cmd1);//cmd1.ExecuteReader();
			CloseOrdersGrid.DataSource = reader1;
			CloseOrdersGrid.Visible = true;
			CloseOrdersGrid.DataBind();
			e.Cancel = true;
		}
	}

	protected void ReportsMenu_MenuItemClick(object sender, MenuEventArgs e)
	{

		switch (e.Item.Value)
		{
			case "1":
				ReportsGrid.DataSource = AutoshipReportsUtil.CancelledOrders();
				ReportsGrid.EmptyDataText = "No cancelled orders found.";
				ReportHeader.InnerText = "Cancelled Orders";
				ReportsGrid.DataBind();
				VisDiv.Visible = true;
				break;
			case "2":
				ReportsGrid.DataSource = AutoshipReportsUtil.OpenOrders();
				ReportsGrid.EmptyDataText = "No open orders found.";
				ReportHeader.InnerText = "Open Orders";
				ReportsGrid.DataBind();
				VisDiv.Visible = true;
				break;
			case "3":
				ProdReportVis.Visible = true;
				break;
		}

	}

	protected void btnProdGen_Click(object sender, EventArgs e)
	{
		ReportsGrid.DataSource = AutoshipReportsUtil.ProductDemand(DateTime.Parse(txtProdDate.Text));
		ReportsGrid.EmptyDataText = "No product demand found.";
		ReportHeader.InnerText = "Product Demand";
		ReportsGrid.DataBind();
		VisDiv.Visible = true;
	}


	protected void ManageRightsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("Staff_UpdateAutoship", conn);
			cmd.Parameters.AddWithValue("@EmployeeID", ManageRightsGrid.Rows[ManageRightsGrid.EditIndex].Cells[1].Text);
			string Access = ((DropDownList)ManageRightsGrid.Rows[ManageRightsGrid.EditIndex].Cells[4].Controls[1]).SelectedValue;

			if (Access != "Blank")
				cmd.Parameters.AddWithValue("@AutoshipAccess", Access);
			else
				cmd.Parameters.AddWithValue("@AutoshipAccess", "");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.ExecuteNonQuery();
			e.Cancel = true;
			ManageRightsGrid.EditIndex = -1;
			ManageRightsGrid.DataBind();
		}
	}

	protected void ManageRightsGrid_RowEditing(object sender, GridViewEditEventArgs e)
	{
		string sql = "select AutoshipAccess from staff where EmployeeID = " + ManageRightsGrid.Rows[e.NewEditIndex].Cells[1].Text;
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.CommandType = CommandType.Text;
			try
			{
				string Access = (string)cmd.ExecuteScalar();
				Session["Access"] = Access;
			}
			catch
			{
				Session["Access"] = "Blank";
			}
			if ((string)Session["Access"] == "") Session["Access"] = "Blank";
		}
	}

	protected void ManageRightsGrid_DataBound(object sender, EventArgs e)
	{
		if (ManageRightsGrid.EditIndex != -1)
		{
			((DropDownList)ManageRightsGrid.Rows[ManageRightsGrid.EditIndex].Cells[4].Controls[1]).SelectedValue = (String)Session["Access"];
		}
	}

	protected void btnCloseOrders_Click(object sender, EventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("Orders_GetToClose", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@StartDate", txtBegin.Text);
			cmd.Parameters.AddWithValue("@EndDate", txtEnd.Text);
			SqlDataReader reader = AutoshipUtilities.OpenReader(cmd);//cmd.ExecuteReader();
			CloseOrdersGrid.DataSource = reader;
			CloseOrdersGrid.Visible = true;
			CloseOrdersGrid.DataBind();
		}
	}


	protected void btnCancel_Click(object sender, EventArgs e)
	{
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            AutoshipReportsUtil.RemoveBacth((int)Session["Batch"],conn);
            OrderPreviewTable.Visible = false;
        }
	}

	protected void btnRrefershOrders_Click(object sender, EventArgs e)
	{
		OrderPreviewTable.Visible = false;
	}


	protected void btnCreateInvoices_OnClick(object sender, EventArgs e)
	{

	}

	protected void CloseOrdersGrid_Editing(object sender, GridViewEditEventArgs e)
	{
		CloseOrdersGrid.EditIndex = e.NewEditIndex;
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand("Orders_GetToClose", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@StartDate", txtBegin.Text);
			cmd.Parameters.AddWithValue("@EndDate", txtEnd.Text);
			SqlDataReader reader = AutoshipUtilities.OpenReader(cmd);//cmd.ExecuteReader();
			CloseOrdersGrid.DataSource = reader;
			CloseOrdersGrid.Visible = true;
			CloseOrdersGrid.DataBind();
		}
	}

	protected void CloseOrdersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand();
			string newNote = (string)e.NewValues["Note"];
			if (e.NewValues["Note"] != null) newNote = newNote.Replace("'", "''"); else newNote = "";
			cmd.CommandText = "Update Orders set Note='" + newNote + "' where OrderID=" + e.Keys["OrderID"];
			cmd.Connection = conn;
			cmd.CommandType = CommandType.Text;
			cmd.ExecuteNonQuery();
			CloseOrdersGrid.EditIndex = -1;
			cmd = new SqlCommand("Orders_GetToClose", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@StartDate", txtBegin.Text);
			cmd.Parameters.AddWithValue("@EndDate", txtEnd.Text);
			SqlDataReader reader = AutoshipUtilities.OpenReader(cmd);//cmd.ExecuteReader();
			CloseOrdersGrid.DataSource = reader;
			CloseOrdersGrid.Visible = true;
			CloseOrdersGrid.DataBind();
		}
	}

	protected void CloseOrdersGrid_RowCancelingEdit(object sender, EventArgs e)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
		{
			conn.Open(); CloseOrdersGrid.EditIndex = -1;
			SqlCommand cmd = new SqlCommand("Orders_GetToClose", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@StartDate", txtBegin.Text);
			cmd.Parameters.AddWithValue("@EndDate", txtEnd.Text);
			SqlDataReader reader = AutoshipUtilities.OpenReader(cmd);//cmd.ExecuteReader();
			CloseOrdersGrid.DataSource = reader;
			CloseOrdersGrid.Visible = true;
			CloseOrdersGrid.DataBind();
		}
	}
}
