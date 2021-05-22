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
    public class UnitOfMeasuresController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: UnitOfMeasures
        public ActionResult Index()
        {
            return View(db.UnitOfMeasures.ToList());
        }

        // GET: UnitOfMeasures/Details/5
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
            //return View(unitOfMeasure);
            return PartialView("_Details", unitOfMeasure);
        }

        // GET: UnitOfMeasures/Create
        public ActionResult Create()
        {
            var unitOfMeasure = new UnitOfMeasure();
            return PartialView("_AddUnitOfMeasure", unitOfMeasure);
            //return View();
        }

        // POST: UnitOfMeasures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: UnitOfMeasures/Edit/5
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
            //return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfMeasuresRepository = new UnitOfMeasuresRepository())
                {
                    var result = unitOfMeasuresRepository.UpdateUnitMeasure(unitOfMeasure);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasures/Delete/5
        public ActionResult Delete(int? id)
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
            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Delete/5
        [HttpGet, ActionName("DeleteConfirmed")]
        //[ValidateAntiForgeryToken]
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
