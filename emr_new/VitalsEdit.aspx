<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="VitalsEdit.aspx.cs" Inherits="VitalsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <script src="Scripts/jquery.filter_input.js" type="text/javascript"></script>
    <script type='text/javascript'>
        $(function () {

            // only numbers are allowed

            $('#<%= txtHeight.ClientID %>').filter_input({ regex: '[0-9.]' });
            $('#<%= txtWeight.ClientID %>').filter_input({ regex: '[0-9.]' });
            $('#<%= txtHip.ClientID %>').filter_input({ regex: '[0-9.]' });
            $('#<%= txtWaist.ClientID %>').filter_input({ regex: '[0-9.]' });
            
           
        });


       
       function validateData() {
        

           if ($('[id$=txtWeight]').val() == "")
            {
                alert("Please enter weight");
                return false;
            }
           else if ($('[id$=txtHeight]').val() == '') {
               alert('Please enter the height');
               return false;
           }
           else {
               return true; 
           }
        }

  
    </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="410" border="0" cellpadding="8" cellspacing="0" class="border">
		<tr>
			<td width="142" nowrap="nowrap">
				<strong>Weight (lbs)</strong>
			</td>
			<td width="246">
				<label>
					<asp:TextBox ID="txtWeight" runat="server" CssClass="FormField" /><br />
					(Enter numeric values only)<%--<asp:CompareValidator ID="valWeight" runat="server" ControlToValidate="txtWeight"
						Type="Double" Operator="DataTypeCheck" ErrorMessage="Must be numeric" Display="Dynamic"
						ForeColor="Red" /></label>
				<asp:RequiredFieldValidator ID="reqWeight" runat="server" ControlToValidate="txtHeight"
					ErrorMessage="Weight is required" Display="Dynamic" ForeColor="Red" />--%>
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Blood Pressure</strong>
			</td>
			<td>
				<asp:TextBox ID="txtBloodPress" runat="server" CssClass="FormField" />
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Temperature</strong>
			</td>
			<td>
				<asp:TextBox ID="txtTemp" runat="server" CssClass="FormField" />
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Pulse</strong>
			</td>
			<td>
				<asp:TextBox ID="txtPulse" runat="server" CssClass="FormField" />
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Waist Circumference (in)</strong>
			</td>
			<td>
				<asp:TextBox ID="txtWaist" runat="server" CssClass="FormField" /><br />
				(Enter numeric values only)
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Hip Circumference (in)</strong>
			</td>
			<td>
				<asp:TextBox ID="txtHip" runat="server" CssClass="FormField" /><br />
				(Enter numeric values only)
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Height (in)</strong>
			</td>
			<td>
				<asp:TextBox ID="txtHeight" runat="server" CssClass="FormField" /><br />
				(Enter numeric values only)
				<%--<asp:CompareValidator ID="valHeight" runat="server" ControlToValidate="txtHeight"
					Type="Double" Operator="DataTypeCheck" ErrorMessage="Must be numeric" Display="Dynamic"
					ForeColor="Red" />
				<asp:RequiredFieldValidator ID="reqHeight" runat="server" ControlToValidate="txtHeight"
					ErrorMessage="Height is required" Display="Dynamic" ForeColor="Red" />--%>
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Left Grip</strong>
			</td>
			<td>
				<asp:TextBox ID="txtLeftGrip" runat="server" CssClass="FormField" />
				lbs
			</td>
		</tr>
		<tr>
			<td nowrap="nowrap">
				<strong>Right Grip</strong>
			</td>
			<td>
				<asp:TextBox ID="txtRightGrip" runat="server" CssClass="FormField" />
				lbs
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="button" OnClick="btnSave_Click"
					OnClientClick="return validateData(); "/>&nbsp;
				<input name="button2" type="button" class="button" id="button2" onclick="MM_goToURL('self','apt_console.aspx?aptid=<%= Request.QueryString["AptID"] %>');return document.MM_returnValue"
					value="Back to Appointment Console" />
			</td>
		</tr>
	</table>
</asp:Content>
