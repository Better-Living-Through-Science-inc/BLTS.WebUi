using BLTS.WebUi.Models;
using System.Collections.Generic;

namespace BLTS.WebUi.Infrastructure.AzureApi
{
    public class PagedResultEntity<TEntity> : IPagedResultEntity<TEntity>
    {
        public PagedResultEntity()
        {
        }

        public PagedResultEntity(List<TEntity> itemCollection, int totalCount)
        {
            ItemCollection = itemCollection;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Return collection of selected members
        /// </summary>
        public virtual List<TEntity> ItemCollection { get; set; }
        /// <summary>
        /// Total number of items in original collection 
        /// </summary>
        public virtual long TotalCount { get; set; }
    }
}