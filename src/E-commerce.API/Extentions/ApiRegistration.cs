using E_commerce.API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace E_commerce.API.Extentions
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services)
        {
            // Configure AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Configure IFileProvider
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = context.ModelState.Values.Where(X => X.Errors.Count > 0)
                                                   .SelectMany(X => X.Errors)
                                                   .Select(X => X.ErrorMessage)
                                                   .ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            // Enable CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins("http://localhost:4200");

                });
            });

            return services;
        }
    }
}
