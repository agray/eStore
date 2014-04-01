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
using System.Text;
using System.Web;
using com.phoenixconsulting.common.mail;
using com.phoenixconsulting.exceptions;
using eStoreBLL;
using eStoreDAL;
using NLog;
using phoenixconsulting.common.handlers;

namespace eStoreWeb {
    public class Global : HttpApplication {
        private Logger errorLogger = LogManager.GetLogger("ErrorLogger");

        protected void Application_Start(object sender, EventArgs e) {
            SettingsBLL settings = new SettingsBLL();
            DAL.SettingsDataTable dt;
            dt = settings.getSettings();

            try {
                ApplicationHandler.Instance.TradingName = dt.Rows[0]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorMerchantID = dt.Rows[1]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorPassword = dt.Rows[2]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorPaymentType = int.Parse(dt.Rows[3]["Value"].ToString());
                ApplicationHandler.Instance.CCProcessorLiveEchoURL = dt.Rows[4]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorTestEchoURL = dt.Rows[5]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorLivePaymentURL = dt.Rows[6]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorTestPaymentURL = dt.Rows[7]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorLivePaymentDCURL = dt.Rows[8]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorTestPaymentDCURL = dt.Rows[9]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorRequestType = dt.Rows[10]["Value"].ToString();
                ApplicationHandler.Instance.CCProcessorServer = dt.Rows[11]["Value"].ToString();
                ApplicationHandler.Instance.RefundAdminFeePercent = double.Parse(dt.Rows[12]["Value"].ToString());
                ApplicationHandler.Instance.CancelAdminFeePercent = double.Parse(dt.Rows[13]["Value"].ToString());
                ApplicationHandler.Instance.SMTPServer = dt.Rows[14]["Value"].ToString();
                ApplicationHandler.Instance.HomePageURL = dt.Rows[15]["Value"].ToString();
                ApplicationHandler.Instance.SystemEmailAddress = dt.Rows[18]["Value"].ToString();
                ApplicationHandler.Instance.SupportEmail = dt.Rows[29]["Value"].ToString();
                ApplicationHandler.Instance.PaypalVersion = dt.Rows[30]["Value"].ToString();
                //ApplicationHandler.Instance.PaypalUsername = dt.Rows[29]["Value"].ToString();
                ApplicationHandler.Instance.PaypalAPIUsername = dt.Rows[32]["Value"].ToString();
                ApplicationHandler.Instance.PaypalAPIPassword = dt.Rows[33]["Value"].ToString();
                ApplicationHandler.Instance.PaypalAPISignature = dt.Rows[34]["Value"].ToString();
                ApplicationHandler.Instance.PaypalAPIURL = dt.Rows[35]["Value"].ToString();
                ApplicationHandler.Instance.PaypalURL = dt.Rows[36]["Value"].ToString();
            } finally {
                settings = null;
                dt = null;
            }
        }

        protected void Application_Error() {
            Exception lastException = Server.GetLastError().GetBaseException();
            Type t = lastException.GetType();

            if(t.Name == "PayPalException") {
                //Handle PayPalException
                handlePayPalException((PayPalException)lastException);
            } else {
                //Handle Other Exception
                errorLogger.Error(lastException.ToString());
                MailMessageBuilder.SendExceptionEmail(lastException);
            }
        }

        private void handlePayPalException(PayPalException pe) {
            StringBuilder exStringBuilder = new StringBuilder();
            string errorString;
            errorLogger.Error("Error occurred when calling PayPal.");
            for(int i = 0; i < pe.errors.Length; i++) {
                int errorNum = i + 1;
                errorString = "Error " + errorNum + ":\n" +
                              "Error Code: " + pe.errors[i].ErrorCode + "\n" +
                              "Severity Code: " + pe.errors[i].SeverityCode + "\n" +
                              "Error Parameters: " + pe.errors[i].ErrorParameters + "\n" +
                              "Message: " + pe.errors[i].LongMessage;
                exStringBuilder.Append(errorString);
                exStringBuilder.Replace("\n", "<br/>");

                errorLogger.Error(errorString);
            }
            //Summary Email of all errors in PayPalException
            MailMessageBuilder.SendPayPalExceptionEmail(exStringBuilder.ToString());
        }
    }
}