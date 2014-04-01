<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheckOutMaster.Master" AutoEventWireup="true" CodeBehind="Checkout1.aspx.cs" Inherits="eStoreWeb.checkout1" %>
<%@ MasterType VirtualPath="~/MasterPages/CheckOutMaster.Master" %>

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
                            <span class="CheckOutDoing">1. Billing details</span>
                            <span class="CheckOutNotDone"> - 2. Shipping details - 3. Payment details - 4. Place order</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle">
                            <h1>1. Enter your billing details</h1>
                        </td>
                        <td style="text-align:right">
                            <asp:Image ID="SecureShopping" runat="server"
                                       ImageUrl="~/Images/System/SecureShopping.gif"/>
                        </td>
                    </tr>
                </table>
                <fieldset class="CheckOut">
                    <legend>Billing Details</legend>
                    <table>
                        <tr>
                            <th>Email Address:</th>
                            <td align="left">
                                <asp:TextBox ID="EmailTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="30"/>
                                <asp:RequiredFieldValidator ID="EmailTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="EmailTextBox"
                                                            ValidationGroup="CheckOut1Group" 
                                                            Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="EmailTextBoxREV" runat="server" 
                                                                ErrorMessage="Invalid Email Address" 
                                                                ControlToValidate="EmailTextBox" 
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                                ValidationGroup="CheckOut1Group"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>First Name:</th>
                            <td align="left">
                                <asp:TextBox ID="CustomerFirstNameTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="30"/>
                                <asp:RequiredFieldValidator ID="CustomerFirstNameTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CustomerFirstNameTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="CustomerFirstNameREV" runat="server"
                                                                ControlToValidate="CustomerFirstNameTextBox"
                                                                ValidationExpression="^[a-zA-Z\-]*$"
                                                                ValidationGroup="CheckOut1Group"
                                                                ErrorMessage="Invalid First Name"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Last Name:</th>
                            <td align="left">
                                <asp:TextBox ID="CustomerLastNameTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="30"/>
                                <asp:RequiredFieldValidator ID="CustomerLastNameTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CustomerLastNameTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="CustomerLastNameREV" runat="server"
                                                                ControlToValidate="CustomerLastNameTextBox"
                                                                ValidationExpression="^[a-zA-Z\-]*$"
                                                                ValidationGroup="CheckOut1Group"
                                                                ErrorMessage="Invalid Last Name"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Address:</th>
                            <td align="left">
                                <asp:TextBox ID="CustomerAddressTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="40"/>
                                <asp:RequiredFieldValidator ID="CustomerAddressTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CustomerAddressTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic"/>
                            </td>
                        </tr>
                        <tr>
                            <th>Suburb/City:</th>
                            <td align="left">
                                <asp:TextBox ID="CustomerSuburbTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="35"/>
                                <asp:RequiredFieldValidator ID="CustomerSuburbTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CustomerSuburbTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic"/>
                                <asp:RegularExpressionValidator ID="CustomerSuburbREV" runat="server"
                                                                ControlToValidate="CustomerSuburbTextBox"
                                                                ValidationExpression="^[a-zA-Z\-]*$"
                                                                ValidationGroup="CheckOut1Group"
                                                                ErrorMessage="Invalid Suburb/City"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>State/Province/Region:</th>
                            <td align="left">
                                <asp:TextBox ID="CustomerStateTextBox" runat="server"
                                             MaxLength="50"
                                             Columns="50"/>
                                <asp:RequiredFieldValidator ID="CustomerStateTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CustomerStateTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic"/>
                                <asp:RegularExpressionValidator ID="CustomerStateREV" runat="server"
                                                                ControlToValidate="CustomerStateTextBox"
                                                                ValidationExpression="^[a-zA-Z]*$"
                                                                ValidationGroup="CheckOut1Group"
                                                                ErrorMessage="Invalid State/Province/Region"
                                                                Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Zip/Postcode:</th>
                            <td align="left">    
                                <asp:TextBox ID="CustomerPostcodeTextBox" runat="server"
                                             MaxLength="10"
                                             Columns="10"/>
                                <asp:RequiredFieldValidator ID="CustomerPostcodeTextBoxRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CustomerPostcodeTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <th>Country:</th>
                            <td align="left">
                                <country:CountryDDL ID="countryDDL" runat="server"
                                                    OnSelectedIndexChanged="countryDDL_SelectedIndexChanged" />
                            </td>
                        </tr>
                        <%if(isLoggedIn()) { %>
                            <tr>
                                <td align="right">
                                    <asp:CheckBox ID="CurrentAddressCheckBox" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="CurrentAddressLabel" runat="server"
                                               AssociatedControlID="CurrentAddressCheckBox" 
                                               Text="Yes, save this as my current billing address"/>
                                </td>
                            </tr>
                        <%} %>
                        <%--<tr>
                            <td align="left">
                                <%--<cc2:CaptchaControl ID="CaptchaControl1" runat="server" 
                                                    CaptchaFont="Tahoma" 
                                                    CaptchaTimeout="600"
                                                    CaptchaChars="9" 
                                                    Height="15px"
                                                    Width="169px"
                                                    ForeColor="Blue" />--%>
                                                    <%--CaptchaChars="9"--%>
                                                    
                                <%--<cc1:CaptchaControl ID="ccCaptcha" runat="server" 
                                                    CaptchaBackgroundNoise="High"
                                                    CaptchaLength="5" 
                                                    CaptchaHeight="35" 
                                                    CaptchaWidth="100" 
                                                    CaptchaLineNoise="High"
                                                    CaptchaMinTimeout="5" 
                                                    CaptchaMaxTimeout="240"
                                                    CaptchaFontWarping="High" />--%>
                            <%--</td>
                            <td align="left">
                                <asp:TextBox ID="CaptchaTextBox" runat="server" />
                                <asp:RequiredFieldValidator ID="CaptchaRFV" runat="server" 
                                                            ErrorMessage="Required Field"
                                                            ControlToValidate="CaptchaTextBox"
                                                            ValidationGroup="CheckOut1Group"
                                                            Display="Dynamic" />
                                <asp:CustomValidator ID="CaptchaCV" runat="server"
                                                     ControlToValidate="CaptchaTextBox"
                                                     OnServerValidate="CaptchaTextBox_CustomServerValidate"
                                                     ErrorMessage="Invalid Code"
                                                     ValidationGroup="CheckOut1Group"
                                                     Enabled="true" />
                            </td>
                        </tr>--%>
                    </table>
                </fieldset>
                <p>
                    <asp:Button ID="BackButton" Text="Back" OnClick="GoToViewCartPage" runat="server" />
                    <asp:Button ID="NextButton" Text="Next" OnClick="GoToNextCheckoutPage" ValidationGroup="CheckOut1Group" runat="server" />
                </p>
            </td>
        </tr>
    </table>
</asp:Content>