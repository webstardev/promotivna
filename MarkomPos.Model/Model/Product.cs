using MarkomPos.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.Model
{
    public class Product : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Artikl mora imati naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; } // naziv
        [Required(AllowEmptyStrings = false, ErrorMessage = "Artikl mora imati naziv za prikaz !")]
        [DisplayName("Naziv za prikaz")]
        public string DisplayName { get; set; }
        public string Note { get; set; } // napomena
        public string Note2 { get; set; } // napomena 2
        [Required(AllowEmptyStrings = false, ErrorMessage = "Artikl mora imati Šifra !")]
        [DisplayName("Šifra")]
        public string Code { get; set; } // šifra
        public int? UnitOfMeasureId { get; set; } // mjera
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int? ProductGroupId { get; set; } // prodajna grupa
        public ProductGroup ProductGroup { get; set; }
        public IEnumerable<ProductInfo> ProductInfos { get; set; }
    }
}
