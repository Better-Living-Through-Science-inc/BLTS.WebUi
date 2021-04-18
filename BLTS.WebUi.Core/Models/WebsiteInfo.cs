using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public partial class WebsiteInfo : Entity<long>
    {
        public WebsiteInfo()
        {
            WebsiteNavigationMenuCollection = new List<WebsiteNavigationMenu>();
            WebsitePermissionCollection = new List<WebsitePermission>();
        }

        public long ApplicationInfoId { get; set; }
        public string Name { get; set; }
        public string Metatag { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Footer { get; set; }
        public string BaseUrl { get; set; }
        public string CssThemePath { get; set; }
        public bool IsAuthorizationRequired { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ApplicationInfo ApplicationInfo { get; set; }
        public virtual List<WebsiteNavigationMenu> WebsiteNavigationMenuCollection { get; set; }
        public virtual List<WebsitePermission> WebsitePermissionCollection { get; set; }
    }
}
