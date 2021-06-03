using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkomPos.Model.ViewModel
{
    public class CodeBookVm : BaseVm
    {
        public int CodePrefixId { get; set; } // povezani prefix
        public CodePrefix CodePrefix { get; set; }

        [DisplayName("Sljedeći broj")]
        public int NextNumber { get; set; }
        public int Year { get; set; }
        public List<SelectListItem> CodePrefixes { get; set; }
    }
}
