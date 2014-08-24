<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="RemindPassword.aspx.cs" Inherits="eStoreWeb.Home.RemindPassword" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/" Text="Home" />&nbsp;&gt;&nbsp;
        <asp:HyperLink ID="Remind_Breadcrumb" runat="server" Text="Send Password" />
        <hr />
        <h1>Remind Password</h1>
        <h2>Create New Password</h2>
        <hr/>
        <asp:Label runat="server" 
                   AssociatedControlID="EmailTextBox"
                   Text="Email Address: " 
                   Font-Bold="true" />
        <asp:TextBox ID="EmailTextBox" runat="server" 
                     MaxLength="128" 
                     Width="200px" 
                     Font-Size="Smaller" />
        <asp:Button ID="RemindEmailButton" runat="server" 
                    Text="Send new password" 
                    Font-Size="Smaller"
                    OnClick="RemindPassword_OnClick" />
        <br /><br />
        
        <asp:Label ID="PasswordChangeLabel" runat="server"  
                   Visible="false"
                   Font-Bold="true"
                   ForeColor="Red" />
    </div>
</asp:Content>