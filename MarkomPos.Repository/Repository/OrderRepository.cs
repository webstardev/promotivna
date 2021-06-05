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
    public class OrderRepository : IDisposable
    {
        public List<OrderVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.Orders
                    .Include(i => i.DeliveryTerm)
                    .Include(i => i.DocumentParity)
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.ResponsibleUser)
                    .Include(i => i.Contact)
                    .Adapt<List<OrderVm>>().ToList();
            }
        }
        public OrderVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var orderVm = new OrderVm();
                var orderData = context.Orders
                    .Include(i => i.DeliveryTerm)
                    .Include(i => i.DocumentParity)
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.ResponsibleUser)
                    .FirstOrDefault(f => f.ID == id)
                    .Adapt<OrderVm>();

                if (orderData != null)
                {
                    orderVm = orderData;
                    orderVm.Contacts = new SelectList(context.Contacts, "ID", "Name", orderData.ContactId).ToList();
                    orderVm.DeliveryTerms = new SelectList(context.DeliveryTerms, "ID", "Name", orderData.DeliveryTermId).ToList();
                    orderVm.DocumentParities = new SelectList(context.DocumentParities, "ID", "Name", orderData.DocumentParityId).ToList();
                    orderVm.PaymentMethods = new SelectList(context.PaymentMethods, "ID", "Name", orderData.PaymentMethodId).ToList();
                    using (var userRepository = new UserRepository())
                    {
                        orderVm.Users = new SelectList(userRepository.getAllUser(), "ID", "Name", orderData.ResponsibleUserId).ToList();
                    }
                }
                else
                {
                    orderVm.Contacts = new SelectList(context.Contacts, "ID", "Name").ToList();
                    orderVm.DeliveryTerms = new SelectList(context.DeliveryTerms, "ID", "Name").ToList();
                    orderVm.DocumentParities = new SelectList(context.DocumentParities, "ID", "Name").ToList();
                    orderVm.PaymentMethods = new SelectList(context.PaymentMethods, "ID", "Name").ToList();
                    using (var userRepository = new UserRepository())
                    {
                        orderVm.Users = new SelectList(userRepository.getAllUser(), "ID", "Name").ToList();
                    }
                }
                return orderVm;
            }
        }
        public bool AddUpdateOrder(OrderVm order)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (order.ID > 0)
                    {
                        var dbData = context.Orders.Find(order.ID);
                        if (dbData != null)
                        {
                            dbData.ID = order.ID;
                            dbData.IsExternalOrder = order.IsExternalOrder;
                            dbData.OrderDate = order.OrderDate;
                            dbData.OrderNumber = order.OrderNumber;
                            dbData.ExpirationDate = order.ExpirationDate;
                            dbData.DeliveryTermId = order.DeliveryTermId;
                            dbData.DocumentParityId = order.DocumentParityId;
                            dbData.PaymentMethodId = order.PaymentMethodId;
                            dbData.ResponsibleUserId = order.ResponsibleUserId;
                            dbData.ContactId = order.ContactId;
                            dbData.ContactName = order.ContactName;
                            dbData.ContactAddress = order.ContactAddress;
                            dbData.ContactCountry = order.ContactCountry;
                            dbData.PrintNote = order.PrintNote;
                            dbData.Note = order.Note;
                            dbData.Note2 = order.Note2;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        order.DateCreated = DateTime.Now;
                        order.DateModified = DateTime.Now;
                        var orderData = order.Adapt<Order>();
                        context.Orders.Add(orderData);
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
