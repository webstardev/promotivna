using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.ViewModel
{
    public class ProductGroupsListVm
    {
        public List<ProductGroupVm> MainGroup { get; set; }
        public List<ProductGroupVm> SubGroup { get; set; }
        public List<ProductGroupVm> BasicGroup { get; set; }
    }
}
