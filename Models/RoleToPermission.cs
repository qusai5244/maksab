﻿namespace Maksab.Models
{
    public class RoleToPermission : BaseModel
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
