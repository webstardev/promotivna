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
    public class DeliveryTermsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: DeliveryTerms
        public ActionResult Index()
        {
            return View(db.DeliveryTerms.ToList());
        }

        // GET: DeliveryTerms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryTerm deliveryTerm = db.DeliveryTerms.Find(id);
            if (deliveryTerm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", deliveryTerm);
        }

        // GET: DeliveryTerms/Create
        public ActionResult Create()
        {
            var deliveryTerm = new DeliveryTerm();
            return PartialView("_AddDeliveryTerm", deliveryTerm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeliveryTerm deliveryTerm)
        {
            if (ModelState.IsValid)
            {
                using (var deliveryTermRepository = new DeliveryTermRepository())
                {
                    var result = deliveryTermRepository.AddUpdateDeliveryTerm(deliveryTerm);
                    if (result)
                        return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryTerm deliveryTerm = db.DeliveryTerms.Find(id);
            if (deliveryTerm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddDeliveryTerm", deliveryTerm);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryTerm deliveryTerm = db.DeliveryTerms.Find(id);
            if (deliveryTerm == null)
            {
                return HttpNotFound();
            }
            db.DeliveryTerms.Remove(deliveryTerm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult IsExist(int id, string deliveryTerm)
        {
            using (var deliveryTermRepository = new DeliveryTermRepository())
            {
                bool isExist = true;
                isExist = deliveryTermRepository.IsExist(id, deliveryTerm);
                return Json(isExist, JsonRequestBehavior.AllowGet);
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
