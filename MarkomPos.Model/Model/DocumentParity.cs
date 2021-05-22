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
    public class DocumentParity : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Paritet mora imati puni naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Paritet mora imati kratki naziv (za prikaz u aplikaciji) !")]
        [DisplayName("Kratki naziv")]
        public string DisplayName { get; set; }
    }
}
