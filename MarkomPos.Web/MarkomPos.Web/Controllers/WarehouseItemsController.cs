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
    public class WarehouseItemsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: WarehouseItems
        public ActionResult Index()
        {
            var warehouseItems = db.warehouseItems.Include(w => w.Product).Include(w => w.Warehouse);
            return View(warehouseItems.ToList());
        }

        // GET: WarehouseItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarehouseItem warehouseItem = db.warehouseItems.Find(id);
            if (warehouseItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name", warehouseItem.ProductId);
            ViewBag.WarehouseId = new SelectList(db.warehouses, "ID", "Name", warehouseItem.WarehouseId);
            return PartialView("_Details", warehouseItem);
        }

        // GET: WarehouseItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name");
            ViewBag.WarehouseId = new SelectList(db.warehouses, "ID", "Name");
            //WarehouseItem warehouseItem = new WarehouseItem();
            return PartialView("_AddWarehouseItem", null);
        }

        // POST: WarehouseItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WarehouseItem warehouseItem)
        {
            if (ModelState.IsValid)
            {
                using (var warehouseRepository = new WarehouseRepository())
                {
                    var result = warehouseRepository.AddUpdateWareHouseItem(warehouseItem);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: WarehouseItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarehouseItem warehouseItem = db.warehouseItems.Find(id);
            if (warehouseItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ID", "Name", warehouseItem.ProductId);
            ViewBag.WarehouseId = new SelectList(db.warehouses, "ID", "Name", warehouseItem.WarehouseId);
            return PartialView("_AddWarehouseItem", warehouseItem);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            WarehouseItem warehouseItem = db.warehouseItems.Find(id);
            db.warehouseItems.Remove(warehouseItem);
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
