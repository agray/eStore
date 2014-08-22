<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderHistoryTable.ascx.cs" Inherits="eStoreWeb.Controls.OrderHistoryTable" %>

<asp:ListView ID="orderHistoryItemLV" runat="server"
              DataSourceID="OrderHistoryODS"
              OnItemCommand="orderHistoryItemLV_ItemCommand">
    <LayoutTemplate>
        <table id="OrderHistoryTable" class="PPTable" width="100%" border="1">
            <tr>   
                <th>Order Date</th>
                <th>Settlement Date</th>
                <th>Dispatch Date</th>
            </tr>
            <tr ID="itemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <asp:Label Visible="false" ID="HiddenIDLabel" runat="server" Text='<%#Eval("ID")%>' />
            <td><%#Eval("OrderDate")%></td>
            <td><%#Eval("SettlementDate")%></td>
            <td><%#Eval("ShipDate")%></td>
            <td valign="middle" style="text-align:center">     
                <asp:LinkButton ID="ViewOrderDetailLinkButton" runat="server" 
                                CommandName="viewdetail"
                                CommandArgument='<%#Eval("ID")%>'>
                                <asp:Image ID="ViewDetailImage" runat="server"
                                           ImageUrl="~/Images/System/ViewDetails.gif"  />
                </asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        Your Order History is empty.
    </EmptyDataTemplate>
</asp:ListView>
<asp:ObjectDataSource ID="OrderHistoryODS" runat="server" 
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getOrderHistory" 
                      TypeName="eStoreBLL.OrdersBLL"
                      OnSelecting="OrderHistoryODS_Selecting">
    <SelectParameters>
        <asp:Parameter DbType="Guid" Name="userID" />
    </SelectParameters>
</asp:ObjectDataSource>