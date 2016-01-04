using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WforViolation.Models;

namespace WforViolation.Controllers
{
    public class CommentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Comment/
        [HttpPost]
        public ActionResult AddComment(CommentViewModel commentViewModel)
        {
            Comment comment = new Comment();
            var userID = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userID))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                comment.ApplicationUser = currentUser;
            }
            var violationId = commentViewModel.ViolationId;
            Violation violationCommentedOn = context.Violations.Where(x => x.Id == violationId).FirstOrDefault();
            comment.Violation = violationCommentedOn;
            comment.Content = commentViewModel.Content;
            comment.PostedDateTime = DateTime.Now;
            context.Comments.Add(comment);
            context.SaveChanges();
            return PartialView("_Comments", violationCommentedOn.Comments);
        }
        public ActionResult GetComments(int violationId)
        {
            string abc;
            List<Comment> comments = context.Comments.Where(x => x.Violation.Id == violationId).ToList();
            foreach (var item in comments)
            {
                abc = item.ApplicationUser.UserName;
            }
            return PartialView("_Comments", context.Comments.Where(x => x.Violation.Id == violationId).ToList());
        }
    }
}