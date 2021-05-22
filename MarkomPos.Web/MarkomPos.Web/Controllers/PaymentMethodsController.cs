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
    public class PaymentMethodsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: PaymentMethods
        public ActionResult Index()
        {
            return View(db.PaymentMethods.ToList());
        }

        // GET: PaymentMethods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethods.Find(id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", paymentMethod);
        }

        public ActionResult Create()
        {
            var paymentMethod = new PaymentMethod();
            return PartialView("_AddPaymentMethod", paymentMethod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                using (var paymentMethodRepository = new PaymentMethodRepository())
                {
                    var result = paymentMethodRepository.AddUpdatePaymentMethod(paymentMethod);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // GET: PaymentMethods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethods.Find(id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddPaymentMethod", paymentMethod);
        }

        // Get: PaymentMethods/Delete/5
        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentMethod paymentMethod = db.PaymentMethods.Find(id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            db.PaymentMethods.Remove(paymentMethod);
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
