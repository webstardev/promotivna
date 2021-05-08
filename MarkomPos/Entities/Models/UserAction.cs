using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    public class UserAction : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Akcija mora imati puni naziv !")]
        [DisplayName("Naziv akcije")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Akcija se mora odnositi na modul !")]
        [DisplayName("Modul akcije")]
        public int UserActionModulEnum { get; set; }

        [Required(ErrorMessage = "Akcija mora imati vrstu !")]
        [DisplayName("Vrsta akcije")]
        public int UserActionTypeEnum { get; set; }

    }
}
