<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="eStoreWeb.Controls.TopMenu" %>
<div>
    <table style="text-align:center" width="100%">
        <tr>
            <td align="center">
                <asp:Menu ID="NavBarTopMenu_SkipLink" runat="server" 
                          Enabled="True" 
                          Orientation="Horizontal"
                          SkinID="TopMenu">
                    <Items>
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem NavigateUrl="~/Splash.aspx" Text="Home" Value="Home"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/WishList/WishList.aspx" Text="Wishlist" Value="Wishlist"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Profile/AccountInfo.aspx" Text="Membership" Value="Membership"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Testimonials.aspx" Text="Testimonials" Value="Testimonials"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/ContactUs.aspx" Text="Contact Us" Value="Contact Us"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Links.aspx" Text="Links" Value="Links"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Policies.aspx" Text="Policies" Value="Policies"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/FAQ.aspx" Text="FAQ" Value="FAQ"></asp:MenuItem>
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                        <asp:MenuItem Selectable="false" Enabled="false" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                    </Items>
                </asp:Menu>
            </td>
        </tr>
    </table>
</div>
