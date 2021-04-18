using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public partial class WebpageContent : Entity<long>
    {
        public WebpageContent()
        {
            NavigationMenuCollection = new List<NavigationMenu>();
            WebpageContentPermissionCollection = new List<WebpageContentPermission>();
        }

        public string Title { get; set; }
        public string Metatag { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public bool IsAuthorizationRequired { get; set; }

        public virtual List<NavigationMenu> NavigationMenuCollection { get; set; }
        public virtual List<WebpageContentPermission> WebpageContentPermissionCollection { get; set; }
    }
}
