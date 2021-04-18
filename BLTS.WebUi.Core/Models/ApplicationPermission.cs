namespace BLTS.WebUi.Models
{
    public partial class ApplicationPermission : Entity<long>
    {
        public long ApplicationPermissionId { get; set; }
        public long ApplicationInfoId { get; set; }
        public long ActiveDirectoryGroupId { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ActiveDirectoryGroup ActiveDirectoryGroup { get; set; }
        public virtual ApplicationInfo ApplicationInfo { get; set; }
    }
}
