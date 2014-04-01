<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YesNoDDL.ascx.cs" Inherits="eStoreWeb.Controls.YesNoDDL" %>

<asp:DropDownList ID="YesNoDropDownList" runat="server"
                  OnSelectedIndexChanged="OnSelectedIndexChanged">
    <asp:ListItem Value="1">Yes</asp:ListItem>
    <asp:ListItem Value="0">No</asp:ListItem>
</asp:DropDownList>