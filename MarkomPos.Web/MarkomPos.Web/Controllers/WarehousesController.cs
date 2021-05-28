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
    public class WarehousesController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: Warehouses
        public ActionResult Index()
        {
            return View(db.warehouses.ToList());
        }

        // GET: Warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", warehouse);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            var warehous = new Warehouse();
            return PartialView("_AddWarehouse", warehous);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                using (var warehouseRepository = new WarehouseRepository())
                {
                    var result = warehouseRepository.AddUpdateWareHouse(warehouse);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = db.warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddWarehouse", warehouse);
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Warehouse warehouse = db.warehouses.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            db.warehouses.Remove(warehouse);
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
