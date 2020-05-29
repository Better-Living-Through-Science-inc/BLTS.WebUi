using Abp.AutoMapper;
using BLTS.WebUi.Roles.Dto;
using BLTS.WebUi.Web.Models.Common;

namespace BLTS.WebUi.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
