using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WforViolation.Models;

namespace WforViolation.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        
        //
        // GET: /Category/
        [Authorize(Roles="Admin")]
        public ActionResult AddSubCategory()
        {
            return View(new ViolationSubType());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddSubCategory(ViolationSubType violationSubType, int MainViolationTypesList)
        {
            ViolationMainType violationMain= context.ViolationMainTypes.Where(x => x.Id == MainViolationTypesList).FirstOrDefault();
            violationMain.ViolationSubTypes.Add(violationSubType);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddMainCategory()
        {
            return View(new ViolationMainType());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddMainCategory(ViolationMainType violationMainType)
        {
            context.ViolationMainTypes.Add(violationMainType);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
	}
}