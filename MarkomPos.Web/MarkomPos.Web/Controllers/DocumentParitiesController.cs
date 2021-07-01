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
    public class DocumentParitiesController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: DocumentParities
        public ActionResult Index()
        {
            return View(db.DocumentParities.ToList());
        }

        // GET: DocumentParities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentParity documentParity = db.DocumentParities.Find(id);
            if (documentParity == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", documentParity);
        }

        // GET: DocumentParities/Create
        public ActionResult Create()
        {
            var documentParity = new DocumentParity();
            return PartialView("_AddDocumentParity", documentParity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentParity documentParity)
        {
            if (ModelState.IsValid)
            {
                using (var documentParityRepository = new DocumentParityRepository())
                {
                    var result = documentParityRepository.AddUpdateDocumentParty(documentParity);
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
            DocumentParity documentParity = db.DocumentParities.Find(id);
            if (documentParity == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddDocumentParity", documentParity);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentParity documentParity = db.DocumentParities.Find(id);
            if (documentParity == null)
            {
                return HttpNotFound();
            }
            db.DocumentParities.Remove(documentParity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult IsExist(int id, string documentParity)
        {
            using (var documentParityRepository = new DocumentParityRepository())
            {
                bool isExist = true;
                isExist = documentParityRepository.IsExist(id, documentParity);
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
