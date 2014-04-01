<%@Page Language="C#" Explicit="True" %>

<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Xml" %>

<%
// Do not allow browser to cache this page.
Response.CacheControl = "no-cache";
%>

<%
/*
SecureXML Example.
----------------------------
This example is based on the SecureXML documentation and should be used in conjunction with that documentation. Please feel free to edit this code to suit your own requirements. This example comes with no warranty or support. Use at your own risk.


Important Note
---------------
If you receive the following error: “HttpWebRequet Protocol Error, the underlying HTTP connection has been closed.”

Try adding the following to your web.config file :
<system.net>
	<settings>
		<httpWebRequest useUnsafeHeaderParsing="true" />
	</settings>
 </system.net>

In .NET Framework 1.1 Service Pack 1, Microsoft locked down the logic in their HTTP protocol API's to prevent possible security holes. With this change, .NET applications will now throw an HTTP protocol violation if the server responds with any HTTP headers that Microsoft considers invalid. It is likely that if you get this error, it is because the server is sending back a header named 'Last Modified' instead of the standard header named 'last-modified'.
*/
%>

<script runat="server" language="C#">

void Page_Load(Object s, EventArgs e)
{
    if(phoenixconsulting.common.handlers.RequestHandler.Instance.PageID == "process")
	{
		setTimestamp();
		setURL();
		setMessage();
		processXMLmessage();
	}
}

string messageID, messageTimestamp, apiVersion, requestType, merchantID, statusCode, statusDescription, txnType, txnSource, amount, currency, purchaseOrderNo, approved, responseCode, responseText, settlementDate, txnID, preauthID, pan, expiryDate, cardType, cardDescription, bsbNumber, accountNumber, accountName, strURL, message, myTimestamp;

public void setTimestamp()
{
	// Generate GMT timestamp.
	DateTime value;
	value = DateTime.UtcNow;
	myTimestamp = value.ToString("yyyyMMddHHmmss000000zz");
    //Response.Write("myTimestamp = " + myTimestamp + "<br />");
}

public void setURL()
{
	if (Request.Form["server"] == "live")
	{
		if (Request.Form["payment_type"] == "15" || Request.Form["payment_type"] == "17")
		{
			strURL = "https://www.securepay.com.au/xmlapi/directentry";
		}
		else
		{
			strURL = "https://www.securepay.com.au/xmlapi/payment";
		}
	}
	else
	{
		if (Request.Form["payment_type"] == "15" || Request.Form["payment_type"] == "17")
		{
			strURL = "https://www.securepay.com.au/test/directentry";
		}
		else
		{
			strURL = "http://test.securepay.com.au/xmlapi/payment";
			// Or if using SSL:
			// strURL = "https://www.securepay.com.au/test/payment";
		}
	}
    //Response.Write("strURL = " + strURL + "<br />");
}

