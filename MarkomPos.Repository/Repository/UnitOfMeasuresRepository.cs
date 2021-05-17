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
                        var dbData = context.UnitOfMeasures.Find(unitOfMeasure.ID);
                        if (dbData != null)
                        {
                            dbData.ID = unitOfMeasure.ID;
                            dbData.Name = unitOfMeasure.Name;
                            dbData.DisplayName = unitOfMeasure.DisplayName;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        unitOfMeasure.DateCreated = DateTime.Now;
                        unitOfMeasure.DateModified = DateTime.Now;
                        context.UnitOfMeasures.Add(unitOfMeasure);
                    }
                    
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
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
                    if(dbData != null)
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

        public void Dispose()
        {
        }
    }
}
