using MarkomPoss.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPoss.Model.Model
{
    public class ProductGroup : DbEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int? ParrentGroupId { get; set; }
        public ProductGroup ParrentGroup { get; set; }
    }
}
