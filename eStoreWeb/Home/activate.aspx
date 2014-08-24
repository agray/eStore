<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="activate.aspx.cs" Inherits="eStoreWeb.Home.activate" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/" Text="Home" />&nbsp;&gt;&nbsp;
        <asp:HyperLink ID="Activate_Breadcrumb" runat="server" Text="Account Activation" />
        <hr />
        <h1>Your <%=ApplicationHandler.Instance.TradingName%> online membership account has been activated.</h1>
        <p>
            To visit the <%=ApplicationHandler.Instance.TradingName%> homepage, go to www.<%=ApplicationHandler.Instance.TradingName.ToLower()%>.com.au or click on the <%=ApplicationHandler.Instance.TradingName%> logo at the top left of the screen.  
            You can also utilise the search options at the top right of the screen, as well as the menus listed beneath the banner.
            <br /><br />
            Should you require any further assistance, please email us at enquiries@<%=ApplicationHandler.Instance.TradingName.ToLower()%>.com.au.
        </p>
</asp:Content>
