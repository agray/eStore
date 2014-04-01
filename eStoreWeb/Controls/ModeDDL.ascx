<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModeDDL.ascx.cs" Inherits="eStoreWeb.Controls.ModeDDL" %>

<asp:DropDownList ID="ModeDropDownList" runat="server"
                  DataSourceID="ModesODS"
                  DataTextField="Name" 
                  DataValueField="ID"
                  EnableViewState="False"
                  AutoPostBack="True"
                  OnSelectedIndexChanged="OnSelectedIndexChanged" />
                                       
<asp:ObjectDataSource ID="ModesODS" runat="server" 
                      TypeName="eStoreBLL.ModesBLL"
                      OldValuesParameterFormatString="original_{0}" 
                      SelectMethod="getModes"
                      EnableCaching="True"
                      CacheDuration="3600"
                      CacheExpirationPolicy="Absolute" />