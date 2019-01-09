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

namespace PaganDating.Controllers
{
    public class UsersController : Controller
    {
        private PaganDatingModelContainer db = new PaganDatingModelContainer();

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
        public ActionResult Edit([Bind(Include = "Id,Name,Password,ProfileImage,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
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
