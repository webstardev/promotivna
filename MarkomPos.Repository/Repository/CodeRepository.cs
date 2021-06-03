using MarkomPos.Model.Model;
using MarkomPos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using System.Web.Mvc;

namespace MarkomPos.Repository.Repository
{
    public class CodeRepository : IDisposable
    {
        public List<CodeBookVm> GetAll()
        {
            using (var context = new markomPosDbContext())
            {
                return context.CodeBooks
                    .Include(i => i.CodePrefix)
                    .Adapt<List<CodeBookVm>>().ToList();
            }
        }
        public CodeBookVm GetById(int id)
        {
            using (var context = new markomPosDbContext())
            {
                var codeBookVm = new CodeBookVm();
                var codeBookData = context.CodeBooks
                    .Include(i => i.CodePrefix)
                    .FirstOrDefault(f => f.ID == id)
                    .Adapt<CodeBookVm>();

                if (codeBookData != null)
                {
                    codeBookVm = codeBookData;
                    codeBookVm.CodePrefixes = new SelectList(context.CodePrefixes, "ID", "Name", codeBookData.CodePrefix).ToList();
                }
                else
                {
                    codeBookVm.CodePrefixes = new SelectList(context.CodePrefixes, "ID", "Name").ToList();
                }
                return codeBookVm;
            }
        }
        public bool AddUpdateCodeBook(CodeBookVm codeBook)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (codeBook.ID > 0)
                    {
                        var dbData = context.CodeBooks.Find(codeBook.ID);
                        if (dbData != null)
                        {
                            dbData.ID = codeBook.ID;
                            dbData.CodePrefixId = codeBook.CodePrefixId;
                            dbData.NextNumber = codeBook.NextNumber;
                            dbData.Year = codeBook.Year;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.CodeBooks.Any(f => f.CodePrefixId == codeBook.CodePrefixId);
                        if (!isExist)
                        {
                            codeBook.DateCreated = DateTime.Now;
                            codeBook.DateModified = DateTime.Now;
                            var codeBookData = codeBook.Adapt<CodeBook>();
                            context.CodeBooks.Add(codeBookData);
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
        public bool AddUpdateCodePrefix(CodePrefix codePrefix)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (codePrefix.ID > 0)
                    {
                        var dbData = context.CodePrefixes.Find(codePrefix.ID);
                        if (dbData != null)
                        {
                            dbData.ID = codePrefix.ID;
                            dbData.Name = codePrefix.Name;
                            dbData.DisplayName = codePrefix.DisplayName;
                            dbData.DocumentTypeEnum = codePrefix.DocumentTypeEnum;
                            dbData.StartNumber = codePrefix.StartNumber;
                            dbData.NewStartNumberEachYear = codePrefix.NewStartNumberEachYear;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.CodePrefixes.Any(f => f.Name == codePrefix.Name);
                        if (!isExist)
                        {
                            codePrefix.DateCreated = DateTime.Now;
                            codePrefix.DateModified = DateTime.Now;
                            context.CodePrefixes.Add(codePrefix);
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
        public void Dispose()
        {
        }
    }
}
