<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpiryMonthDDL.ascx.cs" Inherits="eStoreWeb.Controls.ExpiryMonthDDL" %>

<asp:DropDownList ID="ExpiryMonthDropDownList" runat="server" 
                  DataSourceID="CCExpiryMonthODS" 
                  DataTextField="CCExpiryMonth" 
                  DataValueField="ID" 
                  AppendDataBoundItems="True"
                  Font-Size="Small">
    <asp:ListItem Selected="True"></asp:ListItem>
</asp:DropDownList>
<asp:ObjectDataSource ID="CCExpiryMonthODS" runat="server" 
                      TypeName="eStoreBLL.CCMonthsBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getCCExpiryMonths"
                      EnableCaching="True"
                      CacheDuration="Infinite"
                      CacheExpirationPolicy="Absolute" />