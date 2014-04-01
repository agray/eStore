#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2008-2013, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using com.phoenixconsulting.common.mail;
using eStoreBLL;
using phoenixconsulting.common.handlers;
using phoenixconsulting.common.logging;

namespace phoenixconsulting.paymentservice.creditcard {
    public class CreditCardPaymentProcessor {

        #region Attributes
        //****************************
        // Attributes
        //****************************
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        string messageID; 
        string messageTimestamp;
        string apiVersion;
        string requestType;
        string merchantID;
        string statusCode;
        string statusDescription;
        string txnType;
        string txnSource;
        string amount;
        string currency;
        string purchaseOrderNo;
        string approved;
        string responseCode;
        string responseText;
        string settlementDate;
        string txnID;
        string preauthID;
        string pan;
        string expiryDate;
        string cardType;
        string cardDescription;
        string bsbNumber;
        string accountNumber;
        string accountName;
        string strURL;
        string message;
        string timestamp;

        // Additional variables for Periodic Transactions.
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        string actionType;
        string clientID;
        string successful;
        string recurringFlag;
        string creditFlag;
        string periodicType;
        string paymentInterval;
        string startDate;
        string endDate;
        string merchantID7Char;
        string merchantID5Char;
        //string visaRecurring;

        #endregion

        #region Constructor
        public CreditCardPaymentProcessor() {
            messageID = null;
            messageTimestamp = null;
            apiVersion = String.Empty;
            requestType = String.Empty;
            merchantID = String.Empty;
            statusCode = String.Empty;
            statusDescription = String.Empty;
            txnType = String.Empty;
            txnSource = String.Empty;
            amount = String.Empty;
            currency = String.Empty;
            purchaseOrderNo = String.Empty;
            approved = String.Empty;
            responseCode = String.Empty;
            responseText = String.Empty;
            settlementDate = String.Empty;
            txnID = String.Empty;
            preauthID = String.Empty;
            pan = String.Empty;
            expiryDate = String.Empty;
            cardType = String.Empty;
            cardDescription = String.Empty;
            bsbNumber = String.Empty;
            accountNumber = String.Empty;
            accountName = String.Empty;
            strURL = String.Empty;
            message = String.Empty;
            timestamp = String.Empty;

            actionType = String.Empty;
            clientID = String.Empty;
            successful = String.Empty;
            recurringFlag = String.Empty;
            creditFlag = String.Empty;
            periodicType = String.Empty;
            paymentInterval = String.Empty;
            startDate = String.Empty;
            endDate = String.Empty;
            merchantID7Char = String.Empty;
            merchantID5Char = String.Empty;
            //visaRecurring = String.Empty;
        }
        #endregion

