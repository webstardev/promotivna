using System;
using System.ComponentModel.DataAnnotations;

namespace MarkomPos.Model.BaseModel
{
    public class DbEntity
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
