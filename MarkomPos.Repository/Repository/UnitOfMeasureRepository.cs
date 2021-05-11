using MarkomPos.IRepository.IRepository;
using MarkomPoss.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class UnitOfMeasureRepository : RepositoryBase<UnitOfMeasure>, IUnitOfMeasureRepository
    {
        public UnitOfMeasureRepository(MarkomPosContext context) : base(context)
        {

        }
    }
}
