namespace owasp10.A01.Models.TokenValidation;

public record TokenValidationOptions
{
    public TimeSpan ClockSkew { get; set; }
    public string DebugId { get; set; }
    public bool IgnoreTrailingSlashWhenValidatingAudience { get; set; }
    public bool IncludeTokenOnFailedValidation { get; set; }
    public bool LogTokenId { get; set; }
    public bool RequireAudience { get; set; }
}
