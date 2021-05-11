using MarkomPos.IRepository;
using MarkomPos.IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarkomPosContext _context;
        public IProductRepository Product { get; }
        public IUnitOfMeasureRepository UnitOfMeasure { get; }
        public UnitOfWork(MarkomPosContext markomPosContext,
            IProductRepository productRepository, IUnitOfMeasureRepository unitOfMeasure)
        {
            this._context = markomPosContext;
            this.Product = productRepository;
            this.UnitOfMeasure = unitOfMeasure;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
