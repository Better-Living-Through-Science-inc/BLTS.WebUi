using Abp.Authorization;
using BLTS.WebUi.Authorization.Roles;
using BLTS.WebUi.Authorization.Users;

namespace BLTS.WebUi.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
