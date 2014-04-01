using System.Web.UI;
using System.Net.Mail;
using eStoreWeb.Properties;
using System;
using com.domaintransformations.exceptions;
using System.Text;

namespace eStoreWeb {
    public class MailMessageBuilder : Page {

        //
        // Order Confirmation Email
        //
        public static void SendOrderConfirmationEmail() {
            MailSender.SendMail(CreateMailMessageObject(false,
                                                        MailPriority.Normal,
                                                        Settings.Default.FromEmailAddress,
                                                        SessionHandler.BillingEmailAddress,
                                                        "Confirmation of your order",
                                                        Settings.Default.OrderConfirmationSalutation + 
                                                        SessionHandler.BillingFirstName + ",\n\n" +
                                                        Settings.Default.OrderConfirmationOpening +
                                                        "This email is to confirm that you have purchased the following items:\n\n" + 
                                                        Settings.Default.OrderConfirmationClosing +
                                                        ApplicationHandler.TradingName + "Customer Service Team"));
        }

        //
        // Send Exception Email
        //
        public static void SendExceptionEmail(Exception ex) {
            MailSender.SendMail(CreateMailMessageObject(true,
                                                        MailPriority.High,
                                                        ApplicationHandler.SystemEmailAddress,
                                                        ApplicationHandler.SupportEmail,
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
        // Send Exception Email
        //
        public static void SendPayPalExceptionEmail(string exceptions) {
            MailSender.SendMail(CreateMailMessageObject(true,
                                                        MailPriority.High,
                                                        ApplicationHandler.SystemEmailAddress,
                                                        ApplicationHandler.SupportEmail,
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