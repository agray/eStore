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
using System.Net.Mail;
using System.Web.UI;
using phoenixconsulting.common.handlers;
using PhoenixConsulting.Common.Properties;

namespace com.phoenixconsulting.common.mail {
    public class MailMessageBuilder : Page {

        //
        // Order Confirmation Email
        //
        public static void SendOrderConfirmationEmail() {
            MailSender.SendMail(CreateMailMessageObject(false,
                                                        MailPriority.Normal,
                                                        Settings.Default.FromEmailAddress,
                                                        SessionHandler.Instance.BillingEmailAddress,
                                                        "Confirmation of your order",
                                                        Settings.Default.OrderConfirmationSalutation +
                                                        SessionHandler.Instance.BillingFirstName + ",\n\n" +
                                                        Settings.Default.OrderConfirmationOpening +
                                                        "This email is to confirm that you have purchased the following items:\n\n" + 
                                                        Settings.Default.OrderConfirmationClosing +
                                                        ApplicationHandler.Instance.TradingName + "Customer Service Team"));
        }

        //
        // Send Exception Email
        //
        public static void SendExceptionEmail(Exception ex) {
            MailSender.SendMail(CreateMailMessageObject(true,
                                                        MailPriority.High,
                                                        ApplicationHandler.Instance.SystemEmailAddress,
                                                        ApplicationHandler.Instance.SupportEmail,
                                                        "Exception occurred in Customer Website",
                                                        "<html><body>" +
                                                        "<b>Type:</b> " + ex.GetType() + "<br/><br/>" +
                                                        "<b>Message:</b> " + ex.Message + "<br/><br/>" +
                                                        "<b>Source:</b> " + ex.Source + "<br/><br/>" +
                                                        "<b>Method:</b> " + ex.TargetSite + "<br/><br/>" +
                                                        "<b>InnerException:</b> " + ex.InnerException + "<br/><br/>" +
                                                        "<b>Exception String:</b> <pre>" + ex.ToString() + "</pre>" +
                                                        //"<b>Session Dump</b><br/>" +
                                                        //"<i>UserID: </i>" + SessionHandler.UserID + "<br/>" +
                                                        //"<i>User Name: </i>" + SessionHandler.UserName + "<br/>" +
                                                        //"<i>User Role: </i>" + SessionHandler.UserRole + "<br/>" +
                                                        //"<i>Unsuccessful Login Count: </i>" + SessionHandler.UnsuccessfulLoginCount + "<br/>" +
                                                        //"<i>Has Logged In Before: </i>" + SessionHandler.HasLoggedInBefore + "<br/>" +
                                                        //"<i>Is Locked Out: </i>" + SessionHandler.IsLockedOut + "<br/>" +
                                                        //"<i>Is Registered: </i>" + SessionHandler.IsRegistered + "<br/>" +
                                                        "</body></html>"));
        }

        //
        // Website Password Reset Email
        //
        public static void SendPasswordResetEmail(string emailAddress, 
                                                  string firstName, 
                                                  string lastName,
                                                  string newPassword) {
            MailSender.SendMail(CreateMailMessageObject(true,
                                                        MailPriority.Normal,
                                                        ApplicationHandler.Instance.SystemEmailAddress,
                                                        emailAddress,
                                                        ApplicationHandler.Instance.TradingName + " Website Password Reset",
                                                        "<html><body>" +
                                                        "Dear " + firstName + " " + lastName + "," + 
                                                        "<br/><br/>" +
                                                        "As per your recent request, here is your new password for the " + ApplicationHandler.Instance.TradingName + " website." + 
                                                        "<br/><br/>" + 
                                                        "<b>NEW PASSWORD</b>" + 
                                                        "<br/><br/>" + 
                                                        "<b>Your password has been reset to: </b>" + newPassword + 
                                                        "<br>" +
                                                        "Your login account name (email address) is: " + emailAddress + 
                                                        "<br><br>" +
                                                        "Once you've logged back into the website using the automatically generated password above, you can change the password to something easier for you to remember or you can continue to use the password provided in this email." +
                                                        "<br/><br/>" +
                                                        "If you have any questions or concerns, contact us at enquiries@" + ApplicationHandler.Instance.TradingName.ToLower() + ".com.au." +
                                                        "<br/><br/>" + 
                                                        "Regards," + 
                                                        "<br>" + 
                                                        "Customer Service Team" + 
                                                        "<br>" +
                                                        ApplicationHandler.Instance.TradingName + 
                                                        "</body></html>"));
        }

