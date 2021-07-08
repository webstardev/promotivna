using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarkomPos.Model.Enum;
using MarkomPos.Model.Model;
using MarkomPos.Repository;
using MarkomPos.Repository.Repository;

namespace MarkomPos.Web.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class CodePrefixesController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            return View(db.CodePrefixes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var codePrefix = db.CodePrefixes.Find(id);
            if (codePrefix == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", codePrefix);
        }

        public ActionResult Create()
        {
            var codePrefix = new CodePrefix();
            return PartialView("_AddCodePrefix", codePrefix);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodePrefix codePrefix)
        {
            if (ModelState.IsValid)
            {
                using (var codeRepository = new CodeRepository())
                {
                    var result = codeRepository.AddUpdateCodePrefix(codePrefix);
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
            var codePrefix = db.CodePrefixes.Find(id);
            if (codePrefix == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddCodePrefix", codePrefix);
        }


        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            CodePrefix codePrefix = db.CodePrefixes.Find(id);
            if (codePrefix == null)
            {
                return HttpNotFound();
            }

            CodeBook codeBook = db.CodeBooks.FirstOrDefault(f => f.CodePrefixId == id);
            if (codeBook == null)
            {
                return HttpNotFound();
            }
            db.CodeBooks.Remove(codeBook);
            db.SaveChanges();

            db.CodePrefixes.Remove(codePrefix);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult IsCodePrefixExist(int id, DocumentTypeEnum codePrefix)
        {
            using (var codeRepository = new CodeRepository())
            {
                bool isExist = true;
                isExist = codeRepository.IsCodePrefixExist(id, codePrefix);
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
