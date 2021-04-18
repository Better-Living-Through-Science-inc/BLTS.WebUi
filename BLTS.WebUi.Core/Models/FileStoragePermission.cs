namespace BLTS.WebUi.Models
{
    public partial class FileStoragePermission : Entity<long>
    {
        public long FileStorageId { get; set; }
        public long ActiveDirectoryGroupId { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ActiveDirectoryGroup ActiveDirectoryGroup { get; set; }
        public virtual FileStorage FileStorage { get; set; }
    }
}
