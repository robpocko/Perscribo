using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Interview
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime InterviewDate { get; set; }

        [Required]
        public virtual Role Role { get; set; }
        public virtual ICollection<Person> Interviewers { get; set; }
    }
}
