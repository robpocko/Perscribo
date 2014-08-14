using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Street1 must be no longer than 100 characters")]
        public string Street1 { get; set; }

        [MaxLength(100, ErrorMessage = "Street1 must be no longer than 100 characters")]
        public string Street2 { get; set; }

        [MaxLength(100, ErrorMessage = "Street1 must be no longer than 100 characters")]
        public string Street3 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Suburb must be no longer than 50 characters")]
        public string Suburb { get; set; }

        [Required]
        public StateName StateID { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "Postcode must be 4 characters"), MinLength(4, ErrorMessage = "Postcode must be 4 characters")]
        public string Postcode { get; set; }

        public virtual ICollection<Agency> Agencies { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Engagement> Engagements { get; set; }
    }
}
