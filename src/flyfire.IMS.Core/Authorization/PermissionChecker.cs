using Abp.Authorization;
using flyfire.IMS.Authorization.Roles;
using flyfire.IMS.Authorization.Users;

namespace flyfire.IMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
