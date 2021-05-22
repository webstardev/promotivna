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
    public class OfferValidationsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: OfferValidations
        public ActionResult Index()
        {
            var offerValidations = db.OfferValidations.Include(o => o.Offer).Include(o => o.User);
            return View(offerValidations.ToList());
        }

        // GET: OfferValidations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferValidation offerValidation = db.OfferValidations.Find(id);
            if (offerValidation == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName", offerValidation.OfferId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "Name", offerValidation.UserId);
            return PartialView("_Details", offerValidation);
        }

        // GET: OfferValidations/Create
        public ActionResult Create()
        {
            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName");
            ViewBag.UserId = new SelectList(db.Users, "ID", "Name");
            OfferValidation offerValidation = new OfferValidation();
            return PartialView("_AddOfferValidation", offerValidation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferValidation offerValidation)
        {
            if (ModelState.IsValid)
            {
                using (var offerRepository = new OfferRepository())
                {
                    var result = offerRepository.AddUpdateOfferValidation(offerValidation);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName", offerValidation.OfferId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "Name", offerValidation.UserId);

            return RedirectToAction("Index");
        }

        // GET: OfferValidations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferValidation offerValidation = db.OfferValidations.Find(id);
            if (offerValidation == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName", offerValidation.OfferId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "Name", offerValidation.UserId);

            return PartialView("_AddOfferValidation", offerValidation);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferValidation offerValidation = db.OfferValidations.Find(id);
            if (offerValidation == null)
            {
                return HttpNotFound();
            }
            db.OfferValidations.Remove(offerValidation);
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
