using MarkomPos.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Model.Model
{
    public class UserRoleMapping : DbEntity
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Korisnik mora imati ime !")]
        [DisplayName("Ime")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Skladište mora imati naziv !")]
        [DisplayName("Naziv akcije")]
        public int RolesId { get; set; }
        public Roles Roles { get; set; }
    }
}