public void setMessage()
{
    string paymentAmount;
    paymentAmount = Request.Form["payment_amount"];

    if (paymentAmount != null)
    {
        paymentAmount = paymentAmount.Replace(".", "");
    }
    
    message = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"+
    "<SecurePayMessage>"+
	    "<MessageInfo>"+
		    "<messageID>8af793f9af34bea0cf40f5fb5c630c</messageID>"+
		    "<messageTimestamp>"+myTimestamp+"</messageTimestamp>"+
		    "<timeoutValue>60</timeoutValue>"+
		    "<apiVersion>xml-4.2</apiVersion>"+
	    "</MessageInfo>"+
	    "<MerchantInfo>"+
		    "<merchantID>"+Request.Form["merchant_id"]+"</merchantID>"+
		    "<password>"+Request.Form["transaction_password"]+"</password>"+
	    "</MerchantInfo>"+
	    "<RequestType>"+Request.Form["request_type"]+"</RequestType>"+
	    "<Payment>"+
		    "<TxnList count=\"1\">"+
			    "<Txn ID=\"1\">"+
				    "<txnType>"+Request.Form["payment_type"]+"</txnType>"+
				    "<txnSource>23</txnSource>"+
				    "<amount>"+paymentAmount+"</amount>"+
				    "<purchaseOrderNo>"+Request.Form["payment_reference"]+"</purchaseOrderNo>"+
				    "<currency>"+Request.Form["currency"]+"</currency>"+
				    "<preauthID>"+Request.Form["preauthid"]+"</preauthID>"+
				    "<txnID>"+Request.Form["txnid"]+"</txnID>"+
				    "<CreditCardInfo>"+
					    "<cardNumber>"+Request.Form["card_number"]+"</cardNumber>"+
					    "<cvv>"+Request.Form["card_cvv"]+"</cvv>"+
					    "<expiryDate>"+Request.Form["card_expiry_month"]+"/"+Request.Form["card_expiry_year"]+"</expiryDate>"+
				    "</CreditCardInfo>"+
				    "<DirectEntryInfo>"+
					    "<bsbNumber>"+Request.Form["bsb_number"]+"</bsbNumber>"+
					    "<accountNumber>"+Request.Form["account_number"]+"</accountNumber>"+
					    "<accountName>"+Request.Form["account_name"]+"</accountName>"+
				    "</DirectEntryInfo>"+
			    "</Txn>"+
		    "</TxnList>"+
	    "</Payment>"+
    "</SecurePayMessage>";
    //Response.Write("message = <br /><pre>" + message + "</pre><br />");
}

