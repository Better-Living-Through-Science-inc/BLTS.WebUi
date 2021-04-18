using System.Collections.Generic;

namespace BLTS.WebUi.DtoModels
{
    public partial class CmsNavigationMenuDtoEntity
    {
        public CmsNavigationMenuDtoEntity()
        {
            ChildNavigationMenuCollection = new List<CmsNavigationMenuDtoEntity>();
        }

        public string DisplayText { get; set; }
        public int DisplayOrder { get; set; }
        public string ToolTip { get; set; }
        public string SubPath { get; set; }
        public string IconClass { get; set; }
        public string NavLinkText { get; set; }

        public virtual List<CmsNavigationMenuDtoEntity> ChildNavigationMenuCollection { get; set; }
    }
}
