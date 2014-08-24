<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="AccountAddress.aspx.cs" Inherits="eStoreWeb.Profile.AccountAddress" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Reference Control="~/Controls/CountryDDL.ascx" %>
<%@ Reference Control="~/Controls/YesNoDDL.ascx" %>

<%@ Register tagprefix="yesNo" tagname="YesNoDDL" src="~/Controls/YesNoDDL.ascx" %>
<%@ Register tagprefix="country" tagname="CountryDDL" src="~/Controls/CountryDDL.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <h1>Account Addresses</h1>
        <fieldset class="CheckOut">
            <legend>Your Billing Address</legend>
        <%--<h2>Your Billing Address</h2>--%>
        <asp:Label ID="AddressCountLabel" runat="server"
                   Visible="false" />
        <asp:Label ID="SameAddressLabel" runat="server"
                   Visible="false" />
        <asp:Label ID="BillingAddressIDLabel" runat="server"
                   Visible="false" />
        <asp:Label ID="ShippingAddressIDLabel" runat="server"
                   Visible="false" />
        <table>
            <tr>
                <th>First Name:</th>
                <td align="left">
                    <asp:TextBox ID="CustomerFirstNameTextBox" runat="server"
                                 MaxLength="50"
                                 Columns="30"
                                 OnTextChanged="CustomerFirstNameTextBox_TextChanged"
                                 AutoPostBack="true"/>
                    <asp:RequiredFieldValidator ID="CustomerFirstNameTextBoxRFV" runat="server" 
                                                ErrorMessage="Required Field"
                                                ControlToValidate="CustomerFirstNameTextBox"
                                                ValidationGroup="AccountAddressGroup"
                                                Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="CustomerFirstNameREV" runat="server"
                                                    ControlToValidate="CustomerFirstNameTextBox"
                                                    ValidationExpression="^[a-zA-Z\-]*$"
                                                    ValidationGroup="AccountAddressGroup"
                                                    ErrorMessage="Invalid First Name"
                                                    Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>Last Name:</th>
                <td align="left">
                    <asp:TextBox ID="CustomerLastNameTextBox" runat="server"
                                 MaxLength="50"
                                 Columns="30"
                                 OnTextChanged="CustomerLastNameTextBox_TextChanged"
                                 AutoPostBack="true"/>
                    <asp:RequiredFieldValidator ID="CustomerLastNameTextBoxRFV" runat="server" 
                                                ErrorMessage="Required Field"
                                                ControlToValidate="CustomerLastNameTextBox"
                                                ValidationGroup="AccountAddressGroup"
                                                Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="CustomerLastNameREV" runat="server"
                                                    ControlToValidate="CustomerLastNameTextBox"
                                                    ValidationExpression="^[a-zA-Z\-]*$"
                                                    ValidationGroup="AccountAddressGroup"
                                                    ErrorMessage="Invalid Last Name"
                                                    Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>Address:</th>
                <td align="left">
                    <asp:TextBox ID="CustomerAddressTextBox" runat="server"
                                 MaxLength="50"
                                 Columns="40"
                                 OnTextChanged="CustomerAddressTextBox_TextChanged"
                                 AutoPostBack="true"/>
                    <asp:RequiredFieldValidator ID="CustomerAddressTextBoxRFV" runat="server" 
                                                ErrorMessage="Required Field"
                                                ControlToValidate="CustomerAddressTextBox"
                                                ValidationGroup="AccountAddressGroup"
                                                Display="Dynamic"/>
                </td>
            </tr>
            <tr>
                <th>Suburb/City:</th>
                <td align="left">
                    <asp:TextBox ID="CustomerSuburbTextBox" runat="server"
                                 MaxLength="50"
                                 Columns="35"
                                 OnTextChanged="CustomerSuburbTextBox_TextChanged"
                                 AutoPostBack="true"/>
                    <asp:RequiredFieldValidator ID="CustomerSuburbTextBoxRFV" runat="server" 
                                                ErrorMessage="Required Field"
                                                ControlToValidate="CustomerSuburbTextBox"
                                                ValidationGroup="AccountAddressGroup"
                                                Display="Dynamic"/>
                    <asp:RegularExpressionValidator ID="CustomerSuburbREV" runat="server"
                                                    ControlToValidate="CustomerSuburbTextBox"
                                                    ValidationExpression="^[a-z A-Z\-]*$"
                                                    ValidationGroup="AccountAddressGroup"
                                                    ErrorMessage="Invalid Suburb/City"
                                                    Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>State/Province/Region:</th>
                <td align="left">
                    <asp:TextBox ID="CustomerStateTextBox" runat="server"
                                 MaxLength="50"
                                 Columns="50"
                                 OnTextChanged="CustomerStateTextBox_TextChanged"
                                 AutoPostBack="true"/>
                    <asp:RequiredFieldValidator ID="CustomerStateTextBoxRFV" runat="server" 
                                                ErrorMessage="Required Field"
                                                ControlToValidate="CustomerStateTextBox"
                                                ValidationGroup="AccountAddressGroup"
                                                Display="Dynamic"/>
                    <asp:RegularExpressionValidator ID="CustomerStateREV" runat="server"
                                                    ControlToValidate="CustomerStateTextBox"
                                                    ValidationExpression="^[a-zA-Z]*$"
                                                    ValidationGroup="AccountAddressGroup"
                                                    ErrorMessage="Invalid State/Province/Region"
                                                    Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>Zip/Postcode:</th>
                <td align="left">    
                    <asp:TextBox ID="CustomerPostcodeTextBox" runat="server"
                                 MaxLength="10"
                                 Columns="10"
                                 OnTextChanged="CustomerPostcodeTextBox_TextChanged"
                                 AutoPostBack="true"/>
                    <asp:RequiredFieldValidator ID="CustomerPostcodeTextBoxRFV" runat="server" 
                                                ErrorMessage="Required Field"
                                                ControlToValidate="CustomerPostcodeTextBox"
                                                ValidationGroup="AccountAddressGroup"
                                                Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <th>Country:</th>
                <td align="left">
                    <country:CountryDDL ID="billingCountryDDL" runat="server"
                                        OnSelectedIndexChanged="billingCountryDDL_SelectedIndexChanged" />
                </td>
            </tr>
        </table>
        </fieldset>
        <fieldset class="CheckOut">
            <legend>Your Shipping Address</legend>
        <%--<h2>Your Shipping Address</h2>--%>
        <b>Same as Billing Address?</b>
        <yesNo:YesNoDDL ID="sameAddressDDL" runat="server"
                        AutoPostBack="true"
                        SelectedValue='<%#SameAddressLabel.Text%>'
                        OnSelectedIndexChanged="SameAddressDDL_SelectedIndexChanged" />
        
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
                                                    ValidationGroup="AccountAddressGroup"
                                                    InitialValue=""
                                                    Display="Dynamic"/>
                        <asp:RegularExpressionValidator ID="ShippingFirstNameREV" runat="server"
                                                        ControlToValidate="ShippingFirstNameTextBox"
                                                        ValidationExpression="^[a-zA-Z\-]*$"
                                                        ValidationGroup="AccountAddressGroup"
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
                                                    ValidationGroup="AccountAddressGroup"
                                                    InitialValue=""
                                                    Display="Dynamic"/>
                        <asp:RegularExpressionValidator ID="ShippingLastNameREV" runat="server"
                                                        ControlToValidate="ShippingLastNameTextBox"
                                                        ValidationExpression="^[a-zA-Z\-]*$"
                                                        ValidationGroup="AccountAddressGroup"
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
                                                    ValidationGroup="AccountAddressGroup"
                                                    InitialValue=""
                                                    Display="Dynamic"/>
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
                                                    ValidationGroup="AccountAddressGroup"
                                                    InitialValue=""
                                                    Display="Dynamic"/>
                        <asp:RegularExpressionValidator ID="ShippingSuburbREV" runat="server"
                                                        ControlToValidate="ShippingSuburbTextBox"
                                                        ValidationExpression="^[a-zA-Z\-]*$"
                                                        ValidationGroup="AccountAddressGroup"
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
                                                    ValidationGroup="AccountAddressGroup"
                                                    InitialValue=""
                                                    Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="ShippingStateREV" runat="server"
                                                        ControlToValidate="ShippingStateTextBox"
                                                        ValidationExpression="^[a-zA-Z]*$"
                                                        ValidationGroup="AccountAddressGroup"
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
                                                    ValidationGroup="AccountAddressGroup"
                                                    InitialValue=""
                                                    Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>Country:</th>
                    <td align="left">
                        <country:CountryDDL ID="shippingCountryDDL" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </fieldset>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Button ID="SaveButton" runat="server" 
                                Text="Save Changes"
                                ValidationGroup="AccountAddressGroup"
                                OnClick="SaveChanges" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
