using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class OfferRepository : IDisposable
    {
        public bool AddUpdateOffer(Offer offer)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (offer.ID > 0)
                    {
                        var dbData = context.Offers.Find(offer.ID);
                        if (dbData != null)
                        {
                            dbData.ID = offer.ID;
                            dbData.OfferDate = offer.OfferDate;
                            dbData.OfferNumber = offer.OfferNumber;
                            dbData.ExpirationDate = offer.ExpirationDate;
                            dbData.DeliveryTermId = offer.DeliveryTermId;
                            dbData.DocumentParityId = offer.DocumentParityId;
                            dbData.PaymentMethodId = offer.PaymentMethodId;
                            dbData.ResponsibleUserId = offer.ResponsibleUserId;
                            dbData.ContactId = offer.ContactId;
                            dbData.ContactName = offer.ContactName;
                            dbData.ContactAddress = offer.ContactAddress;
                            dbData.ContactCountry = offer.ContactCountry;
                            dbData.PrintNote = offer.PrintNote;
                            dbData.Note = offer.Note;
                            dbData.Note2 = offer.Note2;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        offer.DateCreated = DateTime.Now;
                        offer.DateModified = DateTime.Now;
                        context.Offers.Add(offer);
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
        public bool AddUpdateOfferItem(OfferItem offerItem)
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
                        context.OfferItems.Add(offerItem);
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
        public bool AddUpdateOfferValidation(OfferValidation offerValidation)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (offerValidation.ID > 0)
                    {
                        var dbData = context.OfferValidations.Find(offerValidation.ID);
                        if (dbData != null)
                        {
                            dbData.ID = offerValidation.ID;
                            dbData.OfferId = offerValidation.OfferId;
                            dbData.UserDiplayName = offerValidation.UserDiplayName;
                            dbData.UserId = offerValidation.UserId;
                            dbData.Note = offerValidation.Note;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        offerValidation.DateCreated = DateTime.Now;
                        offerValidation.DateModified = DateTime.Now;
                        context.OfferValidations.Add(offerValidation);
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
