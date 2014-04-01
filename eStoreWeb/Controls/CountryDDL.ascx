<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountryDDL.ascx.cs" Inherits="eStoreWeb.Controls.CountryDDL" %>

<asp:DropDownList ID="CountryDropDownList" runat="server" 
                  DataSourceID="CountriesODS" 
                  DataTextField="Name" 
                  DataValueField="ID"
                  EnableViewState="False"
                  AutoPostBack="True" 
                  OnSelectedIndexChanged="OnSelectedIndexChanged" />
                  
<asp:ObjectDataSource ID="CountriesODS" runat="server" 
                      TypeName="eStoreBLL.CountriesBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getCountries"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute" />