using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public interface IPagedResultEntity<TEntity>
    {
        List<TEntity> ItemCollection { get; set; }
        long TotalCount { get; set; }
    }
}