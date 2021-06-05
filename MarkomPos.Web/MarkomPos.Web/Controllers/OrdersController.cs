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
    public class OrdersController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        public ActionResult Index()
        {
            using (var orderRepository = new OrderRepository())
            {
                var orders = orderRepository.GetAll();
                return View(orders);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var orderRepository = new OrderRepository())
            {
                var order = orderRepository.GetById(id.GetValueOrDefault(0));
                if (order == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", order);
            }
        }

        public ActionResult Create()
        {
            using (var orderRepository = new OrderRepository())
            {
                var order = orderRepository.GetById(0);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOrder", order);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderVm order)
        {
            if (ModelState.IsValid)
            {
                using (var orderRepository = new OrderRepository())
                {
                    var result = orderRepository.AddUpdateOrder(order);
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
            using (var orderRepository = new OrderRepository())
            {
                var order = orderRepository.GetById(id.GetValueOrDefault(0));
                if (order == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOrder", order);
            }
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            db.Orders.Remove(order);
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
