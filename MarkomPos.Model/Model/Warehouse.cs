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
    public class Warehouse : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Skladište mora imati naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; }
    }
}
