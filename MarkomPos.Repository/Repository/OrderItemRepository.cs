using MarkomPos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mapster;
using System.Web.Mvc;
using MarkomPos.Model.Model;

namespace MarkomPos.Repository.Repository
{
    public class OrderItemRepository : IDisposable
    {
        public List<OrderItemVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.OrderItems
                    .Include(i => i.Product)
                    .Include(i => i.Product)
                    .Include(i => i.UnitOfMeasure)
                    .Adapt<List<OrderItemVm>>().ToList();
            }
        }
        public List<OrderItemVm> getOrderItemList(int orderId)
        {
            using (var context = new markomPosDbContext())
            {
                return context.OrderItems
                    .Where(x => x.OrderId == orderId)
                    .Include(o => o.Order)
                    .Include(o => o.Product)
                    .Include(o => o.UnitOfMeasure)
                    .Adapt<List<OrderItemVm>>().ToList();
            }
        }
        public OrderItemVm GetById(int id, int orderId)
        {
            using (var context = new markomPosDbContext())
            {
                var orderItemVm = new OrderItemVm();
                var orderItemData = context.OrderItems
                    .Include(o => o.Order)
                    .Include(o => o.Product)
                    .Include(o => o.UnitOfMeasure)
                    .FirstOrDefault(f => f.ID == id)
                    .Adapt<OrderItemVm>();

                if (orderItemData != null)
                {
                    orderItemVm = orderItemData;
                    orderItemVm.Products = new SelectList(context.Products, "ID", "Name", orderItemData.ProductId).ToList();
                    orderItemVm.Orders = new SelectList(context.Orders, "ID", "ContactName", orderItemData.OrderId).ToList();
                    orderItemVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name", orderItemData.UnitOfMeasureId).ToList();
                }
                else
                {
                    orderItemVm.Products = new SelectList(context.Products, "ID", "Name").ToList();
                    orderItemVm.Orders = new SelectList(context.Orders, "ID", "ContactName").ToList();
                    orderItemVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name").ToList();
                }

                if (orderItemData == null)
                    orderItemVm.Orders = new SelectList(context.Orders, "ID", "ContactName", orderId).ToList();

                return orderItemVm;
            }
        }
        public bool AddUpdateOrderItem(OrderItemVm orderItem)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (orderItem.ID > 0)
                    {
                        var dbData = context.OrderItems.Find(orderItem.ID);
                        if (dbData != null)
                        {
                            dbData.ID = orderItem.ID;
                            dbData.OrderId = orderItem.OrderId;
                            dbData.Ordinal = orderItem.Ordinal;
                            dbData.ProductId = orderItem.ProductId;
                            dbData.ProductName = orderItem.ProductName;
                            dbData.ShortDescription = orderItem.ShortDescription;
                            dbData.UnitOfMeasureId = orderItem.UnitOfMeasureId;
                            dbData.UnitOfMeasureName = orderItem.UnitOfMeasureName;
                            dbData.Quantity = orderItem.Quantity;
                            dbData.Price = orderItem.Price;
                            dbData.Discount = orderItem.Discount;
                            dbData.Porez = orderItem.Porez;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        orderItem.DateCreated = DateTime.Now;
                        orderItem.DateModified = DateTime.Now;
                        var orderItemData = orderItem.Adapt<OrderItem>();
                        context.OrderItems.Add(orderItemData);
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public void Dispose()
        {
        }
    }
}
