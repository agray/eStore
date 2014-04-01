<%@ Page Title="Error - Page Not Found" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="eStoreWeb.PageNotFound" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Page not found.</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="DivMain">
        <h1>Page not found.</h1>
    </div>
</asp:Content>
