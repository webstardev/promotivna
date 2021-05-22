using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class ContactRepository : IDisposable
    {
        public bool AddUpdateContact(Contact contact)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (contact.ID > 0)
                    {
                        var dbData = context.Contacts.Find(contact.ID);
                        if (dbData != null)
                        {
                            dbData.ID = contact.ID;
                            dbData.Name = contact.Name;
                            dbData.Code = contact.Code;
                            dbData.OIB = contact.OIB;
                            dbData.Address = contact.Address;
                            dbData.Country = contact.Country;
                            dbData.CountryCode = contact.CountryCode;
                            dbData.Phone = contact.Phone;
                            dbData.Phone2 = contact.Phone2;
                            dbData.Fax = contact.Fax;
                            dbData.MobilePhone = contact.MobilePhone;
                            dbData.Email = contact.Email;
                            dbData.WebAddress = contact.WebAddress;
                            dbData.Person = contact.Person;
                            dbData.AccountNumber = contact.AccountNumber;
                            dbData.CreditLimit = contact.CreditLimit;
                            dbData.Discount = contact.Discount;
                            dbData.HasWarranty = contact.HasWarranty;
                            dbData.WarrantyNote = contact.WarrantyNote;
                            dbData.Note = contact.Note;
                            dbData.Note2 = contact.Note2;
                            dbData.IsBuyer = contact.IsBuyer;
                            dbData.IsSupplier = contact.IsSupplier;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        contact.DateCreated = DateTime.Now;
                        contact.DateModified = DateTime.Now;
                        context.Contacts.Add(contact);
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
