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
    public class UserRoleMappingVm : BaseVm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Korisnik mora imati ime !")]
        [DisplayName("Ime")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Skladište mora imati naziv !")]
        [DisplayName("Naziv akcije")]
        public int RolesId { get; set; }
        public Roles Roles { get; set; }

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> UserRoles { get; set; }
    }
}
