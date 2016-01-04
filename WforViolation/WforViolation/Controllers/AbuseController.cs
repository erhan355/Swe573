using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WforViolation.Models;

namespace WforViolation.Controllers
{
    public class AbuseController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Abuse/
        [HttpPost]
        [Authorize]
        public ActionResult Notify(int violationId)
        {
            var userID = User.Identity.GetUserId();

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            int count = context.Abuses.Where(x => x.ViolationId == violationId && x.NotifierId == currentUser.Id).Count();
            if (count > 0)
                return Json(new { Result = "0" });
            else
            {
                Abuse abuse = new Abuse();

                abuse.NotifierId = currentUser.Id;
                abuse.ViolationId = violationId;
                abuse.NotificationDateTime = DateTime.Now;
                context.Abuses.Add(abuse);
                context.SaveChanges();
                return Json(new { Result = "1" });

            }
        }
         [Authorize(Roles = "Admin")]
        public ActionResult ListAll()
        {
            return View(context.Abuses.ToList().OrderByDescending(x=>x.NotificationDateTime));
        }
        public ActionResult Delete(int violationId)
         {
             Abuse abuse = context.Abuses.Where(x => x.ViolationId == violationId).FirstOrDefault();
             context.Abuses.Attach(abuse);
             context.Abuses.Remove(abuse);
             context.SaveChanges();
             return RedirectToAction("ListAll", "Abuse");

         }
    }
}