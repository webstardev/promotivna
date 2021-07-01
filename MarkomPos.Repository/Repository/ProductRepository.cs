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
                    productVm.ProductGroups = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Basic), "ID", "Name", productData.ProductGroupId).ToList();
                    productVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name", productData.UnitOfMeasureId).ToList();
                }
                else
                {
                    productVm.ProductGroups = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Basic), "ID", "Name").ToList();
                    productVm.UnitOfMeasures = new SelectList(context.UnitOfMeasures, "ID", "Name").ToList();
                }

                productVm.MainProductGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Main), "ID", "Name").ToList();
                productVm.SubProductGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Sub), "ID", "Name").ToList();

                if (productVm.ProductGroupId != null && productVm.ProductGroupId > 0)
                {
                    var subID = context.ProductGroups.FirstOrDefault(f => f.productGroupType == ProductGroupTypeEnum.Basic && f.ID == productVm.ProductGroupId).ParrentGroupId;

                    if (subID != null && subID > 0)
                    {
                        productVm.SubProductGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Sub), "ID", "Name", subID).ToList();
                        var mainID = context.ProductGroups.FirstOrDefault(f => f.productGroupType == ProductGroupTypeEnum.Sub && f.ID == subID).ParrentGroupId;
                        productVm.MainProductGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Main), "ID", "Name", mainID).ToList();
                    }
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
                            dbData.UnitOfMeasureId = product.UnitOfMeasureId;
                            dbData.ProductGroupId = product.ProductGroupId;
                            dbData.DateModified = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        var codeData = (from cp in context.CodePrefixes
                                        join cb in context.CodeBooks on cp.ID equals cb.CodePrefixId
                                        where cp.DocumentTypeEnum == DocumentTypeEnum.Product
                                        select new
                                        {
                                            cb.NextNumber,
                                            cp.DisplayName
                                        }).FirstOrDefault();

                        var nextNumber = codeData.NextNumber;
                        product.Code = codeData.DisplayName + nextNumber;

                        var isExistCode = context.Products.Any(a => a.Code == product.Code);
                        if (isExistCode)
                        {
                            nextNumber = nextNumber + 1;
                            product.Code = codeData.DisplayName + nextNumber;
                        }

                        product.DateCreated = DateTime.Now;
                        product.DateModified = DateTime.Now;
                        var productData = product.Adapt<Product>();
                        context.Products.Add(productData);
                        context.SaveChanges();

                        var codeBookData = (from cb in context.CodeBooks
                                            join cp in context.CodePrefixes on cb.CodePrefixId equals cp.ID
                                            where cp.DocumentTypeEnum == DocumentTypeEnum.Product
                                            select cb).FirstOrDefault();
                        codeBookData.NextNumber = nextNumber + 1;
                        context.SaveChanges();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool IsProductCodeExist()
        {
            bool response = false;
            using (var context = new markomPosDbContext())
            {
                response = context.CodePrefixes.Any(a => a.DocumentTypeEnum == DocumentTypeEnum.Product);
            }
            return response;
        }

        public void Dispose()
        {
        }
    }
}
