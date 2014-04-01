<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="ChangeEmailPassword.aspx.cs" Inherits="eStoreWeb.Profile.ChangeEmailPassword" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb"%>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/" Text="Home" />&nbsp;&gt;&nbsp;
        <asp:HyperLink ID="ChangePreferences_Breadcrumb" runat="server" Text="Change Details" />
        <hr />
        <h1>Modify Email & Password</h1>
        <%--<h2>Contact Preferences</h2>
        <asp:CheckBox ID="NewsletterPreference" runat="server" />
        <asp:Label ID="NewletterLabel" runat="server" 
                   AssociatedControlID="NewsletterPreference" 
                   Text="Yes, please contact me about new product updates." />
        <hr />--%>
        <h2>Change Email or Password</h2>
        Once email is changed, account will be disabled and activation email will be send to new address.
        <asp:ChangePassword ID="ChangePassword1" runat="server">
            <ChangePasswordTemplate>
                <table border="0" cellpadding="1" cellspacing="0" 
                       style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="EmailLabel" runat="server" 
                                                   AssociatedControlID="EmailTextBox" 
                                                   Text="Email Address:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="EmailTextBox" runat="server" 
                                                     Font-Size="Smaller"
                                                     MaxLength="128"
                                                     Width="150" />
                                        <asp:RequiredFieldValidator ID="EmailRFV" runat="server" 
                                                                    ControlToValidate="EmailTextBox" 
                                                                    ErrorMessage="Email is required." 
                                                                    ToolTip="Email is required." 
                                                                    ValidationGroup="ChangePasswordVG" />
                                        <asp:RegularExpressionValidator ID="EmailTextBoxREV" runat="server" 
                                                                ErrorMessage="Invalid Email Address" 
                                                                ControlToValidate="EmailTextBox" 
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                                ValidationGroup="ChangePasswordVG"
                                                                Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="CurrentPasswordLabel" runat="server" 
                                                   AssociatedControlID="CurrentPassword" 
                                                   Text="Current Password:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CurrentPassword" runat="server" 
                                                     TextMode="Password"
                                                     Font-Size="Smaller"
                                                     MaxLength="128"
                                                     Width="150" />
                                        <asp:RequiredFieldValidator ID="CurrentPasswordRFV" runat="server" 
                                                                    ControlToValidate="CurrentPassword" 
                                                                    ErrorMessage="Password is required." 
                                                                    ToolTip="Password is required." 
                                                                    ValidationGroup="ChangePasswordVG" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="NewPasswordLabel" runat="server" 
                                                   AssociatedControlID="NewPassword" 
                                                   Text="New Password:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewPassword" runat="server" 
                                                     TextMode="Password"
                                                     Font-Size="Smaller"
                                                     MaxLength="128"
                                                     Width="150" />
                                        <asp:RequiredFieldValidator ID="NewPasswordRFV" runat="server" 
                                                                    ControlToValidate="NewPassword" 
                                                                    ErrorMessage="New Password is required." 
                                                                    ToolTip="New Password is required." 
                                                                    ValidationGroup="ChangePasswordVG" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                                                   AssociatedControlID="ConfirmNewPassword" 
                                                   Text="Confirm New Password:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ConfirmNewPassword" runat="server" 
                                                     TextMode="Password"
                                                     Font-Size="Smaller"
                                                     MaxLength="128"
                                                     Width="150" />
                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRFV" runat="server" 
                                                                    ControlToValidate="ConfirmNewPassword" 
                                                                    ErrorMessage="Confirm New Password is required." 
                                                                    ToolTip="Confirm New Password is required." 
                                                                    ValidationGroup="ChangePasswordVG" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:CompareValidator ID="NewPasswordCV" runat="server" 
                                                              ControlToCompare="NewPassword" 
                                                              ControlToValidate="ConfirmNewPassword" 
                                                              Display="Dynamic" 
                                                              ErrorMessage="The Confirm New Password must match the New Password entry." 
                                                              ValidationGroup="ChangePasswordVG" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" 
                                                     EnableViewState="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="ChangePasswordButton" runat="server"  
                                                    Text="Change"
                                                    Font-Size="Smaller"
                                                    ValidationGroup="ChangePasswordVG"
                                                    OnClick="ChangePassword_Click" />
                                        <asp:Button ID="CancelButton" runat="server" 
                                                    CausesValidation="False" 
                                                    CommandName="Cancel"
                                                    Font-Size="Smaller"
                                                    Text="Cancel"
                                                    OnClick="Cancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ChangePasswordTemplate>
            <SuccessTemplate>
                <table border="0" cellpadding="1" cellspacing="0" 
                    style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Your password has been changed successfully!</td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="ContinueButton" runat="server" 
                                                    CausesValidation="False" 
                                                    OnClick="PasswordChangedContinue_Click"
                                                    Text="Continue" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </SuccessTemplate>
        </asp:ChangePassword>
    </div>
</asp:Content>
