using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkomPos.Model.ViewModel
{
    public class ProductVm : BaseVm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Artikl mora imati naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; } // naziv
        [Required(AllowEmptyStrings = false, ErrorMessage = "Artikl mora imati naziv za prikaz !")]
        [DisplayName("Naziv za prikaz")]
        public string DisplayName { get; set; }
        public string Note { get; set; } // napomena
        public string Note2 { get; set; } // napomena 2
        [DisplayName("Šifra")]
        public string Code { get; set; } // šifra
        public int? UnitOfMeasureId { get; set; } // mjera
        public int? ProductGroupId { get; set; } // prodajna grupa
        public List<SelectListItem> MainProductGroupVms { get; set; }
        public List<SelectListItem> SubProductGroupVms { get; set; }
        public List<SelectListItem> ProductGroups { get; set; }
        public List<SelectListItem> UnitOfMeasures { get; set; }
        public ProductGroupVm ProductGroup { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
