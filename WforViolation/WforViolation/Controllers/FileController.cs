using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WforViolation.Helpers;
using WforViolation.Models;

namespace WforViolation.Controllers
{
    public class FileController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //
        // GET: /File/
        [HttpPost]
        [Authorize]
        public JsonResult UploadFile(int id)
        {
            string mediumPath="";
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var violationss = context.Violations.Find(id);
                        ViolationPicture oldPicture = violationss.ViolationPictures.FirstOrDefault();
                        context.ViolationPictures.Attach(oldPicture);
                        context.Entry(oldPicture).State = EntityState.Deleted;
                        context.SaveChanges();
                        ViolationPicture newViolationPicture = new ViolationPicture();
                        newViolationPicture = ImageHelper.SavePic(fileContent, HttpContext, newViolationPicture) as ViolationPicture;
                        violationss.ViolationPictures.Add(newViolationPicture);
                        context.SaveChanges();
                        mediumPath = newViolationPicture.MediumPath;
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json(mediumPath,JsonRequestBehavior.AllowGet);
        }
	}
}