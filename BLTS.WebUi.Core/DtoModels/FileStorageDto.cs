namespace BLTS.WebUi.DtoModels
{
    public partial class FileStorageDto : DtoEntity<long>
    {
        public FileStorageDto()
        {
        }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string RootPath { get; set; }
        public long SizeKb { get; set; }
        public string SubPath { get; set; }
        public string FullUrl { get { return RootPath + '/' + SubPath + '/' + FileName; } }
    }
}
