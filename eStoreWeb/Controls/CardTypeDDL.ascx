<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardTypeDDL.ascx.cs" Inherits="eStoreWeb.Controls.CardTypeDDL" %>

<asp:DropDownList ID="CardTypeDropDownList" runat="server"
                  Font-Size="Small">
    <asp:ListItem Selected="True"></asp:ListItem>
    <asp:ListItem>Visa</asp:ListItem>
    <asp:ListItem>MasterCard</asp:ListItem>
    <asp:ListItem>AMEX</asp:ListItem>
</asp:DropDownList>