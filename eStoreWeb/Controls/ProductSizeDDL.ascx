<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSizeDDL.ascx.cs" Inherits="eStoreWeb.Controls.ProductSizeDDL" %>

<asp:DropDownList ID="SizeDropDownList" runat="server" 
                  DataSourceID="SizesODS" 
                  DataTextField="Name" 
                  DataValueField="ID" />

<asp:ObjectDataSource ID="SizesODS" runat="server" 
                      TypeName="eStoreBLL.SizesBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getSizesByProductID"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute">
    <SelectParameters>
        <asp:QueryStringParameter Name="productID" 
                                  QueryStringField="ProdID" 
                                  Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>