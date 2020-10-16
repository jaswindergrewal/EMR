using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using dotnetCHARTING;

namespace Quest
{
	public partial class LabChart : LMCBase
	{
		// The patient ID has to be in the query string, or we can't do anything
		private int patientID = -1;

		// The array of charts is a bit hacky, but lots of charts may have to be drawn on a single page
		private Chart[] charts = new Chart[20];

		// These are the dates that bound the default queries for lab results
		private readonly DateTime firstSearchDate = new DateTime(1900, 1, 1);
		private readonly DateTime lastSearchDate = new DateTime(2200, 1, 1);

		private void Page_Load(object sender, System.EventArgs e)
		{
			charts[0] = Chart1;
			charts[1] = Chart2;
			charts[2] = Chart3;
			charts[3] = Chart4;
			charts[4] = Chart5;
			charts[5] = Chart6;
			charts[6] = Chart7;
			charts[7] = Chart8;
			charts[8] = Chart9;
			charts[9] = Chart10;
			charts[10] = Chart11;
			charts[11] = Chart12;
			charts[12] = Chart13;
			charts[13] = Chart14;
			charts[14] = Chart15;
			charts[15] = Chart16;
			charts[16] = Chart17;
			charts[17] = Chart18;
			charts[18] = Chart19;
			charts[19] = Chart20;

			for (int i = 0; i < charts.Length; i++)
			{
				charts[i].TempDirectory = "temp";
				charts[i].Visible = false;
				charts[i].Debug = false;
			}

			this.patientID = Convert.ToInt32(this.Request.QueryString["patientID"]);

			if (!this.IsPostBack)
			{
				GetNameAndLabOptions();
			}
		}

		private bool GetNameAndLabOptions()
		{
			AppSettingsReader appSettings = new AppSettingsReader();
			using (SqlConnection connection = new SqlConnection(
					appSettings.GetValue("lmc_connection_string", typeof(string)).ToString()))
			{
				connection.Open();

				SqlCommand command = connection.CreateCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = @"
SELECT FirstName, MiddleInitial, LastName
FROM Patients
WHERE PatientID = @PatientID";
				command.Parameters.Add("@PatientID", this.patientID);
				SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
				if (reader.Read())
				{
					StringBuilder sb = new StringBuilder();
					sb.Append(reader["FirstName"]);
					sb.Append(' ');
					object middleInitial = reader["MiddleInitial"];
					if ((middleInitial != null) && (middleInitial != DBNull.Value))
					{
						sb.Append(middleInitial);
						sb.Append(' ');
					}
					sb.Append(reader["LastName"]);
					this.LabelPatientName.Text = sb.ToString();
				}
				else
				{
					this.LabelErrorMessage.Text = "Could not find the patient in the database.";
					reader.Close();
					connection.Close();
					return false;
				}
				reader.Close();

				this.DropDownListTestType.Items.Add(new ListItem("--- Select a lab order ---", ""));
				command = connection.CreateCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = @"
SELECT DISTINCT lab_OrderSegmentDetails.UniversalServiceID AS ServiceID
FROM lab_Patients, lab_CommonOrderSegments, lab_OrderSegmentDetails, lab_ObservationResultDetailSegments
WHERE (lab_OrderSegmentDetails.CommonOrderSegmentID = lab_CommonOrderSegments.ID)
  AND (lab_CommonOrderSegments.PatientID = lab_Patients.ID)
  AND (lab_ObservationResultDetailSegments.OrderSegmentDetailID = lab_OrderSegmentDetails.ID)
  AND (lab_ObservationResultDetailSegments.ValueType = 'NM')
  AND (lab_Patients.CorrespondingPatientID = @PatientID)";
				command.Parameters.Add("@PatientID", this.patientID);
				reader = command.ExecuteReader(CommandBehavior.SingleResult);

				// We cannot just use the universal ID, as some of them resolve to the same string.
				// For example, both seem to refer to the same test, but they have different universal IDs.
				//       7600CLSB=^LIPID PANEL^^7600CLSB=^LIPID PANEL
				//       7600SB=^LIPID PANEL^^7600SB=^LIPID PANEL
				// So, extract the "short names" from the universal IDs and make sure not to have duplicates.
				SortedList testTypes = new SortedList();
				while (reader.Read())
				{
					string serviceID = reader["ServiceID"].ToString();
					int lastCaratPosition = serviceID.LastIndexOf('^');
					if ((lastCaratPosition < 0) || (lastCaratPosition >= serviceID.Length - 1))
					{
						continue;
					}
					string shortID = serviceID.Substring(lastCaratPosition + 1);
					if (! testTypes.ContainsKey(shortID))
					{
						testTypes.Add(shortID, null);
					}
				}
				reader.Close();
				connection.Close();

				foreach (string key in testTypes.Keys)
				{
					this.DropDownListTestType.Items.Add(key);
				}
			}

			return true;
		}

