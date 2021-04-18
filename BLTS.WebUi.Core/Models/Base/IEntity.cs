using System;

namespace BLTS.WebUi.Models
{
    public interface IEntity<TPrimaryKey>
    {
        DateTime CreationDate { get; set; }
        TPrimaryKey Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime LastModificationDate { get; set; }
    }
}