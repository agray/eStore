<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderHistoryDetailTable.ascx.cs" Inherits="eStoreWeb.Controls.OrderHistoryDetailTable" %>
<%@ Import Namespace="eStoreWeb" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:ListView ID="orderHistoryDetailItemLV" runat="server"
              DataSourceID="OrderHistoryDetailODS">
    <LayoutTemplate>
        <table id="OrderHistoryDetailTable" class="PPTable" width="100%" border="1">
            <tr> 
                <th colspan="2">Product(s)</th>
                <th>Quantity</th>
                <th align="right">
                    <asp:Label ID="TotalTitle" runat="server">
                        Total Price (<span><%=SessionHandler.Instance.CurrencyValue%></span>)
                    </asp:Label>
                </th>
            </tr>
            <tr ID="itemPlaceholder" runat="server" />
            <tr>
                <td align="right" colspan="4">
                    <asp:LinkButton ID="AddToCartLinkButton" runat="server"
                                    OnClick="AddOrderToCart">
                        <asp:Image ID="AddToCartImage" runat="server"
                                   ImageUrl="~/Images/System/AddToCart.gif"  />
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <asp:Label Visible="false" ID="HiddenIDLabel" runat="server" Text='<%#Eval("ID")%>' />
            <asp:Label Visible="false" ID="HiddenDepIDLabel" runat="server" Text='<%#Eval("DepartmentID")%>' />
            <asp:Label Visible="false" ID="HiddenCatIDLabel" runat="server" Text='<%#Eval("CategoryID")%>' />
            <asp:Label Visible="false" ID="HiddenProdIDLabel" runat="server" Text='<%#Eval("ProductID")%>' />
            <asp:Label Visible="false" ID="HiddenImgPathLabel" runat="server" Text='<%#Eval("ImgPath")%>' />
            <asp:Label Visible="false" ID="HiddenUnitPriceLabel" runat="server" Text='<%#Eval("UnitPrice")%>' />
            <asp:Label Visible="false" ID="HiddenProdWeightLabel" runat="server" Text='<%#Eval("ProductWeight")%>' />
            <asp:Label Visible="false" ID="HiddenProdQuantityLabel" runat="server" Text='<%#Eval("Quantity")%>' />
            <asp:Label Visible="false" ID="HiddenIsOnSaleLabel" runat="server" Text='<%#Eval("IsOnSale")%>' />
            <asp:Label Visible="false" ID="HiddenDiscPriceLabel" runat="server" Text='<%#Eval("DiscPrice")%>' />
            <asp:Label Visible="false" ID="HiddenColorIDLabel" runat="server" Text='<%#Eval("ColorID")%>' />
            <asp:Label Visible="false" ID="HiddenSizeIDLabel" runat="server" Text='<%#Eval("SizeID")%>' />
            <asp:Label Visible="false" ID="HiddenSizeNameLabel" runat="server" Text='<%#Eval("SizeName")%>' />
            <asp:Label Visible="false" ID="HiddenColorNameLabel" runat="server" Text='<%#Eval("ColorName")%>' />
            <td style="width:39px" >
                <asp:HyperLink ID="ProductLink" runat="server" NavigateUrl='<%#"~/BrowseItem.aspx?ProdID=" + Eval("ProductID") + "&CatID=" + Eval("CategoryID") + "&DepID=" + Eval("DepartmentID")%>'>
                    <asp:Image ID="ItemImage" runat="server" 
                               Height="65px" 
                               Width="75px"
                               ImageUrl='<%#Eval("ImgPath")%>' />
                </asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="ProductDetailsLabel" runat="server"
                           OnDataBinding="ProductDetailsLabel_DataBinding">
                    <%#Eval("Product")%> From <%#Eval("Supplier")%>
                </asp:Label>
                <%#DTUtil.EmitSizeInformation(Eval("SizeName").ToString())%>
                <%#DTUtil.EmitColorInformation(Eval("ColorName").ToString())%>
            </td>
            <td style="text-align:center" valign="middle"><%#Eval("Quantity")%></td>
            <td style="text-align:right">
                <asp:Label ID="UnitPriceLabel" runat="server">
                    <%#CultureService.getConvertedPrice((Convert.ToDouble(Eval("UnitPrice").ToString()) * Convert.ToDouble(Eval("Quantity").ToString())).ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%>
                </asp:Label>
            </td>
            
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        No items in this order.
    </EmptyDataTemplate>
</asp:ListView>
<asp:ObjectDataSource ID="OrderHistoryDetailODS" runat="server" 
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getOrderDetailsByOrderID" 
                      TypeName="eStoreBLL.OrdersBLL">
    <SelectParameters>
        <asp:QueryStringParameter Name="orderID" QueryStringField="orderID" 
                                  Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>