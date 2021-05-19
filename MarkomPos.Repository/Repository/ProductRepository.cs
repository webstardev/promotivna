using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class ProductRepository : IDisposable
    {
        public bool AddUpdateProduct(Product product)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (product.ID > 0)
                    {
                        var dbData = context.Products.Find(product.ID);
                        if (dbData != null)
                        {
                            dbData.ID = product.ID;
                            dbData.Name = product.Name;
                            dbData.DisplayName = product.DisplayName;
                            dbData.Note = product.Note;
                            dbData.Note2 = product.Note2;
                            dbData.Code = product.Code;
                            dbData.UnitOfMeasureId = product.UnitOfMeasureId;
                            dbData.ProductGroupId = product.ProductGroupId;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        product.DateCreated = DateTime.Now;
                        product.DateModified = DateTime.Now;
                        context.Products.Add(product);
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
