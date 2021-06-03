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
    public class CodeBooksController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            using (var codeRepository = new CodeRepository())
            {
                var codeBooks = codeRepository.GetAll();
                return View(codeBooks);
            }
        }

        // GET: CodeBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var codeRepository = new CodeRepository())
            {
                var codeBook = codeRepository.GetById(id.GetValueOrDefault(0));
                if (codeBook == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", codeBook);
            }
        }

        // GET: CodeBooks/Create
        public ActionResult Create()
        {
            using (var codeRepository = new CodeRepository())
            {
                var codeBook = codeRepository.GetById(0);
                if (codeBook == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddCodeBook", codeBook);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodeBookVm codeBook)
        {
            if (ModelState.IsValid)
            {
                using (var codeRepository = new CodeRepository())
                {
                    var result = codeRepository.AddUpdateCodeBook(codeBook);
                    if (result)
                        return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var codeRepository = new CodeRepository())
            {
                var codeBook = codeRepository.GetById(id.GetValueOrDefault(0));
                if (codeBook == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddCodeBook", codeBook);
            }
        }


        [HttpGet, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeBook codeBook = db.CodeBooks.Find(id);
            if (codeBook == null)
            {
                return HttpNotFound();
            }
            db.CodeBooks.Remove(codeBook);
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
