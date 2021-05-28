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
    public class CodePrefixesController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: CodePrefixes
        public ActionResult Index()
        {
            return View(db.CodePrefixes.ToList());
        }

        // GET: CodePrefixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodePrefix codePrefix = db.CodePrefixes.Find(id);
            if (codePrefix == null)
            {
                return HttpNotFound();
            }
            return View(codePrefix);
        }

        // GET: CodePrefixes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodePrefixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,DisplayName,DocumentTypeEnum,StartNumber,NewStartNumberEachYear,DateCreated,CreatedBy,DateModified,ModifiedBy")] CodePrefix codePrefix)
        {
            if (ModelState.IsValid)
            {
                db.CodePrefixes.Add(codePrefix);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codePrefix);
        }

        // GET: CodePrefixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodePrefix codePrefix = db.CodePrefixes.Find(id);
            if (codePrefix == null)
            {
                return HttpNotFound();
            }
            return View(codePrefix);
        }

        // POST: CodePrefixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DisplayName,DocumentTypeEnum,StartNumber,NewStartNumberEachYear,DateCreated,CreatedBy,DateModified,ModifiedBy")] CodePrefix codePrefix)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codePrefix).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codePrefix);
        }

        // GET: CodePrefixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodePrefix codePrefix = db.CodePrefixes.Find(id);
            if (codePrefix == null)
            {
                return HttpNotFound();
            }
            return View(codePrefix);
        }

        // POST: CodePrefixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodePrefix codePrefix = db.CodePrefixes.Find(id);
            db.CodePrefixes.Remove(codePrefix);
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
