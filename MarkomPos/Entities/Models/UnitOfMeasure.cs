﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarkomPos.Entities
{
    public class UnitOfMeasure : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Jedinična mjera mora imati puni naziv !")]
        [DisplayName("Jedinična mjera")]        
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Jedinična mjera mora imati kratki naziv (za prikaz u aplikaciji) !")]
        [DisplayName("Jed.mj.")]
        public string DisplayName { get; set; }
    }
}