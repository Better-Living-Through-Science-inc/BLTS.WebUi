namespace BLTS.WebUi.Models
{
    public partial class WebpageContentPermission : Entity<long>
    {
        public long WebpageContentId { get; set; }
        public long ActiveDirectoryGroupId { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ActiveDirectoryGroup ActiveDirectoryGroup { get; set; }
        public virtual WebpageContent WebpageContent { get; set; }
    }
}
