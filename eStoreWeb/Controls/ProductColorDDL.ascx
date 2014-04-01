<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductColorDDL.ascx.cs" Inherits="eStoreWeb.Controls.ProductColorDDL" %>
<asp:DropDownList ID="ColorDropDownList" runat="server" 
                  DataSourceID="ColorsODS" 
                  DataTextField="Name" 
                  DataValueField="ID">
</asp:DropDownList>

<asp:ObjectDataSource ID="ColorsODS" runat="server" 
                      TypeName="eStoreBLL.ColorsBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getColorsByProductID"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute">
    <SelectParameters>
        <asp:QueryStringParameter Name="productID" 
                                  QueryStringField="ProdID" 
                                  Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>