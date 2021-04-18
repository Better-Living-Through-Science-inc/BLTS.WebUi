using BLTS.WebUi.Models;
using System.Collections.Generic;

namespace BLTS.WebUi.DtoModels
{
    public partial class WebsiteInfoDtoEntity : DtoEntity<long>
    {
        public WebsiteInfoDtoEntity()
        {
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

        public virtual ApplicationInfoDtoEntity ApplicationInfo { get; set; }
    }
}
