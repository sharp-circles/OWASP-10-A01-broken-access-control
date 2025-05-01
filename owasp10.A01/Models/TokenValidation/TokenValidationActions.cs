namespace owasp10.A01.Models.TokenValidation;

public record TokenValidationActions
{
    public IEnumerable<string> ValidAlgorithms { get; set; }
    public bool ValidateActor { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateLifetime { get; set; }
    public bool ValidateTokenReplay { get; set; }
    public IEnumerable<string> ValidAudiences { get; set; }
    public IEnumerable<string> ValidIssuers { get; set; }
    public IEnumerable<string> ValidTypes { get; set; }
}
