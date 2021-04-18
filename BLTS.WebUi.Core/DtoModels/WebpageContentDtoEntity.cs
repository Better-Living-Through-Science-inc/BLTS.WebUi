using BLTS.WebUi.DtoModels;
using System.Collections.Generic;

namespace BLTS.WebUi.DtoModels
{
    public partial class WebpageContentDtoEntity : DtoEntity<long>
    {
        public WebpageContentDtoEntity()
        {
            NavigationMenuCollection = new List<NavigationMenuDtoEntity>();
        }

        public string Title { get; set; }
        public string Metatag { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public bool IsAuthorizationRequired { get; set; }

        public virtual List<NavigationMenuDtoEntity> NavigationMenuCollection { get; set; }
    }
}
