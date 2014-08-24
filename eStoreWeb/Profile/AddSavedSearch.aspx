<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="AddSavedSearch.aspx.cs" Inherits="eStoreWeb.Profile.AddSavedSearch" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Stylesheets/PP_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
	<h1>Add Saved Search</h1>
	<table>
	    <tr>
	        <th>Name:</th>
            <td>
                <asp:TextBox ID="NameTextBox" runat="server"
                             MaxLength="50"/>
                <asp:RequiredFieldValidator ID="NameTextBoxRFV" runat="server" 
                                            ErrorMessage="Required Field"
                                            ControlToValidate="NameTextBox"
                                            ValidationGroup="AddSavedSearchGroup"
                                            InitialValue=""
                                            Display="Dynamic"/>
            </td>
        </tr>
        <tr>
            <th>Criteria:</th>
            <td>
                <asp:TextBox ID="CriteriaTextBox" runat="server"
                             MaxLength="20"/>
                <asp:RequiredFieldValidator ID="CriteriaTextBoxRFV" runat="server" 
                                            ErrorMessage="Required Field"
                                            ControlToValidate="CriteriaTextBox"
                                            ValidationGroup="AddSavedSearchGroup"
                                            InitialValue=""
                                            Display="Dynamic"/>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="AddSavedSearchButton" runat="server" 
                            Text="Save"
                            CausesValidation="true"
                            ValidationGroup="AddSavedSearchGroup"
                            OnClick="SaveSearch" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>