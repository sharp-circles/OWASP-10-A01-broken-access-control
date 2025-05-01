namespace owasp10.A01.Models.TokenValidation;

public record TokenKeyManagement
{
    public string IssuerSigningKey { get; set; }
    public string TokenDecryptionKey { get; set; }
}
