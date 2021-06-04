using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MarkomPos.Model.ViewModel;
using Mapster;
using System.Web.Mvc;
using MarkomPos.Model.Model;

namespace MarkomPos.Repository.Repository
{
    public class WarehouseItemRepository : IDisposable
    {
        public List<WarehouseItemVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.warehouseItems.Include(p => p.Warehouse).Include(i => i.Product).Adapt<List<WarehouseItemVm>>().ToList();
            }
        }
        public WarehouseItemVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var warehouseItemVm = new WarehouseItemVm();
                var warehouseItemData = context.warehouseItems.Include(p => p.Warehouse).Include(i => i.Product).FirstOrDefault(f => f.ID == id).Adapt<WarehouseItemVm>();
                if (warehouseItemData != null)
                {
                    warehouseItemVm = warehouseItemData;
                    warehouseItemVm.Warehouses = new SelectList(context.warehouses, "ID", "Name", warehouseItemData.WarehouseId).ToList();
                    warehouseItemVm.Products = new SelectList(context.Products, "ID", "Name", warehouseItemData.ProductId).ToList();
                }
                else
                {
                    warehouseItemVm.Warehouses = new SelectList(context.warehouses, "ID", "Name").ToList();
                    warehouseItemVm.Products = new SelectList(context.Products, "ID", "Name").ToList();
                }
                return warehouseItemVm;
            }
        }
        public bool AddUpdateWareHouseItem(WarehouseItemVm warehouseItem)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (warehouseItem.ID > 0)
                    {
                        var dbData = context.warehouseItems.Find(warehouseItem.ID);
                        if (dbData != null)
                        {
                            dbData.ID = warehouseItem.ID;
                            dbData.WarehouseId = warehouseItem.WarehouseId;
                            dbData.ProductId = warehouseItem.ProductId;
                            dbData.CurrentQuantity = warehouseItem.CurrentQuantity;
                            dbData.ReservedQuantity = warehouseItem.ReservedQuantity;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        warehouseItem.DateCreated = DateTime.Now;
                        warehouseItem.DateModified = DateTime.Now;
                        var warehouseItemData = warehouseItem.Adapt<WarehouseItem>();
                        context.warehouseItems.Add(warehouseItemData);
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
