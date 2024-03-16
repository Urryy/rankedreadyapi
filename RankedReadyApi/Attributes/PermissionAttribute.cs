using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Attributes;

public class PermissionAttribute : TypeFilterAttribute
{
    public PermissionAttribute(params Role[] roles) : base(typeof(PermissionRequirementFilter))
    {
        Arguments = new object[] { roles };
    }
}

public class PermissionRequirementFilter : IAuthorizationFilter
{
    private readonly ICurrentUserAccessor _accessor;
    private readonly List<Role> _systemRoles;
    private readonly IUserService _srvcUser;

    public PermissionRequirementFilter(Role[] roles, ICurrentUserAccessor accessor,
        IUserService srvcUser, ILogger<PermissionRequirementFilter> logger)
    {
        _accessor = accessor;
        _systemRoles = roles.ToList();
        _srvcUser = srvcUser;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = _accessor.GetCurrentUserId();

        if (userId != null && Guid.TryParse(userId, out var id))
        {
            var user = _srvcUser.GetAsync(id).Result;
            if (_systemRoles.Exists(i => i == user.Role.ToEnum<Role>()))
                return;
        }

        context.HttpContext.Response.StatusCode = 403;
        context.Result = new RedirectResult("rankedready/error");
    }
}
