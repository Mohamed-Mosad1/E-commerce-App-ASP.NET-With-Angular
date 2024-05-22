using Microsoft.OpenApi.Models;

namespace E_commerce.API.Extentions
{
    public static class SwaggerServicesExtention
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // Define the security scheme
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer", // Specify the scheme
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                // Add the security definition
                options.AddSecurityDefinition("Bearer", securitySchema);

                // Define the security requirement
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };

                // Add the security requirement to Swagger
                options.AddSecurityRequirement(securityRequirement);
            });


            return services;
        }

        public static WebApplication UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
