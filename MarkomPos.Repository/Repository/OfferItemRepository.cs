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
    public class OfferItemRepository : IDisposable
    {
        public List<OfferItemVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.OfferItems
                    .Include(i => i.Product)
                    .Include(i => i.Product)
                    .Include(i => i.UnitOfMeasure)
                    .Adapt<List<OfferItemVm>>().ToList();
            }
        }
        public List<OfferItemVm> getOfferItemList(int OfferId)
        {
            using (var context = new markomPosDbContext())
            {
                return context.OfferItems
                    .Where(x => x.OfferId == OfferId)
                    .Include(o => o.Offer)
                    .Include(o => o.Product)
                    .Include(o => o.UnitOfMeasure)
                    .Adapt<List<OfferItemVm>>().ToList();
            }
        }
        public OfferItemVm GetById(int id, int offerId)
        {
            using (var context = new markomPosDbContext())
            {
                var offerItemVm = new OfferItemVm();
                var offerItemData = context.OfferItems
                    .Include(o => o.Offer)
                    .Include(o => o.Product)
                    .Include(o => o.UnitOfMeasure)
                    .FirstOrDefault(f => f.ID == id)
                    .Adapt<OfferItemVm>();

                if (offerItemData != null)
                {
                    offerItemVm = offerItemData;
                    offerItemVm.Products = new SelectList(context.Products, "ID", "Name", offerItemData.ProductId).ToList();
                    offerItemVm.Offers = new SelectList(context.Offers, "ID", "ContactName", offerItemData.OfferId).ToList();
                    offerItemVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name", offerItemData.UnitOfMeasureId).ToList();
                }
                else
                {
                    offerItemVm.Products = new SelectList(context.Products, "ID", "Name").ToList();
                    offerItemVm.Offers = new SelectList(context.Offers, "ID", "ContactName").ToList();
                    offerItemVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name").ToList();
                }

                if (offerItemData == null)
                    offerItemVm.Offers = new SelectList(context.Offers, "ID", "ContactName", offerId).ToList();

                return offerItemVm;
            }
        }
        public bool AddUpdateOfferItem(OfferItemVm offerItem)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (offerItem.ID > 0)
                    {
                        var dbData = context.OfferItems.Find(offerItem.ID);
                        if (dbData != null)
                        {
                            dbData.ID = offerItem.ID;
                            dbData.OfferId = offerItem.OfferId;
                            dbData.Ordinal = offerItem.Ordinal;
                            dbData.ProductId = offerItem.ProductId;
                            dbData.ProductName = offerItem.ProductName;
                            dbData.UnitOfMeasureId = offerItem.UnitOfMeasureId;
                            dbData.UnitOfMeasureName = offerItem.UnitOfMeasureName;
                            dbData.Quantity = offerItem.Quantity;
                            dbData.Price = offerItem.Price;
                            dbData.Discount = offerItem.Discount;
                            dbData.Porez = offerItem.Porez;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        offerItem.DateCreated = DateTime.Now;
                        offerItem.DateModified = DateTime.Now;
                        var offerItemData = offerItem.Adapt<OfferItem>();
                        context.OfferItems.Add(offerItemData);
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
