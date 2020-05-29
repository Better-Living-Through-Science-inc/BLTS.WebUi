using System.Collections.Generic;
using System.Linq;
using BLTS.WebUi.Roles.Dto;
using BLTS.WebUi.Users.Dto;

namespace BLTS.WebUi.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
