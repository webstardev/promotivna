using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkomPos.Model.ViewModel
{
    public class OfferValidationVm : BaseVm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Jedinica mora biti popunjena !")]
        [DisplayName("Jedinična mjera")]
        public string UserDiplayName { get; set; }

        public int UserId { get; set; } // povezani korisnik
        public User User { get; set; }

        public int OfferId { get; set; } // povezana ponuda
        public OfferVm Offer { get; set; }

        public string Note { get; set; }

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Offers { get; set; }
    }
}
