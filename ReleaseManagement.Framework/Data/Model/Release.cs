using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("Release")]
    public class Release : Entity
    {
        public Release() : base()
        { }

        [Required]
        [Display(Name = "Release Name")]
        public string ReleaseName { get; set; }

        public virtual ICollection<ComponentApproval> ComponentApprovals { get;set; }
    }
}
