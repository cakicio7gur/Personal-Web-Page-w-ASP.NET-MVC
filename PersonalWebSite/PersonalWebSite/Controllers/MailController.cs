using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace PersonalWebSite.Controllers
{
    public class MailController : Controller
    {
        public ActionResult SendMail(string ad, string mail,string konu, string mesaj)
        {
            System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            string isim = textInfo.ToTitleCase(ad);
            string email = textInfo.ToTitleCase(mail);
            string subject = textInfo.ToTitleCase(konu);
            string message = textInfo.ToTitleCase(mesaj);

            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("groupbyazilim@gmail.com");
            ePosta.To.Add("cakicozgur@gmail.com");
            ePosta.Subject = subject;
            ePosta.Body = isim + "[" + email + "]" + "+ ziyaretçisinden mesaj;"
                          + Environment.NewLine
                          + Environment.NewLine
                          + message;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("groupbyazilim@gmail.com", "159654357456");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
                return View() ;
            }
            catch (SmtpException ex)
            {
                throw new SmtpException(ex.ToString());
            }

        }
    }
}