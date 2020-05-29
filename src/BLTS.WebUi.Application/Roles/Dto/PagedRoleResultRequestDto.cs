using Abp.Application.Services.Dto;

namespace BLTS.WebUi.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

