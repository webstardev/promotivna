using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.ViewModel
{
    public class OfferIndexVm
    {
        public List<OfferVm> OfferList { get; set; }
        public List<OfferValidationVm> OfferValidationList { get; set; }
    }
}
