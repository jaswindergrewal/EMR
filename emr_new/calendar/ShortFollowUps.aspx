<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShortFollowUps.aspx.cs" Inherits="ShortFollowUps"
	MasterPageFile="~/sub.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

	<asp:UpdatePanel ID="pnlFup" runat="server">
		<ContentTemplate>
			<div>
				<h4>
					Manage Followups</h4>
				<asp:Button ID="btnComplete" runat="server" Text="Complete all checked" CssClass="button"
					OnClick="btnComplete_Click" />
				<asp:GridView ID="FollowupGrid" runat="server" DataSourceID="SqlSourceFollowUp" BackColor="#DEBA84"
					BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" OnSelectedIndexChanged="FollowupGrid_SelectedIndexChanged"
					EmptyDataText="No Followups Found." AllowSorting="True">
					<Columns>
						<asp:TemplateField HeaderText="Complete" ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<asp:CheckBox ID="cbComplete" runat="server" Checked="false" />
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
					<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
					<HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
					<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
					<RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
					<SortedAscendingCellStyle BackColor="#FFF1D4" />
					<SortedAscendingHeaderStyle BackColor="#B95C30" />
					<SortedDescendingCellStyle BackColor="#F1E5CE" />
					<SortedDescendingHeaderStyle BackColor="#93451F" />
				</asp:GridView>
				<asp:SqlDataSource ID="SqlSourceFollowUp" runat="server" SelectCommand="Appointment_FollowUp"
					SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:db %>" />
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
