﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace GmailEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var fromAddress = new MailAddress("2Speech.us@gmail.com", "2Speech.us");
            var toAddress = new MailAddress("gaddam_ashwin@yahoo.com", "AA");
            const string fromPassword = "jagadeeshwar";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
                       {
                           Host = "smtp.gmail.com",
                           Port = 587,
                           EnableSsl = true,
                           DeliveryMethod = SmtpDeliveryMethod.Network,
                           UseDefaultCredentials = false,
                           Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                       };
            using (var message = new MailMessage(fromAddress, toAddress)
                                 {
                                     Subject = subject,
                                     Body = body
                                 })
            {
                smtp.Send(message);
            }
            Console.ReadLine();
        }
    }
}
