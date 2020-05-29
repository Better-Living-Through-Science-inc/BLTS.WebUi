using Abp.AutoMapper;
using BLTS.WebUi.Sessions.Dto;

namespace BLTS.WebUi.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
