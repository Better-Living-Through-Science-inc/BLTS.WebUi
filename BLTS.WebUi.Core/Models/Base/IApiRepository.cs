using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLTS.WebUi.Models
{
    public interface IApiRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        Task<bool> Delete(TEntity currentObject, bool isSoftDelete = false);
        Task<bool> DeleteCollection(List<TEntity> entityCollection, bool isSoftDelete = false);
        Task<IPagedResultEntity<TEntity>> GetAll(string sortRequest = "Id desc", int skipCount = 0, int maxResultCount = 99);
        Task<IPagedResultEntity<TEntity>> GetAllByExample(IPagedResultRequestEntity<TEntity> exampleSearchEntityPagingRequest);
        Task<List<TEntity>> GetAllById(List<TPrimaryKey> primaryKeyCollection);
        Task<TEntity> GetByExample(TEntity exampleSearchEntity);
        Task<TEntity> GetById(TPrimaryKey primaryKey);
        Task<TEntity> Save(TEntity saveEntity);
        Task<bool> SaveCollection(List<TEntity> saveEntityCollection);
    }
}