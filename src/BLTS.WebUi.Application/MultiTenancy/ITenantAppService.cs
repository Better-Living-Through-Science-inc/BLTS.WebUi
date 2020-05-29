using Abp.Application.Services;
using BLTS.WebUi.MultiTenancy.Dto;

namespace BLTS.WebUi.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

