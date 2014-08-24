<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="eStoreWeb.Search" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/Splash.aspx">Home</asp:HyperLink>
                <hr />
                <h1>Search results for <%=SessionHandler.Instance.SearchString%></h1>
                <%if(IsLoggedIn()) { %>
                    <table>
                        <tr>
                            <th>Name:</th>
                            <td><asp:TextBox ID="SavedSearchTextBox" runat="server"
                                             MaxLength="50"/>
                                <asp:RequiredFieldValidator ID="SavedSearchTextBoxRFV" runat="server" 
                                                    ErrorMessage="Required Field"
                                                    ControlToValidate="SavedSearchTextBox"
                                                    ValidationGroup="SavedSearchGroup"
                                                    InitialValue=""
                                                    Display="Dynamic"/>
                            </td>   
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="SaveSearchButton" runat="server" 
                                            Text="Save Search"
                                            CausesValidation="true"
                                            ValidationGroup="SavedSearchGroup"
                                            OnClick="SaveSearch"
                                            Font-Size="Small" />
                            </td>
                        </tr>
                    </table>
                <%} %>
                <hr />
                <asp:ListView ID="SearchResultsList" runat="server" 
                              DataSourceID="SearchResultsODS" 
                              DataKeyNames="ID" 
                              GroupItemCount="3" 
                              OnDataBound="SearchResultsList_DataBound">
                     <EmptyItemTemplate>
                              <td/>
                    </EmptyItemTemplate>
                    <ItemSeparatorTemplate>
                        <td>
                            <asp:Image ID="imgSpacer" runat="server"
                                       ImageUrl="~/Images/spacer.gif"
                                       Width="1"
                                       ImageAlign="Left"
                                       BorderWidth="0"
                                       Height="230"/>
                        </td>
                    </ItemSeparatorTemplate>
                    <GroupSeparatorTemplate>
                        <td colspan="6">
                            <hr />
                        </td>
                    </GroupSeparatorTemplate>
                    <ItemTemplate>
                        <td style="text-align:center; width:1000px">
                            <div class="Product">
                                <asp:HyperLink ID="ProductHyperLink" runat="server"
                                               NavigateUrl='<%#"BrowseItem.aspx?ProdID=" + Eval("ID") + "&CatID=" + Eval("CatID") + "&DepID=" + Eval("DepID")%>'>
                                    <asp:Image ID="ProductImage" runat="server"
                                               ImageUrl='<%#Eval("DefaultImage")%>'
                                               AlternateText='<%#Eval("Name")%>'
                                               BorderWidth="0px"
                                               Height="130px"
                                               Width="150px"/>
                                    <asp:Label ID="ProductNameLabel" runat="server" 
                                               class="ProductName" >
                                               <%#Eval("Name")%>
                                    </asp:Label>
                                </asp:HyperLink>
                            </div>
                            <div>        
                                <asp:Label ID="ProductPriceLabel" class="Price" runat="server" Visible='<%#Int32.Parse(Eval("IsOnSale").ToString())==0%>'><%#Eval("CurrValue") + " " + CultureService.getConvertedPrice(Eval("UnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></asp:Label>
                                <asp:Label ID="ProductDealLabel" class="PriceDeal" runat="server" Visible='<%#Int32.Parse(Eval("IsOnSale").ToString())==1%>'><%#Eval("CurrValue") + " " + CultureService.getConvertedPrice(Eval("DiscountUnitPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></asp:Label>
                                <asp:Label ID="ProductDiscountPriceLabel" class="PriceNow" runat="server"></asp:Label>
                            </div>
                            <div class="ProductFields">
                                <asp:HyperLink ID="HyperLink1" 
                                    NavigateUrl='<%#"BrowseItem.aspx?ProdID=" + Eval("ID") + "&CatID=" + Eval("CatID") + "&DepID=" + Eval("DepID")%>' 
                                    runat="server"><asp:Image ID="Image1" ImageUrl="~/Images/System/ViewDetails.gif"  runat="server" 
                                    AlternateText="View Details" BorderWidth="0" BorderStyle="None" /></asp:HyperLink>
                            </div>
                        </td>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align:left">
                                                <b><asp:Label ID="TopTotalProductsReturnedLabel" runat="server" /></b>
                                                <asp:Label ID="TopShowingLabel" runat="server" />
                                            </td>
                                            <td style="text-align:right">
                                                <asp:DataPager ID="BeforeListDataPager" runat="server" 
                                                               PagedControlID="SearchResultsList"
                                                               PageSize="12">
                                                    <Fields>
                                                        
                                                        <%--<asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                <b><%# Container.TotalRowCount%></b>&nbsp;products&nbsp;(showing&nbsp;<asp:Label runat="server" ID="TopStartItemLabel" Text="<%#Container.StartRowIndex + 1%>" />&nbsp;-&nbsp;<asp:Label runat="server" ID="TopEndItemLabel" Text="<%#Container.StartRowIndex + Container.PageSize%>" />)
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>--%>
                                                        
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table ID="groupPlaceholderContainer" runat="server" border="0" style="">
                                        <tr ID="groupPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr />
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align:left">
                                                <b><asp:Label ID="BottomTotalProductsReturnedLabel" runat="server" /></b>
                                                <asp:Label ID="BottomShowingLabel" runat="server" />
                                            </td>
                                            
                                            <td style="text-align:right">
                                                <asp:DataPager ID="AfterListDataPager" runat="server" 
                                                               PagedControlID="SearchResultsList"
                                                               PageSize="12">
                                                    <Fields>
                                                    
                                                        <%--<asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                <td style="text-align:left"><b><%#Container.TotalRowCount%></b>&nbsp;products&nbsp;(showing&nbsp;<asp:Label runat="server" ID="BottomStartItemLabel" Text="<%#Container.StartRowIndex + 1%>" />&nbsp;-&nbsp;<asp:Label runat="server" ID="BottomEndItemLabel" Text="<%#Container.StartRowIndex + Container.PageSize%>" />)</td>
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>--%>
                                                        
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
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr ID="itemPlaceholderContainer" runat="server">
                            <td ID="itemPlaceholder" runat="server"></td>
                        </tr>
                    </GroupTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="SearchResultsODS" runat="server" 
                                      TypeName="eStoreBLL.ProductsBLL"
                                      OldValuesParameterFormatString="original_{0}" 
                                      SelectMethod="getSearchResults"
                                      EnableCaching="True"
                                      CacheDuration="3600"
                                      CacheExpirationPolicy="Absolute">
                    <SelectParameters>
                        <asp:SessionParameter Name="query" 
                                              SessionField="SearchString" 
                                              Type="String" 
                                              DefaultValue=""  />
                        <asp:SessionParameter Name="currencyID" 
                                              SessionField="Currency" 
                                              Type="Int32"
                                              DefaultValue="1" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>