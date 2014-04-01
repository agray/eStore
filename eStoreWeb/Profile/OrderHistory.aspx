<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="eStoreWeb.Profile.OrderHistory" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb"%>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<%@ Register Src="~/Controls/OrderHistoryTable.ascx" TagName="OrderHistory" TagPrefix="eStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<div id="DivMain">
		<h1>Order History</h1>
        <eStore:OrderHistory ID="OrderHistoryTable" runat="server" />	
	</div>
</asp:Content>
