using System.ComponentModel.DataAnnotations;

namespace ReleaseManagement.Framework.Data.Model
{
    public class Entity
    {
        public Entity() { }

        [Key]
        public int Id { get; set; }
    }
}
