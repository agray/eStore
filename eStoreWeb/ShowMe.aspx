<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMe.aspx.cs" Inherits="eStoreWeb.ShowMe" %>

<%
// Do not allow browser to cache this page.
Response.CacheControl = "no-cache";
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="eStoreWeb" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <h1>URL submitted to was:</h1>
    <p><%=HttpUtility.HtmlEncode(SessionHandler.Url)%></p>
    
    <h1>RequestXML is:</h1>
    <p><%=HttpUtility.HtmlEncode(SessionHandler.CCRequestXml)%></p>
    
    <h1>ResponseXML is:</h1>
    <p><%=HttpUtility.HtmlEncode(SessionHandler.CCResponseXml)%></p>
    
</body>
</html>
