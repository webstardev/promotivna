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
    public class ProductRepository : IDisposable
    {
        public List<ProductVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.Products.Include(p => p.ProductGroup).Include(i => i.UnitOfMeasure).Adapt<List<ProductVm>>().ToList();
            }
        }
        public ProductVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var productVm = new ProductVm();
                var productData = context.Products.Include(p => p.ProductGroup).Include(i => i.UnitOfMeasure).FirstOrDefault(f => f.ID == id).Adapt<ProductVm>();
                if (productData != null)
                {
                    productVm = productData;
                    productVm.ProductGroups = new SelectList(context.ProductGroups, "ID", "Name", productData.ProductGroupId).ToList();
                    productVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name", productData.UnitOfMeasureId).ToList();
                }
                else
                {
                    productVm.ProductGroups = new SelectList(context.ProductGroups, "ID", "Name").ToList();
                    productVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name").ToList();
                }
                return productVm;
            }
        }
        public bool AddUpdateProduct(ProductVm product)
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
                        var productData = product.Adapt<Product>();
                        context.Products.Add(productData);
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
