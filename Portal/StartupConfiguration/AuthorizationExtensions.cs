using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Portal.Models;


namespace Portal.StartupConfiguration
{
    internal static class AuthorizationExtensions
    {
        private const string DefaultSchema = "Bearer";
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public static void AddAuthorization(this IServiceCollection services, AuthConfig auth)
        {
            services
                .AddAuthentication(DefaultSchema)
                .AddJwtBearer(DefaultSchema, options =>
                {
                    options.Authority = $"{auth.ServerAddress}/auth/realms/{auth.Realm}";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudiences = auth.Audiences
                    };
                });
        }

        public static void UseAuthentication(this IApplicationBuilder app, AuthConfig auth)
        {
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseRolesAuthorization(options =>
            {
                options.ClientId = auth.ClientID;
            });
        }
    }
}
