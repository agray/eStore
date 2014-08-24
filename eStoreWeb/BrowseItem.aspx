<%@ Page Title="PetsPlayground" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="BrowseItem.aspx.cs" Inherits="eStoreWeb.BrowseItem" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<%@ Register tagprefix="productSize" tagname="ProductSizeDDL" src="~/Controls/ProductSizeDDL.ascx" %>
<%@ Register tagprefix="productColor" tagname="ProductColorDDL" src="~/Controls/ProductColorDDL.ascx" %>
<%@ Register tagprefix="productQuantity" tagname="ProductQuantityDDL" src="~/Controls/ProductQuantityDDL.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Stylesheets/multizoom.css" rel="stylesheet" type="text/css" />
    <script src="js/multizoom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //var el = $("#[id$=MainImage]").attr("id");
        //alert("el=" + el.toString);
        jQuery(document).ready(function ($) {
            $("#ctl00_ContentPlaceHolder1_ProductFormView_MainImage").addimagezoom({ // multi-zoom: options same as for previous Featured Image Zoomer's addimagezoom unless noted as '- new'
                descArea: '#description', // description selector (optional - but required if descriptions are used) - new
                descpos: true, // if set to true - description position follows image position at a set distance, defaults to false (optional) - new
                imagevertcenter: true, // zoomable image centers vertically in its container (optional) - new
                magvertcenter: true, // magnified area centers vertically in relation to the zoomable image (optional) - new
                zoomrange: [3, 10],
                magnifiersize: [300, 300],
                magnifierpos: 'left',
                cursorshadecolor: '#fff',
                cursorshade: true,
                zoomablefade: false
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="BrowseItemUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <div>
                    <% if(!IsEmpty(ProductFormView)) {%>
                        <%--bulk of content goes here--%>
                        <asp:FormView ID="ProductFormView" runat="server" 
                                      DataKeyNames="ID" 
                                      DataSourceID="ProductODS"
                                      OnDataBound="ProductFormView_DataBound">
                            <ItemTemplate>
                                <div>
                                    <asp:HyperLink ID="Home_Breadcrumb" runat="server" 
                                                   NavigateUrl="~/Splash.aspx" 
                                                   Text="Home" />&nbsp;&gt;&nbsp
                                    <asp:HyperLink ID="DepartmentName_Breadcrumb" runat="server"
                                                   NavigateUrl='<%#"~/BrowseDepartment.aspx?DepID=" + Eval("DepID").ToString()%>'
                                                   Text='<%#Eval("DepartmentName")%>' />&nbsp;&gt;&nbsp
                                    <asp:HyperLink ID="CatName_Breadcrumb" runat="server" 
                                                   NavigateUrl='<%#"~/BrowseCategory.aspx?CatID=" + Eval("CatID").ToString()%>'
                                                   Text='<%#Eval("CategoryName")%>' />
                                </div>
                                <hr />
                                <table width="100%" cellpadding="0" cellspacing="2">
                                    <tbody>
                                        <tr>
                                            <td valign="top">
                                                <h1>
                                                    <asp:Label ID="HeaderNameLabel" runat="server" Text='<%#Eval("Name")%>' />
                                                </h1>
                                                <div class="ProductDescription">
                                                    <asp:Label ID="ProductDescriptionLabel" runat="server" Text='<%#Eval("Description")%>' />
                                                </div>
                                                
                                                <%if(HasProductSizes()) {%>
                                                    <div class="ProductOptions" style="text-align:right">
                                                        <%--Hide this dropdown if there are no sizes--%>
                                                        <asp:Label ID="SizeDDLLabel" class="ProductOptions" runat="server" Text="Size" />
                                                        <productSize:ProductSizeDDL ID="productSizeDDL" runat="server" />
                                                    </div>
                                                <%}%>
                                                
                                                <%if(HasProductColors()) {%>
                                                    <div class="ProductOptions" style="text-align:right">
                                                        <%--Hide this dropdown if there are no colors--%>
                                                        <asp:Label ID="ColorDDLLabel" class="ProductOptions" runat="server" Text="Color" />
                                                        <productColor:ProductColorDDL ID="productColorDDL" runat="server" />
                                                    </div>
                                                <%}%>
                                                <div class="ProductOptions" style="text-align:right">
                                                    <span class="ProductOptions">Quantity</span>
                                                    <productQuantity:ProductQuantityDDL ID="productQuantityDDL" runat="server" />
                                                </div>
                                                <div class="ProductFields" style="text-align:right">
                                                    <asp:TextBox ID="ProductIDHiddenField" runat="server"
                                                                 Visible="false" 
                                                                 Text='<%#Request["ProdID"]%>' />
                                                    <asp:ImageButton ID="AddToCartImageButton" runat="server"
                                                                     AlternateText='Add <%#Eval("Name")%> From <%#Eval("CompanyName")%> to cart'
                                                                     ImageUrl="~/Images/System/AddToCart.gif"
                                                                     OnClick="AddToCart"
                                                                     CommandArgument='<%#ConcatKeys(Eval("DepID").ToString(), Eval("CatID").ToString(), Eval("ID").ToString())%>' />
                                                    <%if(Request.IsAuthenticated) {%> 
                                                        <asp:ImageButton ID="AddToWishListImageButton" runat="server"
                                                                         AlternateText='Add <%#Eval("Name")%> From <%#Eval("CompanyName")%> to WishList'
                                                                         ImageUrl="~/Images/System/AddToWishList.gif"
                                                                         OnClick="AddToWishList" />
                                                    <%} %>
                                                </div>
                                                <asp:Label Visible="false" ID="HasSN" runat="server" Text='<%#Eval("HasSN")%>' />
                                                <%if(HasSn()) {%>
                                                    <div class="ProductBookmarks">
                                                        <h2>Bookmark this page</h2>
                                                        <p>
                                                            <asp:HyperLink ID="DiggHyperLink" runat="server"
                                                                           Target="_blank">
                                                                <asp:Image ID="DiggImage" runat="server"
                                                                           ImageUrl="~/Images/System/icon_digg.gif" />Digg</asp:HyperLink>
                                                            <asp:HyperLink ID="DeliciousHyperLink" runat="server"
                                                                           Target="_blank">
                                                                <asp:Image ID="DelImage" runat="server"
                                                                           ImageUrl="~/Images/System/icon_del.gif" />del.icio.us</asp:HyperLink>
                                                            <asp:HyperLink ID="RedditHyperLink" runat="server"
                                                                           Target="_blank">
                                                                <asp:Image ID="RedditImage" runat="server"
                                                                           ImageUrl="~/Images/System/icon_reddit.gif" />Reddit</asp:HyperLink>
                                                            <asp:HyperLink ID="GoogleHyperLink" runat="server"
                                                                           Target="_blank">
                                                                <asp:Image ID="GoogleImage" runat="server"
                                                                           ImageUrl="~/Images/System/icon_google.gif" />Google</asp:HyperLink>
                                                            <asp:HyperLink ID="EmailHyperLink" runat="server"
                                                                           Target="_blank">
                                                                <asp:Image ID="EmailImage" runat="server"
                                                                           ImageUrl="~/Images/System/icon_email.gif" />Email</asp:HyperLink>
                                                        </p>
                                                    </div>
                                                <%} %>
                                                                                               
                                                <div>
                                                    <h2>Exchange and Returns Policy</h2>
                                                    <p>
                                                        PetsPlayground will exchange your product for a different size or refund your money if it is return within 30 days of receipt. Please see the
                                                        <a href="Policies.aspx">Policies</a>
                                                        page for more information.
                                                    </p>
                                                    <h2>Taxes and import duties</h2>
                                                    <p>
                                                        All import duties, taxes or VAT are the sole responsibility of the customer and are not 
                                                        included in the shipping charges. If you are unsure if your parcel will be subject to such fees 
                                                        you should contact your local customs or postal authority.
                                                    </p>
                                                    <h2>Payment Methods</h2>
                                                    <p> We accept payment via Visa, MasterCard, American Express and Paypal.</p>
                                                    <div class="DivSideBar" style="text-align:center">
                                                        <img height="24" width="40" alt="Visa Card" src="Images/System/VisaCard.gif"/>
                                                        <img height="24" width="40" alt="Master Card" src="Images/System/MasterCard.gif"/>
                                                        <img height="24" width="40" alt="Amex Card" src="Images/System/AmexCard.gif"/>
                                                        <img height="24" width="40" alt="PayPal" src="Images/System/PayPal.gif"/>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="text-align:center" valign="top">
                                                <%--images go here--%>
                                                <%--<span class="PriceDeal"/>--%>
                                                <span class="Price">
                                                    <asp:Label ID="ProductPriceLabel" runat="server" Visible='<%#Int32.Parse(Eval("IsOnSale").ToString())==0%>'>Price <%#Eval("CurrValue")%>&nbsp;<%#CultureService.getConvertedPrice(Eval("UnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></asp:Label>
                                                    <asp:Label ID="ProductDealLabel" class="PriceDeal" runat="server" Visible='<%#Int32.Parse(Eval("IsOnSale").ToString())==1%>'><%#Eval("CurrValue") + " " + CultureService.getConvertedPrice(Eval("DiscountUnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></asp:Label>
                                                    <asp:Label Visible="false" ID="HiddenUnitPriceLabel" runat="server" Text='<%#Eval("UnitPrice")%>' />
                                                    <asp:Label Visible="false" ID="ProductNameLabel" runat="server" Text='<%#Eval("Name")%>' />
                                                    <asp:Label Visible="false" ID="CompanyNameLabel" runat="server" Text='<%#Eval("CompanyName")%>' />
                                                    <asp:Label Visible="false" ID="DiscountUnitPriceLabel" runat="server" Text='<%#Eval("DiscountUnitPrice")%>' />
                                                    <asp:Label Visible="false" ID="ProductWeightLabel" runat="server" Text='<%#Eval("Weight")%>' />
                                                    <asp:Label Visible="false" ID="OnSaleLabel" runat="server" Text='<%#Eval("IsOnSale")%>' />
                                                    <asp:Label Visible="false" ID="ImagePathLabel" runat="server" Text='<%#Eval("DefaultImage")%>' />
                                                    <asp:Label Visible="false" ID="NumProductSizes" runat="server" Text='<%#Eval("NumProductSizes")%>' />
                                                    <asp:Label Visible="false" ID="NumProductColors" runat="server" Text='<%#Eval("NumProductColors")%>' />
                                                </span>
                                                <div class="targetarea">
                                                    <asp:Image ID="MainImage" runat="server"
                                                               Height="300px" 
                                                               Width="260px" 
                                                               ImageUrl='<%#Eval("DefaultImage")%>' />
                                                </div>
                                                <asp:Label ID="ProductImageName" runat="server" Text='<%#Eval("imgName")%>' />
                                                <asp:Panel ID="MiniImagePanel" runat="server">
                                                    <hr/>
                                                    <%--<ul class="Product">
                                                        <asp:ListView ID="MiniImageListView" runat="server" 
                                                                      DataSourceID="ImagesODS"
                                                                      DataKeyNames="ID"
                                                                      OnItemDataBound="MiniImageListView_ItemDataBound">
                                                            <EmptyItemTemplate>
                                                                  <li/>
                                                            </EmptyItemTemplate>
                                                            <LayoutTemplate>
                                                                <li ID="itemPlaceholder" runat="server"
                                                                        class="MiniProduct">
                                                                </li>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <li id="MiniProductImage" runat="server"
                                                                    class="MiniProduct">
                                                                    <asp:Image ID="MiniImage" runat="server" 
                                                                               Height="45px" 
                                                                               Width="39px" 
                                                                               ImageUrl='<%#Eval("imgPath")%>'
                                                                               AlternateText='<%#Eval("imgName")%>'/>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </ul>--%>

                                                    <div class="MainImage thumbs">
                                                        <asp:ListView ID="MiniImageListView" runat="server" 
                                                                      DataSourceID="ImagesODS"
                                                                      DataKeyNames="ID"
                                                                      OnItemDataBound="MiniImageListView_ItemDataBound">
                                                            <EmptyItemTemplate>
                                                            </EmptyItemTemplate>
                                                            <LayoutTemplate>
                                                                    <span ID="itemPlaceholder" runat="server"
                                                                          class="MiniProduct"/>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="MiniProductImage" runat="server"
                                                                               NavigateUrl='<%#Eval("imgPath")%>'
                                                                               data-large='<%#Eval("imgLargePath")%>'
                                                                               data-title='<%#Eval("imgName")%>'
                                                                               class="MiniProduct">
                                                                    <asp:Image ID="MiniImage" runat="server" 
                                                                               Height="45px" 
                                                                               Width="39px"
                                                                               ImageUrl='<%#Eval("imgPath")%>'
                                                                               AlternateText='<%#Eval("imgName")%>'/>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        
                                        <%if(SessionHandler.Instance.AlsoBought)
                                          {%>
                                            <tr>
                                                <td colspan="2">
                                                    <hr/>
                                                    <h2 style="text-align: center;">Customers who bought this item also bought...</h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table cellspacing="0" cellpadding="0" width="100%">
                                                        <tbody>
                                                            
                                                        
                                                        
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        <%} else {%>
                                            <tr>
                                                <td colspan="2">
                                                    <hr/>
                                                </td>
                                            </tr>
                                        <%}%>
                                    </tbody>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    <%} else { %>
                        <div>
                            <asp:HyperLink ID="HyperLink1" runat="server"
                                           NavigateUrl="~/Splash.aspx"
                                           Text="Home" />
                            <hr />
                        </div>
                        <h1>Product not found.</h1>
                    <%} %>
                    <asp:ObjectDataSource ID="ImagesODS" runat="server" 
                                          TypeName="eStoreBLL.ImagesBLL"
                                          OldValuesParameterFormatString="original_{0}" 
                                          SelectMethod="getImagesByProductID"
                                          EnableCaching="True"
                                          CacheDuration="3600"
                                          CacheExpirationPolicy="Absolute">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="productID" 
                                                      QueryStringField="ProdID" 
                                                      Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ProductODS" runat="server"
                                          TypeName="eStoreBLL.ProductsBLL" 
                                          OldValuesParameterFormatString="original_{0}" 
                                          SelectMethod="getProductByIDAndCurrencyID"
                                          EnableCaching="True"
                                          CacheDuration="3600"
                                          CacheExpirationPolicy="Absolute">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ID" 
                                                      QueryStringField="ProdID" 
                                                      Type="Int32" />
                            <asp:SessionParameter Name="currencyID" SessionField="Currency" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>