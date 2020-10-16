<%@ Page Title="" Language="C#" MasterPageFile="sub.master" AutoEventWireup="true"
	CodeFile="Subjective.aspx.cs" Inherits="DictationConsole_Subjective" %>

<%@ Register Assembly="obout_Window_NET" Namespace="OboutInc.Window" TagPrefix="owd" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.SuperForm" Assembly="obout_SuperForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link href="resources/custom-styles/persist/style.css" rel="Stylesheet" type="text/css" />
	<link href="resources/custom-styles/new/style.css" rel="Stylesheet" type="text/css" />
	<link href="resources/custom-styles/resolved/style.css" rel="Stylesheet" type="text/css" />
	<link href="resources/custom-styles/goals/style.css" rel="Stylesheet" type="text/css" />
	<link href="resources/custom_scripts/row-reorder/row-reorder.css" rel="stylesheet"
		type="text/css" />
	<script type="text/javascript">
		function Grid1_ClientAdd(sender, record) {

			Window1.Open();

			document.getElementById('SymptomID').value = 0;
			SuperForm1_Symptom.value('');
			SuperForm1_ShowAll.checked(false);

			return false;
		}
		function Grid2_ClientAdd(sender, record) {

			Window2.Open();

			document.getElementById('SymptomID').value = 0;
			SuperForm2_Symptom.value('');
			SuperForm2_ShowAll.checked(false);

			return false;
		}
		//Drag drop code

		var rowPositionField = "RowPosition";
		var primaryKeyField = "SymptomID";

		window.onload = function () {
			grdPersist.OnClientRowsDropped = saveRowsPosition;
			grdResolved.OnClientRowsDropped = saveRowsPosition;
		}

		function saveRowsPosition() {
			var datagrdPersist = getGridData(grdPersist);
			var datagrdResolved = getGridData(grdResolved);

			var succ = PageMethods.SaveRowsPosition(datagrdPersist.join(',') + "|" + datagrdResolved.join(','), null, null);

		}

		function getRowPositions(grid) {
			var positions = new Array();

			for (var i = 0; i < grid.Rows.length; i++) {
				if (grid.Rows[i] && grid.Rows[i].Cells) {
					positions.push(grid.Rows[i].Cells[rowPositionField].Value);
				}
			}

			positions.sort(function (a, b) { return a - b })

			return positions;
		}

		function getGridData(grid) {
			var positions = getRowPositions(grid);

			var data = new Array();
			var j = 0;

			for (var i = 0; i < grid.Rows.length; i++) {
				if (grid.Rows[i] && grid.Rows[i].Cells) {
					var item = grid.Rows[i].Cells[primaryKeyField].Value + '*' + positions[j];
					grid.Rows[i].Cells[rowPositionField].Value = positions[j];
					data.push(item);
					j++;
				}
			}

			return data;
		}
				
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script type="text/javascript" src="resources/custom_scripts/row-reorder/row-reorder.js"></script>
	<span id="WindowPositionHelper"></span>
		<table cellpadding="5" cellspacing="5" width="980px">
		<tr>
			<td valign="top">
				<div class="persist" style="background-color: Transparent; padding-left: 0px;">
					<obout:Grid ID="grdPersist" runat="server" ShowLoadingMessage="false" AllowPaging="false"
						PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="false"
						AllowPageSizeSelection="false" Serialize="false" FolderStyle="_" Width="90px">
						<ScrollingSettings EnableVirtualScrolling="true" />
						<Columns>
							<obout:Column DataField="Symptom" HeaderText="Symptoms Persist" />
							<obout:Column DataField="SymptomID" Visible="false" />
							<obout:Column DataField="RowPosition" Visible="false" />
							<obout:Column DataField="dir" HeaderText="Change" runat="server" Width="70px">
								<TemplateSettings TemplateId="ImageTemplate" />
							</obout:Column>
						</Columns>
						<CssSettings CSSFolderImages="resources/custom-styles/persist" />
						<PagingSettings ShowRecordsCount="false" />
						<Templates>
							<obout:GridTemplate runat="server" ID="ImageTemplate">
								<Template>
									<img src="<%# Container.Value %>" alt="" />
								</Template>
							</obout:GridTemplate>
						</Templates>
					</obout:Grid>
				</div>
			</td>
			<td valign="top">
				<div class="new" style="background-color: Transparent; padding-left: 0px;">
					<obout:Grid ID="grdNew" runat="server" ShowLoadingMessage="false" AllowPaging="false"
						PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="true"
						AllowPageSizeSelection="false" Serialize="false" FolderStyle="_">
						<ScrollingSettings EnableVirtualScrolling="true" />
						<Columns>
							<obout:Column DataField="Symptom" HeaderText="New Symptoms">
							</obout:Column>
							<obout:Column DataField="SymptomID" Visible="false" />
						</Columns>
						<TemplateSettings RowEditTemplateId="tplRowEdit" />
						<CssSettings CSSFolderImages="resources/custom-styles/new" />
						<PagingSettings ShowRecordsCount="false" />
						<ClientSideEvents OnBeforeClientAdd="Grid1_ClientAdd" ExposeSender="true" />
					</obout:Grid>
				</div>
				<owd:Window ID="Window1" runat="server" IsModal="true" ShowCloseButton="true" Status=""
					RelativeElementID="grdNew" Top="50" Left="200" Height="170" Width="351" VisibleOnLoad="false"
					StyleFolder="wdstyles/grandgray" Title="Add Symptom">
					<input type="hidden" id="SymptomID" />
					<div class="super-form">
						<obout:SuperForm ID="SuperForm1" runat="server" AutoGenerateRows="false" AutoGenerateInsertButton="false"
							AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" DataKeyNames="OrderID"
							DefaultMode="Insert" Width="325">
							<Fields>
								<obout:DropDownListField DataField="Symptom" HeaderText="Symptom" DataSourceID="SqlDataSource" />
								<obout:CheckBoxField DataField="ShowAll" HeaderText="Show All" />
								<obout:TemplateField>
									<EditItemTemplate>
										<obout:OboutButton ID="OboutButton1" runat="server" Text="Save" Width="75" />
										<obout:OboutButton ID="OboutButton2" runat="server" Text="Cancel" Width="75" />
									</EditItemTemplate>
								</obout:TemplateField>
							</Fields>
						</obout:SuperForm>
					</div>
				</owd:Window>
			</td>
			<td valign="top">
				<div class="resolved" style="background-color: Transparent; padding-left: 0px;">
					<obout:Grid ID="grdResolved" runat="server" ShowLoadingMessage="false" AllowPaging="false"
						PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="false"
						AllowPageSizeSelection="false" Serialize="false" FolderStyle="_" Width="90px">
						<ScrollingSettings EnableVirtualScrolling="true" />
						<Columns>
							<obout:Column DataField="Symptom" HeaderText="Symptoms Resolved" />
							<obout:Column DataField="SymptomID" Visible="false" />
							<obout:Column DataField="RowPosition" Visible="false" />
							<obout:Column ID="Column1" DataField="dir" HeaderText="Change" runat="server" Visible="false"/>
						</Columns>
						<CssSettings CSSFolderImages="resources/custom-styles/persist" />
						<PagingSettings ShowRecordsCount="false" />
					</obout:Grid>
				</div>
			</td>
			<td valign="top">
				<div class="goals" style="background-color: Transparent; padding-left: 0px;">
					<obout:Grid ID="grdGoals" runat="server" AllowAddingRecords="true" AllowPageSizeSelection="False"
						AllowPaging="False" AllowRecordSelection="False" AllowSorting="False" AutoGenerateColumns="false"
						FolderStyle="_" Width="90px">
						<ScrollingSettings EnableVirtualScrolling="true" />
						<Columns>
							<obout:Column DataField="Symptom" HeaderText="Goals" />
							<obout:Column DataField="SymptomID" Visible="false" />
							<obout:Column ID="Column3" DataField="dir" HeaderText="Change" runat="server" Width="70px">
								<TemplateSettings TemplateId="GridTemplate3" />
							</obout:Column>
						</Columns>
						<CssSettings CSSFolderImages="resources/custom-styles/goals" />
						<ClientSideEvents OnBeforeClientAdd="Grid2_ClientAdd" ExposeSender="true" />
						<PagingSettings ShowRecordsCount="false" />
						<Templates>
							<obout:GridTemplate runat="server" ID="GridTemplate3">
								<Template>
									<img src="<%# Container.Value %>" alt="" />
								</Template>
							</obout:GridTemplate>
						</Templates>
					</obout:Grid>
				</div>
				<owd:Window ID="Window2" runat="server" IsModal="true" ShowCloseButton="true" Status=""
					RelativeElementID="grdGoals" Top="50" Left="670" Height="170" Width="351" VisibleOnLoad="false"
					StyleFolder="wdstyles/grandgray" Title="Add Goal">
					<input type="hidden" id="Hidden1" />
					<div class="super-form">
						<obout:SuperForm ID="SuperForm2" runat="server" AutoGenerateRows="false" AutoGenerateInsertButton="false"
							AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" DataKeyNames="OrderID"
							DefaultMode="Insert" Width="325">
							<Fields>
								<obout:DropDownListField DataField="Symptom" HeaderText="Goal" DataSourceID="SqlDataSource" />
								<obout:CheckBoxField DataField="ShowAll" HeaderText="Show All" />
								<obout:TemplateField>
									<EditItemTemplate>
										<obout:OboutButton ID="OboutButton1" runat="server" Text="Save" Width="75" />
										<obout:OboutButton ID="OboutButton2" runat="server" Text="Cancel" Width="75" />
									</EditItemTemplate>
								</obout:TemplateField>
							</Fields>
						</obout:SuperForm>
					</div>
				</owd:Window>
			</td>
		</tr>
		<tr>
			<td colspan="4">
				<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="PnlContent"
					ExpandControlID="pnlTitle" CollapseControlID="pnlTitle" TextLabelID="Label2"
					CollapsedText="Click to Show Lifestyle and Clinical Notes" ExpandedText="Click to Hide Lifestyle and Clinical Notes"
					Collapsed="True" SuppressPostBack="true" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
					CollapsedImage="~/images/expand.jpg">
				</cc1:CollapsiblePanelExtender>
				<asp:Panel ID="pnlTitle" runat="server">
					<asp:Image ID="Image1" runat="server" ImageUrl="~/images/expand.jpg" BorderWidth="0">
					</asp:Image>
					<asp:Label ID="Label2" runat="server" Text="Show Lifestyle and Clinical Notes" CssClass="regText">
					</asp:Label>
				</asp:Panel>
				<asp:Panel ID="pnlContent" runat="server">
					<table cellpadding="3" cellspacing="3">
						<tr>
							<td valign="top">
								<asp:Label ID="Label1" runat="server" CssClass="PageTitle" Text="Lifestyle information" /><br />
								<table cellpadding="2" cellspacing="2" width="555px" class="regText" style="background-image: ur(../images/export/beige_back.gif);
									border-style: solid; border-color: Gray; border-width: thin;">
									<tr>
										<td valign="top" width="20%" align="right">
											Water Intake
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:TextBox ID="txtWater" Columns="3" runat="server" />&nbsp;
											<asp:RadioButtonList ID="rdoWater" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
												<asp:ListItem Text="glasses/day " />
												<asp:ListItem Text="oz/day " />
												<asp:ListItem Text="qt/day " />
												<asp:ListItem Text="liter/day " />
											</asp:RadioButtonList>
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Water Source
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoWaterSource" runat="server" RepeatDirection="Horizontal"
												RepeatLayout="Flow">
												<asp:ListItem Text="Distilled " />
												<asp:ListItem Text="Bottled " />
												<asp:ListItem Text="Tap Water " />
												<asp:ListItem Text="Well Water " />
											</asp:RadioButtonList>
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Exercise Frequency
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoExercise" runat="server" RepeatDirection="Horizontal"
												RepeatLayout="Flow">
												<asp:ListItem Text="1-2x/week " />
												<asp:ListItem Text="3-5x/week " />
												<asp:ListItem Text="6x or more/week " />
											</asp:RadioButtonList>
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Exercise Type
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:CheckBoxList ID="chkExcerciseType" runat="server" RepeatDirection="Horizontal"
												RepeatLayout="Flow">
												<asp:ListItem Text="Aerobic " />
												<asp:ListItem Text="Resistance " />
												<asp:ListItem Text="Stretching " />
												<asp:ListItem Text="Sports " />
											</asp:CheckBoxList>
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Workout Length
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoWorkoutLength" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Text="< 20 min " />
												<asp:ListItem Text="20--40 min " />
												<asp:ListItem Text="40-60 min " />
												<asp:ListItem Text="l> 60 min " />
											</asp:RadioButtonList>
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Sleep Quality
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoSleep" runat="server" RepeatDirection="Horizontal">
												<asp:ListItem Text="Poor " />
												<asp:ListItem Text="Fair " />
												<asp:ListItem Text="Good " />
												<asp:ListItem Text="Excellent " />
											</asp:RadioButtonList>
											Avg Hours/Night:
											<asp:TextBox ID="txtHours" runat="server" Columns="3" />
											hours Mg. of melatonin you take:
											<asp:TextBox ID="txtMelatonin" runat="server" Columns="3" />
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Avg Servings of Raw fruits/Vegetables
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoFruitVeggie" runat="server" RepeatDirection="Horizontal"
												RepeatLayout="Flow">
												<asp:ListItem Text="< 3/day " />
												<asp:ListItem Text="3-5/day " />
												<asp:ListItem Text="5-8/day " />
												<asp:ListItem Text="> 8/day " />
											</asp:RadioButtonList>
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Diet Quality
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoDiet" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
												<asp:ListItem Text="Poor " />
												<asp:ListItem Text="Fair " />
												<asp:ListItem Text="Good " />
												<asp:ListItem Text="Excellent " />
											</asp:RadioButtonList>
											Comments:
											<asp:TextBox ID="txtDiet" runat="server" Columns="10" />
										</td>
									</tr>
									<tr>
										<td valign="top" align="right">
											Energy Level
										</td>
										<td nowrap="nowrap" valign="top">
											<asp:RadioButtonList ID="rdoEnergy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
												<asp:ListItem Text="Poor " />
												<asp:ListItem Text="Fair " />
												<asp:ListItem Text="Good " />
												<asp:ListItem Text="Excellent " />
											</asp:RadioButtonList>
											Comments:
											<asp:TextBox ID="txtEnergy" runat="server" Columns="10" />
										</td>
									</tr>
								</table>
							</td>
							<td valign="top">
								<asp:Label ID="Lable1" runat="server" CssClass="PageTitle" Text="Clinical Note" /><br />
								<obout:Editor PathPrefix="Editor_data/" ID="ed" runat="server" Appearance="custom"
									ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14"
									Submit="false" AjaxWait="true" Height="276" SpellCheckAutoComplete="true" InitialCleanUp="false"
									ModeSwitch="false">
									<Buttons>
										<obout:Toggle Name="Bold" />
										<obout:Toggle Name="Italic" />
										<obout:Toggle Name="Underline" />
										<obout:Toggle Name="StrikeThrough" />
										<obout:HorizontalSeparator />
										<obout:Method Name="ClearStyles" />
										<obout:HorizontalSeparator />
										<obout:Select Name="FontName" />
										<obout:Select Name="FontSize" />
										<obout:HorizontalSeparator />
									</Buttons>
								</obout:Editor>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</td>
		</tr>
	</table>
	<asp:SqlDataSource ID="SqlDataSource" runat="server" SelectCommand="SELECT TOP 1000 [SymptomID],[SymptomName] as Symptom,[viewable_yn]  FROM [emr_dev].[dbo].[Symptoms]"
		ConnectionString="<%$ ConnectionStrings:db %>" SelectCommandType="Text" />
</asp:Content>
