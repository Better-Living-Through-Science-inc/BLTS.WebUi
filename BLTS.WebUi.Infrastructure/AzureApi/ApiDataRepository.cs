using AutoMapper;
using BLTS.WebUi.Configurations;
using BLTS.WebUi.DtoModels;
using BLTS.WebUi.Logs;
using BLTS.WebUi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLTS.WebUi.Infrastructure.AzureApi
{
    public class ApiDataRepository<TEntity, TDtoEntity, TPrimaryKey, TDeleteDtoEntity> : ApiRepositoryStandard<TEntity, TDtoEntity, TPrimaryKey, TDeleteDtoEntity>, IApiRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
        where TDtoEntity : class, IDtoEntity<TPrimaryKey>, new()
        where TDeleteDtoEntity : class, IDeleteDtoEntity<TPrimaryKey>, new()
    {

        private readonly IApplicationLogTools _applicationLogTools;

        public ApiDataRepository(IApplicationLogTools applicationLogTools
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
                                                       , "ApiData")
        {
            _applicationLogTools = applicationLogTools;
        }

        #region Get
        /// <summary>
        /// Get object by Primary key reference
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(TPrimaryKey primaryKey)
        {
            try
            {
                return await base.GetById(primaryKey);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// Get object collection by Primary key reference
        /// </summary>
        /// <param name="primaryKeyCollection"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllById(List<TPrimaryKey> primaryKeyCollection)
        {
            try
            {
                return await base.GetAllById(primaryKeyCollection);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// Get all objects in a sorted paged collection
        /// </summary>
        /// <param name="sortRequest"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public virtual async Task<IPagedResultEntity<TEntity>> GetAll(string sortRequest = "Id desc", int skipCount = 0, int maxResultCount = 99)
        {
            try
            {
                return await base.GetAll(sortRequest, skipCount, maxResultCount);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// Get first object by partially populated object search
        /// </summary>
        /// <param name="exampleSearchEntity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByExample(TEntity exampleSearchEntity)
        {
            try
            {
                return await base.GetByExample(exampleSearchEntity);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// Get all objects by partially populated object search in a sorted paged collection
        /// </summary>
        /// <param name="exampleSearchEntityPagingRequest"></param>
        /// <returns></returns>
        public virtual async Task<IPagedResultEntity<TEntity>> GetAllByExample(IPagedResultRequestEntity<TEntity> exampleSearchEntityPagingRequest)
        {
            try
            {
                return await base.GetAllByExample(exampleSearchEntityPagingRequest);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        #endregion

        #region Save
        /// <summary>
        /// Save object, insert will be performed where PK = null or default value, else update will be performed
        /// </summary>
        /// <param name="saveEntity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Save(TEntity saveEntity)
        {
            try
            {
                return await base.Save(saveEntity);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    return null;
            }
        }

        /// <summary>
        /// Save object collection, insert will be performed where PK = null or default value, else update will be performed
        /// </summary>
        /// <param name="saveEntityCollection"></param>
        /// <returns></returns>
        public virtual async Task<bool> SaveCollection(List<TEntity> saveEntityCollection)
        {
            bool isSuccess;
            try
            {
                isSuccess = await base.SaveCollection(saveEntityCollection);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    isSuccess = false;
            }
            return isSuccess;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete requested object by primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="isSoftDelete"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TEntity currentObject, bool isSoftDelete = false)
        {
            bool isSuccess;
            try
            {
                isSuccess = await base.Delete(currentObject, isSoftDelete);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    isSuccess = false;
            }
            return isSuccess;
        }

        /// <summary>
        /// Delete requested collection of objects by primary key
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <param name="isSoftDelete"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteCollection(List<TEntity> entityCollection, bool isSoftDelete = false)
        {
            bool isSuccess;
            try
            {
                isSuccess = await base.DeleteCollection(entityCollection, isSoftDelete);
            }
            catch (Exception apiRepositoryError)
            {
                _applicationLogTools.LogError(apiRepositoryError, new Dictionary<string, dynamic> { { "ClassName", $"AzureApi.ApiRepository<{typeof(TEntity).Name}>" } });
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                else
                    isSuccess = false;
            }
            return isSuccess;
        }
        #endregion

    }
}