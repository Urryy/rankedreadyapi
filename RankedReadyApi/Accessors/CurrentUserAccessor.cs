using RankedReadyApi.Business.Accessors;
using System.Security.Claims;

namespace RankedReadyApi.Accessors;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? GetCurrentUserId()
        => _contextAccessor.HttpContext?.User?.Claims?
                    .FirstOrDefault(usr => usr.Type == ClaimTypes.NameIdentifier)?.Value;
}
