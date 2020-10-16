<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="DiabetesCallBack.aspx.cs" Inherits="DiabetesCallBack" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781" class="regText">
			<td>
				<b>Diabetes Call Back</b>
			</td>
			<td>
				<div align="right">
				</div>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table class="border">
					<tr>
						<td>
							Patient:
							<asp:Label ID="lblName" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Provider:
							<asp:Label ID="lblProvider" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Appointment Date:
							<asp:Label ID="lblApptDate" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Home Phone:
							<asp:Label ID="lblHome" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Work Phone:
							<asp:Label ID="lblwork" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							Cell Phone:
							<asp:Label ID="lblCell" runat="server" /><br /><br />
							<asp:Button ID="btnEscalate" runat="server" Text="Escalate" CssClass="button" OnClick="btnEscalate_Click" />
							<asp:Button ID="btnVoiceMail" runat="server" CssClass="button" Text="Left voice mail" OnClick="btnVoiceMail_Click" />
						</td>
					</tr>
					<tr>
						<td>
							<hr />
						</td>
					</tr>
					<tr>
						<td>
							<p>
								1. Rate how well you have stuck to your diet since the last time we checked in with
								you.<br />
								<asp:RadioButtonList ID="rdoQ1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Not at all " Value="1"></asp:ListItem>
									<asp:ListItem Text="So-So " Value="2"></asp:ListItem>
									<asp:ListItem Text="Spot-on " Value="3"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								2. What is your current weight?
								<asp:TextBox ID="txtQ2" runat="server" Text='' Columns="3" />
							</p>
							<p>
								3. How many times per week have you exercised?
								<asp:TextBox ID="txtQ3" runat="server" Columns="3" /><br />
								&nbsp;&nbsp;&nbsp;How long?
								<asp:TextBox ID="txtQ4" runat="server" Columns="3" />
								in minutes.
								<br />
								<br />
								&nbsp;&nbsp;&nbsp;Rate how well you have done over all in your exercise. &nbsp;&nbsp;&nbsp;<asp:RadioButtonList
									ID="rdoQ5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Not at all " Value="1"></asp:ListItem>
									<asp:ListItem Text="So-So " Value="2"></asp:ListItem>
									<asp:ListItem Text="Spot-on " Value="3"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								4. Are you taking all of the supplements and medications your doctor prescribed?
								<asp:RadioButtonList ID="rdoQ6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Not at all " Value="1"></asp:ListItem>
									<asp:ListItem Text="So-So " Value="2"></asp:ListItem>
									<asp:ListItem Text="Spot-on " Value="3"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								5. Is there anything I can escalate for you - any medical issues or concerns - any
								supplement or medicaation issues?
								<asp:RadioButtonList ID="rdoQ7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
									<asp:ListItem Text="No" Value="False"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								6. Are we meeting your expectations?
								<asp:RadioButtonList ID="rdoQ9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
									<asp:ListItem Text="No" Value="False"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								7. Do you have any questions regarding your Longevity program?
								<asp:RadioButtonList ID="rdoQ10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
									<asp:ListItem Text="No" Value="False"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								8. Is there anything else I can do for you?
								<asp:RadioButtonList ID="rdoQ11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
									<asp:ListItem Text="Yes" Value="True"></asp:ListItem>
									<asp:ListItem Text="No" Value="False"></asp:ListItem>
								</asp:RadioButtonList>
							</p>
							<p>
								Comments:<br />
								<asp:TextBox ID="txtQ8" runat="server" TextMode="MultiLine" Rows="5" Columns="80" />
							</p>
							<p>
								<asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click" />
							</p>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
