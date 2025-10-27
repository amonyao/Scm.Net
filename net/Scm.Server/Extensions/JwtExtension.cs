﻿using Com.Scm.Config;
using Com.Scm.Jwt;
using Com.Scm.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Com.Scm
{
    public static class JwtExtension
    {
        public static void SetupJwt(this IServiceCollection services)
        {
            services.AddScoped(typeof(JwtContextHolder));

            var section = AppUtils.Configuration.GetSection(JwtConfig.Name);
            services.Configure<JwtConfig>(section);
            var token = section.Get<JwtConfig>();
            token.Prepare(null);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Security)),
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    /*AudienceValidator = (m, n, z) => 
                        m != null && m.FirstOrDefault()!.Equals(JwtConst.ValidAudience),*/
                };
                x.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var values = context.Request.Headers[JwtToken.TokenName];
                        context.Token = values.FirstOrDefault();
                        return Task.CompletedTask;
                    },
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("App", policy => policy.RequireRole("App").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            });
        }
    }
}