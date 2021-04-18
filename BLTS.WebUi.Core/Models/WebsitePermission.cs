namespace BLTS.WebUi.Models
{
    public partial class WebsitePermission : Entity<long>
    {
        public long WebsiteInfoId { get; set; }
        public long ActiveDirectoryGroupId { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ActiveDirectoryGroup ActiveDirectoryGroup { get; set; }
        public virtual WebsiteInfo WebsiteInfo { get; set; }
    }
}
