using System;

namespace BLTS.WebUi.Models
{
    /// <summary>
    /// base entity used by the repository pattern
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class BaseEntity<TPrimaryKey>
    {
        protected BaseEntity()
        {
            //indicates new record or not yet sent to API for sync
            //SynchronizeDate = DateTime.MaxValue;
        }

        /// <summary>
        /// Primary identifier of object
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        ///// <summary>
        ///// DateTime of sync with API - 9999-12-31 indicates new record or not yet sent to API for sync
        ///// </summary>
        //public DateTime SynchronizeDate { get; set; }
    }
}
