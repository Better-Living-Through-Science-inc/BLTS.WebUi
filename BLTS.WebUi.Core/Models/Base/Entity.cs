using System;
using System.ComponentModel.DataAnnotations;

namespace BLTS.WebUi.Models
{
    /// <summary>
    /// base entity used by the repository pattern
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        protected Entity()
        {
            CreationDate = DateTime.UtcNow;
            LastModificationDate = CreationDate;
            IsDeleted = false;
        }

        /// <summary>
        /// Primary identifier of object
        /// </summary>
        [Key]
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
