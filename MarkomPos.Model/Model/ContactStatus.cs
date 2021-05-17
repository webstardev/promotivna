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
    public class ContactStatus : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Status mora imati puni naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Status mora imati kratki naziv !")]
        [DisplayName("Kratki naziv")]
        public string DisplayName { get; set; }

        [DisplayName("Napomena")]
        public string Note { get; set; }
    }
}
