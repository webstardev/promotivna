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
    public class OrderItem : DbEntity
    {
        public int OrderId { get; set; } // povezana narudžba
        public Offer Offer { get; set; }

        public decimal Ordinal { get; set; } // poredak u prikazu

        public int ProductId { get; set; } // povezani artikl
        public Product Product { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Naziv artikla mora biti popunjen !")]
        [DisplayName("Naziv artikla")]
        public string ProductName { get; set; }

        [DisplayName("Kratki opis")]
        public string ShortDescription { get; set; }

        public int UnitOfMeasureId { get; set; } // mjera
        public UnitOfMeasure UnitOfMeasure { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Jedinica mora biti popunjena !")]
        [DisplayName("Jedinična mjera")]
        public string UnitOfMeasureName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Količina mora biti popunjena !")]
        [DisplayName("Količina")]
        public decimal Quantity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cijena mora biti popunjena !")]
        [DisplayName("Cijena")]
        public decimal Price { get; set; }

        [DisplayName("Rabat")]
        public decimal Discount { get; set; }

        [DisplayName("Porez")]
        public decimal Porez { get; set; }
    }
}
