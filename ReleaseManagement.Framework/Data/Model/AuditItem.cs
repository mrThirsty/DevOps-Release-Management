using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("AuditItem")]
    public class AuditItem : Entity
    {
        public AuditItem()
        {
        }

        [Required]
        public string Field { get;set; }

        [Required]
        public string OldValue { get;set; }

        [Required]
        public string NewValue { get;set; }

        [Required]
        [ForeignKey(nameof(AuditHeader))]
        public int AuditHeaderId {  get;set; }

        public virtual AuditHeader AuditHeader { get;set; }
    }
}
