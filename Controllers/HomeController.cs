using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using week4_database.Models;
using System.Data.Entity;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace week4_database.Controllers
{
    public class HomeController : Controller
    {
        string apiKey = ConfigurationManager.AppSettings["test_email"];
        private ApplicationDbContext _context;
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            _context = new ApplicationDbContext();
            using (_context)
            {
                var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                var users = await _userManager.Users.ToListAsync();
                return View(users);
            }
        }
        public ActionResult Delete(string id)
        {
            _context = new ApplicationDbContext();

            ApplicationUser user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("About"); 
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        public async Task<ActionResult> Contact()
        {
            string apiKey = ConfigurationManager.AppSettings["sender_email"];
            var client = new SendGridClient(apiKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("xxio0014@student.monash.edu"),
                Subject = "Test Email",
                PlainTextContent = "This is a test email sent from SendGrid.",
                HtmlContent = "<p>This is a test email sent from SendGrid.</p>"
            };

            msg.AddTo(new EmailAddress("xhui2434@gmail.com"));

            var response = await client.SendEmailAsync(msg);

            // Check the response to see if the email was sent successfully
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                ViewBag.Message = "Email sent successfully!";
            }
            else
            {
                ViewBag.Message = "Email sending failed.";
            }

            return View();
        }
    }
}