        //
        // Website Password Reset Email
        //
        public static void SendActivationEmail(string emailAddress,
                                               string firstName,
                                               string lastName,
                                               string newPassword) {
            MailSender.SendMail(CreateMailMessageObject(true,
                                                        MailPriority.Normal,
                                                        ApplicationHandler.Instance.SystemEmailAddress,
                                                        emailAddress,
                                                        ApplicationHandler.Instance.TradingName + " Website Account Information",
                                                        "<html><body>" +
                                                        "Dear " + firstName + " " + lastName + "," +
                                                        "<br/><br/>" +
                                                        "As per your recent request, your " + ApplicationHandler.Instance.TradingName + " website membership details have been altered to reflect your new/amended email address." +
                                                        "<br/><br/>" +
                                                        "In order to protect your privacy and to ensure that you are the authorised user of this account, you will need to activate it via one of the following options:" + 
                                                        "<br/><br/>" +
                                                        "<i>Option 1)</i> You can activate the your account by clicking on this <a href=\"http://www." + ApplicationHandler.Instance.TradingName.ToLower() + ".com.au/Home/activate.aspx?email=" + emailAddress + "\">link</a>." + 
                                                        "<br/>" +
                                                        "or" +
                                                        "<br/>" +
                                                        "<i>Option 2)</i> Simply cut and paste the following url in your browser: http://www." + ApplicationHandler.Instance.TradingName.ToLower() + ".com.au/Home/activate.aspx?email=" + emailAddress +
                                                        "<br/><br/>" +
                                                        "If you need assistance or have any further enquiries, please contact us via email at enquiries@" + ApplicationHandler.Instance.TradingName.ToLower() + ".com.au." +
                                                        "<br/><br/>" +
                                                        "Regards," +
                                                        "<br>" +
                                                        "Customer Service Team" +
                                                        "<br>" +
                                                        ApplicationHandler.Instance.TradingName +
                                                        "</body></html>"));
        }

        //
        // Send Exception Email
        //
        public static void SendPayPalExceptionEmail(string exceptions) {
            MailSender.SendMail(CreateMailMessageObject(true,
                                                        MailPriority.High,
                                                        ApplicationHandler.Instance.SystemEmailAddress,
                                                        ApplicationHandler.Instance.SupportEmail,
                                                        "Exception occurred in Customer Website",
                                                        "<html><body>" +
                                                        exceptions +
                                                        //"<b>Session Dump</b><br/>" +
                                                        //"<i>UserID: </i>" + SessionHandler.UserID + "<br/>" +
                                                        //"<i>User Name: </i>" + SessionHandler.UserName + "<br/>" +
                                                        //"<i>User Role: </i>" + SessionHandler.UserRole + "<br/>" +
                                                        //"<i>Unsuccessful Login Count: </i>" + SessionHandler.UnsuccessfulLoginCount + "<br/>" +
                                                        //"<i>Has Logged In Before: </i>" + SessionHandler.HasLoggedInBefore + "<br/>" +
                                                        //"<i>Is Locked Out: </i>" + SessionHandler.IsLockedOut + "<br/>" +
                                                        //"<i>Is Registered: </i>" + SessionHandler.IsRegistered + "<br/>" +
                                                        "</body></html>"));
        }


        //
        // Construct MailMessage object
        //
        private static MailMessage CreateMailMessageObject(bool isHtml, MailPriority priority, string from, string to, string subject, string body) {
            MailMessage oMsg = new MailMessage();

            oMsg.IsBodyHtml = isHtml;
            oMsg.From = new MailAddress(from);
            oMsg.To.Add(to);
            oMsg.Subject = subject;
            oMsg.Body = body;

            return oMsg;
        }
    }
}