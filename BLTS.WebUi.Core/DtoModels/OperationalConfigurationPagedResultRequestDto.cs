using BLTS.WebUi.Models;

namespace BLTS.WebUi.DtoModels
{
    public class OperationalConfigurationPagedResultRequestDto : PagedResultRequestDtoEntity<OperationalConfiguration>
    {
        public OperationalConfigurationPagedResultRequestDto()
        {
            IncludeDeleted = false;
        }

        public long ApplicationId { get; set; }
        public bool IncludeDeleted { get; set; }
    }
}


