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
    public class ProductInfo : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Jedinična mjera mora imati puni naziv !")]
        [DisplayName("Naziv definicije")]
        public string DisplayName { get; set; } // naziv
        public int ProductId { get; set; }
        public Product Product { get; set; } // artikl
        public int ContactId { get; set; }
        public Contact Contact { get; set; } // dobavljač

        [DisplayName("Vanjska šifra")]
        public string ExternalCode { get; set; } // naziv

        [DisplayName("Ulazna cijena")]
        public decimal InputPrice { get; set; } // ulazna cijena
        [DisplayName("Marža")]
        public decimal MarkUp { get; set; } // marža
        [DisplayName("Izlazna cijena")]
        public decimal OutputPrice { get; set; } // izlazna cijena (prodajna)
        [DisplayName("Favorit")]
        public bool Default { get; set; } // preferiran odabir
        [DisplayName("Aktivna definicija")]
        public bool Active { get; set; } // aktivna definicija
    }
}
