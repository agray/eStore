<%@ Page Title="Information" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="eStoreWeb.Links" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="LinkUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <table width="100%">
	                <tr>
	                    <td>
	                        <h1>Links to related sites</h1>
	                    </td>
	                </tr>
	                <tr>
	                    <td>
	                        <h3>Resources</h3>
	                    </td>
	                </tr>
                    <asp:Repeater ID="ResourcesRepeater" runat="server" DataSourceID="ResourceLinksODS">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="ResourceHyperLink" runat="server"
                                                   Text='<%#Eval("LinkText")%>'
                                                   NavigateUrl='<%#Eval("LinkURL")%>' />
                                     - <%#Eval("LinkDescription")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
		                        <td colspan="2"><hr /></td>
		                    </tr>
                        </FooterTemplate>
                    </asp:Repeater>
	                <asp:ObjectDataSource ID="ResourceLinksODS" runat="server" 
                                          OldValuesParameterFormatString="original_{0}" 
                                          TypeName="eStoreBLL.LinksBLL"
                                          SelectMethod="getLinksByType"
                                          EnableCaching="True"
                                          CacheDuration="3600"
                                          CacheExpirationPolicy="Absolute">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Resources" 
                                           Name="linkType" 
                                           Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
	                <tr>
	                    <td>
	                        <h3>Shopping</h3>
	                    </td>
	                </tr>
	                <asp:Repeater ID="ShoppingRepeater" runat="server" DataSourceID="ShoppingLinksODS">
	                    <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="ShoppingHyperLink" runat="server"
                                                       Text='<%#Eval("LinkText")%>'
                                                       NavigateUrl='<%#Eval("LinkURL")%>' />
                                         - <%#Eval("LinkDescription")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
		                        <td colspan="2"><hr /></td>
		                    </tr>
                        </FooterTemplate>
                    </asp:Repeater>
	                <asp:ObjectDataSource ID="ShoppingLinksODS" runat="server" 
                                          TypeName="eStoreBLL.LinksBLL"
                                          OldValuesParameterFormatString="original_{0}" 
                                          SelectMethod="getLinksByType" 
                                          EnableCaching="True"
                                          CacheDuration="3600"
                                          CacheExpirationPolicy="Absolute">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Shopping" 
                                           Name="linkType" 
                                           Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
	                <tr>
	                    <td>
	                        <h3>Other</h3>
	                    </td>
	                </tr>
	                <asp:Repeater ID="OtherRepeater" runat="server" DataSourceID="OtherLinksODS">
	                    <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="OtherHyperLink" runat="server"
                                                   Text='<%#Eval("LinkText")%>'
                                                   NavigateUrl='<%#Eval("LinkURL")%>' />
                                     - <%#Eval("LinkDescription")%>
                                </td>
                            </tr>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
		                        <td colspan="2"><hr /></td>
		                    </tr>
                        </FooterTemplate>
                    </asp:Repeater>
	                <asp:ObjectDataSource ID="OtherLinksODS" runat="server" 
                                          TypeName="eStoreBLL.LinksBLL"
                                          OldValuesParameterFormatString="original_{0}" 
                                          SelectMethod="getLinksByType" 
                                          EnableCaching="True"
                                          CacheDuration="3600"
                                          CacheExpirationPolicy="Absolute">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Other" 
                                           Name="linkType" 
                                           Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
	                <tr>
	                    <td style="text-align:center">
	                        <b>To get added to this links page please send an email to <asp:HyperLink ID="ContactHyperlink1" runat="server" />.</b>
	                    </td>
	                </tr>
	                <tr>
	                    <td style="text-align:center">
	                        <b>We would love to have you link to our site. Please use the following when creating the link.</b>
	                    </td>
	                </tr>
                </table>
            </div>
	    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
