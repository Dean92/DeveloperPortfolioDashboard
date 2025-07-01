
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortfolioAPI.Data;

namespace PortfolioAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<PortfolioDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDb"),
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null)));

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowPortfolioClient", builder =>
                {
                    builder.WithOrigins("https://localhost:7182") // Adjust to PortfolioClient port
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Automatically includes User Secrets in Development environment
            builder.Configuration.AddUserSecrets<Program>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PortfolioAPI.xml"), true);
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API V1");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowPortfolioClient");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
