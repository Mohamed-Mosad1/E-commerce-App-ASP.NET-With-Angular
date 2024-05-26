using E_commerce.API.Middleware;
using E_commerce.API.Extentions;
using StackExchange.Redis;
using E_commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_commerce.Infrastructure.Data;

namespace E_commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApiRegistration();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();

            builder.Services.InfrastructureConfiquration(builder.Configuration);

            // Configure Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(config);
            });



            var app = builder.Build();

            #region Apple All Pending Migrations

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var _dbContext = services.GetRequiredService<ApplicationDbContext>();

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();

                try
                {
                    await _dbContext.Database.MigrateAsync(); // Update ApplicationDbContext Database
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError(ex, "An error occurred while updating the database.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during data seeding or migration.");
                }
            }

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.InfrastructureMiddleware();

            app.Run();
        }
    }
}
