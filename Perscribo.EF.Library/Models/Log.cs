using System;
using System.ComponentModel.DataAnnotations;

namespace Perscribo.EF.Library.Models
{
    public class Log
    {
        public int ID { get; set; }

        [Required]
        public DateTime LogDate { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Description must be no longer than 100 characters")]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        public virtual Task Task { get; set; }
    }
}
