<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TimeDropDown.ascx.cs" Inherits="Controls_TimeDropDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="ScsComboBox" Namespace="SCS.WebControls" TagPrefix="scs" %>
<style>
    #MainContent_EventDetail_txtStartTime_MainDrop_Wrapper_ListBoxWrapper
    {
        top:80px !important;
        left:250px !important;
    }
   #MainContent_EventDetail_txtEndTime_MainDrop_Wrapper_ListBoxWrapper
    {
        top:100px !important;
        left:250px !important;
    }
    #MainContent_EventDetail_txtEndTime_MainDrop_ComboBoxTextBox, #MainContent_EventDetail_txtStartTime_MainDrop_ComboBoxTextBox
    {
     border: 1px solid #003366;
    font-family: Verdana, Arial, Helvetica, sans-serif;
    font-size: 10px;
    background-color: #F5EEE0;
    
    }
</style>
<scs:ScsComboBox ID="MainDrop" runat="server" EnableFreeText="true"  />
	
