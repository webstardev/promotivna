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
    public class ProductGroupRepository : IDisposable
    {
        public ProductGroupsListVm GetAll()
        {
            var ProductGroupListVm = new ProductGroupsListVm();
            using (var context = new markomPosDbContext())
            {
                ProductGroupListVm.MainGroup = context.ProductGroups.Include(i => i.ParrentGroup).Where(w => w.productGroupType == ProductGroupTypeEnum.Main).Adapt<List<ProductGroupVm>>().ToList();
                ProductGroupListVm.SubGroup = context.ProductGroups.Include(i => i.ParrentGroup).Where(w => w.productGroupType == ProductGroupTypeEnum.Sub).Adapt<List<ProductGroupVm>>().ToList();
                ProductGroupListVm.BasicGroup = context.ProductGroups.Include(i => i.ParrentGroup).Where(w => w.productGroupType == ProductGroupTypeEnum.Basic).Adapt<List<ProductGroupVm>>().ToList();

                return ProductGroupListVm;
            }
        }
        public ProductGroupVm GetById(int productGroupId)
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupVm = new ProductGroupVm();
                productGroupVm = context.ProductGroups.Include(p => p.ParrentGroup).FirstOrDefault(f => f.ID == productGroupId).Adapt<ProductGroupVm>();
                productGroupVm.productGroupVms = new SelectList(context.ProductGroups, "ID", "Name", productGroupVm.ParrentGroupId).ToList();
                productGroupVm.MainProductGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Main), "ID", "Name").ToList();

                return productGroupVm;
            }
        }
        public ProductGroupVm GetEditRecord(int productGroupId)
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupVm = new ProductGroupVm();
                productGroupVm = context.ProductGroups.Include(p => p.ParrentGroup).FirstOrDefault(f => f.ID == productGroupId).Adapt<ProductGroupVm>();


                if (productGroupVm.productGroupType == ProductGroupTypeEnum.Sub)
                    productGroupVm.productGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Main), "ID", "Name").ToList();
                else if (productGroupVm.productGroupType == ProductGroupTypeEnum.Basic)
                    productGroupVm.productGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Sub), "ID", "Name").ToList();
                else
                    productGroupVm.productGroupVms = new List<SelectListItem>();

                productGroupVm.MainProductGroupVms = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Main), "ID", "Name").ToList();

                return productGroupVm;
            }
        }
        public List<SelectListItem> GetSelectListItems(ProductGroupTypeEnum productGroupType)
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupListItem = new List<SelectListItem>();
                if (productGroupType == ProductGroupTypeEnum.Sub)
                    productGroupListItem = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Main), "ID", "Name").ToList();
                else if (productGroupType == ProductGroupTypeEnum.Basic)
                    productGroupListItem = new SelectList(context.ProductGroups.Where(w => w.productGroupType == ProductGroupTypeEnum.Sub), "ID", "Name").ToList();
                return productGroupListItem;
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
                            dbData.productGroupType = productGroup.productGroupType;
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
        public List<ProductGroupVm> GetAllChildGroup(int parentGroupId)
        {
            using (var context = new markomPosDbContext())
            {
                var productGroupVms = new List<ProductGroupVm>();
                productGroupVms = context.ProductGroups.Where(w => w.ParrentGroupId == parentGroupId).Adapt<List<ProductGroupVm>>().ToList();
                return productGroupVms;
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
