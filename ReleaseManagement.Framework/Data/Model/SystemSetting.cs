using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReleaseManagement.Framework.Data.Model
{
    [Table("SystemSetting")]
    public class SystemSetting : Entity
    {
        public SystemSetting()
        {
        }

        [Required]
        public string Key {  get;set; }

        [Required]
        public string Value { get;set; }
    }
}