		private void DrawCharts()
		{
			if (this.DropDownListTestType.SelectedValue.Length < 1)
			{
				return;
			}

			DateTime givenStartDate = this.firstSearchDate;
			DateTime givenEndDate = this.lastSearchDate;

			if ((this.TextBoxStartDate.Text != null) &&
				(this.TextBoxStartDate.Text.Length > 0))
			{
				try
				{
					givenStartDate = Convert.ToDateTime(this.TextBoxStartDate.Text);
				}
				catch (FormatException)
				{
					this.LabelErrorMessage.Text = "Invalid start date";
					return;
				}
			}
			if ((this.TextBoxEndDate.Text != null) &&
				(this.TextBoxEndDate.Text.Length > 0))
			{
				try
				{
					givenEndDate = Convert.ToDateTime(this.TextBoxEndDate.Text);
				}
				catch (FormatException)
				{
					this.LabelErrorMessage.Text = "Invalid end date";
					return;
				}
			}

			DateTime firstDate = DateTime.MaxValue;
			DateTime lastDate = DateTime.MinValue;

			AppSettingsReader appSettings = new AppSettingsReader();
			using (SqlConnection connection = new SqlConnection(
				appSettings.GetValue("lmc_connection_string", typeof(string)).ToString()))
			{
				connection.Open();

				SqlCommand command = connection.CreateCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = @"
SELECT DISTINCT
	lab_ObservationResultDetailSegments.ObservationIdentifier,
	lab_ObservationResultDetailSegments.ObservationValue,
	lab_ObservationResultDetailSegments.Units, 
	lab_ObservationResultDetailSegments.ReferencesRange,
	lab_ObservationResultDetailSegments.ObservationDateTime
FROM
	lab_Patients,
	lab_CommonOrderSegments,
	lab_OrderSegmentDetails,
	lab_ObservationResultDetailSegments
WHERE (lab_OrderSegmentDetails.CommonOrderSegmentID = lab_CommonOrderSegments.ID)
  AND (lab_CommonOrderSegments.PatientID = lab_Patients.ID)
  AND (lab_Patients.CorrespondingPatientID = @PatientID)
  AND (lab_OrderSegmentDetails.UniversalServiceID LIKE @Query)
  AND (lab_ObservationResultDetailSegments.OrderSegmentDetailID = lab_OrderSegmentDetails.ID)
  AND (lab_ObservationResultDetailSegments.ValueType = 'NM')
  AND (lab_ObservationResultDetailSegments.ObservationDateTime >= @StartDate)
  AND (lab_ObservationResultDetailSegments.ObservationDateTime <= @EndDate)
ORDER BY lab_ObservationResultDetailSegments.ObservationDateTime DESC";
				command.Parameters.Add("@PatientID", this.patientID);
				command.Parameters.Add("@Query", String.Format("%^{0}^%", this.DropDownListTestType.SelectedValue));
				command.Parameters.Add("@StartDate", givenStartDate);
				command.Parameters.Add("@EndDate", givenEndDate);
				SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult);

				Hashtable tablesOfResults = new Hashtable();
				Hashtable tablesOfUnits = new Hashtable();
				Hashtable tablesOfNormalRanges = new Hashtable();
				while (reader.Read())
				{
					// Make sure we know the first and last times graphed
					DateTime dateData = Convert.ToDateTime(reader["ObservationDateTime"]);
					if (dateData < firstDate)
					{
						firstDate = dateData;
					}
					if (dateData > lastDate)
					{
						lastDate = dateData;
					}

					// Get the data table for this type of result
					DataTable currentTable = null;
					string resultType = reader["ObservationIdentifier"].ToString();
					if (tablesOfResults.ContainsKey(resultType))
					{
						currentTable = tablesOfResults[resultType] as DataTable;
					}
					else
					{
						currentTable = new DataTable();
						currentTable.Columns.Add("Date", typeof(DateTime));
						currentTable.Columns.Add("Value", typeof(double));

						tablesOfResults.Add(resultType, currentTable);
					}

					DataRow row = currentTable.NewRow();
					row["Date"] = dateData;
					row["Value"] = Convert.ToDouble(reader["ObservationValue"]);
					currentTable.Rows.Add(row);

					if (! tablesOfUnits.ContainsKey(resultType))
					{
						tablesOfUnits.Add(resultType, reader["Units"].ToString());
						tablesOfNormalRanges.Add(resultType, reader["ReferencesRange"].ToString());
					}
				}
				reader.Close();

				connection.Close();

				// Check if we retrieved any data
				if (tablesOfResults.Count == 0)
				{
					this.LabelErrorMessage.Text = "There is no data for this report in the provided date range.";
					return;
				}

				// Extract the data table names
				Array resultTypes = new ArrayList(tablesOfResults.Keys).ToArray(typeof(string));

				// It's possible that there are more data tables that chart objects
				if (resultTypes.Length > charts.Length)
				{
					this.LabelErrorMessage.Text = "More data is contained in the lab reports than can be displayed.";
				}
				else
				{
					this.LabelErrorMessage.Text = String.Empty;
				}

				// Process one data table/chart at a time
				for (int i = 0; i < resultTypes.Length && i < charts.Length; i++)
				{
					// DrawRanges may overwrite these values, so set our default now
					charts[i].XAxis.DefaultTick.GridLine.Color = Color.SlateGray;
					charts[i].YAxis.DefaultTick.GridLine.Color = Color.SlateGray;

					// Process everything that needs the unmodified resultType string
					string resultType = resultTypes.GetValue(i).ToString();
					charts[i].YAxis.Label.Text = tablesOfUnits[resultType] as string;
					charts[i].Series.Data = tablesOfResults[resultType] as DataTable;
					DrawRanges(tablesOfNormalRanges[resultType] as string, charts[i]);

					// Extract something meaningful from the resultType string, which is a weird HL7 string
					int caratCount = 0;
					for (int j = resultType.Length - 1; j >= 0; j--)
					{
						if (resultType[j] == '^')
						{
							caratCount++;
						}
						if (caratCount > 1)
						{
							resultType = resultType.Substring(j + 1);
							resultType = resultType.Substring(0, resultType.Length - 1);
							break;
						}
					}

					// Set all the rest of the values on the given chart
					charts[i].Visible = true;
					charts[i].Title = String.Format("{0} - {1}",
						this.LabelPatientName.Text,
						resultType);
					charts[i].XAxis.Scale = Scale.Time;
					charts[i].XAxis.Label.Text = "Date";
					charts[i].YAxis.AlternateGridBackground.Color = Color.Empty;
					charts[i].LegendBox.Template = "%Name%Icon";
					charts[i].Series.Type = SeriesType.Line;
					charts[i].Series.DefaultElement.ShowValue = true;
					charts[i].Series.DefaultElement.SmartLabel.Text = "<%XValue,MM/dd/yyyy>";
					charts[i].Series.LegendEntry.Visible = false;

					if (givenStartDate != this.firstSearchDate)
					{
						charts[i].Series.StartDate = givenStartDate;
					}
					else
					{
						charts[i].Series.StartDate = firstDate;
					}

					if (givenEndDate != this.lastSearchDate)
					{
						charts[i].Series.EndDate = givenEndDate;
					}
					else
					{
						charts[i].Series.EndDate = lastDate;
					}

					TimeSpan xRange = charts[i].Series.EndDate - charts[i].Series.StartDate;
					if (xRange.TotalDays < 60.0)
					{
						charts[i].XAxis.TimePadding = new TimeSpan(6, 0, 0, 0);
					}
					else
					{
						charts[i].XAxis.TimePadding = new TimeSpan(Convert.ToInt32(xRange.TotalDays / 10.0), 0, 0, 0);
					}

					charts[i].SeriesCollection.Add();
				}

				// Hide unused charts
				for (int i = charts.Length - 1; i > resultTypes.Length - 1; i--)
				{
					charts[i].Visible = false;
				}
			}
		}

