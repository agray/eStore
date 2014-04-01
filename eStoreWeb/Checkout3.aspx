<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheckOutMaster.Master" AutoEventWireup="true" CodeBehind="Checkout3.aspx.cs" Inherits="eStoreWeb.checkout3" %>
<%@ MasterType VirtualPath="~/MasterPages/CheckOutMaster.Master" %>

<%@ Register tagprefix="cardType" tagname="CardTypeDDL" src="~/Controls/CardTypeDDL.ascx" %>
<%@ Register tagprefix="expiryMonth" tagname="ExpiryMonthDDL" src="~/Controls/ExpiryMonthDDL.ascx" %>
<%@ Register tagprefix="expiryYear" tagname="ExpiryYearDDL" src="~/Controls/ExpiryYearDDL.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td valign="top" style="width:800px">
                <table width="100%">
                    <tr style="text-align:center">
                        <td colspan="2">
                            <span class="CheckOutDone">1. Billing details - 2. Shipping details</span>
                            <span class="CheckOutDoing"> - 3. Payment details</span>
                            <span class="CheckOutNotDone"> - 4. Place order</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle">
                            <h1>3. Enter your payment details</h1>
                        </td>
                        <td style="text-align:right">
                            <asp:Image ID="SecureShopping" runat="server"
                                       ImageUrl="~/Images/System/SecureShopping.gif"/>
                        </td>
                    </tr>
                </table>
                <fieldset class="CheckOut">
                    <legend>Payment Details</legend>
                    <table>
                        <tr>
                            <th style="vertical-align: top; padding: 8px 0px 0px 0px">
                                Payment method:
                            </th>
                            <td align="left">
                                <asp:RadioButtonList ID="PaymentMethodRadioButtonList" runat="server" 
                                                     AutoPostBack="true"
                                                     OnSelectedIndexChanged="PaymentMethodRadioButtonList_SelectedIndexChanged">
                                    <asp:ListItem runat="server"
                                                  Value="PayPal" 
                                                  Text="&lt;img src=&#39;/Images/System/PayPal.gif&#39;&gt;" />
                                    <asp:ListItem runat="server"
                                                  Value="CC" 
                                                  Text="&lt;img src=&#39;/Images/System/VisaCard.gif&#39;&gt; 
                                                        &lt;img src=&#39;/Images/System/MasterCard.gif&#39;&gt; 
                                                        &lt;img src=&#39;/Images/System/AmexCard.gif&#39;&gt;"
                                                  Selected="True" />
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <a href='https://personal.paypal.com/cgi-bin/marketingweb?cmd=_render-content&content_ID=marketing_us/How_does_PayPal_work'>What is Paypal?</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="CreditCardDetailPanel" runat="server">
                        <ContentTemplate>
                            <fieldset>
	                            <legend>Credit Card details</legend>
                                <table>
                                    <tr>
                                        <th>Name on Card:</th>
                                        <td align="left">
                                            <asp:TextBox ID="CardholderTextBox" runat="server" 
                                                         MaxLength="50"
                                                         Columns="25"
                                                         Font-Size="Small"/>
                                            <asp:RequiredFieldValidator ID="CardholderTextBoxRequiredFieldValidator" runat="server" 
                                                                        ErrorMessage="Required Field"
                                                                        ControlToValidate="CardholderTextBox"
                                                                        ValidationGroup="CheckOut3Group"
                                                                        Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="CardholderREV" runat="server"
                                                                            ControlToValidate="CardholderTextBox"
                                                                            ValidationExpression="^[a-zA-Z\- ]*$"
                                                                            ValidationGroup="CheckOut3Group"
                                                                            ErrorMessage="Invalid Card Holder Name"
                                                                            Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Card Type:</th>
                                        <td align="left">
                                            <cardType:CardTypeDDL ID="cardTypeDDL" runat="server" />
                                            <asp:RequiredFieldValidator ID="CardTypeDropDownListRequiredFieldValidator" runat="server" 
                                                                        ErrorMessage="Required Field"
                                                                        ControlToValidate="CardTypeDDL:CardTypeDropDownList"
                                                                        ValidationGroup="CheckOut3Group"
                                                                        Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Card Number:</th>
                                        <td align="left">
                                            <asp:TextBox ID="CardNumberTextBox" runat="server" 
                                                         MaxLength="16"
                                                         Columns="18"
                                                         Font-Size="Small"/>
                                            <asp:RequiredFieldValidator ID="CardNumberTextBoxRequiredFieldValidator" runat="server" 
                                                                        ErrorMessage="Required Field"
                                                                        ControlToValidate="CardNumberTextBox"
                                                                        ValidationGroup="CheckOut3Group"
                                                                        Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="CardNumberREV" runat="server"
                                                                            ControlToValidate="CardNumberTextBox"
                                                                            ValidationExpression="^\d+$"
                                                                            ValidationGroup="CheckOut3Group"
                                                                            ErrorMessage="Invalid Card Number"
                                                                            Display="Dynamic" />
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Card Validation Value (CVV):</th>
                                        <td align="left">
                                            <asp:TextBox ID="CardValidationValueTextBox" runat="server" 
                                                         MaxLength="4"
                                                         Columns="6"
                                                         Font-Size="Small"/>
                                            <asp:HyperLink ID="CVVHyperlink" runat="server" 
                                                           Text="What is this?"
                                                           NavigateUrl="~/CVV.aspx"
                                                           Target="_blank"
                                                           Font-Size="Small" />
                                            <asp:RequiredFieldValidator ID="CardValidationValueTextBoxRequiredFieldValidator" runat="server" 
                                                                        ErrorMessage="Required Field"
                                                                        ControlToValidate="CardValidationValueTextBox"
                                                                        ValidationGroup="CheckOut3Group"
                                                                        Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="CVVREV" runat="server"
                                                                            ControlToValidate="CardValidationValueTextBox"
                                                                            ValidationExpression="^\d+$"
                                                                            ValidationGroup="CheckOut3Group"
                                                                            ErrorMessage="Invalid CVV"
                                                                            Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Expiry Date:</th>
                                        <td align="left">
                                            <expiryMonth:ExpiryMonthDDL ID="ExpiryMonthDDL" runat="server" />
                                            <expiryYear:ExpiryYearDDL ID="ExpiryYearDDL" runat="server" />
                                                                                    
                                            <asp:RequiredFieldValidator ID="ExpiryMonthDDLRequiredFieldValidator" runat="server" 
                                                                        ErrorMessage="Required Field"
                                                                        ControlToValidate="ExpiryMonthDDL:ExpiryMonthDropDownList"
                                                                        ValidationGroup="CheckOut3Group"
                                                                        Display="Dynamic" />
                                            <asp:RequiredFieldValidator ID="ExpiryYearDDLRequiredFieldValidator" runat="server" 
                                                                        ErrorMessage="Required Field"
                                                                        ControlToValidate="ExpiryYearDDL:ExpiryYearDropDownList"
                                                                        ValidationGroup="CheckOut3Group"
                                                                        Display="Dynamic" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
                <p>           
                    <asp:Label ID="NoteLabel" runat="server" />
                </p>
                <p>
                    <asp:Button ID="BackButton" runat="server" 
                                Text="Back"
                                OnClick="GoToPreviousPage"
                                CausesValidation="false" />
                    <asp:Button ID="NextButton" runat="server" 
                                Text="Next"
                                OnClick="GoToNextCheckoutPage"
                                ValidationGroup="CheckOut3Group"
                                CausesValidation="true" />
                </p>
            </td>
        </tr>
    </table>
</asp:Content>