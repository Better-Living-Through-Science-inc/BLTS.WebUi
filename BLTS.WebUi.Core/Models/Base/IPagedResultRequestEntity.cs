namespace BLTS.WebUi.Models
{
    public interface IPagedResultRequestEntity<TEntity>
    {
        /// <summary>
        /// take this many records
        /// </summary>
        int MaxResultCount { get; set; }
        /// <summary>
        /// skip this many records
        /// </summary>
        int SkipCount { get; set; }
        /// <summary>
        /// Comma separated list of Property Name and desc/asc
        /// </summary>
        string SortRequest { get; set; }
        /// <summary>
        /// partially populated object used as a query filter
        /// </summary>
        TEntity ObjectFilter { get; set; }
    }
}