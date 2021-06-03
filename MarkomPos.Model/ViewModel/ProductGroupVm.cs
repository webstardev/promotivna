using MarkomPos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkomPos.Model.ViewModel
{
    public class ProductGroupVm : BaseVm
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int? ParrentGroupId { get; set; }
        public ProductGroupVm ParrentGroup { get; set; }
        public List<SelectListItem> productGroupVms { get; set; }
        public ProductGroupTypeEnum productGroupType { get; set; }
    }
}
