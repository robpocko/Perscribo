using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Company Name must be no longer than 50 characters")]
        public string Name { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}
