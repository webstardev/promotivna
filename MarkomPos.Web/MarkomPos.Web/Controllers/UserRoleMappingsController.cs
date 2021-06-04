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
    public class UserRoleMappingsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();
        private RoleMappingRepository roleMappingRepository = new RoleMappingRepository();

        // GET: UserRoleMappings
        public ActionResult Index()
        {
            var offerValidation = roleMappingRepository.GetAll();
            return View(offerValidation.ToList());
        }

        // GET: UserRoleMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var offerValidation = roleMappingRepository.GetById(id.GetValueOrDefault(0));
            if (offerValidation == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", offerValidation);
        }

        // GET: UserRoleMappings/Create
        public ActionResult Create()
        {
            var offerValidation = roleMappingRepository.GetById(0);
            if (offerValidation == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddRoleMapping", offerValidation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRoleMappingVm userRoleMapping)
        {
            if (ModelState.IsValid)
            {
                var result = roleMappingRepository.AddUpdateUserRoleMapping(userRoleMapping);
                if (result)
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userRoleMapping = roleMappingRepository.GetById(id.GetValueOrDefault(0));
            if (userRoleMapping == null)
            {
                return HttpNotFound();
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
