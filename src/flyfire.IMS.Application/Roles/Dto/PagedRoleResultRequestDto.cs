using Abp.Application.Services.Dto;

namespace flyfire.IMS.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