		// Range strings are not fixed in a certain form.  Examples include:
		// "X-Y"
		// "X - Y"
		// "<X"
		// ">X"
		// "<OR=X"
		// "< OR = X"
		// ">OR=X"
		// "> OR = X"
		// "LESS THAN X"
		private void DrawRanges(string normalRange, Chart chart)
		{
			if ((normalRange == null) || (normalRange.Length == 0))
			{
				chart.XAxis.DefaultTick.GridLine.Color = Color.LightGray;
				chart.YAxis.DefaultTick.GridLine.Color = Color.LightGray;
				return;
			}

			double lowRange = Double.MinValue;
			double highRange = Double.MaxValue;

			// This is like "X-Y" or "X - Y"
			if (Char.IsDigit(normalRange[0]))
			{
				StringBuilder low = new StringBuilder();
				StringBuilder high = new StringBuilder();

				int position = 0;
				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]) || normalRange[position] == '.')
					{
						low.Append(normalRange[position]);
					}
					else
					{
						break;
					}
				}
				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]))
					{
						break;
					}
				}
				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]) || normalRange[position] == '.')
					{
						high.Append(normalRange[position]);
					}
					else
					{
						break;
					}
				}

				lowRange = Convert.ToDouble(low.ToString());
				highRange = Convert.ToDouble(high.ToString());
			}
			// This is some form of less than
			else if (normalRange.StartsWith("LESS") || (normalRange[0] == '<'))
			{
				int position = 0;

				StringBuilder high = new StringBuilder();

				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]))
					{
						break;
					}
				}
				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]) || normalRange[position] == '.')
					{
						high.Append(normalRange[position]);
					}
					else
					{
						break;
					}
				}

				highRange = Convert.ToDouble(high.ToString());
			}
			// This is some form of greater than
			else if (normalRange.StartsWith("GREATER") || (normalRange[0] == '>'))
			{
				int position = 0;

				StringBuilder low = new StringBuilder();

				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]))
					{
						break;
					}
				}
				for (;position < normalRange.Length; position++)
				{
					if (Char.IsDigit(normalRange[position]) || normalRange[position] == '.')
					{
						low.Append(normalRange[position]);
					}
					else
					{
						break;
					}
				}

				lowRange = Convert.ToDouble(low.ToString());
			}
			// The range was something we didn't understand
			else
			{
				chart.XAxis.DefaultTick.GridLine.Color = Color.LightGray;
				chart.YAxis.DefaultTick.GridLine.Color = Color.LightGray;
				return;
			}

			DrawRangesHelper(lowRange, highRange, chart);
		}

		private void DrawRangesHelper(double low, double high, Chart chart)
		{
			Color abnormalColor = Color.IndianRed;

			if ((low > Double.MinValue) || (high < Double.MaxValue))
			{
				AxisMarker normalRange = new AxisMarker(String.Empty, new Background(Color.LightGreen), low, high);
				normalRange.LegendEntry.Name = "Normal";
				chart.YAxis.Markers.Add(normalRange);
			}

			if ((low > Double.MinValue) && (high < Double.MaxValue))
			{
				AxisMarker lowRange =  new AxisMarker(String.Empty, new Background(abnormalColor), 0, low);
				lowRange.LegendEntry.Name = "Abnormal";
				chart.YAxis.Markers.Add(lowRange);

				AxisMarker highRange =  new AxisMarker(String.Empty, new Background(abnormalColor), high, high * high * 10.0);
				highRange.LegendEntry.Visible = false;
				chart.YAxis.Markers.Add(highRange);
			}
			else if (high < Double.MaxValue)
			{
				AxisMarker highRange =  new AxisMarker(String.Empty, new Background(abnormalColor), high, high * high * 10.0);
				highRange.LegendEntry.Name = "Abnormal";
				chart.YAxis.Markers.Add(highRange);
			}
			else if (low > Double.MinValue)
			{
				AxisMarker lowRange =  new AxisMarker(String.Empty, new Background(abnormalColor), 0, low);
				lowRange.LegendEntry.Name = "Abnormal";
				chart.YAxis.Markers.Add(lowRange);
			}
		}

		protected void DropDownListTestType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DrawCharts();
		}

		protected void ButtonUpdate_Click(object sender, System.EventArgs e)
		{
			DrawCharts();
		}
	}
}