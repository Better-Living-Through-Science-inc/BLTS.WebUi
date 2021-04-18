using System;

namespace BLTS.WebUi.DtoModels
{
    /// <summary>
    /// base entity used by the repository pattern
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class DeleteDtoEntity<TPrimaryKey> : IDeleteDtoEntity<TPrimaryKey>
    {
        public DeleteDtoEntity()
        {
            IsSoftDelete = false;
        }

        /// <summary>
        /// Primary Key ID
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }
        /// <summary>
        /// Indicates a soft delete 
        /// </summary>
        public virtual bool IsSoftDelete { get; set; }
    }
}
