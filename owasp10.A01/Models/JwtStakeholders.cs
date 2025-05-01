namespace owasp10.A01.Models;

public record JwtStakeholders
{
    public string Authority { get; set; }
    public string Audience { get; set; }
}
