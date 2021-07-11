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
    public class OfferValidationsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            using (var offerValidationRepository = new OfferValidationRepository())
            {
                var offerValidation = offerValidationRepository.GetAll();
                return View(offerValidation.ToList());
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var offerValidationRepository = new OfferValidationRepository())
            {
                var offerValidation = offerValidationRepository.GetById(id.GetValueOrDefault(0));
                if (offerValidation == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", offerValidation);
            }
        }

        public ActionResult Create(int offerId = 0)
        {
            using (var offerValidationRepository = new OfferValidationRepository())
            {
                var offerValidation = offerValidationRepository.GetById(0);
                offerValidation.OfferId = offerId;
                if (offerValidation == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOfferValidation", offerValidation);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferValidationVm offerValidation)
        {
            if (ModelState.IsValid)
            {
                using (var offerValidationRepository = new OfferValidationRepository())
                {
                    var result = offerValidationRepository.AddUpdateOfferValidation(offerValidation);
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
            using (var offerValidationRepository = new OfferValidationRepository())
            {
                var offerValidation = offerValidationRepository.GetById(id.GetValueOrDefault(0));
                if (offerValidation == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOfferValidation", offerValidation);
            }
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
