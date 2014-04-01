<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="eStoreWeb.Profile.AccountInfo" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb"%>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <h1>Account Management</h1>
        <h3><asp:HyperLink ID="OrderHistoryHyperlink" runat="server" 
                           Text="QuikOrder from Order History"
                           NavigateUrl="~/Profile/OrderHistory.aspx"/></h3>
        <asp:Literal runat="server" 
                     Text="Review your past purchases"/><br/><br/>
        <h3><asp:HyperLink ID="ChangeAddressesHyperlink" runat="server" 
                           Text="Change Billing or Shipping Addresses"
                           NavigateUrl="~/Profile/AccountAddress.aspx"/></h3>
        <asp:Literal runat="server" 
                     Text="Modify your billing or shipping information"/><br/><br/>
        <h3><asp:HyperLink ID="ChangePasswordHyperlink" runat="server" 
                           Text="Change Password"
                           NavigateUrl="~/Profile/ChangeEmailPassword.aspx"/></h3>
        <asp:Literal runat="server" 
                     Text="Update your e-mail address, password and contact information"/><br/><br/>
        <h3><asp:HyperLink ID="ContactPreferenceHyperlink" runat="server" 
                           Text="Contact Preferences"
                           NavigateUrl="~/Profile/ContactPreference.aspx"/></h3>
        <asp:Literal runat="server" 
                     Text="Update your contact preferences"/><br/><br/>
        <h3><asp:HyperLink ID="SavedSearchesHyperLink" runat="server" 
                           Text="Saved Searches"
                           NavigateUrl="~/Profile/SavedSearches.aspx"/></h3>
        <asp:Literal runat="server" 
                     Text="Submit a past search"/><br/><br/>
        <h3><asp:HyperLink ID="LogoutHyperlink" runat="server" 
                           Text="Log Out"
                           NavigateUrl="~/Profile/LogOut.aspx"/></h3>
        <asp:Literal runat="server" 
                     Text="Log Out"/>
    </div>
</asp:Content>
