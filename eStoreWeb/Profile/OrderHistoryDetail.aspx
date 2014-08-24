<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="OrderHistoryDetail.aspx.cs" Inherits="eStoreWeb.Profile.OrderHistoryDetail" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Reference Control="~/Controls/OrderHistoryDetailTable.ascx" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<%@ Register Src="~/Controls/OrderHistoryDetailTable.ascx" TagName="OrderHistoryDetailTable" TagPrefix="eStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Stylesheets/PP_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="OrderHistoryDetailUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
	        <div id="DivMain">
		        <h1>Order History Detail</h1>
                <eStore:OrderHistoryDetailTable ID="OrderHistoryDetailTable" runat="server" />	
	        </div>
	    </ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>