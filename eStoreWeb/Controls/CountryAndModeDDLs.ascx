<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CountryAndModeDDLs.ascx.cs" Inherits="eStoreWeb.Controls.CountryAndModeDDLs" %>

<%--<%@ Reference Control="CountryDDL.ascx" %>
<%@ Register Src="~/Controls/CountryDDL.ascx" TagPrefix="Country" TagName="CountryDDL"  %>
<%@ Reference Control="ModeDDL.ascx" %>
<%@ Register Src="~/Controls/ModeDDL.ascx" TagPrefix="Mode" TagName="ModeDDL" %>

<Country:CountryDDL ID="CountryDropList" runat="server" />
via
<Mode:ModeDDL ID="ModeDropList" runat="server" />--%>

<asp:DropDownList ID="cartShipCountry" runat="server"
                                  DataSourceID="CountriesODS"
                                  DataTextField="Name" 
                                  DataValueField="ID"
                                  EnableViewState="False"
                                  AutoPostBack="True"
                                  OnDataBound="cartShipCountry_DataBound"
                                  OnSelectedIndexChanged="cartShipCountry_SelectedIndexChanged" />
via 
<asp:DropDownList ID="cartShipMode" runat="server"
                  DataSourceID="ModesODS"
                  DataTextField="Name" 
                  DataValueField="ID"
                  EnableViewState="False"
                  AutoPostBack="True"
                  OnDataBound="cartShipMode_DataBound"
                  OnSelectedIndexChanged="cartShipMode_SelectedIndexChanged" />
                                       
<asp:ObjectDataSource ID="CountriesODS" runat="server"
                      TypeName="eStoreBLL.CountriesBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getCountries"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute" />
                      
<asp:ObjectDataSource ID="ModesODS" runat="server" 
                      TypeName="eStoreBLL.ModesBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getModes"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute" />