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
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BLTS.WebUi.Infrastructure.AzureApi
{
    /// <summary>
    /// Generic API controller for permission based data access 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDtoEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TDeleteDtoEntity"></typeparam>
    public abstract class ApiRepositoryStandard<TEntity, TDtoEntity, TPrimaryKey, TDeleteDtoEntity> : ApiRepositoryBase
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TDtoEntity : class, IDtoEntity<TPrimaryKey>, new()
        where TDeleteDtoEntity : class, IDeleteDtoEntity<TPrimaryKey>, new()
    {

        private readonly IApplicationLogTools _applicationLogTools;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="applicationLogTools"></param>
        /// <param name="configurationManager"></param>
        /// <param name="httpClient"></param>
        /// <param name="tokenAcquisition"></param>
        /// <param name="mapper"></param>
        /// <param name="apiGroupConfigurationName"></param>
        public ApiRepositoryStandard(IApplicationLogTools applicationLogTools
                                   , ConfigurationManager configurationManager
                                   , HttpClient httpClient
                                   , ITokenAcquisition tokenAcquisition
                                   , IHttpContextAccessor contextAccessor
                                   , IMapper mapper
                                   , string apiGroupConfigurationName) : base(applicationLogTools
                                                                            , configurationManager
                                                                            , httpClient
                                                                            , tokenAcquisition
                                                                            , contextAccessor
                                                                            , mapper
                                                                            , apiGroupConfigurationName)
        {
            _applicationLogTools = applicationLogTools;
        }

        #region Get
        /// <summary>
        /// Get object by Primary key reference
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        internal virtual async Task<TEntity> GetById(TPrimaryKey primaryKey, string subPathUri = null)
        {
            TEntity currentReturnobject = null;
            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/GetById?primaryKey={primaryKey}";

            try
            {
                HttpResponseMessage responseResult = await base.ApiGetAsync(subPathUri);
                responseResult.EnsureSuccessStatusCode();

                TDtoEntity deserializedContent = JsonConvert.DeserializeObject<TDtoEntity>(await responseResult.Content.ReadAsStringAsync());
                currentReturnobject = base.MapToEntity<TEntity, TDtoEntity>(deserializedContent);

            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }

            return currentReturnobject;
        }

        /// <summary>
        /// Get object collection by Primary key reference
        /// </summary>
        /// <param name="primaryKeyCollection"></param>
        /// <returns></returns>
        internal virtual async Task<List<TEntity>> GetAllById(List<TPrimaryKey> primaryKeyCollection, string subPathUri = null)
        {
            List<TEntity> currentReturnCollection = null;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/GetAllById";

            try
            {
                HttpResponseMessage responseResult = await base.ApiPostAsync(primaryKeyCollection, subPathUri);
                responseResult.EnsureSuccessStatusCode();

                List<TDtoEntity> deserializedContent = JsonConvert.DeserializeObject<List<TDtoEntity>>(await responseResult.Content.ReadAsStringAsync());

                currentReturnCollection = base.MapToEntityCollection<TEntity, TDtoEntity>(deserializedContent);

            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }

            return currentReturnCollection;
        }

        /// <summary>
        /// Get all objects in a sorted paged collection
        /// </summary>
        /// <param name="sortRequest"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="subPathUri"></param>
        /// <param name="apiEndpointName"></param>
        /// <returns></returns>
        internal virtual async Task<PagedResultEntity<TEntity>> GetAll(string sortRequest = "Id desc", int skipCount = 0, int maxResultCount = 99, string subPathUri = null)
        {
            PagedResultEntity<TEntity> currentReturnCollection = null;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/GetAll?sortRequest={sortRequest}&skipCount={skipCount}&maxResultCount={maxResultCount}";

            try
            {
                HttpResponseMessage responseResult = await base.ApiGetAsync(subPathUri);
                responseResult.EnsureSuccessStatusCode();

                PagedResultDtoEntity<TDtoEntity> deserializedContent = JsonConvert.DeserializeObject<PagedResultDtoEntity<TDtoEntity>>(await responseResult.Content.ReadAsStringAsync());

                currentReturnCollection = base.MapToPagedResultEntity<TEntity, TDtoEntity>(deserializedContent);

            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }

            return currentReturnCollection;
        }

        /// <summary>
        /// Get first object by partially populated object search
        /// </summary>
        /// <param name="exampleSearchEntity"></param>
        /// <param name="subPathUri"></param>
        /// <param name="apiEndpointName"></param>
        /// <returns></returns>
        internal virtual async Task<TEntity> GetByExample(TEntity exampleSearchEntity, string subPathUri = null)
        {
            if (exampleSearchEntity == null)
                return null;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/GetByExample";

            try
            {
                TDtoEntity entityDto = base.MapToDtoEntity<TEntity, TDtoEntity>(exampleSearchEntity);

                HttpResponseMessage responseResult = await base.ApiPostAsync(entityDto, subPathUri);
                responseResult.EnsureSuccessStatusCode();

                TDtoEntity deserializedContent = JsonConvert.DeserializeObject<TDtoEntity>(await responseResult.Content.ReadAsStringAsync());
                TEntity currentReturnobject = base.MapToEntity<TEntity, TDtoEntity>(deserializedContent);

                return currentReturnobject;
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }

            return null;
        }

        /// <summary>
        /// Get all objects by partially populated object search in a sorted paged collection
        /// </summary>
        /// <param name="exampleSearchEntityPagingRequest"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<IPagedResultEntity<TEntity>> GetAllByExample(IPagedResultRequestEntity<TEntity> exampleSearchEntityPagingRequest, string subPathUri = null)
        {
            IPagedResultEntity<TEntity> currentReturnCollection = null;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/GetAllByExample";

            try
            {
                PagedResultRequestDtoEntity<TDtoEntity> pagedResultRequestEntity = base.MapToPagedResultRequestDtoEntity<TEntity, TDtoEntity>(exampleSearchEntityPagingRequest);

                HttpResponseMessage responseResult = await base.ApiPostAsync(pagedResultRequestEntity, subPathUri);
                responseResult.EnsureSuccessStatusCode();

                PagedResultDtoEntity<TDtoEntity> deserializedContent = JsonConvert.DeserializeObject<PagedResultDtoEntity<TDtoEntity>>(await responseResult.Content.ReadAsStringAsync());

                currentReturnCollection = base.MapToPagedResultEntity<TEntity, TDtoEntity>(deserializedContent);

            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }

            return currentReturnCollection;
        }
        #endregion

        #region Save
        /// <summary>
        /// post object to API
        /// </summary>
        /// <param name="currentObject"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<TEntity> Save(TEntity currentObject, string subPathUri = null)
        {
            if (currentObject == null)
                return null;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/Save";

            try
            {
                TDtoEntity entityDto = base.MapToDtoEntity<TEntity, TDtoEntity>(currentObject);

                HttpResponseMessage responseResult = await base.ApiPostAsync(entityDto, subPathUri);
                responseResult.EnsureSuccessStatusCode();

                TDtoEntity deserializedContent = JsonConvert.DeserializeObject<TDtoEntity>(await responseResult.Content.ReadAsStringAsync());
                TEntity currentReturnobject = base.MapToEntity<TEntity, TDtoEntity>(deserializedContent);

                return currentReturnobject;
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }

            return null;
        }

        /// <summary>
        /// post list of objects to API
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<bool> SaveCollection(List<TEntity> entityCollection, string subPathUri = null)
        {
            if (entityCollection.Count == 0)
                return true;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/SaveCollection";

            try
            {
                List<TDtoEntity> entityDtoCollection = base.MapToDtoEntityCollection<TEntity, TDtoEntity>(entityCollection);

                HttpResponseMessage responseResult = await base.ApiPostAsync(entityDtoCollection, subPathUri);
                responseResult.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return false;
            }

            return false;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete requested object by primary key
        /// </summary>
        /// <param name="currentObject"></param>
        /// <param name="isSoftDelete"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<bool> Delete(TEntity currentObject, bool isSoftDelete = false, string subPathUri = null)
        {
            if (currentObject == null)
                return true;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/Delete?primaryKey={currentObject.Id}&isSoftDelete={isSoftDelete}";

            try
            {
                HttpResponseMessage responseResult = await base.ApiDeleteAsync(subPathUri);
                responseResult.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Delete requested collection of objects by primary key
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<bool> DeleteCollection(List<TEntity> entityCollection, bool isSoftDelete = false, string subPathUri = null)
        {
            List<TDeleteDtoEntity> entityDtoCollection = entityCollection.Select(singleEntity => new TDeleteDtoEntity() { Id = singleEntity.Id, IsSoftDelete = isSoftDelete }).ToList();
            return await DeleteCollection(entityDtoCollection, subPathUri);
        }

        /// <summary>
        /// Delete requested collection of objects by primary key
        /// </summary>
        /// <param name="entityDtoCollection"></param>
        /// <param name="subPathUri"></param>
        /// <returns></returns>
        internal virtual async Task<bool> DeleteCollection(List<TDeleteDtoEntity> entityDtoCollection, string subPathUri = null)
        {
            if (entityDtoCollection.Count == 0)
                return true;

            if (string.IsNullOrWhiteSpace(subPathUri))
                subPathUri = $"/api/{typeof(TEntity).Name}/DeleteCollection";

            try
            {
                HttpResponseMessage responseResult = await base.ApiPostAsync(entityDtoCollection, subPathUri);
                responseResult.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception apiCommunicationError)
            {
                _applicationLogTools.LogError(apiCommunicationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiRepositoryStandard<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return false;
            }

            return false;
        }
        #endregion

    }
}