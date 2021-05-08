using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarkomPos.Entities
{
    public class Warehouse : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Skladište mora imati naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; }
    }
}