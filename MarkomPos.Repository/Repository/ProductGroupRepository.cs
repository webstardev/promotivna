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
    public class ProductGroupRepository : IDisposable
    {
        public List<ProductGroupVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.ProductGroups.Include(p => p.ParrentGroup).OrderBy(o => o.ParrentGroupId).Adapt<List<ProductGroupVm>>().ToList();
            }
        }
        public ProductGroupVm GetById(int productGroupId)
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupVm = new ProductGroupVm();
                productGroupVm = context.ProductGroups.Include(p => p.ParrentGroup).FirstOrDefault(f => f.ID == productGroupId).Adapt<ProductGroupVm>();
                productGroupVm.productGroupVms = new SelectList(context.ProductGroups, "ID", "Name", productGroupVm.ParrentGroupId).ToList();

                return productGroupVm;
            }
        }
        public ProductGroupVm GetEditRecord(int productGroupId)
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupVm = new ProductGroupVm();
                productGroupVm = context.ProductGroups.Include(p => p.ParrentGroup).FirstOrDefault(f => f.ID == productGroupId).Adapt<ProductGroupVm>();
                productGroupVm.productGroupVms = new SelectList(context.ProductGroups.Where(w => w.ID != productGroupId), "ID", "Name", productGroupVm.ParrentGroupId).ToList();

                return productGroupVm;
            }
        }
        public List<SelectListItem> GetSelectListItems()
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupListItem = new SelectList(context.ProductGroups, "ID", "Name");
                return productGroupListItem.ToList();
            }
        }
        public bool AddUpdateProductGroups(ProductGroupVm productGroup)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (productGroup.ID > 0)
                    {
                        var dbData = context.ProductGroups.Find(productGroup.ID);
                        if (dbData != null)
                        {
                            dbData.ID = productGroup.ID;
                            dbData.Name = productGroup.Name;
                            dbData.DisplayName = productGroup.DisplayName;
                            dbData.ParrentGroupId = productGroup.ParrentGroupId;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.ProductGroups.Any(f => f.Name == productGroup.Name);
                        if (!isExist)
                        {
                            productGroup.DateCreated = DateTime.Now;
                            productGroup.DateModified = DateTime.Now;
                            var productGroupData = productGroup.Adapt<ProductGroup>();
                            context.ProductGroups.Add(productGroupData);
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

        public bool checkIsMainGroup(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var groupData = context.ProductGroups.Find(id);
                if (groupData != null && groupData.ParrentGroupId == null)
                {
                    return context.ProductGroups.Any(a => a.ParrentGroupId == id);
                }
                return false;
                //return context.ProductGroups.Any(a => a.ParrentGroupId == id);
            }
        }
        public bool checkIsLastLevel(int parentId)
        {
            using (var context = new markomPosDbContext())
            {
                var subGroupId = context.ProductGroups.Find(parentId).ParrentGroupId;
                if (subGroupId != null)
                {
                    var parentGroupId = context.ProductGroups.Find(subGroupId).ParrentGroupId;
                    if (parentGroupId != null)
                        return false;
                }
            }
            return true;
        }
        public void Dispose()
        {
        }
    }
}
