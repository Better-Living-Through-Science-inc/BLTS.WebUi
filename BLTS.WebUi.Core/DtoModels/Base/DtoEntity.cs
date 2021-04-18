using System;

namespace BLTS.WebUi.DtoModels
{
    /// <summary>
    /// base entity used by the repository pattern
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class DtoEntity<TPrimaryKey> : IDtoEntity<TPrimaryKey>
    {
        public DtoEntity()
        {
        }

        /// <summary>
        /// Primary Key ID
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }
        /// <summary>
        /// DateTime of Creation
        /// </summary>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// DateTime of last modification
        /// </summary>
        public virtual DateTime LastModificationDate { get; set; }
        /// <summary>
        /// Indicates a soft delete 
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
