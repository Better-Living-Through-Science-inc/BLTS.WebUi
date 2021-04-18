using BLTS.WebUi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLTS.WebUi.ContentManagementSystem
{
    public class ContentManagementSystemManager
    {
        private readonly IApiCmsRepository _apiCmsRepository;
        public ContentManagementSystemManager(IApiCmsRepository apiCmsRepository)
        {
            _apiCmsRepository = apiCmsRepository;
        }

        public async Task<CmsWebsiteInfoEntity> GetWebsiteInformation()
        {
            return await _apiCmsRepository.GetWebsiteInformation("betterlivingthroughscience.org");
        }

        public async Task<List<CmsNavigationMenuEntity>> GetUserNavigationMenu()
        {
            return await _apiCmsRepository.GetWebsiteNavigationMenu("betterlivingthroughscience.org");
        }
    }
}
