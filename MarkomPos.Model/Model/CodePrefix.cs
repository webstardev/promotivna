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
    public class CodePrefix : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Prefiks mora imati puni naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Prefix mora imati kratki naziv (za dodjelu) !")]
        [DisplayName("Kratki naziv")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Prefix se mora odnositi na vrstu dokumenta !")]
        [DisplayName("Vrsta dokumenta")]
        public int DocumentTypeEnum { get; set; }

        [Required(ErrorMessage = "Prefix mora imati početni broj!")]
        [DisplayName("Početni broj dokumenta")]
        public int StartNumber { get; set; }

        public bool NewStartNumberEachYear { get; set; }
    }
}
