<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheckOutMaster.Master" AutoEventWireup="true" CodeBehind="Checkout5.aspx.cs" Inherits="eStoreWeb.Checkout5" %>
<%@ MasterType VirtualPath="~/MasterPages/CheckOutMaster.Master" %>
<%@ Import Namespace="eStoreWeb" %>
<%@ Import Namespace="phoenixconsulting.common.handlers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%if(IsApproved(SessionHandler.Instance.IsPaymentApproved, SessionHandler.Instance.PaymentResponseCode)) { %>
        <h1>Payment Approved</h1>
        <p>
            Thank you for shopping with <%=ApplicationHandler.Instance.TradingName%>.
        </p>
        <p>
            <asp:HyperLink ID="HomeLink" runat="server" NavigateUrl="~/">Return Home</asp:HyperLink>
        </p>
    <%} else { %>
        <h1>Payment Declined</h1>
        <p>
            <asp:Label ID="DeclinedDetailsLabel" runat="server">
                <%=SessionHandler.Instance.PaymentResponseText%>
            </asp:Label>
        </p>
        <asp:Button ID="BackButton" runat="server" 
                    Text="Back"
                    OnClick="GoToPreviousPage" />
    <%} %>
</asp:Content>
