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
    public class OffersController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Offers
        public ActionResult Index()
        {
            var offers = db.Offers.Include(o => o.Contact).Include(o => o.DeliveryTerm).Include(o => o.DocumentParity).Include(o => o.PaymentMethod).Include(o => o.ResponsibleUser);
            return View(offers.ToList());
        }

        // GET: Offers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }

            ViewBag.ContactId = new SelectList(db.Contacts, "ID", "Name");
            ViewBag.DeliveryTermId = new SelectList(db.DeliveryTerms, "ID", "Name");
            ViewBag.DocumentParityId = new SelectList(db.DocumentParities, "ID", "Name");
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "ID", "Name");
            ViewBag.ResponsibleUserId = new SelectList(db.Users, "ID", "Name");
            return PartialView("_Details", offer);
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.Contacts, "ID", "Name");
            ViewBag.DeliveryTermId = new SelectList(db.DeliveryTerms, "ID", "Name");
            ViewBag.DocumentParityId = new SelectList(db.DocumentParities, "ID", "Name");
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "ID", "Name");
            ViewBag.ResponsibleUserId = new SelectList(db.Users, "ID", "Name");
            Offer offer = new Offer();
            return PartialView("_AddOffer", offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Offer offer)
        {
            if (ModelState.IsValid)
            {
                using (var offerRepository = new OfferRepository())
                {
                    var result = offerRepository.AddUpdateOffer(offer);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            ViewBag.ContactId = new SelectList(db.Contacts, "ID", "Name", offer.ContactId);
            ViewBag.DeliveryTermId = new SelectList(db.DeliveryTerms, "ID", "Name", offer.DeliveryTermId);
            ViewBag.DocumentParityId = new SelectList(db.DocumentParities, "ID", "Name", offer.DocumentParityId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "ID", "Name", offer.PaymentMethodId);
            ViewBag.ResponsibleUserId = new SelectList(db.Users, "ID", "Name", offer.ResponsibleUserId);

            return RedirectToAction("Index");
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "ID", "Name", offer.ContactId);
            ViewBag.DeliveryTermId = new SelectList(db.DeliveryTerms, "ID", "Name", offer.DeliveryTermId);
            ViewBag.DocumentParityId = new SelectList(db.DocumentParities, "ID", "Name", offer.DocumentParityId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "ID", "Name", offer.PaymentMethodId);
            ViewBag.ResponsibleUserId = new SelectList(db.Users, "ID", "Name", offer.ResponsibleUserId);

            return PartialView("_AddOffer", offer);
        }

        // Get: Offers/DeleteConfirmed/5
        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            db.Offers.Remove(offer);
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
