using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;

namespace BLTS.WebUi.Models
{
    public partial class CmsNavigationMenuEntity
    {
        public CmsNavigationMenuEntity()
        {
            ChildNavigationMenuCollection = new List<CmsNavigationMenuEntity>();
        }

        public string DisplayText { get; set; }
        public int DisplayOrder { get; set; }
        public string ToolTip { get; set; }
        public string SubPath { get; set; }
        public string IconClass { get; set; }
        public string NavLinkText { get; set; }

        public NavLinkMatch MenuNavLinkMatch
        {
            get
            {
                return (NavLinkMatch)Enum.Parse(typeof(NavLinkMatch), NavLinkText, true);
            }
        }

        public virtual List<CmsNavigationMenuEntity> ChildNavigationMenuCollection { get; set; }
    }
}
