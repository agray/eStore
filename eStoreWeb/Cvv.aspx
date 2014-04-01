<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CVV.aspx.cs" Inherits="eStoreWeb.CVV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>What is CVV</title>
        <link href="Stylesheets/PP_style.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <div id="DivMain">
            <h1>Card Verification Value (CVV)</h1>
            <h2>Visa Card and Master Card</h2>
            <p>
                For Visa and MasterCard the CVV number is the last 3 digits on the Signature Panel
                on the back of the credit card.
            </p>
            <asp:Image ID="CVVMasterVisaImage" runat="server"
                       AlternateText="CVV Master Visa"
                       ImageUrl="~/Images/System/CVVMasterVisa.jpg" />
            <h2>Amex</h2>
            <p>
                For American Express, the CVV number is the last 4 digits (printed in white) on
                the front of the credit card.
            </p>
            <asp:Image ID="Image1" runat="server"
                       AlternateText="CVV Amex"
                       ImageUrl="~/Images/System/CVVAmex.jpg" />
        </div>
    </body>
</html>