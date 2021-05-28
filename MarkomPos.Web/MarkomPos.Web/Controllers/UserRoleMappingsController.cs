using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarkomPos.Model.Model;
using MarkomPos.Repository;
using MarkomPos.Repository.Repository;

namespace MarkomPos.Web.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class UserRoleMappingsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: UserRoleMappings
        public ActionResult Index()
        {
            var userRoleMappings = db.UserRoleMappings.Where(w => w.RolesId != 1).Include(u => u.User).Include(u => u.Roles);
            return View(userRoleMappings.ToList());
        }

        // GET: UserRoleMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            using (var roleRepository = new RoleRepository())
            {
                ViewBag.RolesId = new SelectList(roleRepository.getAllRole(), "ID", "Name", userRoleMapping.RolesId);
            }
            using (var userRepository = new UserRepository())
            {
                ViewBag.UserId = new SelectList(userRepository.getAllUser(), "ID", "Name", userRoleMapping.UserId);
            }
            return PartialView("_Details", userRoleMapping);
        }

        // GET: UserRoleMappings/Create
        public ActionResult Create()
        {
            using (var userRepository = new UserRepository())
            {
                ViewBag.UserId = new SelectList(userRepository.getAllUser(), "ID", "Name");
            }
            using (var roleRepository = new RoleRepository())
            {
                ViewBag.RolesId = new SelectList(roleRepository.getAllRole(), "ID", "Name");
            }

            var userRoleMapping = new UserRoleMapping();
            return PartialView("_AddRoleMapping", userRoleMapping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRoleMapping userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                using (var roleRepository = new RoleRepository())
                {
                    var result = roleRepository.AddUpdateUserRoleMapping(userRoleMapping);
                    if (result)
                        return RedirectToAction("Index");

                    ViewBag.RolesId = new SelectList(roleRepository.getAllRole(), "ID", "Name", userRoleMapping.RolesId);
                }
                using (var userRepository = new UserRepository())
                {
                    ViewBag.UserId = new SelectList(userRepository.getAllUser(), "ID", "Name", userRoleMapping.UserId);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: UserRoleMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            using (var userRepository = new UserRepository())
            {
                ViewBag.UserId = new SelectList(userRepository.getAllUser(), "ID", "Name", userRoleMapping.UserId);
            }
            using (var roleRepository = new RoleRepository())
            {
                ViewBag.RolesId = new SelectList(roleRepository.getAllRole(), "ID", "Name", userRoleMapping.RolesId);
            }

            return PartialView("_AddRoleMapping", userRoleMapping);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRoleMapping userRoleMapping = db.UserRoleMappings.Find(id);
            if (userRoleMapping == null)
            {
                return HttpNotFound();
            }
            db.UserRoleMappings.Remove(userRoleMapping);
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
