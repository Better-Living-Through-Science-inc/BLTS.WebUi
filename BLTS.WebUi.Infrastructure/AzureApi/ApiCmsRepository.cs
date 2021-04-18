using AutoMapper;
using BLTS.WebUi.Configurations;
using BLTS.WebUi.DtoModels;
using BLTS.WebUi.Logs;
using BLTS.WebUi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLTS.WebUi.Infrastructure.AzureApi
{
    public class ApiCmsRepository : ApiRepositoryBase, IApiCmsRepository
    {

        private readonly IApplicationLogTools _applicationLogTools;

        public ApiCmsRepository(IApplicationLogTools applicationLogTools
                              , ConfigurationManager configurationManager
                              , HttpClient httpClient
                               , ITokenAcquisition tokenAcquisition
                               , IHttpContextAccessor contextAccessor
                               , IMapper mapper) : base(applicationLogTools
                                                      , configurationManager
                                                      , httpClient
                                                      , tokenAcquisition
                                                      , contextAccessor
                                                      , mapper
                                                      , "ApiCms")
        {
            _applicationLogTools = applicationLogTools;
        }

        public virtual async Task<CmsWebsiteInfoEntity> GetWebsiteInformation(string websiteBaseUrl)
        {
            try
            {
                string subPathUri = $"/api/CmsOutput/GetWebsiteInformation?websiteBaseUrl={websiteBaseUrl}";

                HttpResponseMessage responseResult = await base.ApiGetAsync(subPathUri);
                responseResult.EnsureSuccessStatusCode();

                CmsWebsiteInfoDtoEntity deserializedContent = JsonConvert.DeserializeObject<CmsWebsiteInfoDtoEntity>(await responseResult.Content.ReadAsStringAsync());

                return base.MapToEntity<CmsWebsiteInfoEntity, CmsWebsiteInfoDtoEntity>(deserializedContent);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiCmsRepository<{typeof(CmsWebsiteInfoDtoEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        public virtual async Task<List<CmsNavigationMenuEntity>> GetWebsiteNavigationMenu(string websiteBaseUrl)
        {
            try
            {
                string subPathUri = $"/api/CmsOutput/GetWebsiteNavigationMenu?websiteBaseUrl={websiteBaseUrl}";

                HttpResponseMessage responseResult = await base.ApiGetAsync(subPathUri);
                responseResult.EnsureSuccessStatusCode();

                List<CmsNavigationMenuDtoEntity> deserializedContent = JsonConvert.DeserializeObject<List<CmsNavigationMenuDtoEntity>>(await responseResult.Content.ReadAsStringAsync());

                return base.MapToEntityCollection<CmsNavigationMenuEntity, CmsNavigationMenuDtoEntity>(deserializedContent);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiCmsRepository<{typeof(CmsNavigationMenuDtoEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

    }
}