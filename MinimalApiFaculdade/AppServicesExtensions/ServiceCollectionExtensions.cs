using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApiFaculdade.Context;
using MinimalApiFaculdade.Services;
using System.Text;

namespace MinimalApiFaculdade.AppServicesExtensions
{
        public static class ServiceCollectionExtensions
        {
            public static WebApplicationBuilder AddApiSwagger(this WebApplicationBuilder builder)
            {
                builder.Services.AddSwagger();
                return builder;
            }

            public static IServiceCollection AddSwagger(this IServiceCollection services)
            {
                services.AddEndpointsApiExplorer();

                //colocando token no swagger
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiagenda", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = @"JWT Authorization header using the Bearer scheme.
                   \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.
                    \r\n\r\nExample: \'Bearer 12345abcdef\'",
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                });
                return services;
            }

            public static WebApplicationBuilder AddPersistenc
                    (this WebApplicationBuilder builder)
            {

            //SERVICO DO BANCO EM MEMORIA COM O APPDBCONTEXT
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("FaculdadeDB"));


            //serviço do TOKEN
            builder.Services.AddSingleton<ITokenService>(new TokenService());
                return builder;
            }

            public static WebApplicationBuilder AddAutenticationJwt(this WebApplicationBuilder builder)
            {
                builder.Services.AddAuthentication
                    (JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                        };
                    });

                //serviço de autorização do token
                builder.Services.AddAuthorization();
                return builder;

            }


        }
    
}
