<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SameAddressDDL.ascx.cs" Inherits="eStoreWeb.Controls.SameAddressDDL" %>

<asp:DropDownList ID="SameAddressDropDownList" runat="server"  
                  AutoPostBack="True"
                  OnSelectedIndexChanged="OnSelectedIndexChanged">
    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="2">No</asp:ListItem>
</asp:DropDownList>
