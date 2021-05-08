using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    // ŠIFRARNICI

    public class CodeBook : DbEntity
    {
        public int CodePrefixId { get; set; } // povezani prefix
        public CodePrefix CodePrefix { get; set; }

        [DisplayName("Sljedeći broj")]
        public int NextNumber { get; set; }
        public int Year { get; set; }
    }
}
