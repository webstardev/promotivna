using MarkomPos.Model.Model;
using MarkomPos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mapster;
using System.Web.Mvc;

namespace MarkomPos.Repository.Repository
{
    public class OfferRepository : IDisposable
    {
        public List<OfferVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.Offers
                    .Include(i => i.DeliveryTerm)
                    .Include(i => i.DocumentParity)
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.ResponsibleUser)
                    .Include(i => i.Contact)
                    .Adapt<List<OfferVm>>().ToList();
            }
        }
        public OfferVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var offerVm = new OfferVm();
                var offerData = context.Offers
                    .Include(i => i.DeliveryTerm)
                    .Include(i => i.DocumentParity)
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.ResponsibleUser)
                    .FirstOrDefault(f => f.ID == id)
                    .Adapt<OfferVm>();

                if (offerData != null)
                {
                    offerVm = offerData;
                    offerVm.Contacts = new SelectList(context.Contacts, "ID", "Name", offerData.ContactId).ToList();
                    offerVm.DeliveryTerms = new SelectList(context.DeliveryTerms, "ID", "Name", offerData.DeliveryTermId).ToList();
                    offerVm.DocumentParities = new SelectList(context.DocumentParities, "ID", "Name", offerData.DocumentParityId).ToList();
                    offerVm.PaymentMethods = new SelectList(context.PaymentMethods, "ID", "Name", offerData.PaymentMethodId).ToList();
                    using (var userRepository = new UserRepository())
                    {
                        offerVm.Users = new SelectList(userRepository.getAllUser(), "ID", "Name", offerData.ResponsibleUserId).ToList();
                    }
                }
                else
                {
                    offerVm.Contacts = new SelectList(context.Contacts, "ID", "Name").ToList();
                    offerVm.DeliveryTerms = new SelectList(context.DeliveryTerms, "ID", "Name").ToList();
                    offerVm.DocumentParities = new SelectList(context.DocumentParities, "ID", "Name").ToList();
                    offerVm.PaymentMethods = new SelectList(context.PaymentMethods, "ID", "Name").ToList();
                    using (var userRepository = new UserRepository())
                    {
                        offerVm.Users = new SelectList(userRepository.getAllUser(), "ID", "Name").ToList();
                    }
                }
                return offerVm;
            }
        }
        public bool AddUpdateOffer(OfferVm offer)
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
                        var offerData = offer.Adapt<Offer>();
                        context.Offers.Add(offerData);
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
