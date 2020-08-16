using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using OrderManagementSystem.Models;
using System.Text;

namespace OrderManagementSystem.Models
{
    public class ResponseMail
    {
        public static void SendMail(int orderId)
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("Hello, <br/><br/>");
            mailBody.Append("The Generated Order id=" + orderId);

            MailMessage mailMessage = new MailMessage("Soruce mail", "Destination mail"); //Need to add source and destination mail
            mailMessage.Body = mailBody.ToString();
            mailMessage.Subject = "Your order is created";
            mailMessage.IsBodyHtml = true;

            SmtpClient mailsender = new SmtpClient();
            mailsender.Send(mailMessage);
        }
    }
}