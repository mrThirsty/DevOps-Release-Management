using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("AuditHeader")]
    public class AuditHeader : Entity
    {
        public AuditHeader()
        {
        }

        [Required]
        public string User { get;set; }

        [Required]
        public string UserId { get;set; }

        [Required]
        public string RecordType { get;set; }

        [Required]
        public string ChangeType { get;set; }

        [Required]
        public DateTime Timestamp { get;set; }

        [Required]
        public int RecordId { get;set; }

        public virtual ICollection<AuditItem> AuditItems { get;set; }

        [NotMapped]
        public Entity RegardingRecord { get;set; }
    }
}
