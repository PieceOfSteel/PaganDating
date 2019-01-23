using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using PaganDating.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.IO;

namespace PaganDating.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private PaganDatingModelContainer db = new PaganDatingModelContainer();
        public UserApiController UserApi = new UserApiController();
        public FriendshipsApiController FriendsApi = new FriendshipsApiController();

        // GET: Users
        [HttpGet]
        public ActionResult Index(string searchString)
        {
            var users = db.UserSet.ToList();

            if(!string.IsNullOrEmpty(searchString))
            {
                users = db.UserSet.Where(u => u.Name.Contains(searchString)).ToList();
            }
            
            return View(users);
        }

        // GET: Users/Details/5
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

            var userId = UserApi.GetUserId();
            ViewBag.UserId = userId;
            
            if(FriendsApi.GetFriendship(userId, (int)id) != null 
                || FriendsApi.GetFriendship((int)id, userId) != null)
            {
                viewModel.FriendWithProfileOwner = true;
            }
            
            return View(viewModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        public ActionResult SendFriendRequest(User recipient)
        {
            var request = new FriendRequestViewModel();
            request.Recipient = recipient;
            return View(request);
        }

        
        [HttpGet]
        public ActionResult Search(string searchString)
        {
            var users = db.UserSet.Where(u => u.Name.Contains(searchString));
            return View(users.ToList());
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Password,ProfileImage,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                
                db.UserSet.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.UserSet.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,ProfileImage,Description,AccountId")] User user)
        {
            if (ModelState.IsValid)
            {
                if(user.Description == null)
                {
                    user.Description = "";
                }
                if(user.ProfileImage == null)
                {
                    user.ProfileImage = "";
                }

                db.Entry(user).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                }
                return RedirectToAction("Details", new { id = user.Id });
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult SetProfileImage(HttpPostedFileBase img)
        {
            if (img != null)
            {
                string imgName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(img.FileName);

                string imgPath = Path.Combine(Server.MapPath("~/ProfileImages/"), imgName);
                img.SaveAs(imgPath);

                var ProfileImagePath = @"../../ProfileImages/" + imgName;

                var userId = UserApi.GetUserId();
                var user = db.UserSet.FirstOrDefault(u => u.Id == userId);

                user.ProfileImage = ProfileImagePath;

                db.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = UserApi.GetUserId() });
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.UserSet.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.UserSet.Find(id);
            db.UserSet.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
