using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkomPos.Model.ViewModel
{
    public class WarehouseItemVm : BaseVm
    {
        public int WarehouseId { get; set; } // povezano skladište
        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; } // povezani artikl
        public ProductVm Product { get; set; }

        public decimal CurrentQuantity { get; set; } // trenutna količina

        public decimal ReservedQuantity { get; set; } // rezervirana količina

        public List<SelectListItem> Warehouses { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
