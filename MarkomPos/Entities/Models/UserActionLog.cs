using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    public class UserActionLog : DbEntity
    {
        [DisplayName("Korisnik")]
        public string UserDisplayName { get; set; }
        public int? UserId { get; set; } // korisnik
        public User User { get; set; }

        [DisplayName("Akcija")]
        public string UserActionName { get; set; }
        public int? UserPermissionId { get; set; } // pravo korisnika (akcija)
        public UserPermission UserPermission { get; set; }
    }
}
