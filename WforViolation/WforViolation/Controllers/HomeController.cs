using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WforViolation.Models;
using PagedList;

namespace WforViolation.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index(int pagination=1)
        {
            return View(context.Violations.OrderByDescending(x => x.CreationDateTime).ToPagedList(pagination, 1));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
  

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Deneme()
        {
            return View();
        }
        public ActionResult Deneme2()
        {
            return View(new ViolationDetailView());
        }
        [HttpPost]
        public ActionResult Deneme2(ViolationDetailView violationDetailView)
        {
            return View();
        }
        public ActionResult Deneme3()
        {
            return View(new Deneme());
        }
    }
}