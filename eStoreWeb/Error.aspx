<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="eStoreWeb.ErrorPage" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="DivMain">
        <h2>An unexpected website failure has occured!</h2>
        <div>
          We are sorry for any inconvenience this may have caused you.<br />
          The problem has been logged and our support team has been notified via e-mail.<br />
        </div>
    </div>
</asp:Content>
