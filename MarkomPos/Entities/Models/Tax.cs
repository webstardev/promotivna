using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    // POREZ
    public class Tax : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Porez mora imati puni naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Porez mora imati kratki naziv (za prikaz u aplikaciji) !")]
        [DisplayName("Kratki naziv")]
        public string DisplayName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("Vrijednost (%)")]
        public int TaxValue { get; set; }
    }
}
