using MarkomPos.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.Model
{
    public class WarehouseItem : DbEntity
    {
        public int WarehouseId { get; set; } // povezano skladište
        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; } // povezani artikl
        public Product Product { get; set; }

        public decimal CurrentQuantity { get; set; } // trenutna količina

        public decimal ReservedQuantity { get; set; } // rezervirana količina
    }
}
