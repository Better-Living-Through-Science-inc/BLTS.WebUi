using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public partial class ApplicationInfo : Entity<long>
    {
        public ApplicationInfo()
        {
            ApplicationLogCollection = new List<ApplicationLog>();
            ApplicationPermissionCollection = new List<ApplicationPermission>();
            OperationalConfigurationCollection = new List<OperationalConfiguration>();
            WebsiteInfoCollection = new List<WebsiteInfo>();
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PocEmail { get; set; }
        public string PocNumber { get; set; }
        public bool IsAuthorizationRequired { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual List<ApplicationLog> ApplicationLogCollection { get; set; }
        public virtual List<ApplicationPermission> ApplicationPermissionCollection { get; set; }
        public virtual List<OperationalConfiguration> OperationalConfigurationCollection { get; set; }
        public virtual List<WebsiteInfo> WebsiteInfoCollection { get; set; }
    }
}
