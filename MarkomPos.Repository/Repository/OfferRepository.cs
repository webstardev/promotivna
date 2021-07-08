using MarkomPos.Model.Model;
using MarkomPos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mapster;
using System.Web.Mvc;
using MarkomPos.Model.Enum;

namespace MarkomPos.Repository.Repository
{
    public class OfferRepository : IDisposable
    {
        public OfferIndexVm GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                var offerIndexVm = new OfferIndexVm()
                {
                    OfferList = new List<OfferVm>(),
                    OfferValidationList = new List<OfferValidationVm>()
                };
                offerIndexVm.OfferList = context.Offers
                    .Include(i => i.DeliveryTerm)
                    .Include(i => i.DocumentParity)
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.ResponsibleUser)
                    .Include(i => i.Contact)
                    .Adapt<List<OfferVm>>().ToList();

                if (offerIndexVm.OfferList != null && offerIndexVm.OfferList.Count > 0)
                {
                    var offer = offerIndexVm.OfferList.FirstOrDefault();
                    if (offer != null)
                    {
                        offerIndexVm.OfferValidationList = context.OfferValidations
                            .Include(p => p.User)
                            .Include(i => i.Offer)
                            .Where(w => w.OfferId == offer.ID)
                            .Adapt<List<OfferValidationVm>>().ToList();
                    }
                }


                return offerIndexVm;
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
                        offerVm.ResponsibleUsers = new SelectList(userRepository.getAllUser(), "ID", "Name", offerData.ResponsibleUserId).ToList();
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
                        offerVm.ResponsibleUsers = new SelectList(userRepository.getAllUser(), "ID", "Name").ToList();
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
                    int nextNumber = 0;
                    if (offer.ID > 0)
                    {
                        var dbData = context.Offers.Find(offer.ID);
                        if (dbData != null)
                        {
                            dbData.ID = offer.ID;
                            dbData.OfferDate = offer.OfferDate;
                            //dbData.OfferNumber = offer.OfferNumber;
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
                        var codeData = (from cp in context.CodePrefixes
                                        join cb in context.CodeBooks on cp.ID equals cb.CodePrefixId
                                        where cp.DocumentTypeEnum == DocumentTypeEnum.Offer
                                        select new
                                        {
                                            cb.NextNumber,
                                            cp.DisplayName
                                        }).FirstOrDefault();

                        if (codeData != null)
                        {
                            nextNumber = codeData.NextNumber;
                            offer.OfferNumber = codeData.DisplayName + nextNumber;

                            var isExistCode = context.Products.Any(a => a.Code == offer.OfferNumber);
                            if (isExistCode)
                            {
                                nextNumber = nextNumber + 1;
                                offer.OfferNumber = codeData.DisplayName + nextNumber;
                            }
                        }

                        offer.DateCreated = DateTime.Now;
                        offer.DateModified = DateTime.Now;
                        var offerData = offer.Adapt<Offer>();
                        context.Offers.Add(offerData);
                    }
                    context.SaveChanges();

                    var codeBookData = (from cb in context.CodeBooks
                                        join cp in context.CodePrefixes on cb.CodePrefixId equals cp.ID
                                        where cp.DocumentTypeEnum == DocumentTypeEnum.Product
                                        select cb).FirstOrDefault();
                    codeBookData.NextNumber = nextNumber + 1;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public OfferIndexVm ChangeOrder(int offerId)
        {
            using (var context = new markomPosDbContext())
            {
                var offerIndexVm = new OfferIndexVm()
                {
                    OfferList = new List<OfferVm>(),
                    OfferValidationList = new List<OfferValidationVm>()
                };
                offerIndexVm.OfferList = context.Offers
                    .Include(i => i.DeliveryTerm)
                    .Include(i => i.DocumentParity)
                    .Include(i => i.PaymentMethod)
                    .Include(i => i.ResponsibleUser)
                    .Include(i => i.Contact)
                    .Adapt<List<OfferVm>>().ToList();

                if (offerIndexVm.OfferList != null && offerIndexVm.OfferList.Count > 0 && offerId == 0)
                {
                    var offer = offerIndexVm.OfferList.FirstOrDefault();
                    if (offer != null)
                    {
                        offerIndexVm.OfferValidationList = context.OfferValidations
                            .Include(p => p.User)
                            .Include(i => i.Offer)
                            .Where(w => w.OfferId == offer.ID)
                            .Adapt<List<OfferValidationVm>>().ToList();
                    }
                }
                else
                {
                    offerIndexVm.OfferValidationList = context.OfferValidations
                            .Include(p => p.User)
                            .Include(i => i.Offer)
                            .Where(w => w.OfferId == offerId)
                            .Adapt<List<OfferValidationVm>>().ToList();
                }

                return offerIndexVm;
            }
        }
        public OfferIndexVm OfferFilter(DateTime fromDate, DateTime toDate)
        {
            using (var context = new markomPosDbContext())
            {
                var offerIndexVm = new OfferIndexVm()
                {
                    OfferList = new List<OfferVm>(),
                    OfferValidationList = new List<OfferValidationVm>()
                };
                offerIndexVm.OfferList = (from of in context.Offers.Where(w => DbFunctions.TruncateTime(w.DateCreated) >= fromDate && DbFunctions.TruncateTime(w.DateCreated) <= toDate)
                                          select new OfferVm()
                                          {
                                              ID = of.ID,
                                              OfferNumber = of.OfferNumber,
                                              ContactId = of.ContactId,
                                              DeliveryTermId = of.DeliveryTermId,
                                              DocumentParityId = of.DocumentParityId,
                                              PaymentMethodId = of.PaymentMethodId,
                                              ResponsibleUserId = of.ResponsibleUserId,
                                              Contact = of.Contact,
                                              DeliveryTerm = of.DeliveryTerm,
                                              DocumentParity = of.DocumentParity,
                                              PaymentMethod = of.PaymentMethod,
                                              ResponsibleUser = of.ResponsibleUser,
                                              OfferItemPrice = context.OfferItems.Where(w => w.OfferId == of.ID).ToList().Count > 0 ? context.OfferItems.Where(w => w.OfferId == of.ID).Sum(s => s.Price) : 0
                                          })
                                 .Adapt<List<OfferVm>>().ToList();

                //offerIndexVm.OfferList = context.Offers
                //    .Include(i => i.DeliveryTerm)
                //    .Include(i => i.DocumentParity)
                //    .Include(i => i.PaymentMethod)
                //    .Include(i => i.ResponsibleUser)
                //    .Include(i => i.Contact)
                //    .Where(w => w.DateCreated >= fromDate && w.DateCreated <= toDate)
                //    .Adapt<List<OfferVm>>().ToList();

                if (offerIndexVm.OfferList != null && offerIndexVm.OfferList.Count > 0)
                {
                    var offer = offerIndexVm.OfferList.FirstOrDefault();
                    if (offer != null)
                    {
                        offerIndexVm.OfferValidationList = context.OfferValidations
                            .Include(p => p.User)
                            .Include(i => i.Offer)
                            .Where(w => w.OfferId == offer.ID)
                            .Adapt<List<OfferValidationVm>>().ToList();
                    }
                }

                return offerIndexVm;
            }
        }
        public bool IsOfferCodeExist()
        {
            bool response = false;
            using (var context = new markomPosDbContext())
            {
                response = context.CodePrefixes.Any(a => a.DocumentTypeEnum == DocumentTypeEnum.Offer);
            }
            return response;
        }
        public void Dispose()
        {
        }
    }
}
