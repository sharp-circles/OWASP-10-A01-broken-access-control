using owasp10.A01.Models.TokenValidation;

namespace owasp10.A01.Models;

public record AuthConfiguration
{
    public JwtStakeholders JwtStakeholders { get; set; }
    public JwtTokenValidation JwtTokenValidation { get; set; }
}
