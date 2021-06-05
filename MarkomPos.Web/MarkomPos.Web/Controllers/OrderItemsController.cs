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
    public class OrderItemsController : Controller
    {
        private markomPosDbContext db = new markomPosDbContext();

        // GET: OrderItems
        public ActionResult Index()
        {
            using (var orderItemRepository = new OrderItemRepository())
            {
                var orderItems = orderItemRepository.GetAll();
                return View(orderItems);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var orderItemRepository = new OrderItemRepository())
            {
                var orderItem = orderItemRepository.GetById(id.GetValueOrDefault(0), 0);
                if (orderItem == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_Details", orderItem);
            }
        }

        public ActionResult Create(int orderId = 0)
        {
            using (var orderItemRepository = new OrderItemRepository())
            {
                var orderItem = orderItemRepository.GetById(0, orderId);
                if (orderItem == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOrderItem", orderItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderItemVm orderItem)
        {
            if (ModelState.IsValid)
            {
                using (var orderItemRepository = new OrderItemRepository())
                {
                    var result = orderItemRepository.AddUpdateOrderItem(orderItem);
                    if (result)
                        return Redirect(Request.UrlReferrer.ToString());
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var orderItemRepository = new OrderItemRepository())
            {
                var orderItem = orderItemRepository.GetById(id.GetValueOrDefault(0), 0);
                if (orderItem == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_AddOrderItem", orderItem);
            }
        }

        [HttpGet, ActionName("DeleteConfirmed")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            db.OrderItems.Remove(orderItem);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
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
