using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Task
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Task Title must be no longer than 100 characters")]
        public string Title { get; set; }

        [Required]
        public virtual Project Project { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
