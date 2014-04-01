<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eStoreWeb.Profile.Login" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb"%>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/" Text="Home" />&nbsp;&gt;&nbsp;
        <asp:HyperLink ID="Login_Breadcrumb" runat="server" Text="Registration &amp; Login" />
        <hr />
        <h1>Membership Sign In</h1>
        <h2>Member Login</h2>
        <asp:Login ID="Login1" runat="server"
                   OnLoggingIn="Login_OnLoggingIn"
                   OnLoginError="Login_OnLoginError"
                   FailureText="Invalid username/password."
                   BackColor="#F7F6F3" 
                   BorderColor="#E6E2D8" 
                   BorderPadding="4" 
                   BorderStyle="Solid" 
                   BorderWidth="1px" 
                   Font-Names="Verdana" 
                   ForeColor="#333333">
            <LoginButtonStyle BackColor="#FFFBFF" 
                              BorderColor="#CCCCCC" 
                              BorderStyle="Solid" 
                              BorderWidth="1px" 
                              Font-Names="Verdana" 
                              ForeColor="#284775" />
            <LayoutTemplate>
                <table border="0" 
                       cellpadding="4" 
                       cellspacing="0" 
                       style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="EmailLabel" runat="server" 
                                                   AssociatedControlID="UserName" 
                                                   Text="Email:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" />
                                        <asp:RequiredFieldValidator ID="EmailAddressRFV" runat="server" 
                                                                    ControlToValidate="UserName" 
                                                                    ErrorMessage="Email is required." 
                                                                    ToolTip="Email is required." 
                                                                    ValidationGroup="Login1" 
                                                                    Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" 
                                                   AssociatedControlID="Password"
                                                   Text="Password:"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" 
                                                     Font-Size="0.8em" 
                                                     TextMode="Password" />
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                                    ControlToValidate="Password" 
                                                                    ErrorMessage="Password is required." 
                                                                    ToolTip="Password is required." 
                                                                    ValidationGroup="Login1"
                                                                    Text="*"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" 
                                                      Text="Remember me next time." />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" 
                                                     EnableViewState="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" 
                                                    BackColor="#FFFBFF" 
                                                    BorderColor="#CCCCCC" 
                                                    BorderStyle="Solid" 
                                                    BorderWidth="1px" 
                                                    CommandName="Login" 
                                                    Font-Names="Verdana" 
                                                    Font-Size="0.8em" 
                                                    ForeColor="#284775" 
                                                    Text="Log In" 
                                                    ValidationGroup="Login1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TitleTextStyle BackColor="#5D7B9D" 
                            Font-Bold="True" 
                            Font-Size="0.9em" 
                            ForeColor="White" />
        </asp:Login>
        <p>
            Forgot your password? 
            <asp:HyperLink ID="RemindPasswordHyperlink" runat="server"
                       Text="Click here"
                       NavigateUrl="~/Home/RemindPassword.aspx"/>
        </p>
        
        <h2>Membership Sign Up</h2>
        <asp:Button ID="RegisterButton" runat="server" 
                    Text="GET FREE MEMBERSHIP NOW" 
                    Font-Size="Smaller"
                    OnClick="RegisterButton_Click"/>
    </div>
</asp:Content>