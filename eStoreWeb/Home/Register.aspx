<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="eStoreWeb.Home.Register" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb"%>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server"
                          OnCreatingUser="CreateUserWizard_CreatingUser">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="right">
                                <asp:Label ID="FirstNameLabel" runat="server" 
                                           AssociatedControlID="UserName" Text="First Name:"/>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"/>
                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" 
                                                            ControlToValidate="UserName" 
                                                            ErrorMessage="First Name is required." 
                                                            ToolTip="First Name is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LastNameLabel" runat="server" 
                                           AssociatedControlID="LastName" Text="Last Name:"/>
                            </td>
                            <td>
                                <asp:TextBox ID="LastName" runat="server"/>
                                <asp:RequiredFieldValidator ID="LastNameRFV" runat="server" 
                                                            ControlToValidate="LastName" 
                                                            ErrorMessage="Last Name is required." 
                                                            ToolTip="Last Name is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" 
                                           AssociatedControlID="Email"
                                           Text="Email Address:"/>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"/>
                                <asp:RequiredFieldValidator ID="EmailAddressRFV" runat="server" 
                                                            ControlToValidate="Email" 
                                                            ErrorMessage="E-mail is required." 
                                                            ToolTip="E-mail is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
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
                                             TextMode="Password" />
                                <asp:RequiredFieldValidator ID="PasswordRequiredRFV" runat="server" 
                                                            ControlToValidate="Password" 
                                                            ErrorMessage="Password is required." 
                                                            ToolTip="Password is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                           AssociatedControlID="ConfirmPassword"
                                           Text="Confirm Password:"/>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" 
                                             TextMode="Password"/>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRFV" runat="server" 
                                                            ControlToValidate="ConfirmPassword" 
                                                            ErrorMessage="Confirm Password is required." 
                                                            ToolTip="Confirm Password is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" 
                                           AssociatedControlID="Question"
                                           Text="Security Question:"/>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server"/>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                                            ControlToValidate="Question" 
                                                            ErrorMessage="Security question is required." 
                                                            ToolTip="Security question is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" 
                                           AssociatedControlID="Answer"
                                           Text="Security Answer:"/>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRFV" runat="server" 
                                                            ControlToValidate="Answer" 
                                                            ErrorMessage="Security answer is required." 
                                                            ToolTip="Security answer is required." 
                                                            ValidationGroup="CreateUserWizard1"
                                                            Text="*"/>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ReceivesNewsletterLabel" runat="server" 
                                           AssociatedControlID="ReceiveNewsletter"
                                           Text="Receive Newsletter?"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="ReceiveNewsletter" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                                      ControlToCompare="Password" 
                                                      ControlToValidate="ConfirmPassword" 
                                                      Display="Dynamic" 
                                                      ErrorMessage="The Password and Confirmation Password must match." 
                                                      ValidationGroup="CreateUserWizard1"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="ErrorMessage" runat="server" 
                                             EnableViewState="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
