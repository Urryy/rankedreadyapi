using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankedReadyApi.Business.Accessors;

public interface ICurrentUserAccessor
{
    string? GetCurrentUserId();
}
