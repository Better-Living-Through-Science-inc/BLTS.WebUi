using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BLTS.WebUi.Configuration.Dto;

namespace BLTS.WebUi.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : WebUiAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
