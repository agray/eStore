<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WishListTable.ascx.cs" Inherits="eStoreWeb.Controls.WishListTable" %>
<%@ Import Namespace="eStoreWeb" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>
   
<asp:ListView ID="wishListItemLV" runat="server"
              DataSourceID="WishlistODS"
              OnItemCommand="wishListItemLV_ItemCommand">
    <LayoutTemplate>
        <table id="WishListTable" class="PPTable" width="100%" border="1">
            <tr>   
                <th style="width:50px">Remove</th>
                <th colspan="2">Product(s)</th>
                <th align="right">
                    <asp:Label ID="PriceLabel" runat="server">
                        Price (<span><%=SessionHandler.Instance.CurrencyValue%></span>)
                    </asp:Label>
                </th>
                <th>Quantity</th>
            </tr>
            <tr ID="itemPlaceholder" runat="server" />
            <tr align="right">
                <td align="right" colspan="6">
                    <asp:Button ID="UpdateQuantButton" runat="server"
                                Text="Update Quantities"
                                OnClick="UpdateQuantities" />
                    <asp:Button ID="AddAllToCartButton" runat="server"
                                Text="Add All to Cart"
                                OnClick="AddAllToCart" />
                </td>
            </tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td valign="middle" style="text-align:center">     
                <asp:LinkButton ID="DeleteLinkButton" runat="server" 
                                CommandName="delete"
                                CommandArgument='<%#Eval("ID")%>'>
                                <asp:Image ID="RemoveImage" runat="server"
                                           ImageUrl="~/Images/System/Delete.gif"  />
                </asp:LinkButton>
            </td>
            <asp:Label Visible="false" ID="HiddenIDLabel" runat="server" Text='<%#Eval("ID")%>' />
            <asp:Label Visible="false" ID="HiddenSizeNameLabel" runat="server" Text='<%#Eval("SizeName")%>' />
            <asp:Label Visible="false" ID="HiddenColorNameLabel" runat="server" Text='<%#Eval("ColorName")%>' />
            <td style="width:39px" >
                <asp:HyperLink ID="ProductLink" runat="server" NavigateUrl='<%#"~/BrowseItem.aspx?ProdID=" + Eval("ProdID") + "&CatID=" + Eval("CatID") + "&DepID=" + Eval("DepID")%>'>
                    <asp:Image ID="ItemImage" runat="server" 
                               Height="65px" 
                               Width="75px"
                               ImageUrl='<%#Eval("ImgPath")%>' />
                </asp:HyperLink>
            </td>
            <td>
                <%#Eval("ProdDetails")%>
                <%#DTUtil.EmitSizeInformation(Eval("SizeName").ToString())%>
                <%#DTUtil.EmitColorInformation(Eval("ColorName").ToString())%>
            </td>
            <td style="text-align:right">
                <asp:Label ID="UnitPriceLabel" runat="server">
                    <%#CultureService.getConvertedPrice(Eval("UnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%>
                </asp:Label>
            </td>
            <td style="text-align:center" valign="middle">
                <asp:TextBox ID="QuantityTextBox" runat="server" 
                             Text='<%#Eval("Quantity")%>'
                             Width="40px" />
            </td>
            <td valign="middle" style="text-align:center">     
                <asp:LinkButton ID="AddToCartLinkButton" runat="server" 
                                CommandName="addtocart"
                                CommandArgument='<%#Eval("ID")%>'>
                                <asp:Image ID="AddToCartImage" runat="server"
                                           ImageUrl="~/Images/System/AddToCart.gif"  />
                </asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        Your Wishlist is empty.
    </EmptyDataTemplate>
</asp:ListView>
<asp:ObjectDataSource ID="WishListODS" runat="server" 
                      TypeName="eStoreBLL.WishListsBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getWishListByUserID"
                      OnSelecting="WishListODS_Selecting">
    <SelectParameters>
        <asp:Parameter Name="guid" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>