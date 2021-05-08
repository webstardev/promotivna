using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MarkomPos.Entities
{
    public class ProductGroup : DbEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int? ParrentGroupId { get; set; }
        public ProductGroup ParrentGroup { get; set; }
    }
}