public void processXMLmessage()
{
	HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strURL);
	ASCIIEncoding encoding = new ASCIIEncoding();
	Byte[] byte1 = encoding.GetBytes(message);

    myRequest.Method = "POST";
	myRequest.ContentType="application/x-www-form-urlencoded";
	myRequest.ContentLength = byte1.Length;
	myRequest.Pipelined = false;
	myRequest.KeepAlive = false;

    Stream newStream;
    newStream = myRequest.GetRequestStream();
    newStream.Write(byte1, 0, byte1.Length);
    newStream.Close();
    
    try
    {
        //Get the data as an HttpWebResponse object
        HttpWebResponse resp = (HttpWebResponse)myRequest.GetResponse();	
        
		//Convert the data into a string (assumes that you are requesting text)
        StreamReader sr = new StreamReader(resp.GetResponseStream());
        XmlTextReader xmlreader = new XmlTextReader(sr);
        
        xmlreader.WhitespaceHandling = WhitespaceHandling.None;
        XmlDocument myXMLdocument = new XmlDocument();
        myXMLdocument.Load(xmlreader);
		
        XmlNodeList myNodeList;
        myNodeList = myXMLdocument.GetElementsByTagName("*");

        sr.Close();
        xmlreader.Close();
        
        for (int counter = 1; counter < myNodeList.Count; counter++)
        {
            switch (myNodeList.Item(counter).Name.ToString())
            {
                case "messageID":
                    messageID = myNodeList.Item(counter).InnerXml.ToString();
                    messageIDLabel.Text = messageID;
                    break;
                case "messageTimestamp":
                    messageTimestamp = myNodeList.Item(counter).InnerXml.ToString();
                    messageTimestampLabel.Text = messageTimestamp;
                    break;
                case "apiVersion":
                    apiVersion = myNodeList.Item(counter).InnerXml.ToString();
                    apiVersionLabel.Text = apiVersion;
                    break;
                case "RequestType":
                    requestType = myNodeList.Item(counter).InnerXml.ToString();
                    requestTypeLabel.Text = requestType;
                    break;
                case "merchantID":
                    merchantID = myNodeList.Item(counter).InnerXml.ToString();
                    merchantIDLabel.Text = merchantID;
                    break;
                case "statusCode":
                    statusCode = myNodeList.Item(counter).InnerXml.ToString();
                    statusCodeLabel.Text = statusCode;
                    break;
                case "statusDescription":
                    statusDescription = myNodeList.Item(counter).InnerXml.ToString();
                    statusDescriptionLabel.Text = statusDescription;
                    break;
                case "txnType":
                    txnType = myNodeList.Item(counter).InnerXml.ToString();
                    txnTypeLabel.Text = txnType;
                    break;
                case "txnSource":
                    txnSource = myNodeList.Item(counter).InnerXml.ToString();
                    txnSourceLabel.Text = txnSource;
                    break;
                case "amount":
                    amount = myNodeList.Item(counter).InnerXml.ToString();
                    amountLabel.Text = amount;
                    break;
                case "currency":
                    currency = myNodeList.Item(counter).InnerXml.ToString();
                    currencyLabel.Text = currency;
                    break;
                case "purchaseOrderNo":
                    purchaseOrderNo = myNodeList.Item(counter).InnerXml.ToString();
                    purchaseOrderNoLabel.Text = purchaseOrderNo;
                    break;
                case "approved":
                    approved = myNodeList.Item(counter).InnerXml.ToString();
                    approvedLabel.Text = approved;
                    break;
                case "responseCode":
                    responseCode = myNodeList.Item(counter).InnerXml.ToString();
                    responseCodeLabel.Text = responseCode;
                    break;
                case "responseText":
                    responseText = myNodeList.Item(counter).InnerXml.ToString();
                    responseTextLabel.Text = responseText;
                    break;
                case "settlementDate":
                    settlementDate = myNodeList.Item(counter).InnerXml.ToString();
                    settlementDateLabel.Text = settlementDate;
                    break;
                case "txnID":
                    txnID = myNodeList.Item(counter).InnerXml.ToString();
                    txnIDLabel.Text = txnID;
                    break;
                case "preauthID":
                    preauthID = myNodeList.Item(counter).InnerXml.ToString();
                    preauthIDLabel.Text = preauthID;
                    break;
                case "pan":
                    pan = myNodeList.Item(counter).InnerXml.ToString();
                    panLabel.Text = pan;
                    break;
                case "expiryDate":
                    expiryDate = myNodeList.Item(counter).InnerXml.ToString();
                    expiryDateLabel.Text = expiryDate;
                    break;
                case "cardType":
                    cardType = myNodeList.Item(counter).InnerXml.ToString();
                    cardTypeLabel.Text = cardType;
                    break;
                case "cardDescription":
                    cardDescription = myNodeList.Item(counter).InnerXml.ToString();
                    cardDescriptionLabel.Text = cardDescription;
                    break;
                case "bsbNumber":
                    bsbNumber = myNodeList.Item(counter).InnerXml.ToString();
                    bsbNumberLabel.Text = bsbNumber;
                    break;
                case "accountNumber":
                    accountNumber = myNodeList.Item(counter).InnerXml.ToString();
                    accountNumberLabel.Text = accountNumber;
                    break;
                case "accountName":
                    accountName = myNodeList.Item(counter).InnerXml.ToString();
                    accountNameLabel.Text = accountName;
                    break;
            }
         }
        
         resp.Close();
        
    }

    catch (WebException wex)
    {
        Response.Write("<font color=red>There has been an error.<br />Status: " + wex.Status + " Message: " + wex.Message + "</font>");
    }
}
</script>

