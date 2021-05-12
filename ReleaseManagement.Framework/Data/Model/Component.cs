using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("Component")]
    public class Component  : Entity
    {
        public Component() : base() 
        {
            Enabled = false;
        }

        [Required]
        [Display(Name = "Component Name")]
        public string ComponentName { get; set; }

        [ForeignKey(nameof(ComponentType))]
        [Required]
        public int ComponentTypeId { get; set; }

        [Display(Name = "Enable Component")]
        public bool Enabled { get;set; }

        public virtual ComponentType ComponentType { get; set; }
        public virtual ICollection<ComponentApproval> ComponentApprovals { get;set; }
    }
}
