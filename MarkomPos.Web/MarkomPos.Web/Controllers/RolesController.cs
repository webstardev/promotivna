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
    public class RolesController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.Where(w => w.Name != "Super Admin").ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", roles);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            var role = new Roles();
            return PartialView("_AddRole", role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Roles role)
        {
            if (ModelState.IsValid)
            {
                using (var roleRepository = new RoleRepository())
                {
                    var result = roleRepository.AddUpdateRole(role);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddRole", role);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            db.Roles.Remove(role);
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
