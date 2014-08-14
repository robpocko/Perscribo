using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Project
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Project Title must be no longer than 100 characters")]
        public string Title { get; set; }

        [Required]
        public virtual Person ProjectManager { get; set; }
        [Required]
        public virtual Engagement Engagement { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
