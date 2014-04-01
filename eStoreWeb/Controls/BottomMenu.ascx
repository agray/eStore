<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomMenu.ascx.cs" Inherits="eStoreWeb.Controls.BottomMenu" %>
<table cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td align="center" colspan="3">
            <asp:Menu ID="NavBarBottomMenu_SkipLink" runat="server" 
                      Enabled="True" 
                      Orientation="Horizontal">
                <StaticMenuItemStyle ItemSpacing="8px" />
                <Items>
                    <asp:MenuItem NavigateUrl="~/" Text="Home" Value="Home"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/Profile/AccountInfo.aspx" Text="Membership" Value="Membership"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/Testimonials.aspx" Text="Testimonials" Value="Testimonials"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/ContactUs.aspx" Text="Contact Us" Value="Contact Us"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/Links.aspx" Text="Links" Value="Links"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/Policies.aspx" Text="Policies" Value="Policies"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/FAQ.aspx" Text="FAQ" Value="FAQ"></asp:MenuItem>
                </Items>
            </asp:Menu>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:HyperLink ID="HomePageHyperlink" runat="server" Target="_self" />
        </td>
    </tr>
</table>
