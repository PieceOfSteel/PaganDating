using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Microsoft.AspNet.Identity;
using PaganDating.Models;

namespace PaganDating.Controllers
{
    public class HomeController : Controller
    {

        private PaganDatingModelContainer db = new PaganDatingModelContainer();

        public ActionResult UserIndex()
        {

            return View();
        }

        private void ConnectAccount()
        {
            var userApi = new UserApiController();
            var accountId = userApi.GetAccountId();
            var userModel = userApi.GetUserId();

            if (userModel == 0 && accountId != null)
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
        }

        public ActionResult Details(int? id)
        {
            var viewModel = new UserDetailsViewModel(db.UserSet.Find(id));

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (viewModel.User == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        private List<User> ExampleUsers()
        {
            var userList = new List<User>();

            userList.AddRange(new[]
            {
                new User
                {
                    Id = 101,
                    Name = "Frej",
                    Description = "No description",
                    ProfileImage = "(Path)",
                    AccountId = ""
                },
                new User
                {
                    Id = 102,
                    Name = "Freja",
                    Description = "No description",
                    ProfileImage = "(Path)",
                    AccountId = ""
                },
                new User
                {
                    Id = 103,
                    Name = "Örjan",
                    Description = "No description",
                    ProfileImage = "(Path)",
                    AccountId = ""
                }
            });

            return userList;
        }

        public ActionResult Index(string searchString)
        {
            ConnectAccount();
            
            var users = db.UserSet.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = db.UserSet.Where(u => u.Name.Contains(searchString)).ToList();
            }

            return View(users);
        }

        [HttpGet]
        public ActionResult Search(string searchString)
        {
            var users = db.UserSet.Where(u => u.Name.Contains(searchString));
            return View(users.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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