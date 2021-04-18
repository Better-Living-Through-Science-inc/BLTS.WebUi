using BLTS.WebUi.Models;
using System.Collections.Generic;

namespace BLTS.WebUi.DtoModels
{
    public partial class NavigationMenuDtoEntity : DtoEntity<long>
    {
        public NavigationMenuDtoEntity()
        {
            IsEnabled = true;
            ChildNavigationMenuCollection = new List<NavigationMenuDtoEntity>();
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

        public virtual NavigationMenuDtoEntity ParentNavigationMenu { get; set; }
        public virtual WebpageContentDtoEntity WebpageContent { get; set; }
        public virtual List<NavigationMenuDtoEntity> ChildNavigationMenuCollection { get; set; }
        public virtual List<WebsiteNavigationMenu> WebsiteNavigationMenuCollection { get; set; }
    }
}
