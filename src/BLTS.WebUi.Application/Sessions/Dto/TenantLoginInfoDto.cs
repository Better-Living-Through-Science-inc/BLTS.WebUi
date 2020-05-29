using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BLTS.WebUi.MultiTenancy;

namespace BLTS.WebUi.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
