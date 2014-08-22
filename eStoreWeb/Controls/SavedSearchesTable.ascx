<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SavedSearchesTable.ascx.cs" Inherits="eStoreWeb.Controls.SavedSearchesTable" %>

<asp:ListView ID="savedSearchesItemLV" runat="server"
              DataSourceID="SavedSearchesODS"
              OnItemCommand="savedSearchesItemLV_ItemCommand">
    <LayoutTemplate>
        <table id="SavedSearchesTable" class="PPTable" width="100%" border="1">
            <tr>
                <th style="width:50px">Remove</td>
                <th>Name</th>
                <th>Criteria</th>
                <th>Action</th>
            </tr>
            <tr ID="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <asp:Label Visible="false" ID="HiddenIDLabel" runat="server" Text='<%#Eval("ID")%>' />
            <td valign="middle" style="text-align:center">     
                <asp:LinkButton ID="DeleteLinkButton" runat="server" 
                        CommandName="delete"
                        CommandArgument='<%#Eval("ID")%>'>
                        <asp:Image ID="DeleteImage" runat="server"
                                   ImageUrl="~/Images/System/Delete.gif"/>
                </asp:LinkButton>
            </td>
            <td><%#Eval("Name")%></td>
            <td><%#Eval("Criteria")%></td>
            <td valign="middle" style="text-align:center">
                <asp:LinkButton ID="DoSearchLinkButton" runat="server" 
                        CommandName="DoSearch"
                        CommandArgument='<%#Eval("Criteria")%>'>
                        <asp:Image ID="DoSearchImage" runat="server"
                                   ImageUrl="~/Images/System/GoButton.gif"/>
                </asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        You do not currently have any saved searches.
    </EmptyDataTemplate>
</asp:ListView>
<asp:Button ID="AddSavedSearchButton" runat="server" 
            Text="Add"
            OnClick="AddSavedSearch" />
<asp:ObjectDataSource ID="SavedSearchesODS" runat="server" 
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getSavedSearches"
                      DeleteMethod="deleteSavedSearch"
                      TypeName="eStoreBLL.SavedSearchBLL"
                      OnSelecting="SavedSearchODS_Selecting">
    <SelectParameters>
        <asp:Parameter DbType="Guid" Name="userID" />
    </SelectParameters>
</asp:ObjectDataSource>