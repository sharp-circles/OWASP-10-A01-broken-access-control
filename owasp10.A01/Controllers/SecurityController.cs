using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using owasp10.A01.Extensions;
using owasp10.A01.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace owasp10.A01.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class SecurityController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public SecurityController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("token")]
    public IActionResult Authenticate()
    {
        var authConfiguration = _configuration.BindTo<AuthConfiguration>();

        var keyManagement = authConfiguration.JwtTokenValidation.KeyManagement;
        var jwtStakeholders = authConfiguration.JwtStakeholders;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyManagement.IssuerSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtStakeholders.Authority,
            audience: jwtStakeholders.Audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
