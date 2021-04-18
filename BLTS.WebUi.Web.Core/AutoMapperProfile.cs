using AutoMapper;
using BLTS.WebUi.DtoModels;
using BLTS.WebUi.Models;

namespace BLTS.WebUi.Web.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CmsNavigationMenuDtoEntity, CmsNavigationMenuEntity>();
            CreateMap<CmsWebsiteInfoDtoEntity, CmsWebsiteInfoEntity>();

            //CreateMap<IPagedResultRequestEntity<NavigationMenu>, PagedResultRequestDtoEntity<NavigationMenuDtoEntity>>();
            //CreateMap<PagedResultRequestDtoEntity<NavigationMenuDtoEntity>, IPagedResultRequestEntity<NavigationMenu>>();
            //CreateMap<IPagedResultEntity<NavigationMenu>, PagedResultDtoEntity<NavigationMenuDtoEntity>>();
            //CreateMap<PagedResultDtoEntity<NavigationMenuDtoEntity>, IPagedResultEntity<NavigationMenu>>();
            //CreateMap<NavigationMenu, NavigationMenuDtoEntity>();
            //CreateMap<NavigationMenuDtoEntity, NavigationMenu>();
            //CreateMap<NavigationMenu, CmsNavigationMenuDtoEntity>();

            //CreateMap<IPagedResultRequestEntity<WebsiteInfo>, PagedResultRequestDtoEntity<WebsiteInfoDtoEntity>>();
            //CreateMap<PagedResultRequestDtoEntity<WebsiteInfoDtoEntity>, IPagedResultRequestEntity<WebsiteInfo>>();
            //CreateMap<IPagedResultEntity<WebsiteInfo>, PagedResultDtoEntity<WebsiteInfoDtoEntity>>();
            //CreateMap<PagedResultDtoEntity<WebsiteInfoDtoEntity>, IPagedResultEntity<WebsiteInfo>>();
            //CreateMap<WebsiteInfo, WebsiteInfoDtoEntity>();
            //CreateMap<WebsiteInfoDtoEntity, WebsiteInfo>();
            //CreateMap<WebsiteInfo, CmsWebsiteInfoDtoEntity>();
        }
    }
}
