using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortfolioAPI.Data;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace PortfolioAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure connection string with Managed Identity support when in Azure
            var connectionString = builder.Configuration.GetConnectionString("PortfolioDb");

            builder.Services.AddDbContext<PortfolioDbContext>(options =>
            {
                var sqlConnectionString = new SqlConnectionStringBuilder(connectionString);

                // Use Managed Identity when deployed to Azure
                if (builder.Environment.IsProduction() &&
                    sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryDefault)
                {
                    options.UseSqlServer(connectionString,
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null));
                }
                else
                {
                    // Use connection string as-is for local development
                    options.UseSqlServer(connectionString,
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null));
                }
            });

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowPortfolioClient", builder =>
                {
                    builder.WithOrigins(
                        "https://localhost:5001",  // Local WASM development
                        "https://red-desert-0ef331410.3.azurestaticapps.net",  // Azure Static Web App
                        "https://deanm.dev",
                        "https://www.deanm.dev"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // Add Entra ID authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://login.microsoftonline.com/917f9213-2bac-4a41-ba47-8026b0e2adea";
                    options.Audience = "e5b9d857-5798-4a28-88af-22f9615f1f86";
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PortfolioAPI.xml"), true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddOpenApi();
            builder.Configuration.AddUserSecrets<Program>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowPortfolioClient");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}