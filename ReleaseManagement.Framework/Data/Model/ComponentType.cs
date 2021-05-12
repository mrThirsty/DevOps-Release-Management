using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("ComponentType")]
    public class ComponentType : Entity
    {
        public ComponentType() : base()
        {
            Active = true;
        }

        [Required]
        [Display(Name = "Component Name")]
        public string ComponentName { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
