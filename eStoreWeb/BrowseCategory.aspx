<%@ Page Title="PetsPlayground" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="BrowseCategory.aspx.cs" Inherits="eStoreWeb.BrowseCategory" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="CategoryUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <div>
                    <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/Splash.aspx">Home</asp:HyperLink>&nbsp;&gt;&nbsp;
                    <asp:HyperLink ID="DepartmentName_Breadcrumb" runat="server"></asp:HyperLink>
                    <hr />
                
                    <% if(!IsEmpty(CategoryList)) {%>
                        <h1><asp:Label ID="HeaderNameLabel" runat="server"></asp:Label></h1>
                        <asp:Label ID="HeaderDescriptionLabel" runat="server"></asp:Label>
                        <hr />
                        <asp:ListView ID="CategoryList" runat="server" 
                                        DataSourceID="ProductsODS" 
                                        DataKeyNames="ID" 
                                        GroupItemCount="3"
                                        OnDataBound="CategoryList_DataBound"
                                        OnPagePropertiesChanging="CategoryList_PagePropertiesChanging">
                            <EmptyItemTemplate>
                                <table id="EmptyDepartmentTable" runat="server">
                                    <tr>
                                        <td>There are no departments. Please come back later.</td>
                                    </tr>
                                </table>
                            </EmptyItemTemplate>
                            <ItemSeparatorTemplate>
                                <% if(CategoryList.Items.Count > 1) {%>
                                    <td style="border-left: 1px solid #a3b3c0;">&nbsp;</td>
                                <%} %>
                            </ItemSeparatorTemplate>
                            <GroupSeparatorTemplate>
                                <tr>
                                    <td colspan="6"><hr/></td>
                                </tr>
                            </GroupSeparatorTemplate>
                            <ItemTemplate>
                                <td style="text-align:center;width:1000px">
                                    <div class="Product">
                                        <asp:HyperLink ID="ProductHyperLink" runat="server"
                                                        NavigateUrl='<%#"BrowseItem.aspx?ProdID=" + Eval("ID")%>'>
                                            <asp:Image ID="ProductImage" runat="server"
                                                        ImageUrl='<%#Eval("DefaultImage")%>'
                                                        AlternateText='<%#Eval("Name")%>'
                                                        BorderWidth="0px"
                                                        Height="130px"
                                                        Width="150px"/>
                                            <span class="ProductName"><%#Eval("Name")%></span>
                                        </asp:HyperLink>
                                        <div>        
                                            <asp:Label ID="ProductPriceLabel" class="Price" runat="server" Visible='<%#Int32.Parse(Eval("IsOnSale").ToString())==0%>'><%#Eval("CurrValue") + " " + CultureService.getConvertedPrice(Eval("UnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></asp:Label>
                                            <asp:Label ID="ProductDealLabel" class="PriceDeal" runat="server" Visible='<%#Int32.Parse(Eval("IsOnSale").ToString())==1%>'><%#Eval("CurrValue") + " " + CultureService.getConvertedPrice(Eval("DiscountUnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></asp:Label>
                                            <asp:Label ID="ProductDiscountPriceLabel" class="PriceNow" runat="server"></asp:Label>
                                        </div>
                                        <asp:ImageButton ID="ViewDetailsImageButton" runat="server"
                                                            AlternateText='View Details'
                                                            ImageUrl="~/Images/System/ViewDetails.gif"
                                                            OnCommand="ViewDetails_Click"
                                                            CommandArgument='<%#Eval("ID")%>'
                                                            BorderWidth="0"
                                                            BorderStyle="None" />
                                    </div>
                                </td>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="EmptyProductTable" runat="server" style="">
                                    <tr>
                                        <td>There are no products in this category. Please come back later.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div style="clear:both"/>
                                <table width="100%">
                                    <tr>
                                        <td style="text-align:left">
                                            <b><asp:Label ID="TopTotalProductsReturnedLabel" runat="server" /></b>
                                            <asp:Label ID="TopShowingLabel" runat="server" />
                                        </td>
                                        <td style="text-align:right">
                                            <asp:DataPager ID="BeforeListDataPager" runat="server" 
                                                            PagedControlID="CategoryList"
                                                            PageSize="12">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" 
                                                                                ShowFirstPageButton="False" 
                                                                                ShowNextPageButton="False" 
                                                                                ShowPreviousPageButton="True"
                                                                                PreviousPageText="< Previous" />
                                                    <asp:NumericPagerField ButtonCount="6" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" 
                                                                                ShowLastPageButton="False" 
                                                                                ShowNextPageButton="True" 
                                                                                ShowPreviousPageButton="False"
                                                                                NextPageText="Next >" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>

                                <hr />
                                        
                                <table width="100%">
                                    <tr id="groupPlaceholderContainer" runat="server" border="0">
                                        <td id="groupPlaceholder" runat="server"></td>
                                    </tr>
                                </table>

                                <hr />

                                <table width="100%">
                                    <tr>
                                        <td style="text-align:left">
                                            <b><asp:Label ID="BottomTotalProductsReturnedLabel" runat="server" /></b>
                                            <asp:Label ID="BottomShowingLabel" runat="server" />
                                        </td>
                                        <td style="text-align:right">
                                            <asp:DataPager ID="AfterListDataPager" runat="server" 
                                                            PagedControlID="CategoryList"
                                                            PageSize="12">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" 
                                                                                ShowFirstPageButton="False" 
                                                                                ShowNextPageButton="False" 
                                                                                ShowPreviousPageButton="True"
                                                                                PreviousPageText="< Previous" />
                                                    <asp:NumericPagerField ButtonCount="6" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" 
                                                                                ShowLastPageButton="False" 
                                                                                ShowNextPageButton="True" 
                                                                                ShowPreviousPageButton="False"
                                                                                NextPageText="Next >" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <GroupTemplate>
                                <tr id="itemPlaceholderContainer" runat="server">
                                    <td id="itemPlaceholder" runat="server"></td>
                                </tr>
                            </GroupTemplate>
                        </asp:ListView>
                    <%} else { %>
                        <h1>No Products found.  Please come back later.</h1>
                    <%} %>
                </div>
            </div>
            <asp:ObjectDataSource ID="ProductsODS" runat="server" 
                                  TypeName="eStoreBLL.ProductsBLL"
                                  OldValuesParameterFormatString="original_{0}" 
                                  SelectMethod="GetProductsByCategoryIdAndCurrencyId"
                                  EnableCaching="True"
                                  CacheDuration="3600"
                                  CacheExpirationPolicy="Absolute">
                <SelectParameters>
                    <asp:QueryStringParameter Name="categoryId" 
                                              QueryStringField="CatID" 
                                              Type="Int32" />
                    <asp:SessionParameter Name="currencyId" 
                                          SessionField="Currency" 
                                          Type="Int32" 
                                          DefaultValue="1"  />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>