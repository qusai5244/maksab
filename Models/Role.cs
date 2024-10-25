using System.ComponentModel.DataAnnotations;

namespace Maksab.Models
{
    public class Role : BaseModel
    {

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        public ICollection<UserToRole> UsersToRoles { get; set; }
        public ICollection<RoleToPermission> RolesToPermissions { get; set; }
    }
}
