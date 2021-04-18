namespace BLTS.WebUi.DtoModels
{
    public class FileStoragePagedResultRequestDto : PagedResultRequestDtoEntity<FileStorageDto>
    {
        public bool IncludeDeleted { get; set; }
    }
}


