using Abp.Application.Services;
using Abp.Application.Services.Dto;
using flyfire.IMS.MultiTenancy.Dto;

namespace flyfire.IMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

