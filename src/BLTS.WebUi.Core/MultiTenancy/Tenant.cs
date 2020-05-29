using Abp.MultiTenancy;
using BLTS.WebUi.Authorization.Users;

namespace BLTS.WebUi.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
