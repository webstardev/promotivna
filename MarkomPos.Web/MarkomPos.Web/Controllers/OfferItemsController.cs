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
    public class OfferItemsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: OfferItems
        public ActionResult Index()
        {
            var offerItems = db.OfferItems.Include(o => o.Offer).Include(o => o.Product).Include(o => o.UnitOfMeasure);
            return View(offerItems.ToList());
        }

        // GET: OfferItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferItem offerItem = db.OfferItems.Find(id);
            if (offerItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName", offerItem.OfferId);
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name", offerItem.ProductId);
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name", offerItem.UnitOfMeasureId);
            return PartialView("_Details", offerItem);
        }

        // GET: OfferItems/Create
        public ActionResult Create()
        {
            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName");
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name");
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name");
            OfferItem offerItem = new OfferItem();
            return PartialView("_AddOfferItem", offerItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferItem offerItem)
        {
            if (ModelState.IsValid)
            {
                using (var offerRepository = new OfferRepository())
                {
                    var result = offerRepository.AddUpdateOfferItem(offerItem);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName", offerItem.OfferId);
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name", offerItem.ProductId);
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name", offerItem.UnitOfMeasureId);

            return RedirectToAction("Index");
        }

        // GET: OfferItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferItem offerItem = db.OfferItems.Find(id);
            if (offerItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfferId = new SelectList(db.Offers, "ID", "ContactName", offerItem.OfferId);
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name", offerItem.ProductId);
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name", offerItem.UnitOfMeasureId);

            return PartialView("_AddOfferItem", offerItem);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferItem offerItem = db.OfferItems.Find(id);
            if (offerItem == null)
            {
                return HttpNotFound();
            }
            db.OfferItems.Remove(offerItem);
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
