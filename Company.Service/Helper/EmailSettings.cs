using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email) 
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("salmafareed998@gmail.com", "");
            Client.Send("salmafareed998@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
