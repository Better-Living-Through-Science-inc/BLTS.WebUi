using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public partial class NavigationMenu : Entity<long>
    {
        public NavigationMenu()
        {
            IsEnabled = true;
            ChildNavigationMenuCollection = new List<NavigationMenu>();
            WebsiteNavigationMenuCollection = new List<WebsiteNavigationMenu>();
        }

        public long ParentNavigationMenuId { get; set; }
        public long WebpageContentId { get; set; }
        public string DisplayText { get; set; }
        public int DisplayOrder { get; set; }
        public string ToolTip { get; set; }
        public string SubPath { get; set; }
        public string IconClass { get; set; }
        public string NavLinkText { get; set; }
        public bool IsEnabled { get; set; }

        public virtual NavigationMenu ParentNavigationMenu { get; set; }
        public virtual WebpageContent WebpageContent { get; set; }
        public virtual List<NavigationMenu> ChildNavigationMenuCollection { get; set; }
        public virtual List<WebsiteNavigationMenu> WebsiteNavigationMenuCollection { get; set; }
    }
}
