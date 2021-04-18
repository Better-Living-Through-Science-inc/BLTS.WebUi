using AutoMapper;
using BLTS.WebUi.Configurations;
using BLTS.WebUi.DtoModels;
using BLTS.WebUi.Logs;
using BLTS.WebUi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLTS.WebUi.Infrastructure.AzureApi
{
    /// <summary>
    /// Generic API controller for permission based data access 
    /// </summary>
    public abstract class ApiRepositoryBase
    {

        private readonly IApplicationLogTools _applicationLogTools;
        private readonly ConfigurationManager _configurationManager;
        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly string _apiGroupConfigurationName;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="applicationLogTools"></param>
        /// <param name="configurationManager"></param>
        /// <param name="httpClient"></param>
        /// <param name="tokenAcquisition"></param>
        /// <param name="mapper"></param>
        /// <param name="apiGroupConfigurationName"></param>
        public ApiRepositoryBase(IApplicationLogTools applicationLogTools
                               , ConfigurationManager configurationManager
                               , HttpClient httpClient
                               , ITokenAcquisition tokenAcquisition
                               , IHttpContextAccessor contextAccessor
                               , IMapper mapper
                               , string apiGroupConfigurationName)
        {
            _applicationLogTools = applicationLogTools;
            _configurationManager = configurationManager;
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _mapper = mapper;

            _apiGroupConfigurationName = apiGroupConfigurationName;

            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri(_configurationManager.GetValue($"AzureAdPermissions:{_apiGroupConfigurationName}BaseAddress"));
        }

        /// <summary>
        /// Execute Get against API
        /// </summary>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<HttpResponseMessage> ApiGetAsync(string subPathUri)
        {
            try
            {
                await PrepareAuthenticatedClient();

                return await _httpClient.GetAsync(subPathUri);
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// post object to API
        /// </summary>
        /// <param name="entityDto"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<HttpResponseMessage> ApiPostAsync<TDtoEntity>(TDtoEntity entityDto, string subPathUri)
        {
            if (entityDto == null)
                return null;

            try
            {
                await PrepareAuthenticatedClient();

                return await _httpClient.PostAsJsonAsync(subPathUri, entityDto);
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase {typeof(TDtoEntity).Name }" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// post list of objects to API
        /// </summary>
        /// <param name="entityDtoCollection"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<HttpResponseMessage> ApiPostAsync<TDtoEntity>(List<TDtoEntity> entityDtoCollection, string subPathUri = null)
        {
            if (entityDtoCollection.Count == 0)
                return null;

            try
            {
                await PrepareAuthenticatedClient();

                return await _httpClient.PostAsJsonAsync(subPathUri, entityDtoCollection);
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase {typeof(TDtoEntity).Name }" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        internal virtual async Task<HttpResponseMessage> ApiDeleteAsync(string subPathUri)
        {
            try
            {
                await PrepareAuthenticatedClient();

                return await _httpClient.DeleteAsync(subPathUri);
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// login to the API
        /// </summary>
        /// <param name="currentApiHttpClient"></param>
        /// <returns></returns>
        internal virtual async Task PrepareAuthenticatedClient()
        {
            try
            {
                string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new List<string>() { _configurationManager.GetValue($"AzureAdPermissions:{_apiGroupConfigurationName}Scope") });
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception authenticationError)
            {
                _applicationLogTools.LogError(authenticationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase" } });
            }
        }

        internal virtual TEntity MapToEntity<TEntity, TDtoEntity>(TDtoEntity dtoEntity)
        {
            try
            {
                return _mapper.Map<TEntity>(dtoEntity);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

        internal virtual TDtoEntity MapToDtoEntity<TEntity, TDtoEntity>(TEntity internalEntity)
        {
            try
            {
                return _mapper.Map<TDtoEntity>(internalEntity);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

        internal virtual List<TEntity> MapToEntityCollection<TEntity, TDtoEntity>(List<TDtoEntity> dtoEntityCollection)
        {
            try
            {
                return _mapper.Map<List<TEntity>>(dtoEntityCollection);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

        internal virtual List<TDtoEntity> MapToDtoEntityCollection<TEntity, TDtoEntity>(List<TEntity> internalEntityCollection)
        {
            try
            {
                return _mapper.Map<List<TDtoEntity>>(internalEntityCollection);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

        internal virtual PagedResultDtoEntity<TDtoEntity> MapToPagedResultDtoEntity<TEntity, TDtoEntity>(PagedResultEntity<TEntity> pagedResultEntity)
        {
            try
            {
                return _mapper.Map<PagedResultDtoEntity<TDtoEntity>>(pagedResultEntity);
            }
            catch (Exception apiControllerError)
            {
                _applicationLogTools.LogError(apiControllerError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

        internal virtual PagedResultEntity<TEntity> MapToPagedResultEntity<TEntity, TDtoEntity>(PagedResultDtoEntity<TDtoEntity> pagedResultDtoEntity)
        {
            try
            {
                return _mapper.Map<PagedResultEntity<TEntity>>(pagedResultDtoEntity);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

        internal PagedResultRequestDtoEntity<TDtoEntity> MapToPagedResultRequestDtoEntity<TEntity, TDtoEntity>(IPagedResultRequestEntity<TEntity> exampleSearchEntityPagingRequest)
        {
            try
            {
                return _mapper.Map<PagedResultRequestDtoEntity<TDtoEntity>>(exampleSearchEntityPagingRequest);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase<{typeof(TEntity).Name}>" } });
                throw;
            }
        }

    }
}