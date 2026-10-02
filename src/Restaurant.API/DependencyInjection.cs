using Microsoft.OpenApi.Models;
using Restaurant.Application;
using Restaurant.Infrastructure;
using Restaurant.Seeding;
using System.Text.Json.Serialization;

namespace Restaurant.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services from other layers
            services.AddInfrastructure(configuration);
            services.AddApplication();

            // Http client configuration
            services.AddHttpClient();

            // JSON serialization configuration
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters
                    .Add(new JsonStringEnumConverter());
            });

            //Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập JWT token."
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
                        Array.Empty<string>()
                    }
                });
            });

            // CORS configuration
            services.AddCors(options =>
            {
                options.AddPolicy("WebClient", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:8081")
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }

        public static WebApplication UseServices(this WebApplication app)
        {
            app.UseCors("WebClient");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
