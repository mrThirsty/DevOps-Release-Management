using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("Log")]
    public class Log : Entity
    {
        public Log()
        {
        }

        [Required]
        public string Level { get;set; }

        [Required]
        public string Message { get;set; }

        public string Exception { get;set; }

        [Required]
        public string Category { get;set; }

        [Required]
        public DateTime Timestamp { get;set; }

        [Required]
        public string User { get;set; }

        [Required]
        public string UserId { get;set; }
    }
}
