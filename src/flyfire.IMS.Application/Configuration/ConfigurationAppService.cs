using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using flyfire.IMS.Configuration.Dto;

namespace flyfire.IMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
