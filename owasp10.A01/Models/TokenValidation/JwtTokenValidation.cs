namespace owasp10.A01.Models.TokenValidation;

public record JwtTokenValidation
{
    public TokenValidationActions Actions { get; set; }
    public TokenValidationOptions Options { get; set; }
    public TokenKeyManagement KeyManagement { get; set; }
}