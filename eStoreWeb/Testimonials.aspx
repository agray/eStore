<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Testimonials.aspx.cs" Inherits="eStoreWeb.Testimonials" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="TestimonialsUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <table width="100%">
		            <tr>
		                <td valign="top">
		                    <h1>Testimonials</h1>
		                </td>
		            </tr>
                    <tr>
		                <td>We are truly grateful for the feedback we receive from our customers.</td>
		            </tr>
		            <tr>
		                <td><hr noshade="noshade" size="1" /></td>
		            </tr>
		            <asp:Repeater ID="TestimonialRepeater" runat="server" 
                                  DataSourceID="TestimonialsODS">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("TestimonialText")%></td>
                            </tr>
                            <tr>
                                <td><i><%#Eval("CustomerName")%> - <%#Eval("CustomerCountry")%></i></td>
                            </tr>
                            <tr>
		                        <td><hr noshade="noshade" size="1" /></td>
		                    </tr>
                        </ItemTemplate>
		            </asp:Repeater>
		            <asp:ObjectDataSource ID="TestimonialsODS" runat="server" 
                                          TypeName="eStoreBLL.TestimonialsBLL"
                                          OldValuesParameterFormatString="original_{0}" 
                                          SelectMethod="getTestimonials" 
                                          EnableCaching="True"
                                          CacheDuration="3600"
                                          CacheExpirationPolicy="Absolute" />
                    
		            
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>