using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class PaymentMethodRepository : IDisposable
    {
        public bool AddUpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (paymentMethod.ID > 0)
                    {
                        var dbData = context.PaymentMethods.Find(paymentMethod.ID);
                        if (dbData != null)
                        {
                            dbData.ID = paymentMethod.ID;
                            dbData.Name = paymentMethod.Name;
                            dbData.DisplayName = paymentMethod.DisplayName;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.PaymentMethods.Any(f => f.Name == paymentMethod.Name);
                        if (!isExist)
                        {
                            paymentMethod.DateCreated = DateTime.Now;
                            paymentMethod.DateModified = DateTime.Now;
                            context.PaymentMethods.Add(paymentMethod);
                        }
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

        public bool IsExist(int id, string name)
        {
            bool response = false;
            using (var context = new markomPosDbContext())
            {
                if (id > 0)
                    response = context.PaymentMethods.Any(f => f.Name == name && f.ID != id);
                else
                    response = context.PaymentMethods.Any(f => f.Name == name);
            }
            return response;
        }

        public void Dispose()
        {
        }
    }
}
