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
    [Authorize(Roles = "Super Admin,Manager")]
    public class UnitOfMeasuresController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            return View(db.UnitOfMeasures.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitOfMeasure unitOfMeasure = db.UnitOfMeasures.Find(id);
            if (unitOfMeasure == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", unitOfMeasure);
        }

        public ActionResult Create()
        {
            var unitOfMeasure = new UnitOfMeasure();
            return PartialView("_AddUnitOfMeasure", unitOfMeasure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfMeasuresRepository = new UnitOfMeasuresRepository())
                {
                    var result = unitOfMeasuresRepository.AddUnitMeasure(unitOfMeasure);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitOfMeasure unitOfMeasure = db.UnitOfMeasures.Find(id);
            if (unitOfMeasure == null)
            {
                return HttpNotFound();
            }

            return PartialView("_AddUnitOfMeasure", unitOfMeasure);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                UnitOfMeasure unitOfMeasure = db.UnitOfMeasures.Find(id);
                if (unitOfMeasure == null)
                {
                    return HttpNotFound();
                }
                db.UnitOfMeasures.Remove(unitOfMeasure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
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