        #region "PaymentMethods"
        //****************************
        //Payment Methods
        //****************************
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public void processPayment(int orderID, HttpRequest request) {
            SetMerchantId();
            SetTimestamp();
            SetUrlAndMessage(orderID, request);
            ProcessXmlMessage();

            SessionHandler.Instance.PaymentResponseText = responseText;
            SessionHandler.Instance.PaymentResponseCode = responseCode;
            SessionHandler.Instance.IsPaymentApproved = approved;

            //Update order status with result from CC Processor.
            OrdersBLL order = new OrdersBLL();
            order.updateOrderStatus(Convert.ToInt32(orderID),
                                    Convert.ToInt32(responseCode),
                                    txnID,
                                    settlementDate,
                                    "",
                                    "",
                                    "",
                                    0,
                                    "",
                                    "",
                                    "");
            order = null;

            if(IsCCApproved(SessionHandler.Instance.IsPaymentApproved, SessionHandler.Instance.PaymentResponseCode)) {
                //Send Order Confirmation email to customer
                MailMessageBuilder.SendOrderConfirmationEmail();
                LoggerUtil.auditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_CC_PAYMENT_SUCCESS,
                                "StoreSite",
                                null,
                                null, null, null, null, null);
            } else {
                LoggerUtil.auditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_CC_PAYMENT_REJECTED,
                                "StoreSite",
                                null,
                                null, null, null, null, null);
            }
        }

        #endregion

        #region "UtilMethods"
        //****************************
        //Util Methods
        //****************************
        private bool IsCCApproved(string approved, string responseCode) {
            return (approved.Equals("Yes") && responseCode.Equals("00"));
        }

        public void SetMerchantId() {
            merchantID7Char = ApplicationHandler.Instance.CCProcessorMerchantID;
            merchantID5Char = ApplicationHandler.Instance.CCProcessorMerchantID.Remove(5, 2);
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public void SetTimestamp() {
            // Generate GMT timestamp.
            DateTime value;
            value = DateTime.UtcNow;
            timestamp = value.ToString("yyyyMMddHHmmss000000zz");
            //Response.Write("myTimestamp = " + myTimestamp + "<br />");
        }

        public void SetUrlAndMessage(int orderID, HttpRequest request) {
            switch(ApplicationHandler.Instance.CCProcessorRequestType) {
                case "payment":
                    if(ApplicationHandler.Instance.CCProcessorServer == "live") {
                        strURL = ApplicationHandler.Instance.CCProcessorLivePaymentURL;
                        message = SetPaymentCreditCard(merchantID7Char, timestamp, orderID, request);
                    } else {
                        strURL = ApplicationHandler.Instance.CCProcessorTestPaymentURL;
                        //Or if using SSL:
                        //strURL = "https://www.securepay.com.au/test/payment";
                        message = SetPaymentCreditCard(merchantID7Char, timestamp, orderID, request);
                    }
                    break;
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        public string SetPaymentCreditCard(string merchantId, string time, int orderID, HttpRequest request) {
            double amount = SessionHandler.Instance.TotalCost;
            //double amount = 100;  //This is to get Approved in Test Env
            string tempMessage = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<SecurePayMessage>" +
                "<MessageInfo>" +
                    "<messageID>" + orderID + "</messageID>" +
                    "<messageTimestamp>" + time + "</messageTimestamp>" +
                    "<timeoutValue>60</timeoutValue>" +
                    "<apiVersion>xml-4.2</apiVersion>" +
                "</MessageInfo>" +
                "<MerchantInfo>" +
                    "<merchantID>" + merchantId + "</merchantID>" +
                    "<password>" + ApplicationHandler.Instance.CCProcessorPassword + "</password>" +
                "</MerchantInfo>" +
                "<RequestType>" + ApplicationHandler.Instance.CCProcessorRequestType + "</RequestType>" +
                "<Payment>" +
                    "<TxnList count=\"1\">" +
                        "<Txn ID=\"1\">" +
                            "<txnType>" + ApplicationHandler.Instance.CCProcessorPaymentType + "</txnType>" +
                            "<txnSource>23</txnSource>" +
                            "<amount>" + amount.ToString("c").Replace(".", "").Replace(",", "").Replace("$", "").Replace("€", "") + "</amount>" +
                            "<purchaseOrderNo>" + orderID + "</purchaseOrderNo>" +
                            "<currency>AUD</currency>" +
                            "<preauthID>" + SessionHandler.Instance.PreAuthId + "</preauthID>" +
                            "<txnID>" + SessionHandler.Instance.TxnId + "</txnID>" +
                            "<CreditCardInfo>" +
                                "<cardNumber>" + SessionHandler.Instance.CardNumber + "</cardNumber>" +
                                "<cvv>" + SessionHandler.Instance.CVV + "</cvv>" +
                                "<expiryDate>" + SessionHandler.Instance.CCExpiryMonthItem + "/" + SessionHandler.Instance.CCExpiryYearItem.Remove(0, 2) + "</expiryDate>" +
                            "</CreditCardInfo>" +
                            "<BuyerInfo>" +
                                "<firstName>" + SessionHandler.Instance.BillingFirstName + "</firstName>" +
                                "<lastName>" + SessionHandler.Instance.BillingLastName + "</lastName>" +
                                "<zipCode>" + SessionHandler.Instance.BillingPostcode + "</zipCode>" +
                                "<town>" + SessionHandler.Instance.BillingStateRegion + "</town>" +
                                "<billingCountry>" + SessionHandler.Instance.BillingCountryCode + "</billingCountry>" +
                                "<deliveryCountry>" + SessionHandler.Instance.ShippingCountryCode + "</deliveryCountry>" +
                                "<emailAddress>" + SessionHandler.Instance.BillingEmailAddress + "</emailAddress>" +
                                "<ip>" + request.UserHostAddress + "</ip>" +
                            "</BuyerInfo>" +
                        "</Txn>" +
                    "</TxnList>" +
                "</Payment>" +
            "</SecurePayMessage>";

            SessionHandler.Instance.CCRequestXml = tempMessage;
            return tempMessage;
        }

        public void ProcessXmlMessage() {
            SessionHandler.Instance.Url = strURL;
            //Uri newUri = new Uri(strURL);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strURL);
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] byte1 = encoding.GetBytes(message);

            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = byte1.Length;
            myRequest.Pipelined = false;
            myRequest.KeepAlive = false;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            try {
                //Get the data as an HttpWebResponse object
                HttpWebResponse resp = (HttpWebResponse)myRequest.GetResponse();

                //Convert the data into a string (assumes that you are requesting text)
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                XmlTextReader xmlreader = new XmlTextReader(sr);

                xmlreader.WhitespaceHandling = WhitespaceHandling.None;
                XmlDocument myXMLdocument = new XmlDocument();
                myXMLdocument.Load(xmlreader);

                SessionHandler.Instance.CCResponseXml = myXMLdocument.InnerXml;

                XmlNodeList myNodeList;
                myNodeList = myXMLdocument.GetElementsByTagName("*");

                sr.Close();
                xmlreader.Close();

                for(int counter = 1; counter < myNodeList.Count; counter++) {
                    XmlNode node = myNodeList.Item(counter);
                    if(node != null) {
                        string innerXml = node.InnerXml;
                        switch(node.Name) {
                            case "messageID":
                                messageID = innerXml;
                                break;
                            case "messageTimestamp":
                                messageTimestamp = innerXml;
                                break;
                            case "apiVersion":
                                apiVersion = innerXml;
                                break;
                            case "RequestType":
                                requestType = innerXml;
                                break;
                            case "merchantID":
                                merchantID = innerXml;
                                break;
                            case "statusCode":
                                statusCode = innerXml;
                                break;
                            case "statusDescription":
                                statusDescription = innerXml;
                                break;
                            case "txnType":
                                txnType = innerXml;
                                break;
                            case "txnSource":
                                txnSource = innerXml;
                                break;
                            case "amount":
                                amount = innerXml;
                                break;
                            case "currency":
                                currency = innerXml;
                                break;
                            case "purchaseOrderNo":
                                purchaseOrderNo = innerXml;
                                break;
                            case "approved":
                                approved = innerXml;
                                break;
                            case "responseCode":
                                responseCode = innerXml;
                                break;
                            case "responseText":
                                responseText = innerXml;
                                break;
                            case "settlementDate":
                                settlementDate = innerXml;
                                break;
                            case "txnID":
                                txnID = innerXml;
                                break;
                            case "preauthID":
                                preauthID = innerXml;
                                break;
                            case "pan":
                                pan = innerXml;
                                break;
                            case "expiryDate":
                                expiryDate = innerXml;
                                break;
                            case "cardType":
                                cardType = innerXml;
                                break;
                            case "cardDescription":
                                cardDescription = innerXml;
                                break;
                            case "bsbNumber":
                                bsbNumber = innerXml;
                                break;
                            case "accountNumber":
                                accountNumber = innerXml;
                                break;
                            case "accountName":
                                accountName = innerXml;
                                break;
                                // Additional Periodic values.    
                            case "actionType":
                                actionType = innerXml;
                                break;
                            case "clientID":
                                clientID = innerXml;
                                break;
                            case "successful":
                                successful = innerXml;
                                break;
                            case "recurringFlag":
                                recurringFlag = innerXml;
                                break;
                            case "creditFlag":
                                creditFlag = innerXml;
                                break;
                            case "periodicType":
                                periodicType = innerXml;
                                break;
                            case "paymentInterval":
                                paymentInterval = innerXml;
                                break;
                            case "startDate":
                                startDate = innerXml;
                                break;
                            case "endDate":
                                endDate = innerXml;
                                break;
                        }
                    }
                }
                resp.Close();
            } catch(WebException wex) {
                //TODO: Offline Processor
                LoggerUtil.auditLog(NLog.LogLevel.Info,
                                AuditEventType.SUBMIT_CC_PAYMENT_FAILED,
                                "StoreSite",
                                null,
                                wex.Message, wex.Source, wex.Response.ToString(), wex.Status.ToString(), null);
                //Response.Write("<font color=red>There has been an error.<br />Status: " + wex.Status + " Message: " + wex.Message + "</font>");
            }
        }
        #endregion
    }
}