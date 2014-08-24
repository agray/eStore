<%@ Page Title="PetsPlayground" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Splash.aspx.cs" Inherits="eStoreWeb.Splash" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivSlogan"></div>
    <div id="DivHomeMain">
        <asp:ListView ID="DepartmentsListView" runat="server" 
                      DataKeyNames="ID" 
                      DataSourceID="DepartmentsODS" 
                      GroupItemCount="4">
            <EmptyItemTemplate>
                
            </EmptyItemTemplate>
            <ItemTemplate>
                <li class="Department">
                    <asp:HyperLink ID="DeparmentHyperLink" runat="server"
                                   NavigateUrl='<%#"BrowseDepartment.aspx?DepID=" + Eval("ID")%>'>
                        <asp:Image ID="DepartmentImage" runat="server"
                                   ImageUrl='<%#Eval("ImgPath")%>'
                                   AlternateText='<%#Eval("Name")%>'
                                   BorderWidth="0px"
                                   Height="130px"
                                   Width="150px"/>
                        <asp:Label ID="DepartmentName" class="DepartmentName" runat="server" Text='<%# Eval("Name") %>' />
                    </asp:HyperLink>
                    <asp:ImageButton runat="server"
                                     ID="ViewDetailsImageButton"
                                     AlternateText='View Details'
                                     ImageUrl="~/Images/System/ViewDetails.gif"
                                     OnCommand="ViewDetails_Click"
                                     CommandArgument='<%#"?DepID=" + Eval("ID")%>'
                                     BorderWidth="0"
                                     BorderStyle="None" />
                </li>
            </ItemTemplate>
            <EmptyDataTemplate>
                <tr>
                    <td>There are no departments at this time.  Please return later.</td>
                </tr>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <ul class="Departments">
                    <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                </ul>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
            </GroupTemplate>
        </asp:ListView>
        <%--<asp:HyperLink ID="SecureXMLExample" runat="server" NavigateUrl="~/SecureXMLExample.aspx">SecureXMLExample</asp:HyperLink>--%>
        <%--See Page_Init function for Cache Settings on DepartmentsODS--%>
        <asp:ObjectDataSource ID="DepartmentsODS" runat="server" 
                              TypeName="eStoreBLL.DepartmentsBLL"
                              OldValuesParameterFormatString="original_{0}"
                              SelectMethod="getDepartments"
                              EnableCaching="True"
                              CacheDuration="3600"
                              CacheExpirationPolicy="Absolute">
        </asp:ObjectDataSource>
    </div>
</asp:Content>