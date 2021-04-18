namespace BLTS.WebUi.Models
{
    public partial class WebsiteNavigationMenu
    {
        public long WebsiteInfoId { get; set; }
        public long NavigationMenuId { get; set; }

        public virtual NavigationMenu NavigationMenu { get; set; }
        public virtual WebsiteInfo WebsiteInfo { get; set; }
    }
}
