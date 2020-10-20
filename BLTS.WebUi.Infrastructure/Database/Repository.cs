using BLTS.WebUi.DataAccessLayer;
using BLTS.WebUi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLTS.WebUi.Database
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>, new()
    {
        protected readonly DefaultDbContext _context;
        protected readonly IUnitOfWork _unitOfWork;
        public Repository(IServiceScopeFactory serviceScopeFactory
                        , IUnitOfWork unitOfWork)
        {
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DefaultDbContext>();
            _unitOfWork = unitOfWork;
        }

        #region Get
        /// <summary>
        /// returns unordered list of all entities stored
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// returns entity by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey id)
        {
            return _context.Set<TEntity>().FindAsync(id).Result;
        }

        /// <summary>
        /// returns unordered list of all entities that match the expression
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }
        #endregion

        #region Save
        /// <summary>
        /// saves entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity, bool autoCommit = true)
        {
            entity = _context.Set<TEntity>().Add(entity).Entity;
            if (autoCommit)
                _unitOfWork.Complete();
            return entity;
        }

        /// <summary>
        /// saves list of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(IEnumerable<TEntity> entity, bool autoCommit = true)
        {
            _context.Set<TEntity>().AddRange(entity);
            if (autoCommit)
                _unitOfWork.Complete();
        }

        /// <summary>
        /// pass through for the auto selected insert or update based on Id = 0 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Save(TEntity entity, bool autoCommit = true)
        {
            if (!entity.Id.Equals(default(TPrimaryKey)))
                return Update(entity, autoCommit);
            else
                return Insert(entity, autoCommit);
        }

        /// <summary>
        /// pass through for the auto selected insert or update based on Id = 0
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Save(IEnumerable<TEntity> entity, bool autoCommit = true)
        {
            Update(entity.Where(singleEntity => !singleEntity.Id.Equals(default(TPrimaryKey))).ToList());
            Insert(entity.Where(singleEntity => singleEntity.Id.Equals(default(TPrimaryKey))).ToList());
            if (autoCommit)
                _unitOfWork.Complete();
        }

        /// <summary>
        /// saves entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity, bool autoCommit = true)
        {
            entity = _context.Set<TEntity>().Update(entity).Entity;
            if (autoCommit)
                _unitOfWork.Complete();
            return entity;
        }

        /// <summary>
        /// saves list of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Update(IEnumerable<TEntity> entity, bool autoCommit = true)
        {
            _context.Set<TEntity>().UpdateRange(entity);
            if (autoCommit)
                _unitOfWork.Complete();
        }
        #endregion

        #region Delete
        /// <summary>
        /// deletes entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Delete(TEntity entity, bool autoCommit = true)
        {
            entity =  _context.Set<TEntity>().Remove(entity).Entity;
            if (autoCommit)
                _unitOfWork.Complete();
            return entity;
        }

        /// <summary>
        /// deletes list of entity 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Delete(List<TEntity> entity, bool autoCommit = true)
        {
            _context.Set<TEntity>().RemoveRange(entity);
            if (autoCommit)
                _unitOfWork.Complete();
        }
        #endregion
    }
}
