using BankApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BankApi.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user, IConfiguration config);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, IConfiguration config);

    }
}
