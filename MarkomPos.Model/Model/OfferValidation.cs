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
    public class OfferValidation : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Jedinica mora biti popunjena !")]
        [DisplayName("Jedinična mjera")]
        public string UserDiplayName { get; set; }

        public int UserId { get; set; } // povezani korisnik
        public User User { get; set; }

        public int OfferId { get; set; } // povezana ponuda
        public Offer Offer { get; set; }

        public string Note { get; set; }
    }
}
