<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="Vitals.aspx.cs" Inherits="Vitals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/Vital.js" type="text/javascript"></script>         
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <asp:HiddenField ID="HDPatientID" runat="server" />
    <div id="VitalDetailsDiv" class="VitalDetailsDiv">
    </div>
    <div id="AddVitalDiv">
        <table width="410" border="0" cellpadding="8" cellspacing="0" class="border">
            <tr>
                <td width="142" ><strong>Weight (lbs)</strong><span class="Validation_StarMark_Color">*</span></td>
                <td width="246">
                    <label>
                        <input type="text" class="FormField" id="txtweight" size="10" onkeypress="return check_digit(event,this,8,2);" />
                        (Enter numeric values only)</label>
                </td>
            </tr>
            <tr>
                <td ><strong>Blood Pressure</strong></td>
                <td>
                    <input type="text" class="FormField" id="txtbloodPres" size="10" maxlength="7" /></td>
            </tr>
            <tr>
                <td ><strong>Temperature</strong></td>
                <td>
                    <input type="text" class="FormField" id="txttempr" size="10" maxlength="6" /></td>
            </tr>
            <tr>
                <td ><strong>Pulse</strong></td>
                <td>
                    <input type="text" class="FormField" id="txtpulse" size="10" maxlength="3" /></td>
            </tr>
            <tr>
                <td><strong>Waist Circumference (in)</strong></td>
                <td>
                    <input type="text" class="FormField" id="txtwaistcir" size="10" onkeypress="return check_digit(event,this,8,2);" />
                    (Enter numeric values only)
                    
                </td>
            </tr>
            <tr>
                <td ><strong>Hip Circumference (in)</strong></td>
                <td>
                    <input type="text" class="FormField" id="txthipcirm" size="10" onkeypress="return check_digit(event,this,8,2);" />
                    (Enter numeric values only)
                    
                </td>
            </tr>
            <tr>
                <td ><strong>Height (in)</strong><span class="Validation_StarMark_Color">*</span></td>
                <td>
                    <input type="text" class="FormField" id="txtheight" size="10" onkeypress="return check_digit(event,this,8,2);" />
                    (Enter numeric values only)
                   
                </td>
            </tr>
            <tr>
                <td ><strong>Left Grip</strong></td>
                <td>
                    <input type="text" class="FormField" id="txtLeftGrip" size="10" maxlength="6" />
                    lbs</td>
            </tr>
            <tr>
                <td ><strong>Right Grip</strong></td>
                <td>
                    <input type="text" class="FormField" id="txtRightGrip" size="10" maxlength="6"/>
                    lbs</td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="button" id="btnSubmit" value="Submit" class="button" onclick="return SaveVitalData();" />                   
                    <input type="button" id="btnUpdate" value="Update" class="button" onclick="return SaveVitalData();" />
                    <input type="button" id="btnCancle" value="Cancel" class="button"/>
                </td>
            </tr>
        </table>
    </div>   
</asp:Content>
