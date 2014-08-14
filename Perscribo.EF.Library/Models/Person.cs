using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perscribo.EF.Library.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "First Name must be no longer than 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Last Name must be no longer than 30 characters")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(250, ErrorMessage = "Email must be no longer than 250 characters")]
        public string Email { get; set; }

        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
