using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perscribo.EF.Library.Models
{
    public class Consultant
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "First Name must be no longer than 30 characters")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Last Name must be no longer than 30 characters")]
        [Display(Name="Surname")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [MaxLength(12)]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(250, ErrorMessage = "Email must be no longer than 250 characters")]
        public string Email { get; set; }

        public int AgencyID { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}
