using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Microsoft.AspNet.Identity;

namespace PaganDating.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new PaganDatingModelContainer();
            var accountId = User.Identity.GetUserId();
            var userModel = db.UserSet.FirstOrDefault(u => u.AccountId == accountId);

            if (userModel == null && accountId != null)
            {
                var newUserModel = new User
                {
                    AccountId = accountId,
                    Name = User.Identity.Name,
                    ProfileImage = "",
                    Description = ""
                };

                db.UserSet.Add(newUserModel);
                db.SaveChanges();
            }
          

            return View();
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

        public ActionResult ProfileRedirect()
        {
            var userApi = new UserApiController();
            var userId = userApi.GetUserId(userApi.GetAccountId());

            return RedirectToAction("Details", "Users", new { id = userId });
        }
    }
}