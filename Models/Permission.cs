using System.ComponentModel.DataAnnotations;

namespace Maksab.Models
{
    public class Permission : BaseModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public int Code { get; set; }

        public ICollection<RoleToPermission> RolesToPermissions { get; set; }

    }
}
