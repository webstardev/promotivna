using MarkomPos.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.Model
{
    public class CodeBook : DbEntity
    {
        public int CodePrefixId { get; set; } // povezani prefix
        public CodePrefix CodePrefix { get; set; }

        [DisplayName("Sljedeći broj")]
        public int NextNumber { get; set; }
        public int Year { get; set; }
    }
}
