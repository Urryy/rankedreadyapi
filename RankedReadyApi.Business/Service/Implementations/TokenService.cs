using Microsoft.Extensions.Configuration;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.User;
using RankedReadyApi.DataAccess.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace RankedReadyApi.Business.Service.Implementations;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> CreateToken(UserWithoutCredDto user)
    {
        var token = user.CreateClaims()
                .CreateJwtToken(_configuration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
