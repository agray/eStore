<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewCartTable.ascx.cs" Inherits="eStoreWeb.Controls.ReviewCartTable" %>
<%@ Import Namespace="eStoreWeb" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<table id="CartTable" class="PPTable" width="99%">
    <thead>
        <tr>
            <td colspan="2">Product(s)</td>
            <td style="width:105px">Quantity</td>
            <td style="width:60px">Total Price (<asp:Label ID="TotalCurrencyCodeLabel" runat="server" ><%=SessionHandler.Instance.BillingCurrencyValue%></asp:Label>)</td>
        </tr>
    </thead>
    <tbody>
        <%--Loop around Shopping Cart items here--%>
        <asp:Repeater ID="cartItemRepeater" runat="server">
            <ItemTemplate>
                <tr>
                    <td colspan="2"  align="left">
                        <%#Eval("ProductDetails")%> In <%#Eval("ColorName")%>
                        <%#DTUtil.EmitSizeInformation(Eval("SizeName").ToString())%>
                    </td>
                    <td style="text-align:center" valign="middle"><%#Eval("ProductQuantity")%></td>
                    <td style="text-align:right;height:25px"><%#CultureService.getConvertedPrice(Eval("SubTotal").ToString(), SessionHandler.Instance.BillingXRate, SessionHandler.Instance.CurrencyValue)%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="3" align="left">Shipping <%=DTUtil.EmitShippingModeName(Int32.Parse(SessionHandler.Instance.ShippingMode))%></td>
            <td style="text-align:right;height:25px"><%=CultureService.getConvertedPrice(SessionHandler.Instance.TotalShipping.ToString(), SessionHandler.Instance.BillingXRate, SessionHandler.Instance.CurrencyValue)%></td>
        </tr>
        <tr>
            <td style="background-color: #fff" colspan="2"></td>
            <td style="text-align:right;height:25px"><%=DTUtil.EmitTotalLabel() %></td>
            <td style="text-align:right;height:25px"><%=CultureService.getConvertedPrice(SessionHandler.Instance.TotalCost.ToString(), SessionHandler.Instance.BillingXRate, SessionHandler.Instance.CurrencyValue)%></td>
        </tr>
    </tbody>
</table>