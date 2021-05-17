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
    public class Contact : DbEntity
    {
        [DisplayName("Šifra")]
        public int Code { get; set; } // šifra

        [Required(AllowEmptyStrings = false, ErrorMessage = "Kontakt mora imati naziv !")]
        [DisplayName("Naziv")]
        public string Name { get; set; } // naziv *

        [Required(AllowEmptyStrings = false, ErrorMessage = "Kontakt mora imati OIB !")]
        [DisplayName("OIB")]
        public string OIB { get; set; } // oib/matični broj *

        [Required(AllowEmptyStrings = false, ErrorMessage = "Kontakt mora imati adresu !")]
        [DisplayName("Adresa")]
        public string Address { get; set; } // adresa (ulica) *

        [Required(AllowEmptyStrings = false, ErrorMessage = "Kontakt mora imati mjesto !")]
        [DisplayName("Mjesto")]
        public string Country { get; set; } // mjesto *

        [DisplayName("Poštanski broj")]
        public string CountryCode { get; set; } // poštanski broj

        [DisplayName("Telefon")]
        public string Phone { get; set; }

        [DisplayName("Telefon 2")]
        public string Phone2 { get; set; }

        [DisplayName("Fax")]
        public string Fax { get; set; }

        [DisplayName("Mobitel")]
        public string MobilePhone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Web")]
        public string WebAddress { get; set; }

        [DisplayName("Kontakt osoba")]
        public string Person { get; set; } // Osoba (200 znakova)

        [DisplayName("Broj računa")]
        public string AccountNumber { get; set; } // žiro račun

        [DisplayName("Kreditni limit")]
        public decimal CreditLimit { get; set; } // limit

        [DisplayName("Rabat (%)")]
        public decimal Discount { get; set; } // rabat %

        [DisplayName("Garancija plaćanja")]
        public bool HasWarranty { get; set; } // garancija plaćanja

        [DisplayName("Napomena garancije")]
        public string WarrantyNote { get; set; } // napomena garancije

        [DisplayName("Napomena")]
        public string Note { get; set; } // napomena

        [DisplayName("Napomena 2")]
        public string Note2 { get; set; } // napomena

        [DisplayName("Kupac")]
        public bool IsBuyer { get; set; } // kupac

        [DisplayName("Dobavljač")]
        public bool IsSupplier { get; set; } // dobavljač

    }
}
