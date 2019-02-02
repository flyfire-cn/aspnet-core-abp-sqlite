using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using System.Linq;

namespace flyfire.IMS.Authorization
{
    public class IMSAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //修改权限，增加管理单元默认权限
            var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("Pages"));
            var administration = pages.Children.FirstOrDefault(p => p.Name == PermissionNames.Pages_Administration) ?? pages.CreateChildPermission(PermissionNames.Pages_Administration, L("Administration"));

            administration.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));
            administration.CreateChildPermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IMSConsts.LocalizationSourceName);
        }
    }
}
