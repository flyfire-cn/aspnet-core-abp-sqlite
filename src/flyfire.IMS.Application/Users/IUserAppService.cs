using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using flyfire.IMS.Roles.Dto;
using flyfire.IMS.Users.Dto;

namespace flyfire.IMS.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
