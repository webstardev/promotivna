using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarkomPos.Model.Model;
using MarkomPos.Model.ViewModel;
using MarkomPos.Repository;
using MarkomPos.Repository.Repository;

namespace MarkomPos.Web.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class UsersController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Users
        public ActionResult Index()
        {
            using (var userRepository = new UserRepository())
            {
                var result = userRepository.getAllUser();
                return View(result);
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var user = new User();
            return PartialView("_AddUsers", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (var userRepository = new UserRepository())
                {
                    var result = userRepository.AddUser(user);
                    if (result)
                        return RedirectToAction("Index");
                }
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddUsers", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpGet, ActionName("DeleteConfirmed")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult changePassword(int id)
        {
            var changePasswordVm = new ChangePasswordVm();
            changePasswordVm.UserId = id;
            return PartialView("_ChangePassword", changePasswordVm);
        }
        public JsonResult validateOldPassword(int id, string password)
        {
            using (var userRepository = new UserRepository())
            {
                bool isMatch = true;
                isMatch = userRepository.validateOldPassword(id, password);
                return Json(isMatch, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChangeUserPassword(ChangePasswordVm changePasswordVm)
        {
            using (var userRepository = new UserRepository())
            {
                bool response = true;
                response = userRepository.ChangeUserPassword(changePasswordVm);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
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
