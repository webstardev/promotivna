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
    public class ProductsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductGroup).Include(p => p.UnitOfMeasure);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ID", "Name");
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name");
            return PartialView("_Details", product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ID", "Name");
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name");
            return PartialView("_AddProduct", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            using (var productRepository = new ProductRepository())
            {
                var result = productRepository.AddUpdateProduct(product);
                if (result)
                    return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupId = new SelectList(db.ProductGroups, "ID", "Name", product.ProductGroupId);
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "ID", "Name", product.UnitOfMeasureId);
            return PartialView("_AddProduct", product);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
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
