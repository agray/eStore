<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheckOutMaster.Master" AutoEventWireup="true" CodeBehind="Checkout4.aspx.cs" Inherits="eStoreWeb.Checkout4" %>
<%@ MasterType VirtualPath="~/MasterPages/CheckOutMaster.Master" %>
<%@ Import Namespace="eStoreWeb" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<%@ Reference Control="Controls/ReviewCartTable.ascx" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<%@ Register Src="~/Controls/ReviewCartTable.ascx" TagName="ReviewCartTable" TagPrefix="cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td valign="top" style="width:800px">
                <table width="100%">
                    <tr style="text-align:center">
                        <td colspan="2">
                            <span class="CheckOutDone">1. Billing details - 2. Shipping details - 3. Payment details</span>
                            <span class="CheckOutDoing"> - 4. Place order</span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle">
                            <h1>4. Please review and place your order </h1>
                        </td>
                        <td style="text-align:right">
                            <asp:Image ID="SecureShopping" runat="server"
                                       ImageUrl="~/Images/System/SecureShopping.gif"/>
                        </td>
                    </tr>
                </table>
                <fieldset class="CheckOut">
                    <legend>Cart Details</legend>
                    <cart:ReviewCartTable ID="ReviewCartTable" runat="server" />
                </fieldset>
                
                <fieldset class="CheckOut">
                    <legend>Shipping Details</legend>
                    <table>
                        <tbody>
                            <tr>
                                <th>Name:</th>
                                <td align="left">
                                    <span id="ShippingNameLabel"><%=SessionHandler.Instance.ShippingFirstName%>&nbsp;<%=SessionHandler.Instance.ShippingLastName%></span>
                                </td>
                            </tr>
                            <tr>
                                <th>Address:</th>
                                <td align="left">
                                    <span id="ShippingAddressLabel"><%=SessionHandler.Instance.ShippingAddress%></span>
                                </td>
                            </tr>
                            <tr>
                                <th>Suburb/City:</th>
                                <td align="left">
                                    <span id="ShippingSuburbLabel"><%=SessionHandler.Instance.ShippingCitySuburb%></span>
                                </td>
                            </tr>
                            <tr>
                                <th>State/Province/Region:</th>
                                <td align="left">
                                    <span id="ShippingStateLabel"><%=SessionHandler.Instance.ShippingStateRegion%></span>
                                </td>
                            </tr>
                            <tr>
                                <th>Zip/Postcode:</th>
                                <td align="left">
                                    <span id="ShippingPostcodeLabel"><%=SessionHandler.Instance.ShippingPostcode%></span>
                                </td>
                            </tr>
                            <tr>
                                <th>Country:</th>
                                <td align="left">
                                    <span id="ShippingCountryNameLabel"><%=SessionHandler.Instance.ShippingCountryName%></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                
                <fieldset class="CheckOut">
                    <legend>Payment Details</legend>
                    <table>
                        <tbody>
                            <tr>
                                <th>Payment Method:</th>
                                <td align="left"><%=SessionHandler.Instance.PaymentType%></td>
                            </tr>
                            <tr>
                                <th>Name on Card:</th>
                                <td align="left">
                                    <asp:Label ID="CardholderLabel" runat="server" Text="Label">
                                        <%=SessionHandler.Instance.CardholderName%>
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Card Number:</th>
                                <td align="left">
                                    <asp:Label ID="CardNumberLabel" runat="server" Text="Label">
                                        <%=SessionHandler.Instance.CardNumber%>
                                    </asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                
                <%if(SessionHandler.Instance.Wrapping) {%>
                    <fieldset id="GiftTagFieldSet" class="CheckOut">
                        <legend>Gift Tag Message</legend>
                        <asp:TextBox ID="GiftTagMessageTextBox" runat="server"
                                     TextMode="MultiLine"
                                     Rows="5"
                                     Columns="80">
                        </asp:TextBox>
                    </fieldset>
                <%}%>
                
                <fieldset class="CheckOut">
                    <legend>Comments</legend>
                    <asp:TextBox ID="CommentTextBox" runat="server"
                                 TextMode="MultiLine"
                                 Rows="5"
                                 Columns="80">
                    </asp:TextBox>
                </fieldset>
                <p>
                    By authorising the payment you agree to these 
                    <asp:HyperLink ID="PolicyHyperlink" runat="server"
                                   NavigateUrl="~/Policies.aspx"
                                   Target="_blank">
                        terms and conditions.
                    </asp:HyperLink>
                </p>
                <p>
                    <asp:Button ID="BackButton" runat="server" 
                                Text="Back"
                                OnClick="GoToPreviousPage" />
                    <asp:Button ID="NextButton" runat="server" 
                                Text="Place Order"
                                ValidationGroup="CheckOut4Group"
                                OnClick="PlaceOrder" />
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
