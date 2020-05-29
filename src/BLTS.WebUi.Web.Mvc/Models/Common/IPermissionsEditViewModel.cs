using System.Collections.Generic;
using BLTS.WebUi.Roles.Dto;

namespace BLTS.WebUi.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}