<%@ Page Title="Maintain Critical Tasks" Language="C#" MasterPageFile="~/Site.master"
	AutoEventWireup="true" CodeFile="Patients_CriticalTasksMaint.aspx.cs" Inherits="Patients_CriticalTasksMaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="NineRays.WebControls.FlyTreeView" Namespace="NineRays.WebControls"
	TagPrefix="NineRays" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

	<asp:UpdatePanel ID="pnl" runat="server">
		<ContentTemplate>
			<table width="1000px" class="border">
				<tr>
					<td colspan="2" align="center" class="PageTitle">
						Maintain Critical Tasks
					</td>
				</tr>
				<tr>
					<td valign="top">
						<asp:RadioButtonList ID="rdoSort" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
							<asp:ListItem Selected="True" Text="Alpha Sort" Value="Alpha" />
							<asp:ListItem Text="Date sort" Value="Date" />
						</asp:RadioButtonList>
						<br />
						<asp:GridView ID="grdUsers" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
							AllowPaging="true" DataSourceID="dsPatients" PageSize="20" OnSelectedIndexChanged="grdUsers_SelectedIndexChanged"
							Caption="Patients with incomplete tasks" CssClass="FormField" OnPageIndexChanged="grdUsers_PageIndexChanged" SelectedRowStyle-BackColor="Gray">
							<PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
								NextPageText="Next" PreviousPageText="Prev" />
							<Columns>
								<asp:CommandField ButtonType="Button" ControlStyle-CssClass="button" ShowSelectButton="true"
									SelectText="Documents" />
								<asp:BoundField DataField="LastName" HeaderText="Last Name" />
								<asp:BoundField DataField="FirstName" HeaderText="First Name" />
								<asp:BoundField DataField="ApptStart" HeaderText="Last Visit" />
							</Columns>
						</asp:GridView>
					</td>
					<td valign="top" align="left">
						<asp:Label ID="lblDate" runat="server" Text="Date: " />
						<asp:TextBox ID="txtDate" runat="server" />
						<cc1:CalendarExtender ID="cal" runat="server" TargetControlID="txtDate" />
						<asp:GridView ID="grdDocs" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="grdDocs_SelectedIndexChanged"
							DataKeyNames="ID,PatientID" Caption="Documents for patient" CssClass="FormField"
							EmptyDataText="No documents" Width="100%">
							<Columns>
								<asp:TemplateField HeaderText="Document">
									<ItemTemplate>
										<a href='uploads/<%# Eval("PatientID")%>/<%# Eval("Upload_Path")%>' target="_blank">
											<%# Eval("Title")  %></a>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:g}" />
								<asp:TemplateField HeaderText="Assign to Task">
									<ItemTemplate>
										<asp:DropDownList ID="ddlTypes" runat="server" DataSourceID="dsTypes" DataTextField="TaskName"
											DataValueField="TaskTypeID" />
										<asp:Button ID="btnAssign" runat="server" Text="Assign" CommandName="Select" CssClass="button" />
										<cc1:ConfirmButtonExtender ID="cfrAssing" runat="server" ConfirmText="Are you sure you want to assign this document?"
											TargetControlID="btnAssign" />
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</td>
				</tr>
			</table>
			<asp:ObjectDataSource ID="dsPatients" runat="server" TypeName="CriticalTasks" SelectMethod="PatientsTasks"
				EnableCaching='true' CacheDuration="Infinite">
				<SelectParameters>
					<asp:ControlParameter DbType="String" ControlID="rdoSort" Name="sort" />
				</SelectParameters>
			</asp:ObjectDataSource>
			<asp:SqlDataSource ID="dsTypes" runat="server" SelectCommand="select TaskName,TaskTypeID from Patients_CriticalTaskType order by TaskName"
				SelectCommandType="Text" />
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
