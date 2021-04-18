using BLTS.WebUi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLTS.WebUi.Models
{
    public interface IApiCmsRepository
    {
        Task<CmsWebsiteInfoEntity> GetWebsiteInformation(string websiteBaseUrl);
        Task<List<CmsNavigationMenuEntity>> GetWebsiteNavigationMenu(string websiteBaseUrl);
    }
}