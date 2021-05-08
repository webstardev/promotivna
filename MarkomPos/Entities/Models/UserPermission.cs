using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    public class UserPermission : DbEntity
    {
        public int UserId { get; set; } // korisnik
        public User User { get; set; }

        public int UserActionId { get; set; } // akcija
        public UserAction UserAction { get; set; }
    }
}
