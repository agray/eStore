using System;
using System.Web.UI;
using System.Net.Mail;
using Slf;

namespace eStoreWeb {
    public class MailSender : Page {
        private static ILogger errorLogger = LoggerService.GetLogger("ErrorLogger");

        //******************************************
        // Utility Mail Sending Functions
        //******************************************

        //
        // actually send the message via SMTP
        //
        public static void SendMail(MailMessage msg) {

            try {
                SmtpClient smtp = new SmtpClient(ApplicationHandler.SMTPServer, 25);
                smtp.Send(msg);
            } catch(Exception ex) {
                errorLogger.Error("Unable to send mail! " + ex.Message);
            } finally {
                msg = null;
            }
        }
    }
}