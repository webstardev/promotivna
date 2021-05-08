using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarkomPos.Entities
{
    public interface IEntity
    {
        int ID { get; set; }
        DateTime DateCreated { get; set; }
        string CreatedBy { get; set; }

        DateTime? DateModified { get; set; }
        string ModifiedBy { get; set; }
    }
    public class DbEntity : IEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
