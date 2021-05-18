using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class ProductGroupRepository : IDisposable
    {
        public bool AddUpdateProductGroups(ProductGroup productGroup)
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
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        productGroup.DateCreated = DateTime.Now;
                        productGroup.DateModified = DateTime.Now;
                        context.ProductGroups.Add(productGroup);
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
                //var groupData = context.ProductGroups.Find(id);
                //if (groupData != null && groupData.ParrentGroupId == null)
                //{
                //    return context.ProductGroups.Any(a => a.ParrentGroupId == id);
                //}
                //return false;
                return context.ProductGroups.Any(a => a.ParrentGroupId == id);
            }
        }
        public bool checkIsLastLevel(int parentId)
        {
            using (var context = new markomPosDbContext())
            {
                var subGroupId = context.ProductGroups.Find(parentId).ParrentGroupId;
                if(subGroupId != null)
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
