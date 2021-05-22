using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class WarehouseRepository : IDisposable
    {
        public bool AddUpdateWareHouse(Warehouse warehouse)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (warehouse.ID > 0)
                    {
                        var dbData = context.warehouses.Find(warehouse.ID);
                        if (dbData != null)
                        {
                            dbData.ID = warehouse.ID;
                            dbData.Name = warehouse.Name;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        warehouse.DateCreated = DateTime.Now;
                        warehouse.DateModified = DateTime.Now;
                        context.warehouses.Add(warehouse);
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
        public bool AddUpdateWareHouseItem(WarehouseItem warehouseItem)
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
                        context.warehouseItems.Add(warehouseItem);
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
