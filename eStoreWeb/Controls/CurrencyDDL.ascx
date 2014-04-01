<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurrencyDDL.ascx.cs" Inherits="eStoreWeb.Controls.CurrencyDDL" %>

<asp:DropDownList ID="CurrencyDropDownList" runat="server" 
                  DataSourceID="CurrenciesODS" 
                  DataTextField="Name" 
                  DataValueField="ID" 
                  AutoPostBack="True" 
                  EnableViewState="True"
                  OnSelectedIndexChanged="OnSelectedIndexChanged" />
<asp:ObjectDataSource ID="CurrenciesODS" runat="server" 
                      TypeName="eStoreBLL.CurrenciesBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getCurrencies"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute" />