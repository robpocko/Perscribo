using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Position Title must be no longer than 150 characters")]
        [Display(Name="Position")]
        public string PositionTitle { get; set; }

        [Required]
        [Display(Name="Application Sent")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d MMM yyyy hh:mm tt}")]
        public DateTime AppliedForOn { get; set; }

        [MaxLength(20, ErrorMessage = "Reference Number must be no longer than 20 characters")]
        [Display(Name="Ref Nbr")]
        public string ReferenceNumber { get; set; }

        [Required]
        [Display(Name="Position Type")]
        public PositionType PositionType { get; set; }

        public int? LowSalaryRange { get; set; }

        public int? HighSalaryRange { get; set; }

        public SalaryType? SalaryType { get; set; }

        [Required]
        public RoleStatus Status { get; set; }

        [Display(Name="Interviewed by Agent")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d MMM yyyy}")]
        public DateTime? AgentInterview { get; set; }

       // public int AgencyID { get; set; }

        //[Required]
        public virtual Agency Agency { get; set; }
        //[Required]
        public virtual Consultant Consultant { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Interview> CompanyInterviews { get; set; }
    }
}
