using Manager_Device_Service.Modules;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Manager_Device_Service.Providers
{
    public static class SwaggerProvider
    {
        public static IServiceCollection AddSwaggerProvider(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerModule>();
                c.OperationFilter<SwaggerModule>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MANAGER DEVICE - TNY NGUYEN", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

                c.UseInlineDefinitionsForEnums();
            });

            return services;
        }
    }
}
