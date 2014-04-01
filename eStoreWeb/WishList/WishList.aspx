<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="WishList.aspx.cs" Inherits="eStoreWeb.WishList.WishList" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<%@ Register Src="~/Controls/WishListTable.ascx" TagName="WishList" TagPrefix="eStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="WishListUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
                <h1>Wish List</h1>
                <eStore:WishList ID="WishListTable" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>