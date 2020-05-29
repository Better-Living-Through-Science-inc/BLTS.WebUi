using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using BLTS.WebUi.Authorization.Users;
using BLTS.WebUi.Editions;

namespace BLTS.WebUi.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
