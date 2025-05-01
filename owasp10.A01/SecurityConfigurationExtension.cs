using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using owasp10.A01.Extensions;
using owasp10.A01.Models;
using owasp10.A01.Models.TokenValidation;
using System.Text;

namespace owasp10.A01;

public static class SecurityConfigurationExtension
{
    private static AuthConfiguration AuthConfiguration = new();

    public static IServiceCollection AddSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AuthConfiguration = configuration.BindTo<AuthConfiguration>();

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.AddStakeholders();
                    jwtOptions.AddTokenValidationConfiguration();
                    jwtOptions.Validate();
                });

        return services;
    }

    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        // Add here the configuration for any security provision, like OICD or Microsoft Identity OAuth 2.0

        return services;
    }

    private static void AddStakeholders(this JwtBearerOptions jwtOptions)
    {
        var jwtStakeholders = AuthConfiguration.JwtStakeholders;

        jwtOptions.Authority = jwtStakeholders.Authority;
        jwtOptions.Audience = jwtStakeholders.Audience;
    }

    private static void AddTokenValidationConfiguration(this JwtBearerOptions jwtOptions)
    {
        var jwtTokenValidation = AuthConfiguration.JwtTokenValidation;

        var tokenValidationParameters = new TokenValidationParameters();

        SetTokenValidationsActions(tokenValidationParameters, jwtTokenValidation.Actions);

        SetTokenValidationOptions(tokenValidationParameters, jwtTokenValidation.Options);

        SetTokenValidationKeyManagement(tokenValidationParameters, jwtTokenValidation.KeyManagement);

        jwtOptions.TokenValidationParameters = tokenValidationParameters;
    }

    private static void SetTokenValidationsActions(TokenValidationParameters tokenValidationParameters, TokenValidationActions actions)
    {
        tokenValidationParameters.ValidAlgorithms = actions.ValidAlgorithms;
        tokenValidationParameters.ValidateActor = actions.ValidateActor;
        tokenValidationParameters.ValidateAudience = actions.ValidateAudience;
        tokenValidationParameters.ValidateIssuer = actions.ValidateIssuer;
        tokenValidationParameters.ValidateIssuerSigningKey = actions.ValidateIssuerSigningKey;
        tokenValidationParameters.ValidateLifetime = actions.ValidateLifetime;
        tokenValidationParameters.ValidateTokenReplay = actions.ValidateTokenReplay;
        tokenValidationParameters.ValidAudiences = actions.ValidAudiences;
        tokenValidationParameters.ValidIssuers = actions.ValidIssuers;
        tokenValidationParameters.ValidTypes = actions.ValidTypes;
    }


    private static void SetTokenValidationOptions(TokenValidationParameters tokenValidationParameters, TokenValidationOptions options)
    {
        tokenValidationParameters.ClockSkew = options.ClockSkew;
        tokenValidationParameters.DebugId = options.DebugId;
        tokenValidationParameters.IgnoreTrailingSlashWhenValidatingAudience = options.IgnoreTrailingSlashWhenValidatingAudience;
        tokenValidationParameters.IncludeTokenOnFailedValidation = options.IncludeTokenOnFailedValidation;
        tokenValidationParameters.LogTokenId = options.LogTokenId;
        tokenValidationParameters.RequireAudience = options.RequireAudience;

        // Set delegates here
    }

    private static void SetTokenValidationKeyManagement(TokenValidationParameters tokenValidationParameters, TokenKeyManagement keyManagement)
    {
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyManagement.IssuerSigningKey));

        // Set delegates here
    }
}
