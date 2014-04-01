<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpiryYearDDL.ascx.cs" Inherits="eStoreWeb.Controls.ExpiryYearDDL" %>

<asp:DropDownList ID="ExpiryYearDropDownList" runat="server" 
                  DataSourceID="CCExpiryYearsODS" 
                  DataTextField="CCExpiryYear" 
                  DataValueField="ID" 
                  AppendDataBoundItems="True"
                  Font-Size="Small">
    <asp:ListItem Selected="True"></asp:ListItem>
</asp:DropDownList>

<!--Caching for half a day so on 31/12 each year so 
        the AnnualShift can take effect in timely fashion-->
<asp:ObjectDataSource ID="CCExpiryYearsODS" runat="server" 
                      TypeName="eStoreBLL.CCYearsBLL"
                      OldValuesParameterFormatString="original_{0}"
                      SelectMethod="getCCExpiryYears"
                      EnableCaching="True"
                      CacheDuration="43200"
                      CacheExpirationPolicy="Absolute" />