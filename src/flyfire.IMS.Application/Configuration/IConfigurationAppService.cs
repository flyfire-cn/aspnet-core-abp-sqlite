using System.Threading.Tasks;
using flyfire.IMS.Configuration.Dto;

namespace flyfire.IMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
