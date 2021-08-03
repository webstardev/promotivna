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
    public class OfferItemsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            using (var offerItemRepository = new OfferItemRepository())
            {
                var offerItems = offerItemRepository.GetAll();
                return View(offerItems);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var offerItemRepository = new OfferItemRepository())
            {
                var offerItem = offerItemRepository.GetById(id.GetValueOrDefault(0), 0);
                if (offerItem == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", offerItem);
            }
        }

        public ActionResult Create(int OfferId = 0)
        {
            using (var offerItemRepository = new OfferItemRepository())
            {
                var offerItem = offerItemRepository.GetById(0, OfferId);
                if (offerItem == null)
                {
                    return HttpNotFound();
                }
                offerItem.OfferId = OfferId;
                return PartialView("_AddOfferItem", offerItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferItemVm offerItem)
        {
            if (ModelState.IsValid)
            {
                using (var offerItemRepository = new OfferItemRepository())
                {
                    var result = offerItemRepository.AddUpdateOfferItem(offerItem);
                    if (result)
                        return Redirect(Request.UrlReferrer.ToString());
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var offerItemRepository = new OfferItemRepository())
            {
                var offer = offerItemRepository.GetById(id.GetValueOrDefault(0), 0);
                if (offer == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOfferItem", offer);
            }
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
            return Redirect(Request.UrlReferrer.ToString());
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
