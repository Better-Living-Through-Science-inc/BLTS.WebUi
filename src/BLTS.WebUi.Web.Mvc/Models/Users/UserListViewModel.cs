using System.Collections.Generic;
using BLTS.WebUi.Roles.Dto;

namespace BLTS.WebUi.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
