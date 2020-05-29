using System.Threading.Tasks;
using BLTS.WebUi.Configuration.Dto;

namespace BLTS.WebUi.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
