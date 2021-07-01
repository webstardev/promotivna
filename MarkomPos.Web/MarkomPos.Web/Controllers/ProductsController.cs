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
    public class ProductsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Products
        public ActionResult Index()
        {
            using (var productRepository = new ProductRepository())
            {
                var products = productRepository.GetAll();
                return View(products.ToList());
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var productRepository = new ProductRepository())
            {
                var product = productRepository.GetById(id.GetValueOrDefault(0));
                if (product == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", product);
            }
        }

        public ActionResult Create()
        {
            using (var productRepository = new ProductRepository())
            {
                var product = productRepository.GetById(0);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddProduct", product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVm product)
        {
            if (ModelState.IsValid)
            {
                using (var productRepository = new ProductRepository())
                {
                    var result = productRepository.AddUpdateProduct(product);
                    if (result)
                        return Json(result, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var productRepository = new ProductRepository())
            {
                var product = productRepository.GetById(id.GetValueOrDefault(0));
                if (product == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddProduct", product);
            }
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

        public JsonResult IsProductCodeExist()
        {
            using (var productRepository = new ProductRepository())
            {
                bool isExist = true;
                isExist = productRepository.IsProductCodeExist();
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
