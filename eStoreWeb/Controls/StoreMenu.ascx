<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoreMenu.ascx.cs" Inherits="eStoreWeb.Controls.StoreMenu" %>
<%@ Import Namespace="System.Data" %>
<asp:Repeater ID="NavCatalogue_DepartmentRepeater" runat="server">
    <ItemTemplate>
        <h1>
            <div>
            <asp:HyperLink ID="DepartmentHyperLink" runat="server" NavigateUrl='<%#"~/BrowseDepartment.aspx?DepID=" + Eval("ID")%>'>
                <%#Eval("Name")%>
            </asp:HyperLink>
            </div>
        </h1>
        <ul>
            <asp:Repeater ID="CategoryRepeater" runat="server" 
                          DataSource='<%#((DataRowView)Container.DataItem).Row.GetChildRows("FK_tCategory_tDepartment")%>'>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="CategoryHyperLink" runat="server" NavigateUrl='<%#"~/BrowseCategory.aspx?CatID=" + Eval("ID")%>'>
                            <%#Eval("Name")%>
                        </asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </ItemTemplate>
</asp:Repeater>
