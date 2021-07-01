using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class DeliveryTermRepository : IDisposable
    {
        public bool AddUpdateDeliveryTerm(DeliveryTerm deliveryTerm)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (deliveryTerm.ID > 0)
                    {
                        var dbData = context.DeliveryTerms.Find(deliveryTerm.ID);
                        if (dbData != null)
                        {
                            dbData.ID = deliveryTerm.ID;
                            dbData.Name = deliveryTerm.Name;
                            dbData.DisplayName = deliveryTerm.DisplayName;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.DeliveryTerms.Any(f => f.Name == deliveryTerm.Name);
                        if (!isExist)
                        {
                            deliveryTerm.DateCreated = DateTime.Now;
                            deliveryTerm.DateModified = DateTime.Now;
                            context.DeliveryTerms.Add(deliveryTerm);
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
        public bool IsExist(int id, string deliveryTerm)
        {
            bool response = false;
            using (var context = new markomPosDbContext())
            {
                if (id > 0)
                    response = context.DeliveryTerms.Any(f => f.Name == deliveryTerm && f.ID != id);
                else
                    response = context.DeliveryTerms.Any(f => f.Name == deliveryTerm);
            }
            return response;
        }

        public void Dispose()
        {
        }
    }
}
