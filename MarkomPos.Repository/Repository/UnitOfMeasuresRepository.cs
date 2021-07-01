using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class UnitOfMeasuresRepository : IDisposable
    {
        public UnitOfMeasuresRepository()
        {

        }
        public bool AddUnitMeasure(UnitOfMeasure unitOfMeasure)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (unitOfMeasure.ID > 0)
                    {
                        var isExist = context.UnitOfMeasures.Any(f => f.Name == unitOfMeasure.Name && f.ID != unitOfMeasure.ID);
                        var dbData = context.UnitOfMeasures.Find(unitOfMeasure.ID);
                        if (dbData != null && !isExist)
                        {
                            dbData.ID = unitOfMeasure.ID;
                            dbData.Name = unitOfMeasure.Name;
                            dbData.DisplayName = unitOfMeasure.DisplayName;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.UnitOfMeasures.Any(f => f.Name == unitOfMeasure.Name);
                        if (!isExist)
                        {
                            unitOfMeasure.DateCreated = DateTime.Now;
                            unitOfMeasure.DateModified = DateTime.Now;
                            context.UnitOfMeasures.Add(unitOfMeasure);
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

        public bool UpdateUnitMeasure(UnitOfMeasure unitOfMeasure)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    var dbData = context.UnitOfMeasures.Find(unitOfMeasure.ID);
                    if (dbData != null)
                    {
                        dbData.ID = unitOfMeasure.ID;
                        dbData.Name = unitOfMeasure.Name;
                        dbData.DisplayName = unitOfMeasure.DisplayName;
                        dbData.DateModified = DateTime.Now;
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

        public bool IsExist(int id, string name)
        {
            bool response = false;
            using (var context = new markomPosDbContext())
            {
                if (id > 0)
                    response = context.UnitOfMeasures.Any(f => f.Name == name && f.ID != id);
                else
                    response = context.UnitOfMeasures.Any(f => f.Name == name);
            }
            return response;
        }

        public void Dispose()
        {
        }
    }
}
