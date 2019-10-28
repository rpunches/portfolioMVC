using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalPortfolioMVC.Models;
using System.Net.Mail;
using System.Net;


namespace PersonalPortfolioMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resume()
        {

            return View();
        }
        public ActionResult Portfolio()
        {

            return View();
        }

        public ActionResult Class()
        {
         
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //validate
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            //message to email
            string message =
                $"<strong>Email From:</strong> {cvm.Name}<br/>" +
                $"<strong>Email:</strong> {cvm.Email}<br/>" +
                //$"<strong>Subject:</strong> {cvm.Subject}<br/>" +
                $"<strong>Message:</strong><br/>" +
                $"{cvm.Message}";
            //sends the email
            MailMessage mm = new MailMessage("admin@rachelpunches.com", "rpunches@gmail.com", null, message);
            //allow html formatting
            mm.IsBodyHtml = true;
            //high priority
            mm.Priority = MailPriority.High;
            //respond to sender
            mm.ReplyToList.Add(cvm.Email);

            //allow email to be sent
            SmtpClient client = new SmtpClient("mail.rachelpunches.com");
            //credentials
            client.Credentials = new NetworkCredential("admin@rachelpunches.com", "Optimus!!11");

            //try-catch
            try
            {
                //send email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"I am so sorry!  It looks like something went wrong and your request couldn't be completed at this time.  Please try again later.<br/><strong>Error Message:</strong>{ex.StackTrace}";
                return View(cvm);
            }

            //confirmation that email was sent
            return View("EmailConfirmation", cvm);

        }

    }
}