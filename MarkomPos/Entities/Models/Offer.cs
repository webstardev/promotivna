﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarkomPos.Entities
{
    public class Offer : DbEntity
    {
        [DisplayName("Datum ponude")]
        public DateTime OfferDate { get; set; }

        [DisplayName("Broj")]
        public int OfferNumber { get; set; } 

        [DisplayName("Valjanost ponude")]
        public DateTime ExpirationDate { get; set; }

        public int DeliveryTermId { get; set; } // rok isporuke
        public DeliveryTerm DeliveryTerm { get; set; }

        public int DocumentParityId { get; set; } // paritet
        public DocumentParity DocumentParity { get; set; }

        public int PaymentMethodId { get; set; } // način plaćanja
        public PaymentMethod PaymentMethod { get; set; }

        public int ResponsibleUserId { get; set; } // odgovorna osoba
        public User ResponsibleUser { get; set; }

        public int? ContactId { get; set; } // povezani kupac
        public Contact Contact { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Naziv kontakta mora biti popunjen !")]
        [DisplayName("Naziv")]
        public string ContactName { get; set; }

        [DisplayName("Adresa")]
        public string ContactAddress { get; set; }

        [DisplayName("Mjesto")]
        public string ContactCountry { get; set; }

        public bool PrintNote { get; set; }

        [DisplayName("Napomena")]
        public string Note { get; set; } // napomena

        [DisplayName("Napomena 2")]
        public string Note2 { get; set; } // napomena 2
    }


}