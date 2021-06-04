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
    public class OfferValidationRepository : IDisposable
    {
        public List<OfferValidationVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.OfferValidations.Include(p => p.User).Include(i => i.Offer).Adapt<List<OfferValidationVm>>().ToList();
            }
        }
        public OfferValidationVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var offerValidationVm = new OfferValidationVm();
                var offerValidationData = context.OfferValidations.Include(p => p.User).Include(i => i.Offer).FirstOrDefault(f => f.ID == id).Adapt<OfferValidationVm>();
                if (offerValidationData != null)
                {
                    offerValidationVm = offerValidationData;
                    offerValidationVm.Offers = new SelectList(context.Offers, "ID", "ContactName", offerValidationData.OfferId).ToList();
                    offerValidationVm.Users = new SelectList(context.Users, "ID", "Name", offerValidationData.UserId).ToList();
                }
                else
                {
                    offerValidationVm.Offers = new SelectList(context.Offers, "ID", "ContactName").ToList();
                    offerValidationVm.Users = new SelectList(context.Users, "ID", "Name").ToList();
                }
                return offerValidationVm;
            }
        }
        public bool AddUpdateOfferValidation(OfferValidationVm offerValidation)
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
                        var offerValidationData = offerValidation.Adapt<OfferValidation>();
                        context.OfferValidations.Add(offerValidationData);
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