<%
switch (Request["pageid"])
{
case null: 
%>
	<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
	<html>
	<head>
		<title>SecureXML C#.Net Example Payment Page.</title>
	</head>

	<body>
		<form method="post" action="SecureXMLExample.aspx?pageid=process">
		<table cellspacing="0" cellpadding="5" border="0" align="center">
		<tr>
			<td align="center">
				<img src="http://www.securepay.com.au/securepay/common/images/logos/securepay380x150white.gif" alt="SecurePay" width="190" height="75" />
				<p><b>SecureXML Example - C#.Net</b></p>
			</td>
		</tr>
		<tr>
			<td>
				<fieldset><legend>Payment Details</legend>
				<table cellspacing="0" cellpadding="5" border="0" width="100%">
				<tr>
					<td width="370px">Request Type:</td>
					<td>
						<select name="request_type"> 
							<option value="echo">Echo Request</option>
							<option value="payment" selected="selected">Payment Request</option>
						</select>
					</td>
				</tr>
				<tr>
					<td>Payment Type:</td>
					<td>
						<select name="payment_type"> 
							<option value="0">Credit Card Payment</option>
							<option value="4">Credit Card Refund</option>
							<option value="6">Credit Card Reversal</option>
							<option value="10">Credit Card Pre-Authorise</option>
							<option value="11">Credit Card Complete</option>
							<option value="15">Direct Debit</option>
							<option value="17">Direct Credit</option>
						</select>
					</td>
				</tr>
				<tr>
						<td>Merchant ID:</td>
					<td><input type="text" name="merchant_id" size="7" value="ABC0001" /></td>
				</tr>
				<tr>
					<td>Transaction Password:</td>
					<td><input type="text" name="transaction_password" size="30" value="abc123" /></td>
				</tr>
				<tr>
					<td>Server:</td>
					<td>
						<select name="server"> 
							<option value="test" selected="selected">Test</option>
							<option value="live">Live</option>
						</select>
					</td>
				</tr>
				<tr>
					<td>Amount <br />
						<font size="2">
							&nbsp;&nbsp;-cents value simulates the response code on Test system.<br />
						</font>
					</td>
					<td><input type="text" name="payment_amount" size="6" value="1.00" /></td>
				</tr>
				<tr>
					<td>Payment Ref <br />
						<font size="2">
							&nbsp;&nbsp;-Refunds/Reversals/Completes: must match original payments ref.<br />
						</font>
					</td>
					<td><input type="text" name="payment_reference" size="50" value="EnterUniqueRefNo" /></td>
				</tr>
				</table>
				</fieldset>
			</td>
		</tr>
		<tr>
			<td>
					<fieldset><legend>Credit Card Details</legend>
					<table cellspacing="0" cellpadding="5" border="0" width="100%">
					<tr>
						<td width="370px">Name:</td>
						<td><input type="text" name="card_holder" size="40" value="John Smith" /></td>
					</tr>
					<tr>
						<td>Card No:</td>
						<td><input name="card_number" type="text" size="16" maxlength="19" value="4444333322221111" /></td>
					</tr>
					<tr>
						<td>CVV No:</td>
						<td><input type="text" name="card_cvv" size="3" value="987" maxlength="6" /></td>
					</tr>
					<tr>
						<td>Exp:</td>
						<td>
							<select name="card_expiry_month"> 
								<option value="01">01</option>
								<option value="02">02</option>
								<option value="03">03</option>
								<option value="04">04</option>
								<option value="05">05</option>
								<option value="06">06</option>
								<option value="07">07</option>
								<option value="08">08</option>
								<option value="09">09</option>
								<option value="10">10</option>
								<option value="11">11</option>
								<option value="12">12</option>
							</select>
							&nbsp;/&nbsp;  
							<select name="card_expiry_year"> 
								<option value="05">05</option>
								<option value="06">06</option>
								<option value="07">07</option>
								<option value="08" selected="selected">08</option>
								<option value="09">09</option>
								<option value="10">10</option>
								<option value="11">11</option>
							</select>
						</td>
					</tr>
					<tr>
						<td>Pre-Auth ID <br />
							<font size="2">
								&nbsp;&nbsp;-for Completion payments only.<br />
							</font>
						</td>
						<td><input type="text" name="preauthid" size="6" value="" /></td>
					</tr>
					<tr>
						<td>Transaction ID <br />
							<font size="2">
								&nbsp;&nbsp;-for Refund/Reversal payments only.<br />
							</font>
						</td>
						<td><input type="text" name="txnid" size="16" value="" /></td>
					</tr>
					<tr>
						<td>Currency:</td>
						<td>
							<select name="currency"> 
								<option value="AUD">Australian Dollars</option>
								<option value="USD">U.S. Dollars</option>
								<option value="GBP">British Pounds</option>
								<option value="EUR">Euros</option>
								<option value="NZD">New Zealand Dollars</option>
							</select>
						</td>
					</tr>
					</table>
					</fieldset>
			</td>
		</tr>
		<tr>
			<td>
					<fieldset><legend>Direct Entry Details</legend>
				<table cellspacing="0" cellpadding="5" border="0" width="100%">
				<tr>
					<td width="370px">BSB Number</td>
					<td><input type="text" name="bsb_number" size="6" value="123456" /></td>
				</tr>
				<tr>
					<td>Account Number</td>
					<td><input type="text" name="account_number" size="9" value="123456789" /></td>
				</tr>
				<tr>
					<td>Account Name</td>
					<td><input type="text" name="account_name" size="50" value="Test" /></td>
				</tr>
				</table>
				</fieldset>
			</td>
		</tr>
		<tr>
			<td>
				<fieldset><legend></legend>
				<table cellspacing="0" cellpadding="5" border="0" width="100%">
				<tr>
					<td width="370px" align="center"><input type="submit" value="Purchase" name="submit" /></td>
					<td align="center"><input type="reset" value="Reset" name="reset" /></td>
				</tr>
				</table>
				</fieldset>
			</td>
		</tr>
		</table>
		</form>
	</body>
	</html>

<%
break;
case "process":
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
	<title>SecurePay - SecureXML Example Response Page.</title>
</head>

<body>
<center>
	<p><img src="http://www.securepay.com.au/securepay/common/images/logos/securepay380x150white.gif" alt="SecurePay" width="190" height="75" /></p>
	<p><b>XML Response from SecurePay Server - C#.Net</b></p>
</center>
<hr />
messageID = <asp:Label id="messageIDLabel" runat="server" /><br />
messageTimestamp = <asp:Label id="messageTimestampLabel" runat="server" /><br />
apiVersion = <asp:Label id="apiVersionLabel" runat="server" /><br />
requestType = <asp:Label id="requestTypeLabel" runat="server" /><br />
merchantID = <asp:Label id="merchantIDLabel" runat="server" /><br />
statusCode = <asp:Label id="statusCodeLabel" runat="server" /><br />
statusDescription = <asp:Label id="statusDescriptionLabel" runat="server" /><br />
txnType = <asp:Label id="txnTypeLabel" runat="server" /><br />
txnSource = <asp:Label id="txnSourceLabel" runat="server" /><br />
amount = <asp:Label id="amountLabel" runat="server" /><br />
currency = <asp:Label id="currencyLabel" runat="server" /><br />
purchaseOrderNo = <asp:Label id="purchaseOrderNoLabel" runat="server" /><br />
approved = <asp:Label id="approvedLabel" runat="server" /><br />
responseCode = <asp:Label id="responseCodeLabel" runat="server" /><br />
responseText = <asp:Label id="responseTextLabel" runat="server" /><br />
settlementDate = <asp:Label id="settlementDateLabel" runat="server" /><br />
txnID = <asp:Label id="txnIDLabel" runat="server" /><br />
preauthID = <asp:Label id="preauthIDLabel" runat="server" /><br />
pan = <asp:Label id="panLabel" runat="server" /><br />
expiryDate = <asp:Label id="expiryDateLabel" runat="server" /><br />
cardType = <asp:Label id="cardTypeLabel" runat="server" /><br />
cardDescription = <asp:Label id="cardDescriptionLabel" runat="server" /><br />
bsbNumber = <asp:Label id="bsbNumberLabel" runat="server" /><br />
accountNumber = <asp:Label id="accountNumberLabel" runat="server" /><br />
accountName = <asp:Label id="accountNameLabel" runat="server" /><br />
<hr />
<p><a href="SecureXMLExample.aspx">Return to Payment Page</a></p>
</body>
</html>

<%
break;
}
%>