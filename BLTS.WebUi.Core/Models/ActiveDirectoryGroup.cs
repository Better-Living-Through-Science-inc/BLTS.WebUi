using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public partial class ActiveDirectoryGroup : Entity<long>
    {
        public ActiveDirectoryGroup()
        {
            ApplicationPermissionCollection = new List<ApplicationPermission>();
            FileStoragePermissionCollection = new List<FileStoragePermission>();
            WebpageContentPermissionCollection = new List<WebpageContentPermission>();
            WebsitePermissionCollection = new List<WebsitePermission>();
        }

        public string Name { get; set; }
        public string GroupSid { get; set; }

        public virtual List<ApplicationPermission> ApplicationPermissionCollection { get; set; }
        public virtual List<FileStoragePermission> FileStoragePermissionCollection { get; set; }
        public virtual List<WebpageContentPermission> WebpageContentPermissionCollection { get; set; }
        public virtual List<WebsitePermission> WebsitePermissionCollection { get; set; }
    }
}
