using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("ComponentApproval")]
    public class ComponentApproval : Entity
    {
        public ComponentApproval() : base()
        {
            Approved = false;
        }

        [ForeignKey(nameof(Release))]
        [Required]
        [Display(Name = "Release")]
        public int ReleaseId { get; set; }

        [ForeignKey(nameof(Component))]
        [Required]
        [Display(Name = "Component")]
        public int ComponentId { get; set; }

        [Display(Name = "Date of Approval")]
        public DateTime ApprovalDate { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get;set; }

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Approved By Id")]
        public string ApprovedById { get;set; }

        public virtual Release Release { get; set; }
        public virtual Component Component { get; set;  }
    }
}
