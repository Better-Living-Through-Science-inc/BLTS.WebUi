using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace BLTS.WebUi.Models
{
    public partial class FileStorage : Entity<long>
    {
        public FileStorage()
        {
            FileStoragePermissionCollection = new List<FileStoragePermission>();
        }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string RootPath { get; set; }
        public long SizeKB { get; set; }
        public string SubPath { get; set; }
        public bool IsAuthorizationRequired { get; set; }
        [NotMapped]
        public Stream FileData { get; set; }
        public virtual List<FileStoragePermission> FileStoragePermissionCollection { get; set; }
    }
}
