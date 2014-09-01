using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perscribo.EF.Library.Models
{
    public class Engagement : IAddressedEntity
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Commenced")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d MMM yyyy}")]
        public DateTime Commencement { get; set; }

        [Display(Name="Completed")]
        public DateTime? Completion { get; set; }

        [NotMapped]
        public bool IsOpen { get { return !Completion.HasValue; } }

        //[Required]
        public virtual Agency Agency { get; set; }
        //[Required]
        public virtual Consultant Consultant { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        [Required]
        public virtual Person CompanyContact { get; set; }
        [Required]
        public virtual Address Address { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
