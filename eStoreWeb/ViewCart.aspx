<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="eStoreWeb.ViewCart" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="phoenixconsulting.common.cart" %>
<%@ Reference Control="Controls/ShoppingCartTable.ascx" %>
<%@ Register Src="~/Controls/ShoppingCartTable.ascx" TagName="ShoppingCartTable" TagPrefix="cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<div>--%>
    <asp:UpdatePanel ID="ViewCartUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <h1>View Cart</h1>
     
                <%var sc = new ShoppingCart();
                  if(sc.IsEmptyList())
                  {%>
                    <table>
                        <tr>
                            <td>Your Cart is Empty.</td>
                        </tr>
                    </table>
                <%}
                  else
                  {%>
                    <cart:ShoppingCartTable ID="ShoppingCartTable" runat="server" />
                    <%if(!sc.HasWrapping()) {%>
                        <div>
                            <table align="center">
                                <tbody>
                                    <tr>
                                        <td>Want your order 
                                            <asp:HyperLink runat="server"
                                                           ID="GiftWrapHyperlink" 
                                                           NavigateUrl="BrowseItem.aspx?ProdID=21&CatID=8&DepID=9">gift wrapped</asp:HyperLink>?
                                        </td>
                                        <td>
                                            <asp:Image runat="server"
                                                       ID="GiftWrapping" 
                                                       ImageUrl="~/Images/System/giftwrapping.gif"
                                                       style="width:75px;height:65px"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    <%} %>
                    <div>
                        <table style="width:100%">
                            <tbody>
                                <tr valign="top">
                                    <td>
                                        <asp:ImageButton runat="server"
                                                 ID="ContinueShoppingImageButton"
                                                 AlternateText='Continue Shopping'
                                                 ImageUrl="~/Images/System/ContinueShopping.gif"
                                                 OnClick="ContinueShopping_Click"
                                                 BorderWidth="0"
                                                 BorderStyle="None" />
                                    </td>
                                    <td style="text-align:right;width:200px">
                                        <asp:ImageButton runat="server"
                                                 ID="CheckOutImageButton"
                                                 AlternateText='CheckOut'
                                                 ImageUrl="~/Images/System/SecureCheckOut.gif"
                                                 OnClick="CheckOut_Click"
                                                 BorderWidth="0"
                                                 BorderStyle="None" />
                                        <div class="DivSideBar" style="text-align:right">
                                          <img src="Images/System/VisaCard.gif" alt="Visa Card" width="40" height="24">
                                          <img src="Images/System/MasterCard.gif" alt="Master Card" width="40" height="24">
                                          <img src="Images/System/AmexCard.gif" alt="Amex Card" width="40" height="24">
                                          <img src="Images/System/PayPal.gif" alt="PayPal" width="40" height="24">
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                <%}%>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>--%>
</asp:Content>