using MarkomPos.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.Model
{
    public class ContactInfo : DbEntity
    {
        public int UserId { get; set; } // putnik
        public User User { get; set; }

        public int ContactId { get; set; } // kontakt
        public Contact Contact { get; set; }

        [DisplayName("Vrijedi od")]
        public DateTime DateTimeValidFrom { get; set; } // datum od

        [DisplayName("Vrijedi do")]
        public DateTime? DateTimeValidTo { get; set; } // datum do

        public int ContactStatusId { get; set; } // kontakt
        public ContactStatus ContactStatus { get; set; }

        public string Note { get; set; }
    }
}
