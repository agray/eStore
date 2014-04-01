<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="SavedSearches.aspx.cs" Inherits="eStoreWeb.Profile.SavedSearches" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb"%>
<%@ Reference Control="../Controls/SavedSearchesTable.ascx" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<%@ Register Src="../Controls/SavedSearchesTable.ascx" TagName="SavedSearchesTable" TagPrefix="eStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Stylesheets/PP_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
	<h1>Saved Searches</h1>
    	<eStore:SavedSearchesTable ID="SavedSearchesTable" runat="server" />	
    </div>
</asp:Content>