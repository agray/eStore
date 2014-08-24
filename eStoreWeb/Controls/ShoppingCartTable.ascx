<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartTable.ascx.cs" Inherits="eStoreWeb.Controls.ShoppingCartTable" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="PhoenixConsulting.Common" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<%@ Reference Control="CountryAndModeDDLs.ascx" %>
<%@ Register Src="~/Controls/CountryAndModeDDLs.ascx" TagName="CountryAndModeDDLs" TagPrefix="CountryAndMode" %>

<%--<%@ Reference Control="CountryAndModeDDLs2.ascx" %>
<%@ Register Src="~/Controls/CountryAndModeDDLs2.ascx" TagName="CountryAndModeDDLs2" TagPrefix="CountryAndMode2" %>--%>

<table class="PPTable" width="100%">
    <thead>
        <tr>
            <td style="width:50px">Remove</td>
            <td colspan="2">Product(s)</td>
            <td style="width:105px">Quantity</td>
            <td style="width:60px">
                <asp:Label ID="TotalTitle" runat="server">
                    Total Price(<span><%=SessionHandler.Instance.CurrencyValue%></span>)
                </asp:Label>
            </td>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="cartItemRepeater" runat="server" 
                      OnItemCommand="cartItemRepeater_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td valign="middle" style="text-align:center">     
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" 
                                        CommandName="delete"
                                        CommandArgument='<%# Container.ItemIndex %>'>
                                        <asp:Image ID="Image1" runat="server"
                                                   ImageUrl="~/Images/System/Delete.gif"  />
                        </asp:LinkButton>
                    </td>
                    <asp:Label Visible="false" ID="HiddenSizeNameLabel" runat="server" Text='<%#Eval("SizeName")%>' />
                    <asp:Label Visible="false" ID="HiddenColorNameLabel" runat="server" Text='<%#Eval("ColorName")%>' />
                    <td style="width:39px" >
                        <asp:HyperLink ID="ProductLink" runat="server" NavigateUrl='<%#"~/BrowseItem.aspx?ProdID=" + Eval("ProductID")%>'>
                            <asp:Image ID="ItemImage" runat="server" 
                                   Height="65px" 
                                   Width="75px"
                                   ImageUrl='<%#Eval("ImagePath")%>' />
                        </asp:HyperLink>
                    </td>
                    <td>
                        <%#Eval("ProductDetails")%>
                        <%#DTUtil.EmitSizeInformation(Eval("SizeName").ToString())%>
                        <%#DTUtil.EmitColorInformation(Eval("ColorName").ToString())%>
                    </td>
                    <td style="text-align:center" valign="middle"><%#Eval("ProductQuantity")%></td>
                    <td style="text-align:right">
                        <asp:Label ID="UnitPriceLabel" runat="server">
                            <%#CultureService.getConvertedPrice(Eval("SubTotal").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%>
                        </asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="4" valign="middle">
                Ship <%=SessionHandler.Instance.TotalWeight%> kg to 
                <CountryAndMode:CountryAndModeDDLs ID="CountryAndModeDDLs" runat="server" />
                <asp:HyperLink ID="HelpHyperlink" NavigateUrl="~/FAQ.aspx#Shipping" runat="server">Help?</asp:HyperLink>
            </td>
            <td style="text-align:right"><%=CultureService.getConvertedPrice(SessionHandler.Instance.TotalShipping.ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></td>
        </tr>
        <tr>
            <td colspan="3" style="background-color: #fff"></td>
            <td style="text-align:right;height:25px"><%=DTUtil.EmitTotalLabel()%></td>
            <td style="text-align:right;height:25px"><%=CultureService.getConvertedPrice(SessionHandler.Instance.TotalCost.ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></td>
        </tr>
    </tbody>
</table>