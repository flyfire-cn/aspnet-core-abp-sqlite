using System.Threading.Tasks;
using Abp.Application.Services;
using flyfire.IMS.Authorization.Accounts.Dto;

namespace flyfire.IMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
