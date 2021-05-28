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

namespace MarkomPos.Web.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class CodeBooksController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: CodeBooks
        public ActionResult Index()
        {
            var codeBooks = db.CodeBooks.Include(c => c.CodePrefix);
            return View(codeBooks.ToList());
        }

        // GET: CodeBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeBook codeBook = db.CodeBooks.Find(id);
            if (codeBook == null)
            {
                return HttpNotFound();
            }
            return View(codeBook);
        }

        // GET: CodeBooks/Create
        public ActionResult Create()
        {
            ViewBag.CodePrefixId = new SelectList(db.CodePrefixes, "ID", "Name");
            return View();
        }

        // POST: CodeBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CodePrefixId,NextNumber,Year,DateCreated,CreatedBy,DateModified,ModifiedBy")] CodeBook codeBook)
        {
            if (ModelState.IsValid)
            {
                db.CodeBooks.Add(codeBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodePrefixId = new SelectList(db.CodePrefixes, "ID", "Name", codeBook.CodePrefixId);
            return View(codeBook);
        }

        // GET: CodeBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeBook codeBook = db.CodeBooks.Find(id);
            if (codeBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodePrefixId = new SelectList(db.CodePrefixes, "ID", "Name", codeBook.CodePrefixId);
            return View(codeBook);
        }

        // POST: CodeBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodePrefixId,NextNumber,Year,DateCreated,CreatedBy,DateModified,ModifiedBy")] CodeBook codeBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codeBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodePrefixId = new SelectList(db.CodePrefixes, "ID", "Name", codeBook.CodePrefixId);
            return View(codeBook);
        }

        // GET: CodeBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeBook codeBook = db.CodeBooks.Find(id);
            if (codeBook == null)
            {
                return HttpNotFound();
            }
            return View(codeBook);
        }

        // POST: CodeBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeBook codeBook = db.CodeBooks.Find(id);
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
