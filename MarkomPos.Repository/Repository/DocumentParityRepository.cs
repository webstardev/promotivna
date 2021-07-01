using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class DocumentParityRepository : IDisposable
    {
        public bool AddUpdateDocumentParty(DocumentParity documentParity)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (documentParity.ID > 0)
                    {
                        var dbData = context.DocumentParities.Find(documentParity.ID);
                        if (dbData != null)
                        {
                            dbData.ID = documentParity.ID;
                            dbData.Name = documentParity.Name;
                            dbData.DisplayName = documentParity.DisplayName;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.DocumentParities.Any(f => f.Name == documentParity.Name);
                        if (!isExist)
                        {
                            documentParity.DateCreated = DateTime.Now;
                            documentParity.DateModified = DateTime.Now;
                            context.DocumentParities.Add(documentParity);
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
        public bool IsExist(int id, string documentParity)
        {
            bool response = false;
            using (var context = new markomPosDbContext())
            {
                if (id > 0)
                    response = context.DocumentParities.Any(f => f.Name == documentParity && f.ID != id);
                else
                    response = context.DocumentParities.Any(f => f.Name == documentParity);
            }
            return response;
        }

        public void Dispose()
        {
        }
    }
}
