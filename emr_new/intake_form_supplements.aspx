<%@ Page Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_supplements.aspx.cs" EnableViewState="true" Inherits="intake_form_supplements" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">


    <script type="text/javascript">

        // get all the selected checkbox values as 1 and unselect as 0
        //jaswinder 23rd aug 2013
        function ReadChkboxlist() {

            var strSuppliment = ($("input[type=checkbox]").map(function () { return $(this).is(':checked') ? 1 : 0 }).get().join(","));
            strSuppliment = strSuppliment + ',' + $("input[type='radio']:checked").val();
            $('[id$=hdnChkSupplimentlist]').val(strSuppliment);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <span class="style1">Nutrients / Supplements</span>
        <br>
        Check those that you are taking:
    </p>

    <table width="900" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="regText">
                        <td width="25">
                            <input name="multi_vitamin_YN" type="checkbox" id="multi_vitamin_YN" value="1"></td>
                        <td>Multi-Vitamin / Mineral (any brand) - Is this one a day? 
                         <input name="multi_vitamin_oneaday_YN" type="radio" value="1">
                            Y
                            <input name="multi_vitamin_oneaday_YN" type="radio" value="0" checked>
                            N </td>
                        <td width="25">
                            <input name="cal_mag_combo_YN" type="checkbox" id="cal_mag_combo_YN" value="1"></td>
                        <td width="400">Calcium / Magnesium Combination </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <input name="vitamin_a_YN" type="checkbox" id="vitamin_a_YN" value="1"></td>
                        <td>Vitamin A Complex</td>
                        <td>
                            <input name="vitamin_b_YN" type="checkbox" id="vitamin_b_YN" value="1"></td>
                        <td>Vitamin B Complex</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <input name="vitamin_c_YN" type="checkbox" id="vitamin_c_YN" value="1"></td>
                        <td>Vitamin C Complex</td>
                        <td>
                            <input name="vitamin_e_YN" type="checkbox" id="vitamin_e_YN" value="1"></td>
                        <td>Vitamin E Complex</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <input name="chon_gluc_combo_YN" type="checkbox" id="chon_gluc_combo_YN" value="1"></td>
                        <td>Chondroitin / Glucoseamine Combination </td>
                        <td>
                            <input name="fish_oil_YN" type="checkbox" id="fish_oil_YN" value="1"></td>
                        <td>Fish Oil </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <input name="other_oil_YN" type="checkbox" id="other_oil_YN" value="1"></td>
                        <td>Other Oil </td>
                        <td>
                            <input name="co_q_10_YN" type="checkbox" id="co_q_10_YN" value="1"></td>
                        <td>Co Q 10 </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <input name="herbal_blend_YN" type="checkbox" id="herbal_blend_YN" value="1"></td>
                        <td>Herbal Blend for Hormones </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <table width="900" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr class="border">
                        <td width="50">other</td>
                        <td>
                            <textarea name="other_Suppliments" cols="50" rows="3" class="border" id="other_Suppliments" runat="server"></textarea></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="900" border="0" cellpadding="10" cellspacing="0">
        <tr>
            <td>
                <div align="center">

                    <asp:Button CssClass="button" ID="btnNext" Text="Next Page ->" runat="server" OnClientClick="ReadChkboxlist();" OnClick="btnNext_Click" />

                </div>
            </td>
        </tr>
        <asp:HiddenField ID="hdnChkSupplimentlist" runat="server" EnableViewState="true" />
    </table>



    <p>&nbsp;</p>
    <p>&nbsp;</p>
</asp:Content>
