using System.Collections.Generic;
using BLTS.WebUi.Roles.Dto;

namespace BLTS.WebUi.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
