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
                    Description = "Jag är glad och go",
                    ProfileImage = "(Path)",
                    AccountId = ""
                },
                new User
                {
                    Id = 102,
                    Name = "Freja",
                    Description = "Jag gillar att ha kul *wink wink*",
                    ProfileImage = "data:image/jpeg;base64,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",
                    AccountId = ""
                },
                new User
                {
                    Id = 103,
                    Name = "Örjan",
                    Description = "Tja, jag är snickare",
                    ProfileImage = "(Path)",
                    AccountId = ""
                }
            });

            return userList;
        }

        public ActionResult Index(string searchString)
        {
            ConnectAccount();
            
            if (db.UserSet.Count() == 0)
            {
                var userList = ExampleUsers();
                foreach(var exampleUser in userList)
                {
                    db.UserSet.Add(exampleUser);
                }
                db.SaveChanges();
            }

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