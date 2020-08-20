using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using OrderManagementSystem.Models;
using System.Text;
using System.Management;
using System.Data;

namespace OrderManagementSystem.Models
{
    public class ResponseMail
    {
        public static void SendMail(int orderId)
        {
            OrdersModelEntities entities = new OrdersModelEntities();
            try
            {
                if (entities.Database.Connection.State == ConnectionState.Closed)
                {
                    entities.Database.Connection.Open();

                    StringBuilder mailBody = new StringBuilder();
                    mailBody.Append("Hello, <br/><br/>");
                    mailBody.Append("The Generated Order id=" + orderId);

                    MailMessage mailMessage = new MailMessage("Need to give source mail", "destination mail"); //Need to add source and destination mail
                    mailMessage.Body = mailBody.ToString();
                    mailMessage.Subject = "Your order is created";
                    mailMessage.IsBodyHtml = true;

                    SmtpClient mailsender = new SmtpClient();                    
                    mailsender.Send(mailMessage);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}