using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Agency
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Agency Name must be no longer than 50 characters")]
        [Display(Name="Agency")]
        public string Name { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "Phone Number must be no longer than 12 characters")]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Consultant> Consultants { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}
