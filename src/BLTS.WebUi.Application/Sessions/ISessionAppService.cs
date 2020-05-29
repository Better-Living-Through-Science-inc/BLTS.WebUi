using System.Threading.Tasks;
using Abp.Application.Services;
using BLTS.WebUi.Sessions.Dto;

namespace BLTS.WebUi.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
