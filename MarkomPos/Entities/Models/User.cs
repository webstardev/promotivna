using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    public class User : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Korisnik mora imati ime !")]
        [DisplayName("Ime")]
        public string Name { get; set; } // ime *

        [DisplayName("Prezime")]
        public string Surname { get; set; } // prezime

        [Required(AllowEmptyStrings = false, ErrorMessage = "Korisnik mora imati adresu !")]
        [DisplayName("Adresa")]
        public string Address { get; set; } // adresa (ulica) *

        [Required(AllowEmptyStrings = false, ErrorMessage = "Korisnik mora imati radno mjesto !")]
        [DisplayName("Radno mjesto")]
        public string JobDescription { get; set; } // radno mjesto *

        [DisplayName("Aktivan")]
        public bool Active { get; set; } // aktivan (prikaz u listi)

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Mobitel")]
        public string MobilePhone { get; set; }

        [DisplayName("Napomena")]
        public string Note { get; set; }

        [DisplayName("Napomena 2")]
        public string Note2 { get; set; }

        [DisplayName("Boja")]
        public string ColorHex { get; set; }

        [DisplayName("Korisničko ime")]
        public string Username { get; set; } // korisničko ime
        public int PasswordSalt { get; set; } // salt
        public string PasswordHash { get; set; } // passHash
    }
}
