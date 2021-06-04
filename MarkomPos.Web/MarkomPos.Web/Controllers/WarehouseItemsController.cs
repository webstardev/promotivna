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
    public class WarehouseItemsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();
        private WarehouseItemRepository warehouseItemRepository = new WarehouseItemRepository();
        // GET: WarehouseItems
        public ActionResult Index()
        {
            var warehouseItem = warehouseItemRepository.GetAll();
            return View(warehouseItem.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var warehouseItem = warehouseItemRepository.GetById(id.GetValueOrDefault(0));
            if (warehouseItem == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", warehouseItem);
        }

        public ActionResult Create()
        {
            var warehouseItem = warehouseItemRepository.GetById(0);
            if (warehouseItem == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddWarehouseItem", warehouseItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WarehouseItemVm warehouseItem)
        {
            if (ModelState.IsValid)
            {
                var result = warehouseItemRepository.AddUpdateWareHouseItem(warehouseItem);
                if (result)
                    return RedirectToAction("Index");
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
            var warehouseItem = warehouseItemRepository.GetById(id.GetValueOrDefault(0));
            if (warehouseItem == null)
            {
                return HttpNotFound();
            }
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
