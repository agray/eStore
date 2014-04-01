<%@ Page Title="PetsPlayground" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="BrowseDepartment.aspx.cs" Inherits="eStoreWeb.BrowseDepartment" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>
<%@ Import Namespace="eStoreWeb" %>
<%@ Import Namespace="com.phoenixconsulting.culture" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="BrowseDepartmentUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <asp:HyperLink ID="Home_Breadcrumb" runat="server" 
                               NavigateUrl="~/Splash.aspx">Home</asp:HyperLink>
                <hr />
                
                <% if(!isEmpty(DepartmentList)) {%>    
                    <h1><asp:Label ID="HeaderNameLabel" runat="server" /></h1>
                    <hr />
                    <asp:ListView ID="DepartmentList" runat="server" 
                                    DataSourceID="DepartmentsODS" 
                                    DataKeyNames="ID" 
                                    GroupItemCount="3">
                        <ItemSeparatorTemplate>
                            <% if(DepartmentList.Items.Count > 1) {%>
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
                                <div class="Category">
                                    <asp:HyperLink ID="CategoryHyperLink" 
                                                    NavigateUrl='<%#"BrowseCategory.aspx?CatID=" + Eval("ID") + "&DepID=" + Request["DepID"]%>' runat="server">
                                        <asp:Image ID="CategoryImage" runat="server"
                                                    ImageUrl='<%#Eval("ImgPath")%>'
                                                    AlternateText='<%#Eval("Name")%>'
                                                    BorderWidth="0px"
                                                    Height="130px"
                                                    Width="150px"/>
                                        <span class="CategoryName"><%#Eval("Name")%></span>
                                        <span class="CategoryPrice">from <%#Eval("CurrValue").ToString() + " " + CultureService.getConvertedPrice(Eval("MinPrice").ToString(), SessionHandler.Instance.CurrencyXRate, SessionHandler.Instance.CurrencyValue)%></span>
                                    </asp:HyperLink>
                                    <asp:ImageButton runat="server"
                                                        ID="ViewDetailsImageButton"
                                                        AlternateText='View Details'
                                                        ImageUrl="~/Images/System/ViewDetails.gif"
                                                        OnCommand="ViewDetails_Click"
                                                        CommandArgument='<%#"?CatID=" + Eval("ID") + "&DepID=" + Request["DepID"]%>'
                                                        BorderWidth="0"
                                                        BorderStyle="None" />
                                </div>
                            </td>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table id="EmptyCategoryTable" runat="server">
                                <tr>
                                    <td>There are no categories in this department. Please come back later.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <div style="clear:both"/>
                            <table width="100%">
                                <tr id="groupPlaceholderContainer" runat="server" border="0">
                                    <td id="groupPlaceholder" runat="server"></td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <tr ID="itemPlaceholderContainer" runat="server">
                                <td ID="itemPlaceholder" runat="server"></td>
                            </tr>
                        </GroupTemplate>
                    </asp:ListView>
                <%} else { %>
                        <h1>No Categories found.  Please come back later.</h1>
                <%} %>
                <asp:ObjectDataSource ID="DepartmentsODS" runat="server" 
                                      TypeName="eStoreBLL.DepartmentsBLL"
                                      OldValuesParameterFormatString="original_{0}" 
                                      SelectMethod="getDepartmentDetailsByDepartmentIDAndCurrencyID"
                                      EnableCaching="True"
                                      CacheDuration="3600"
                                      CacheExpirationPolicy="Absolute">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="departmentID" 
                                                  QueryStringField="DepID" 
                                                  Type="Int32"/>
                        <asp:SessionParameter Name="currencyID" 
                                              SessionField="Currency" 
                                              Type="Int32"
                                              DefaultValue="1"/>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>