<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheckOutMaster.Master" AutoEventWireup="true" CodeBehind="Checkout2.aspx.cs" Inherits="eStoreWeb.Checkout2" %>
<%@ MasterType VirtualPath="~/MasterPages/CheckOutMaster.Master" %>
<%@ Reference Control="~/Controls/CountryDDL.ascx" %>
<%@ Reference Control="~/Controls/YesNoDDL.ascx" %>

<%@ Register tagprefix="yesNo" tagname="YesNoDDL" src="~/Controls/YesNoDDL.ascx" %>
<%@ Register tagprefix="country" tagname="CountryDDL" src="~/Controls/CountryDDL.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td valign="top" width="800px">
                <table width="100%">
                    <tr style="text-align:center">
                        <td colspan="2">
                            <span class="CheckOutDone">1. Billing details</span>
                            <span class="CheckOutDoing"> - 2. Shipping details</span>
                            <span class="CheckOutNotDone"> - 3. Payment details - 4. Place order</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle">
                            <h1>2. Enter your shipping details</h1>
                        </td>
                        <td style="text-align:right">
                            <asp:Image ID="SecureShopping" runat="server"
                                       ImageUrl="~/Images/System/SecureShopping.gif"/>
                        </td>
                    </tr>
                </table>
                <fieldset class="CheckOut">
                    <legend>Shipping Details</legend>
                    <table>
                        <tr>
                            <th>Same as Billing Address?</th>
                            <td align="left">
                                <yesNo:YesNoDDL ID="sameAddressDDL" runat="server"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="SameAddressDDL_SelectedIndexChanged" />
                                <%--<asp:DropDownList ID="SameAddressDropDownList" runat="server" 
                                                  AutoPostBack="True" 
                                                  OnSelectedIndexChanged="SameAddressDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="ShippingFieldsPanel" runat="server">
                    <table>
                        <tr>
                            <th>First Name:</th>
                            <td align="left">
                                <asp:TextBox ID="ShippingFirstNameTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="30"/>
                                <asp:RequiredFieldValidator ID="ShippingFirstNameTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="ShippingFirstNameTextBox"
                                                            ValidationGroup="CheckOut2Group"
                                                            InitialValue=""
                                                            Display="Dynamic"/>
                                <asp:RegularExpressionValidator ID="ShippingFirstNameREV" runat="server"
                                                                ControlToValidate="ShippingFirstNameTextBox"
                                                                ValidationExpression="^[a-zA-Z\-]*$"
                                                                ValidationGroup="CheckOut2Group"
                                                                ErrorMessage="Invalid First Name"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Last Name:</th>
                            <td align="left">
                                <asp:TextBox ID="ShippingLastNameTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="30"/>
                                <asp:RequiredFieldValidator ID="ShippingLastNameTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="ShippingLastNameTextBox"
                                                            ValidationGroup="CheckOut2Group"
                                                            InitialValue=""
                                                            Display="Dynamic"/>
                                <asp:RegularExpressionValidator ID="ShippingLastNameREV" runat="server"
                                                                ControlToValidate="ShippingLastNameTextBox"
                                                                ValidationExpression="^[a-zA-Z\-]*$"
                                                                ValidationGroup="CheckOut2Group"
                                                                ErrorMessage="Invalid Last Name"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Address:</th>
                            <td align="left">
                                <asp:TextBox ID="ShippingAddressTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="40"/>
                                <asp:RequiredFieldValidator ID="ShippingAddressTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="ShippingAddressTextBox"
                                                            ValidationGroup="CheckOut2Group"
                                                            InitialValue=""
                                                            Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>Suburb/City:</th>
                            <td align="left">
                                <asp:TextBox ID="ShippingSuburbTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="35"/>
                                <asp:RequiredFieldValidator ID="ShippingSuburbTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="ShippingSuburbTextBox"
                                                            ValidationGroup="CheckOut2Group"
                                                            InitialValue=""
                                                            Display="Dynamic"/>
                                <asp:RegularExpressionValidator ID="ShippingSuburbREV" runat="server"
                                                                ControlToValidate="ShippingSuburbTextBox"
                                                                ValidationExpression="^[a-zA-Z\-]*$"
                                                                ValidationGroup="CheckOut2Group"
                                                                ErrorMessage="Invalid Suburb/City"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>State/Province/Region:</th>
                            <td align="left">
                                <asp:TextBox ID="ShippingStateTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="50"/>
                                <asp:RequiredFieldValidator ID="ShippingStateTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="ShippingStateTextBox"
                                                            ValidationGroup="CheckOut2Group"
                                                            InitialValue=""
                                                            Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="ShippingStateREV" runat="server"
                                                                ControlToValidate="ShippingStateTextBox"
                                                                ValidationExpression="^[a-zA-Z]*$"
                                                                ValidationGroup="CheckOut2Group"
                                                                ErrorMessage="Invalid State/Province/Region"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Zip/Postcode:</th>
                            <td align="left">
                                <asp:TextBox ID="ShippingPostcodeTextBox" runat="server"
                                             MaxLength="10"
                                             Columns="10"/>
                                <asp:RequiredFieldValidator ID="ShippingPostcodeTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="ShippingPostcodeTextBox"
                                                            ValidationGroup="CheckOut2Group"
                                                            InitialValue=""
                                                            Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>Country:</th>
                            <td align="left">
                                <country:CountryDDL ID="countryDDL" runat="server"
                                                    OnSelectedIndexChanged="countryDDL_SelectedIndexChanged" />
                            </td>
                        </tr>
                    
                </asp:Panel>
                    <%if(IsLoggedIn()) { %>
                        <tr>
                            <td align="right">
                                <asp:CheckBox ID="CurrentAddressCheckBox" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="CurrentAddressLabel" runat="server"
                                           AssociatedControlID="CurrentAddressCheckBox" 
                                           Text="Yes, save this as my current shipping address"/>
                            </td>
                        </tr>
                    <%} %>
                    </table>
                </fieldset>
                <p>
                    <asp:Button ID="BackButton" runat="server"
                                Text="Back" 
                                CausesValidation="false"
                                ValidationGroup="CheckOut2Group"
                                OnClick="GoToPreviousPage" />
                    <asp:Button ID="NextButton" runat="server"
                                Text="Next"
                                CausesValidation="true"
                                ValidationGroup="CheckOut2Group"
                                OnClick="GoToNextCheckoutPage" />
                </p>
            </td>
        </tr>
    </table>
</asp:Content>