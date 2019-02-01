using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Abp.Authorization;
using System.Reflection;

namespace flyfire.IMS.Web.Host.Startup
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //MethodInfo methodInfo;
            if (!context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
                return;

            //var actionAttrs = context.ApiDescription.ActionAttributes();
            var actionAttrs = methodInfo.CustomAttributes;
            if (actionAttrs.OfType<AbpAllowAnonymousAttribute>().Any())
            {
                return;
            }

            //var controllerAttrs = context.ApiDescription.ControllerAttributes();
            var controllerAttrs= methodInfo.CustomAttributes;
            var actionAbpAuthorizeAttrs = actionAttrs.OfType<AbpAuthorizeAttribute>();

            if (!actionAbpAuthorizeAttrs.Any() && controllerAttrs.OfType<AbpAllowAnonymousAttribute>().Any())
            {
                return;
            }

            var controllerAbpAuthorizeAttrs = controllerAttrs.OfType<AbpAuthorizeAttribute>();
            if (controllerAbpAuthorizeAttrs.Any() || actionAbpAuthorizeAttrs.Any())
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });

                var permissions = controllerAbpAuthorizeAttrs.Union(actionAbpAuthorizeAttrs)
                    .SelectMany(p => p.Permissions)
                    .Distinct();

                if (permissions.Any())
                {
                    operation.Responses.Add("403", new Response { Description = "Forbidden" });
                }

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { "bearerAuth", permissions }
                    }
                };
            }
        }
    }
}
